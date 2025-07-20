using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Sql_library
{
    internal class UIDataBase
    {
        public static Form1 form1;

        int row = 3, col;

        Point location;
        Size size;

        public UIDataBase(Size size, Point location)
        {
            this.size = size;
            this.location = location;
        }

        /*public List<TextBox> CreateTextBox(int CountTextBox = 0, int ContOfComboBox = 0, int ContIfCheckListBox = 0)
        {
            int count = 0;
            int i = 0, j = 0;
            int k1 = 10, k2 = 10;
            List<TextBox> list = new List<TextBox>();
            while (count != CountTextBox)
            {
                if (i == row)
                {
                    i = 0;
                    j++;
                }
                TextBox textBox = new TextBox(); 
                textBox.Size = size;
                textBox.Location = new Point(location.X + i * k1 + i * size.Width, location.Y + j * k2 + j * size.Height);

                count++;
                i++;
                form1.Controls.Add(textBox);
                list.Add(textBox);
            }
            return list;
        }*/

        //Створення візуальних компонентів 
        public List<Control> VisualComponents(List<(string, string, bool)>Schema)
        {
            int count = 0;
            int i = 0, j = 0;
            int k1 = 10, k2 = 30;
            List<Control> list = new List<Control>();

            for (int ii =form1.Controls.Count -1; ii >=0; ii--)
            {
                Control control = form1.Controls[ii];
                if(control.Tag != null && control.Tag.Equals(1))
                {
                    form1.Controls.RemoveAt(ii);
                    control.Dispose();
                }
            }

            foreach (var (name,type,key) in Schema)
            {
                if (i == row)
                {
                    i = 0;
                    j++;
                }
                Label label = new Label
                {
                    Text = name,
                    Location = new Point(location.X + i * k1 + i * size.Width, location.Y + j * k2 + j * size.Height - 20),
                    AutoSize = true,
                    Tag = 1
                };
                form1.Controls.Add(label);
                list.Add(label);

                Control control;
                if (key)
                {

                    control = new ComboBox
                    {
                        Size = size,
                        Location = new Point(location.X + i * k1 + i * size.Width, location.Y + j * k2 + j * size.Height),
                        Tag = 1
                    };

                }
                else if (type == "bool"|| type ==  "tinyint"|| type == "bite" )
                {
                    control = new CheckBox
                    {
                        Size = size,
                        AutoSize = true,
                        Location = new Point(location.X + i * k1 + i * size.Width, location.Y + j * k2 + j * size.Height),
                        Tag = 1
                    };

                }
               /* else if (type == "date" || type == "datetime")
                {
                    control = new DateTimePicker
                    {
                        Size = size,
                        Location = new Point(location.X + i * k1 + i * size.Width, location.Y + j * k2 + j * size.Height),
                        Tag = 1
                    };
                }*/
                else
                {
                    control = new TextBox
                    {
                        Size = size,
                        Location = new Point(location.X + i * k1 + i * size.Width, location.Y + j * k2 + j * size.Height),
                        Tag = 1
                    };
                }
                form1.Controls.Add(control);
                list.Add(control);
                i++;


            }


            return list;
        }
    }
}
