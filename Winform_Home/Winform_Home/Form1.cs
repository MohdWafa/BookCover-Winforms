using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;

namespace Winform_Home
{
  

   
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            //https://www.youtube.com/watch?v=D5cUhEXu8Jg
            //to make the default language of the form in english..
           // var changeLanguage = new ChangeLanguage();
          //  changeLanguage.UpdateConfig("language", "en");
            cover.Width = 300;
            cover.Height = 500;
            spine.Width = 50;
            spine.Height = 500;
            back_cover.Width = 300;
            back_cover.Height = 500;
           

            bookcolor = Color.White;
            text_brush = new SolidBrush(Color.Black);
            addbuttonwasclicked = false;
            fontsize = 16;
            number_of_texts = 0;
            number_of_clicks = 0;
           
            first_text = true;
           
            

        }

      
        Size size_form;
        Point location_form;
        int splitter_distance;
        CheckState eng_check;
        CheckState pol_check;
        string t_text;
        string a_text;
       
        public int new_width;
        public int new_height;
        public int new_spine_width;
        public int x1, y1;
      
        public Pen p = new Pen(Color.Black, 1);
        public Rectangle cover;
        public Rectangle back_cover;
        public Rectangle spine;
        public Point sd;
        public string title;
        public string author;
        public bool first_text;// = true;
        public Rectangle heading_cover;
        public Rectangle author_cover;
        public Rectangle heading_spine;
        public Rectangle author_spine;
        public Rectangle txtrectangle;
        public Font add_txt_font;
        public Font title_font = new Font("Arial", 32);
        public Font author_font = new Font("Arial", 24);
        public SolidBrush text_brush;
      
        public string txt;
        public float fontsize;
        public Color bookcolor;
        public bool addbuttonwasclicked;
       
        public StringFormat addtxtformat;
        public string[] additional_text;
        public static int number_of_texts;
        public static int number_of_clicks;
        
        public Point point_of_click = new Point();
        //https://simpledevcode.wordpress.com/2014/07/29/drawing-a-shape-and-moving-it-by-mouse-using-c-net/
        public bool isMouseDown = false;

        public static int number123=0;
        public int center_picturebox_X;
        public int center_picturebox_Y;
        public Point center_picturebox = new Point();
        public class add_text
        {
            public Rectangle rec;
            public SizeF rec_size;
           public int number;
           public string txxt;
            public Point point_txt;
            public StringAlignment txt_format;// = new StringFormat();
            public StringFormat txtformat;
            public float fsize;
            public bool isSelected;
            public int picturebox_resize_offset_X;
            public int picturebox_resize_offset_Y;
           public Point picturebox_resize_offset = new Point();

        }
        
        public List<add_text> list_addtxt = new List<add_text>();
       

        //https://stackoverflow.com/questions/33508574/calculate-coordinate-from-starting-point-having-distance-and-an-angle-for-all-q
        private double AngleToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }
        //https://stackoverflow.com/questions/55010535/c-sharp-finding-angle-between-2-given-points
        public double CalculeAngle(Point start, Point arrival)
        {
            var deltaX = Math.Pow((arrival.X - start.X), 2);
            var deltaY = Math.Pow((arrival.Y - start.Y), 2);

            var radian = Math.Atan2((arrival.Y - start.Y), (arrival.X - start.X));
            var angle = (radian * (180 / Math.PI) + 360) % 360;

            return angle;
        }

        public double CalculeDistance(Point start, Point arrival)
        {
            var deltaX = Math.Pow((arrival.X - start.X), 2);
            var deltaY = Math.Pow((arrival.Y - start.Y), 2);

            var distance = Math.Sqrt(deltaY + deltaX);

            return distance;
        }


      
        public int center_book_X;
        public int center_book_Y;
        public int top_left_height;
        public Point center_book = new Point();
        
      
        public void center_rectangle(PictureBox picture)
        {
            
           
            int total_width = cover.Width + back_cover.Width + spine.Width;
            int top_left_width = (picture.Width - total_width) / 2;
             top_left_height = (picture.Height - cover.Height) / 2;
           
            center_book_X = top_left_width + cover.Width + (spine.Width / 2);
            center_book_Y = top_left_height + (cover.Height / 2);
            center_book = new Point(center_book_X, center_book_Y);


            int cood_y = picture.Top + top_left_height;
            int cood_x = picture.Left + top_left_width;
            x1 = cood_x;
            y1 = cood_y;
            cover = new Rectangle(cood_x, cood_y, cover.Width, cover.Height);
            spine = new Rectangle(cood_x + cover.Width, cood_y, spine.Width, spine.Height);
            back_cover = new Rectangle(cood_x + cover.Width + spine.Width, cood_y, back_cover.Width, back_cover.Height);

            int cood_y_author = cood_y + (cover.Height / 3);
            int cood_y_title = cood_y + (spine.Height / 2);
            heading_cover = new Rectangle(cood_x, cood_y, cover.Width, cover.Height/3);
            author_cover = new Rectangle(cood_x, cood_y_author, cover.Width, cover.Height / 6);

            author_spine = new Rectangle(cood_x + cover.Width, cood_y, spine.Width, spine.Height / 2);
            heading_spine = new Rectangle(cood_x + cover.Width, cood_y_title, spine.Width, spine.Height / 2);


            

        }

      
       
        
        

       

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {
            splitContainer1.Panel2MinSize = 200;
        }

       
        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {// just common sense really :)   changed the width of the picturebox in relation to the panel and refresh after..

            pictureBox1.Width = splitContainer1.Panel1.Width;
            pictureBox1.Refresh();
        }



       

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 txtbox = new Form3();
            txtbox.ShowDialog();
            if (txtbox.DialogResult == DialogResult.OK)
            {
                if (txtbox.return_txt() != string.Empty)
                {
                    
                    number_of_texts++;
                    add_text t = new add_text();
                    t.txxt = txtbox.return_txt();
                 
                    t.fsize = txtbox.return_fontsize();
                    t.txtformat = new StringFormat();
                  
                    t.txt_format = txtbox.return_txt_allignment();

                    t.txtformat.Alignment = txtbox.return_txt_allignment();
                    t.number = number_of_texts;
                  
                   


                    t.rec_size = pictureBox1.CreateGraphics().MeasureString(t.txxt, new Font("Arial", t.fsize));
                   
                    t.rec = new Rectangle(t.point_txt, t.rec_size.ToSize());
                    
                    list_addtxt.Add(t);
                   
                }
                addbuttonwasclicked = true;
            }
         }

       

        //https://www.youtube.com/watch?v=D5cUhEXu8Jg

        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            polishToolStripMenuItem.CheckState = CheckState.Unchecked;
            englishToolStripMenuItem.CheckState = CheckState.Checked;
            ChangeLanguage1("en");
           

        }

        private void polishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            englishToolStripMenuItem.CheckState = CheckState.Unchecked;
            polishToolStripMenuItem.CheckState = CheckState.Checked;

            ChangeLanguage1("pl");
            


        }
        
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // https://docs.microsoft.com/en-us/dotnet/desktop/winforms/how-to-display-dialog-boxes-for-windows-forms?view=netframeworkdesktop-4.8
            Form2 dlg1 = new Form2();
            dlg1.ShowDialog();
            if(dlg1.DialogResult==DialogResult.OK)
            {
                textBox1.Text = " ";
                textBox2.Text = " "; 
                bookcolor = Color.White;
                new_width = dlg1.return_numeric1();
                new_height = dlg1.return_numeric3();
                new_spine_width = dlg1.return_numeric2();
                list_addtxt = new List<add_text>();
                cover.Width = new_width;
                cover.Height = new_height;
                back_cover.Width = new_width;
                back_cover.Height = new_height;
                spine.Width = new_spine_width;
                spine.Height = new_height;

              
            }
            pictureBox1.Refresh();
         
        }
       
        
        //https://docs.microsoft.com/en-us/dotnet/api/system.drawing.graphics.fillrectangle?view=net-5.0
        public void FillRectangleRectangle(Graphics e,Rectangle rect, Color c)
        {

            // Create solid brush.
            SolidBrush brush = new SolidBrush(c);

            // Fill rectangle to screen.
            using (Graphics g = pictureBox1.CreateGraphics())
           

                g.FillRectangle(brush, rect);
                pictureBox1.Refresh();
            }
            
        


       
      //Draws and fills(if changed) the rectangles- use this in paint!!
        public void DrawCenterRectangle(PictureBox p, Graphics g)
        {
           

            Pen pen = new Pen(Color.Black);
            pen.Width = 2;
            StringFormat titletxt = new StringFormat();
            titletxt.LineAlignment = StringAlignment.Center;
            titletxt.Alignment = StringAlignment.Center;
            //titletxt

            
            center_rectangle(pictureBox1);

            g.DrawRectangle(pen, back_cover);
            g.FillRectangle(new SolidBrush(bookcolor), back_cover);
            g.DrawRectangle(pen, spine);
            g.FillRectangle(new SolidBrush(bookcolor), spine);
            g.DrawRectangle(pen, cover);
            g.FillRectangle(new SolidBrush(bookcolor), cover);
           
            foreach (var s in list_addtxt)
            {
                if (s.isSelected == true) { 
                    g.DrawRectangle(invpen, s.rec);
                    break;
                }
            }



            fix_added_text(list_addtxt, g);
            g.DrawString(title, GetAdjustedFont(g,title,title_font,heading_cover.Width,32,1,true), text_brush, heading_cover,titletxt);
            g.DrawString(author, GetAdjustedFont(g, author, author_font, author_cover.Width, 24, 1, true), text_brush, author_cover, titletxt);
           
            DrawSidewaysText(g, GetAdjustedFont(g, author, author_font, author_spine.Height, 24, 1, true), text_brush, author_spine, titletxt, author);
            DrawSidewaysText(g, GetAdjustedFont(g, title, title_font, heading_spine.Height, 24, 1, true), text_brush, heading_spine, titletxt, title);
           

        }

        //http://csharphelper.com/blog/2017/01/easily-draw-rotated-text-on-a-form-in-c/
        private void DrawSidewaysText(Graphics gr, Font font,
    Brush brush, Rectangle bounds, StringFormat string_format,
    string txt)
        {
            // Make a rotated rectangle at the origin.
            Rectangle rotated_bounds = new Rectangle(
                0, 0, bounds.Height, bounds.Width);

            // Rotate.
            gr.ResetTransform();
            gr.RotateTransform(-90);

            // Translate to move the rectangle to the correct position.
            gr.TranslateTransform(bounds.Left, bounds.Bottom,
                MatrixOrder.Append);

            // Draw the text.
            gr.DrawString(txt, font, brush, rotated_bounds, string_format);
        }
       

        //https://stackoverflow.com/questions/27766600/how-to-call-paint-event-from-a-button-click-event
        private void button2_Click(object sender, EventArgs e)
        {// from the winform tutorial..
            ColorDialog colorDialog = new ColorDialog();
             if (colorDialog.ShowDialog() == DialogResult.OK)
                 {
                
                bookcolor = colorDialog.Color;
                pictureBox1.Refresh();
            }
           
        }

        private void button3_Click(object sender, EventArgs e)
        {// from the winform tutorial..
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                text_brush.Color = colorDialog.Color;
                pictureBox1.Refresh();
                System.GC.Collect();
            }
            
        }
       
       
        private void pictureBox1_Paint_1(object sender, PaintEventArgs e)
        {
            
            DrawCenterRectangle(pictureBox1, e.Graphics);
          
            System.GC.Collect();
          
        }

        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {

            center_rectangle(pictureBox1);
            center_picturebox_X = pictureBox1.Width / 2;
            center_picturebox_Y = pictureBox1.Height / 2;
            center_picturebox = new Point(center_picturebox_X, center_picturebox_Y);

            foreach (var i in list_addtxt)
            {
                i.point_txt = new Point(pictureBox1.Width / 2 + i.picturebox_resize_offset_X, pictureBox1.Height / 2 + i.picturebox_resize_offset_Y);
                i.rec.X = i.point_txt.X;
                i.rec.Y = i.point_txt.Y;

                // pictureBox1.Refresh();
            }

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {//https://www.c-sharpcorner.com/article/dialog-boxes-in-c-sharp/
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Open a Text File";
            ofd.Filter = "XML Files (*.xml) | *.xml"; //Here you can filter which all files you wanted allow to open  
            DialogResult dr = ofd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                Book b = Book.LoadFromFile(ofd.FileName);
                cover = b.c;
                spine = b.s;
                back_cover = b.bc;
                heading_cover = b.title_cover;
                author_cover = b.author_txtcover;
                author_spine = b.spine_author;
                heading_spine = b.spine_title;
                title = b.title_text;
                author = b.author_text;
                bookcolor = Color.FromArgb(b.bkColorAsArgb);
                text_brush.Color = Color.FromArgb(b.tcolorAsArgb);
               list_addtxt = b.texxxt;
                textBox1.Text = title;
                textBox2.Text = author;
                pictureBox1.Refresh();
                
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {//https://www.c-sharpcorner.com/article/dialog-boxes-in-c-sharp/
            SaveFileDialog sfdlg = new SaveFileDialog();
            sfdlg.Filter = "XML Files (*.xml) | *.xml"; 
            if (sfdlg.ShowDialog() == DialogResult.OK)
            {
                if (sfdlg.FileName != "")
                {
                    Graphics g = pictureBox1.CreateGraphics();
                    Book b = new Book();
                    b.SaveBook(sfdlg.FileName, cover, spine, back_cover, heading_cover, author_cover, author_spine, heading_spine, title, author, GetAdjustedFont(g, title, title_font, heading_cover.Width, 32, 1, true), GetAdjustedFont(g, author, author_font, author_cover.Width, 24, 1, true), bookcolor.ToArgb(), text_brush.Color.ToArgb(),list_addtxt);
                }

            
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            title = textBox1.Text;
            pictureBox1.Refresh();
        }
        //https://stackoverflow.com/questions/206717/how-do-i-replace-multiple-spaces-with-a-single-space-in-c
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string sentence = textBox2.Text;
            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex("[ ]{2,}", options);
            sentence = regex.Replace(sentence, " ");
            author = sentence;
            pictureBox1.Refresh();

        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }

      
        //https://supportcenter.devexpress.com/ticket/details/t425466/how-to-localize-a-winforms-application
        //I tried a different way using the ChangeLanguage class, but then I had to restart, so this way was more efficient in saving the control settings

        public void ChangeLanguage1(string language)
        {
            CultureInfo.CurrentUICulture = new CultureInfo(language);
            Controls.Clear();

             size_form = this.Size;
             location_form = this.Location;
             splitter_distance = this.splitContainer1.SplitterDistance;
             eng_check = this.englishToolStripMenuItem.CheckState;
             pol_check = this.polishToolStripMenuItem.CheckState;
            t_text = this.textBox1.Text;
            a_text = this.textBox2.Text;
            InitializeComponent();
            this.Size = size_form;
            this.Location = location_form;
            this.splitContainer1.SplitterDistance = splitter_distance;
            this.englishToolStripMenuItem.CheckState = eng_check;
            this.polishToolStripMenuItem.CheckState = pol_check;
            this.textBox1.Text = t_text;
            this.textBox2.Text = a_text;
        }

       

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            if (addbuttonwasclicked == true)
                pictureBox1.Cursor = Cursors.Cross;
            else
                pictureBox1.Cursor = Cursors.Default;
        }
     
        public Color invselection = new Color();
        public Pen invpen = new Pen(Color.Black);
      
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            //https://stackoverflow.com/questions/1165107/how-do-i-invert-a-colour
            invselection = Color.FromArgb(bookcolor.ToArgb() ^ 0xffffff);
             invpen = new Pen(invselection);
           
            switch (e.Button)
            {
                case MouseButtons.Right:
                    {
                        //https://stackoverflow.com/questions/41823875/draw-individual-rectangles-on-each-mouse-click
                        foreach (var s in list_addtxt)
                        {
                            s.isSelected = false;
                        }
                      foreach(var s in list_addtxt)
                        {
                            if(s.rec.Contains(e.X,e.Y))
                            {
                                s.isSelected = true;
                                break;//so only one value is selected, and it doesnt keep repeating the foreach loop!!
                            }
                            foreach (var i in list_addtxt)
                            {
                                if (i.isSelected == true)
                                {
                                    pictureBox1.CreateGraphics().DrawRectangle(invpen, i.rec);

                                }
                            }
                        }

                        pictureBox1.Refresh();
                    }
                    break;

                case MouseButtons.Left:
                    {
                        if (addbuttonwasclicked == true)
                {

                    number_of_clicks++;


                    add_text result = list_addtxt.Find(st => st.number == number_of_clicks);
                    result.point_txt = new Point(e.X -  result.rec.Width/2, e.Y - result.rec.Height/2);
                            Point f = new Point((Size)result.point_txt);
                            result.picturebox_resize_offset_X = f.X - pictureBox1.Width/2;
                            result.picturebox_resize_offset_Y = f.Y - pictureBox1.Height / 2;
                    result.rec.Location = result.point_txt;
                    list_addtxt.Add(result);

                    list_addtxt.RemoveAll(rt => rt.point_txt == new Point(0, 0));
                    fix_added_text(list_addtxt, pictureBox1.CreateGraphics());
                    addbuttonwasclicked = false;
                    pictureBox1.Cursor = Cursors.Default;
                  

                }
                        
            }
            break;
                

        }
            }
           
        
      //  public Rectangle example;
        public void fix_added_text(List<add_text> st, Graphics g)
        {
            list_addtxt.RemoveAll(rt => rt.txxt == null);
            // Rectangle example;
            foreach (var s in st)
            {
                StringFormat y = new StringFormat();
                y.Alignment = s.txt_format;
              
                if (s.txt_format == StringAlignment.Near)
                    g.DrawString(s.txxt, new Font("Arial", s.fsize), text_brush, s.point_txt.X , s.point_txt.Y, y);
                else if (s.txt_format == StringAlignment.Center)
                    g.DrawString(s.txxt, new Font("Arial", s.fsize), text_brush, s.point_txt.X + s.rec.Width/2, s.point_txt.Y, y);
                else
                   g.DrawString(s.txxt, new Font("Arial", s.fsize), text_brush, s.point_txt.X + s.rec.Width , s.point_txt.Y,y);

               // g.DrawRectangle(p, s.rec);
               //the above code is used to see if the rectangles are positioned porperly with relation to the added textx


            }


        }
        //https://stackoverflow.com/questions/15571715/auto-resize-font-to-fit-rectangle
        public Font GetAdjustedFont(Graphics g, string graphicString, Font originalFont, int containerWidth, int maxFontSize, int minFontSize, bool smallestOnFail)
        {
            Font testFont = null;
            // We utilize MeasureString which we get via a control instance           
            for (int adjustedSize = maxFontSize; adjustedSize >= minFontSize; adjustedSize--)
            {
                testFont = new Font(originalFont.Name, adjustedSize, originalFont.Style);

                // Test the string with the new size
                SizeF adjustedSizeNew = g.MeasureString(graphicString, testFont);

                if (containerWidth > Convert.ToInt32(adjustedSizeNew.Width))
                {
                    // Good font, return it
                    return testFont;
                }
            }


            if (smallestOnFail)
            {
                return testFont;
            }
            else
                return testFont;
         
        }
       
        public StringAlignment ss;// = new StringFormat();
        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                foreach (add_text s in list_addtxt)
                {
                    if (s.rec.Contains(e.X, e.Y))
                    {

                        Form3 txtbox = new Form3();
                        txtbox.get_data_form3(s.txxt, s.fsize, s.txt_format);
                        txtbox.ShowDialog();
                        if (txtbox.DialogResult == DialogResult.OK)
                        {

                            s.txxt = txtbox.return_txt();
                            s.fsize = txtbox.return_fontsize();
                            s.txtformat = new StringFormat();
                            s.txt_format = txtbox.return_txt_allignment();
                            s.txtformat.Alignment = s.txt_format;
                            s.rec_size = pictureBox1.CreateGraphics().MeasureString(s.txxt, new Font("Arial", s.fsize));


                            s.rec = new Rectangle(s.point_txt, s.rec_size.ToSize());

                            break;
                        }
                        else if (txtbox.DialogResult == DialogResult.Cancel)
                        {
                            txtbox.Close();

                        }
                    }
                }
                pictureBox1.Refresh();
                 fix_added_text(list_addtxt, pictureBox1.CreateGraphics());
            }




        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
                isMouseDown = true;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Middle)
            isMouseDown = false;
        }

        //https://stackoverflow.com/questions/36127131/clear-drawn-shapes-in-a-form-on-button-click
        //https://stackoverflow.com/questions/36127131/clear-drawn-shapes-in-a-form-on-button-click
        //https://social.msdn.microsoft.com/Forums/windows/en-US/6f0a2c3f-fff3-4cce-a509-ea2de53ef67a/keypresseventargs-whats-the-keychar-for-quotdeletequot?forum=winforms
        public bool should_Delete = false;
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Delete)
            {
                should_Delete = true;
                if (should_Delete)
                {
                    list_addtxt.RemoveAll(rt => rt.isSelected == true);
                    pictureBox1.Refresh();
                }
            }

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Delete)
            {
                for(int i = list_addtxt.Count -1;i>=0;i--)
                {//https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.elementat?view=net-5.0
                    if (list_addtxt.ElementAt(i).isSelected)
                    {//https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.removeat?view=net-5.0
                        list_addtxt.RemoveAt(i);
                        pictureBox1.Refresh();
                        break;
                    }
                }
               
                    
                   
            }
            
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            
            if(e.Button==MouseButtons.Middle)
            {
                if(isMouseDown==true)
                {
                    
                    foreach(var s in list_addtxt)
                    {
                        if(s.rec.Contains(e.X,e.Y))
                        {

                            //https://simpledevcode.wordpress.com/2014/07/29/drawing-a-shape-and-moving-it-by-mouse-using-c-net/
                            s.point_txt = new Point(e.Location.X - s.rec.Width / 2, e.Location.Y - s.rec.Height / 2);
                          
                          



                            s.rec.Location = s.point_txt;
                            pictureBox1.Refresh();

                        }
                       
                    }
                   
                 

                }
            }
        }

       
    }
}
