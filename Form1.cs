using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sql_library
{
    public partial class Form1 : Form
    {

        DBClient dbClient;
        List<List<string>> CurentTable;
        List<Control> currentControls;
        UIDataBase dbBase;


        public Form1()
        {
            InitializeComponent();
            dbClient = DBClient.GetInstance();
            dbBase = new UIDataBase(new Size(187, 22), new Point(650, 119));

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UIDataBase.form1 = this;
            comboBox1.SelectedIndex = 2;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0: // Address

                    this.Text = this.Controls.Count.ToString();
                    dbClient.LoadData("Address", dataGridView1);
                    currentControls = dbBase.VisualComponents(dbClient.GetSchema("Address"));
                    this.Text = this.Controls.Count.ToString();
                    break;
                case 1: // Services
                    dbClient.LoadData("Services", dataGridView1);
                    currentControls = dbBase.VisualComponents(dbClient.GetSchema("Services"));
                    break;
                case 2: // CNAP
                    dbClient.LoadData("CNAP", dataGridView1);
                    currentControls = dbBase.VisualComponents(dbClient.GetSchema("CNAP"));
                    break;
                case 3: // CNAP_Services
                    dbClient.LoadData("CNAP_Services", dataGridView1 );
                    currentControls = dbBase.VisualComponents(dbClient.GetSchema("CNAP_Services"));
                    break;
                case 4: //Client
                    dbClient.LoadData("Client", dataGridView1);
                    currentControls = dbBase.VisualComponents(dbClient.GetSchema("Client"));
                    break;
                case 5: // Employee
                    dbClient.LoadData("Employee", dataGridView1);
                    currentControls = dbBase.VisualComponents(dbClient.GetSchema("Employee"));
                    break;
                case 6: // Statement
                    dbClient.LoadData("Statement", dataGridView1);
                    currentControls = dbBase.VisualComponents(dbClient.GetSchema("Statement"));
                    break;
                case 7: // TypeOfStatus
                    dbClient.LoadData("TypeOfStatus", dataGridView1);
                    currentControls = dbBase.VisualComponents(dbClient.GetSchema("TypeOfStatus"));
                    break;
                case 8: // Document
                    dbClient.LoadData("Document", dataGridView1);
                    currentControls = dbBase.VisualComponents(dbClient.GetSchema("Document"));
                    break;
                case 9: // TypeOfDocument
                    dbClient.LoadData("TypeOfDocument", dataGridView1);
                    currentControls = dbBase.VisualComponents(dbClient.GetSchema("TypeOfDocument"));
                    break;
                default:
                    MessageBox.Show("Please select a valid table.");
                    break;

            }
        }



        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Прив'язка даних до компонентів 
            try
            {
                int CellIndex = 0;
                if (e.RowIndex < 0 || e.ColumnIndex < 0)
                {
                    return; // Ignore header clicks
                }
                foreach (var current in currentControls)
                {
                    if (current is Label)
                    {
                        continue;
                    }
                    string value = dataGridView1.Rows[e.RowIndex].Cells[CellIndex].Value?.ToString() ?? string.Empty;
                    CellIndex++;

                    if (current is TextBox CurrentT)
                    {
                        //((TextBox)current).Text = value;
                       // CurrentT = (TextBox)current;
                        CurrentT.Text = value;
                    }

                    if (current is CheckBox CurrentCheck)
                    {
                        CurrentCheck.Checked = value == "1" || value.ToLower() == "true";                  
                    }
                   

                }
                /*currentControls[0].Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                currentTextBox[1].Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                currentTextBox[2].Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                currentTextBox[3].Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();*/
            }
            catch 
            { }
            //A b = new B();
            //b.pr
            //((B)b).BProperty = 10;
        }
    }

    
}

//public class A { }
//public class  B:A
//{
//    public int BProperty { get; set; }
//}
//public class C : A
//{ }
