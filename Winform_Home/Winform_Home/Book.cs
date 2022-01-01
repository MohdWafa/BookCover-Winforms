using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.IO;
using System.ComponentModel;
using Winform_Home.Properties;

namespace Winform_Home
{
   




    //https://stackoverflow.com/questions/1940127/how-to-xmlserialize-system-drawing-font-class
    public class SerializableFont
    {
        public SerializableFont()
        {
            FontValue = null;
        }

        public SerializableFont(Font font)
        {
            FontValue = font;
        }

        [XmlIgnore]
        public Font FontValue { get; set; }

        [XmlElement("FontValue")]
        public string SerializeFontAttribute
        {
            get
            {
                return FontXmlConverter.ConvertToString(FontValue);
            }
            set
            {
                FontValue = FontXmlConverter.ConvertToFont(value);
            }
        }

        public static implicit operator Font(SerializableFont serializeableFont)
        {
            if (serializeableFont == null)
                return null;
            return serializeableFont.FontValue;
        }

        public static implicit operator SerializableFont(Font font)
        {
            return new SerializableFont(font);
        }
    }

    public static class FontXmlConverter
    {
        public static string ConvertToString(Font font)
        {
            try
            {
                if (font != null)
                {
                    TypeConverter converter = TypeDescriptor.GetConverter(typeof(Font));
                    return converter.ConvertToString(font);
                }
                else
                    return null;
            }
            catch { System.Diagnostics.Debug.WriteLine("Unable to convert"); }
            return null;
        }
        public static Font ConvertToFont(string fontString)
        {
            try
            {
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(Font));
                return (Font)converter.ConvertFromString(fontString);
            }
            catch { System.Diagnostics.Debug.WriteLine("Unable to convert"); }
            return null;
        }
    }
    [Serializable]
    public  class Book
    {

        
        public Rectangle c { get; set; }
        public Rectangle s { get; set; }
        public Rectangle bc { get; set; }
        public Rectangle title_cover { get; set; }
        public Rectangle author_txtcover { get; set; }
        public Rectangle spine_author { get; set; }
        public Rectangle spine_title { get; set; }
      
        public string title_text { get; set; }
        public string author_text { get; set; }
       // SerializableFont MyFont { get; set; }
         public SerializableFont title_text_font { get; set; }
        public SerializableFont author_text_font { get; set; }

        //https://stackoverflow.com/questions/3280362/most-elegant-xml-serialization-of-color-structure
        //https://stackoverflow.com/questions/3280362/most-elegant-xml-serialization-of-color-structure
        //https://stackoverflow.com/questions/3280362/most-elegant-xml-serialization-of-color-structure
        //https://stackoverflow.com/questions/3280362/most-elegant-xml-serialization-of-color-structure
        [XmlIgnore]
        public Color bkcolor { get; set; }

        [XmlElement("bkcolor")]
        public int bkColorAsArgb
        {
            get { return bkcolor.ToArgb(); }
            set { bkcolor = Color.FromArgb(value); }
        }
        [XmlIgnore]
        public Color tcolor { get; set; }

        [XmlElement("tcolor")]
        public int tcolorAsArgb
        {
            get { return tcolor.ToArgb(); }
            set { tcolor = Color.FromArgb(value); }
        }




        public List<Form1.add_text> texxxt;


        public Book()
        {

        }
      
        public Book(Rectangle c, Rectangle s, Rectangle bc, Rectangle title_cover, Rectangle author_txtcover, Rectangle spine_author, Rectangle spine_title, string title_text, string author_text, Font title_text_font, Font author_text_font, int bkcolorargb, int tcolorargb, List<Form1.add_text> t)
        {
            this.c = c;
            this.s = s;
            this.bc = bc;
            this.title_cover = title_cover;
            this.author_txtcover = author_txtcover;
            this.spine_author = spine_author;
            this.spine_title = spine_title;
         
            this.title_text = title_text;
            this.author_text = author_text;
           this.title_text_font = title_text_font;
            this.author_text_font = author_text_font;
              this.bkColorAsArgb= bkcolorargb;
            this.tcolorAsArgb = tcolorargb;
            this.texxxt = t;

        }
        //https://www.youtube.com/watch?v=zAn-ZbJqS90
        //https://www.youtube.com/watch?v=zAn-ZbJqS90
        //https://www.youtube.com/watch?v=zAn-ZbJqS90
        //https://www.youtube.com/watch?v=zAn-ZbJqS90
        //https://www.youtube.com/watch?v=zAn-ZbJqS90
        //https://www.youtube.com/watch?v=zAn-ZbJqS90
        //https://www.youtube.com/watch?v=zAn-ZbJqS90
        //https://www.youtube.com/watch?v=zAn-ZbJqS90
        //https://www.youtube.com/watch?v=zAn-ZbJqS90
        //https://www.youtube.com/watch?v=zAn-ZbJqS90


        public void SaveBook(string filename, Rectangle c, Rectangle s, Rectangle bc, Rectangle title_cover, Rectangle author_txtcover, Rectangle spine_author, Rectangle spine_title, string title_text, string author_text, Font title_text_font, Font author_text_font, int bkcolorargb,int tcolorargb, List<Form1.add_text> t)
        {
            Book booka = new Book(c, s, bc, title_cover, author_txtcover, spine_author, spine_title, title_text, author_text, title_text_font, author_text_font, bkcolorargb,tcolorargb, t);
             using (var stream = new FileStream(filename, FileMode.Create))
            {
                XmlSerializer XML = new XmlSerializer(typeof(Book));
                XML.Serialize(stream, booka);
            }
            }

        public static Book LoadFromFile(string filename)
        {
            using (var stream = new FileStream(filename, FileMode.Open))
            {
                XmlSerializer XML = new XmlSerializer(typeof(Book));
                
                return (Book)XML.Deserialize(stream);
            }
        }
    }
}
