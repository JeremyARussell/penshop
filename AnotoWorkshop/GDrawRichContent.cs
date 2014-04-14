using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AnotoWorkshop {
    public static class GDrawRichContent {
        private static RichContentDrawer _rtfDrawer;

        public static void DrawRtfText(this Graphics graphics, string rtf, Rectangle layoutArea) {
            if (_rtfDrawer == null) {
                _rtfDrawer = new RichContentDrawer();
            }
            _rtfDrawer.Rtf = rtf;
            _rtfDrawer.Draw(graphics, layoutArea);
        }

        private class RichContentDrawer : RichTextBox {

            #region StaticsAndStructs

            [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
            private static extern IntPtr LoadLibrary(string lpFileName);
           
            [DllImport("USER32.dll")]
            private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

            [StructLayout(LayoutKind.Sequential)]
            public struct RECT {
                public int Left;
                public int Top;
                public int Right;
                public int Bottom;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct TEXT_RANGE {
                public int min;        //First character of range (0 for start of doc)
                public int max;        //Last character of range (-1 for end of doc)
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct TEXT_FORMAT {
                public IntPtr hdc;       //Actual surface to draw on
                public IntPtr hdcTarget; //Target surface for determining text formatting
                public RECT rc;          //Region of the surface to draw to (in twips)
                public RECT rcPage;      //Region of the whole surface (page size) (in twips)
                public TEXT_RANGE chrg;   //Range of text to draw
            }

            private const int WM_USER = 0x0400;
            public const int EM_FORMATRANGE = WM_USER + 57;
            public const int WS_EX_TRANSPARENT = 0x20;
            #endregion
            
            protected override CreateParams CreateParams {
                get {
                    CreateParams createParams = base.CreateParams;
                    if (LoadLibrary("msftedit.dll") != IntPtr.Zero) {
                        createParams.ExStyle |= WS_EX_TRANSPARENT;
                        createParams.ClassName = "RICHEDIT50W";
                    }
                    return createParams;
                }
            }

            public void Draw(Graphics graphics, Rectangle layoutArea) {
                double anInchX = 1440 / graphics.DpiX;
                double anInchY = 1440 / graphics.DpiY;

                //Calculate the area to render.
                RECT rectLayoutArea;
                rectLayoutArea.Top = (int)(layoutArea.Top * anInchY);
                rectLayoutArea.Bottom = (int)(layoutArea.Bottom * anInchY);
                rectLayoutArea.Left = (int)(layoutArea.Left * anInchX);
                rectLayoutArea.Right = (int)(layoutArea.Right * anInchX);
                IntPtr hdc = graphics.GetHdc();      //Use a Graphics pointer to refer to the paint surface...

                TEXT_FORMAT fmtRange;
                fmtRange.chrg.max = -1;       //character range...
                fmtRange.chrg.min = 0;        //...
                fmtRange.hdc = hdc;                  //...this one is used for measuring...
                fmtRange.hdcTarget = hdc;            //...
                fmtRange.rc = rectLayoutArea;           //printable area on page
                fmtRange.rcPage = rectLayoutArea;       //size of page

                IntPtr lParam = Marshal.AllocCoTaskMem(Marshal.SizeOf(fmtRange));//The above struct fmtRange gets turned into raw memory data to be passed below in SendMessage
                Marshal.StructureToPtr(fmtRange, lParam, false);

                IntPtr wParam = new IntPtr(1);
                //Where the magic happens, more details later.
                SendMessage(Handle, EM_FORMATRANGE, wParam, lParam);

                //Cleaning up
                Marshal.FreeCoTaskMem(lParam);
                graphics.ReleaseHdc(hdc);
            }
        }
    }
}