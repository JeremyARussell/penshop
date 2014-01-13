using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ImageMagick;

namespace AnotoWorkshop
{
    public partial class pngExporter : Form
    {

        FormPage workingPage;
        Settings _settings = Settings.instance;


        public pngExporter(FormPage page)
        {
            InitializeComponent();

            workingPage = page;
        }

        private void designPanel_Paint(object sender, PaintEventArgs e) {
            Rectangle pageRectangle = new Rectangle();
            pageRectangle.X = 0;
            pageRectangle.Y = 0;
            pageRectangle.Height = 792;
            pageRectangle.Width = 612;

            e.Graphics.FillRectangle(new SolidBrush(Color.White), pageRectangle);

            foreach (Field fi in workingPage.Fields)
            {
                if (!fi.hidden)
                {
                    switch (fi.type)
                    {
                        case Type.TextField:
                            Pen p1 = new Pen(Color.FromArgb(255, 51, 102, 255));
                            e.Graphics.DrawRectangle(p1, new Rectangle((new Point(fi.x, fi.y)), (new Size(fi.width, fi.height))));
                            p1.Color = Color.FromArgb(255, 51, 102, 255);
                            e.Graphics.DrawLine(p1, fi.x + 1, fi.y - 1 + fi.height, //Creates the shadow line for the text box.
                                                    fi.x - 1 + fi.width, fi.y - 1 + fi.height);
                            break;

                        case Type.Label:
                            Pen p2 = new Pen(Color.FromArgb(255, 51, 102, 255));
                            //e.Graphics.DrawRectangle(p2, new Rectangle((new Point(fi.x, fi.y)), fi.rect().Size));

                            //p2.Color = Color.FromArgb(255, 51, 102, 255);
                            e.Graphics.DrawString(fi.text, fi.formatSet.font(), p2.Brush, new Point(fi.x, fi.y));
                            break;

                        case Type.FancyLabel:
                            Pen flPen = new Pen(Color.FromArgb(255, 51, 102, 255));
                            //e.Graphics.DrawRectangle(flPen, new Rectangle((new Point(fi.x, fi.y)), fi.rect().Size));

                            //e.Graphics.DrawString(

                            //flPen.Color = Color.Black;
                            e.Graphics.DrawString(fi.text, Font, flPen.Brush, new Point(fi.x, fi.y));
                            break;

                        case Type.RectangleDraw:
                            Pen recPen = new Pen(Color.FromArgb(255, 51, 102, 255));
                            e.Graphics.DrawRectangle(recPen, new Rectangle((new Point(fi.x, fi.y)),  (new Size(fi.width, fi.height))));
                            break;

                        case Type.LineDraw:
                            Pen linePen = new Pen(Color.FromArgb(255, 51, 102, 255));
                            e.Graphics.DrawRectangle(linePen, new Rectangle((new Point(fi.x, fi.y)),  (new Size(fi.width, fi.height))));
                            break;

                        case Type.Checkbox:
                            Pen p3 = new Pen(Color.FromArgb(255, 51, 102, 255));
                            e.Graphics.DrawRectangle(p3, new Rectangle((new Point(fi.x, fi.y)),  (new Size(fi.width, fi.height))));
                            //p3.Color = Color.Black;
                            e.Graphics.DrawString(fi.text, Font, p3.Brush, new Point(fi.x, fi.y + 3));
                            break;

                        case Type.OptionsGroup:
                            Pen p4 = new Pen(Color.Red);
                            e.Graphics.DrawRectangle(p4, new Rectangle((new Point(fi.x, fi.y)),  (new Size(fi.width, fi.height))));
                            break;
                    }
                }
            }
        
        }

        private void designPanel_Click(object sender, EventArgs e)
        {
            Bitmap bitTest = new Bitmap(612, 792);

            designPanel.DrawToBitmap(bitTest, new Rectangle(0, 0, 612, 792));

            //bitTest.Save(_settings.exportFolder + @"\test.bmp");

            using (MagickImage image = new MagickImage(bitTest))
            {
                //image.
                image.Write(_settings.exportFolder + @"\test.eps");
            }
        }
    }
}
