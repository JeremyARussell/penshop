using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace AnotoWorkshop
{
    public partial class PopupContainer : ToolStripDropDown
    {
        private Control _mPopedContainer;

        private ToolStripControlHost _mHost;
        
        private bool _mFade = true;


   

        public PopupContainer(Control popedControl)
        {
            InitializeComponent();

            if (popedControl == null)
                throw new ArgumentNullException("popedControl");

            _mPopedContainer = popedControl;

            _mFade = SystemInformation.IsMenuAnimationEnabled && SystemInformation.IsMenuFadeEnabled;

            _mHost = new ToolStripControlHost(popedControl);
            _mHost.AutoSize = false;//make it take the same room as the poped control
       
            Padding = Margin = _mHost.Padding = _mHost.Margin = Padding.Empty;
            
            popedControl.Location = Point.Empty;
            
            Items.Add(_mHost);
            
            popedControl.Disposed += delegate
                                         {
                popedControl = null;
                Dispose(true);// this popup container will be disposed immediately after disposion of the contained control
            };
        }

        


        protected override bool ProcessDialogKey(Keys keyData)
        {//prevent alt from closing it and allow alt+menumonic to work
            if ((keyData & Keys.Alt) == Keys.Alt)
                return false;

            return base.ProcessDialogKey(keyData);
        }


        
        
     
        
        public void Show(Control control)
        {
            if (control == null)
                throw new ArgumentNullException("control");
        
            Show(control, control.ClientRectangle);
        }

        public void Show(Form f, Point p)
        {
            Show(f, new Rectangle(p,new Size(0,0)));

        }

        private void Show(Control control, Rectangle area)
        {
            if (control == null)
                throw new ArgumentNullException("control");
           

            Point location = control.PointToScreen(new Point(area.Left, area.Top + area.Height));
            
            Rectangle screen = Screen.FromControl(control).WorkingArea;
            
            if (location.X + Size.Width > (screen.Left + screen.Width))
                location.X = (screen.Left + screen.Width) - Size.Width;
            
            if (location.Y + Size.Height > (screen.Top + screen.Height))
                location.Y -= Size.Height + area.Height;
                    
            location = control.PointToClient(location);
            
            Show(control, location, ToolStripDropDownDirection.BelowRight);
        }

        
        
        
        private const int Frames = 5;
        private const int Totalduration = 100;
        private const int Frameduration = Totalduration / Frames;

        protected override void SetVisibleCore(bool visible)
        {
            double opacity = Opacity;
            if (visible && _mFade) Opacity = 0;
            base.SetVisibleCore(visible);
            if (!visible || !_mFade) return;
            for (int i = 1; i <= Frames; i++)
            {
                if (i > 1)
                {
                    System.Threading.Thread.Sleep(Frameduration);
                }
                Opacity = opacity * i / Frames;
            }
            Opacity = opacity;
        }


               

        protected override void OnOpening(CancelEventArgs e)
        {
            if (_mPopedContainer.IsDisposed || _mPopedContainer.Disposing)
            {
                e.Cancel = true;
                return;
            }
            base.OnOpening(e);
        }

        protected override void OnOpened(EventArgs e)
        {
            _mPopedContainer.Focus();
            
            base.OnOpened(e);
        }



        

    }
}
