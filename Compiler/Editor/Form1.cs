using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Compiler.Editor
{
    public partial class Form1 : Form
    {
        System.Windows.Forms.ToolTip toolTip1 = new System.Windows.Forms.ToolTip();
       
        public static bool buttonPress = false;
        public int flagFirstLine=0;
        public int numberOfErrors=1;


        //auto complete intialization
        bool listShow = false; //state of current list
        string keyword;
        int count = 0;

        //the following flags control which items to be added or deleted to the list box
        int BpressedFlag = 0;
        int CpressedFlag = 0;
        int DpressedFlag = 0;
        int EpressedFlag = 0;
        int IpressedFlag = 0;
        int LpressedFlag = 0;
        int PpressedFlag = 0;
        int RpressedFlag = 0;
        int SpressedFlag = 0;
        int TpressedFlag = 0;
        int VpressedFlag = 0;
        int WpressedFlag = 0;
        //**********************************



        int maxLC = 1; //maxLineCount 


        DataTable table = new DataTable();
        public Form1()
        {
            InitializeComponent();
           
            }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            table.Columns.Add("Line NO.", typeof(int));
           table.Columns.Add("Lexeme", typeof(string));
            table.Columns.Add("Return Token", typeof(string));
            table.Columns.Add("Lexeme NO. in Line", typeof(int));
             table.Columns.Add("Matchability", typeof(string));
        
            dataGridView1.DataSource = table;
            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[2].Width = 150;
            dataGridView1.Columns[3].Width = 200;
            dataGridView1.Columns[4].Width = 150;







        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //compile button
            progressBar1.Value = 0;
            this.timer1.Start();
            dataGridView1.Visible = true;
            if (numberOfErrors > 0)
            {
                button7.Text = "Erros:" + numberOfErrors;
                button7.Enabled = true;
                button7.Visible = true;
            }
            else
            {
                button7.Text = "Errors:";
                button7.Enabled = false;
                button7.Visible = false;
            }

            table.Rows.Add("1","--","Comment","1","Matched");
            dataGridView1.DataSource = table;

            //foreach (var p in returnedList)
            //{
            //    var row = table.NewRow();
            //    row["Line NO."] = p.returnedthing;
            //    row["Lexeme"] = p.returnedthing;
            //    row["Return Token"] = p.returnedthing;
            //    row["Lexeme NO. in Line"] = p.returnedthing;
            //    row["Matchability"] = p.returnedthing;
            //    table.Rows.Add(row);
            //}
            //dataGridView1.DataSource = table;


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedItem = comboBox1.Items[comboBox1.SelectedIndex].ToString();
            if (selectedItem == "20%")
            {
                richTextBox1.SelectAll();
                richTextBox1.SelectionFont = new Font(Font.FontFamily, Font.Size * 0.25f);

            }
            else if (selectedItem == "50%")
            {
                richTextBox1.SelectAll();
                richTextBox1.SelectionFont = new Font(Font.FontFamily, Font.Size * 0.5f);

            }
            else if (selectedItem == "70%")
            {
                richTextBox1.SelectAll();
                richTextBox1.SelectionFont = new Font("Consolas", 11 * 0.7f);

            }
            else if (selectedItem == "100%")
            {
                richTextBox1.SelectAll();
                richTextBox1.SelectionFont = new Font("Consolas", 11);

            }
            else if (selectedItem == "150%")
            {
                richTextBox1.SelectAll();
                richTextBox1.SelectionFont = new Font("Consolas", 11 * 1.5f);

            }
            else if (selectedItem == "200%")
            {
                richTextBox1.SelectAll();
                richTextBox1.SelectionFont = new Font("Consolas", 11 * 2);

            }
            else if (selectedItem == "400%")
            {
                richTextBox1.SelectAll();
                richTextBox1.SelectionFont = new Font("Consolas", 11*4);

            }
        }

        private void comboBox1_TextUpdate(object sender, EventArgs e)
        {
     
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            //label2.Text = "Col:" + richTextBox1.GetFirstCharIndexOfCurrentLine();

            // create the line number in the list box
            try
            {
                int linecount = richTextBox1.GetLineFromCharIndex(richTextBox1.TextLength) + 1;
                if (linecount != maxLC)
                {
                    listBox1.Items.Clear();
                    for (int i = 1; i < linecount + 1; i++)
                    {
                        listBox1.Items.Add(Convert.ToString(i) + "\n");
                    }
                    maxLC = linecount;

                }


            }
            catch (ArgumentOutOfRangeException m)
            { }
         


            // get the total number of lines and control the list box
            int numOflines = int.Parse(richTextBox1.Lines.Length.ToString());
            if (numOflines > 1)
            {
                label1.Text = "Total lines:" + richTextBox1.Lines.Length.ToString();

            }
            else if (numOflines == 1)
            {
                label1.Text = "Total lines:1";
            }
            else
            {
                label1.Text = "Total lines:0";
            }


            //the follwing code controls the color of comments at text changing
            if (!buttonPress)
            {
                int firstCharIndex = richTextBox1.GetFirstCharIndexOfCurrentLine();
                int firstOccurance = richTextBox1.Find("--".ToCharArray(), firstCharIndex);
                if (firstOccurance != -1 && firstOccurance + 1 < richTextBox1.Text.Length && richTextBox1.Text[firstOccurance + 1] == '-')
                {
                   // Console.WriteLine(firstOccurance);
                    int ii = firstOccurance;
                    while (richTextBox1.Text.Length > ii && richTextBox1.Text[ii] != '\n')
                    {
                        ii++;
                    }
                    richTextBox1.Select(firstOccurance, ii);
                    if (richTextBox1.SelectedText != "")
                    {
                        richTextBox1.SelectionColor = Color.Green;
                    }
                    richTextBox1.DeselectAll();
                    richTextBox1.SelectionStart = ii;
                }
                else
                    richTextBox1.SelectionColor = Color.Black;
            }

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //richTextBox1.SelectAll();
            //coloring the text of the selected number in the line number
            if (listBox1.SelectedIndex != -1)
            {

                richTextBox1.SelectAll();
                richTextBox1.SelectionBackColor = richTextBox1.BackColor;
                var lines = richTextBox1.Lines;
                if (listBox1.SelectedIndex < 0 || listBox1.SelectedIndex >= lines.Length)
                    return;
                var start = richTextBox1.GetFirstCharIndexFromLine(listBox1.SelectedIndex);  // Get the 1st char index of the appended text
                var length = lines[listBox1.SelectedIndex].Length;
                richTextBox1.Select(start, length);                 // Select from there to the end
                string colorcode = "#9bcaef";
                int argb = Int32.Parse(colorcode.Replace("#", ""), NumberStyles.HexNumber);
                richTextBox1.SelectionBackColor = Color.FromArgb(argb);
            }


        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            int curItem = listBox1.SelectedIndex;
            richTextBox1.Select(0,2);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            buttonPress = true;
            if (textBox1.Text != "Search")
            {
                button4.Visible = true;
            }
            int startText = 0;
            int endText;
            
            endText = richTextBox1.Text.LastIndexOf(textBox1.Text);

            richTextBox1.SelectAll();
            richTextBox1.SelectionBackColor = Color.White;


            while (startText < endText) {

                richTextBox1.Find(textBox1.Text, startText, richTextBox1.TextLength, RichTextBoxFinds.MatchCase);
            richTextBox1.SelectionBackColor = Color.LightGray;

                startText = richTextBox1.Text.IndexOf(textBox1.Text, startText) + 1;
                    }
            buttonPress = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.progressBar1.Increment(3);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"C:\",//opening location
                Title = "Browse Text Files", //title of dialog

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "txt",//default extension
                Filter = "txt files (*.txt)|*.txt",//txt extension
                FilterIndex = 2,
                RestoreDirectory = true,
               // openFileDialog1.FileName  ---> open file name
                ReadOnlyChecked = true,
                ShowReadOnly = true
            };
            openFileDialog1.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
            richTextBox1.SelectionBackColor = Color.White;
            textBox1.Text = "Search";
            button4.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //comment button
            buttonPress = true;
                string lines = richTextBox1.SelectedText;

                char[] delims = new[] { '\r', '\n' };
                string[] strings = lines.Split(delims);
                string newText ;
                string [] text =new string[strings.Length];
               for (int i=0;i<strings.Length;i++)
                    {
                    string line = strings[i]; 
                    if(line!="")
                    {
                        newText = "--" + line;
                        text [i] = newText;
                    }
                    else
                    {
                        continue;
                    }
                }
                richTextBox1.SelectionColor = Color.Green;
                richTextBox1.SelectedText = String.Join("\n", text);
            richTextBox1.DeselectAll();
            buttonPress = false;
           
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //uncomment button
            buttonPress = true;
    
            string selectedText = richTextBox1.SelectedText;
            string[] lines = selectedText.Split('\n');
            int startSelection = richTextBox1.SelectionStart;
            int countChanges = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                try
                {
                    if (line.Substring(0, 2) == "--")
                    {
                        if (line != "")
                        {
                            countChanges += 2;
                            lines[i] = line.Substring(2, line.Length - 2);
                        }
                    }
                    else
                        continue;
                    if (lines[i].Substring(0, 2) == "--")
                    {
                        richTextBox1.Select(i + startSelection, lines[i].Length);
                        richTextBox1.SelectionColor = Color.Green;
                    }
                    else if(lines[i].Substring(0, 2) != "--")
                    {
                        
                        richTextBox1.Select(i + startSelection, lines[i].Length);
                        richTextBox1.SelectionColor = Color.Black;
                    }
                }
                catch(ArgumentOutOfRangeException o)
                {
                    continue;
                }
            }
            richTextBox1.DeselectAll();
            string newText = String.Join("\n", lines);
            richTextBox1.Select(startSelection, newText.Length+countChanges);
            richTextBox1.SelectedText = newText;
            richTextBox1.DeselectAll();
            buttonPress = false;


        }

        private void richTextBox1_Click(object sender, EventArgs e)
        {
            if (flagFirstLine == 0)
            {
                richTextBox1.SelectAll();
                richTextBox1.SelectionColor = Color.Green;
                flagFirstLine = 1;
                richTextBox1.DeselectAll();
            }
        }

        private void button3_MouseHover(object sender, EventArgs e)
        {
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 10;
            toolTip1.ReshowDelay = 500;
            toolTip1.SetToolTip(button3,"Search for an element");
        }

        private void button4_MouseHover(object sender, EventArgs e)
        {
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 10;
            toolTip1.ReshowDelay = 500;
            toolTip1.SetToolTip(button4, "Stop search");
        }

        private void button5_MouseHover(object sender, EventArgs e)
        {
            toolTip1.AutoPopDelay =5000;
            toolTip1.InitialDelay = 10;
            toolTip1.ReshowDelay = 500;
            toolTip1.SetToolTip(button5, "Comment the selected lines");
        }

        private void button6_MouseHover(object sender, EventArgs e)
        {
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 10;
            toolTip1.ReshowDelay = 500;
            toolTip1.SetToolTip(button6, "Uncomment the selected lines");
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 10;
            toolTip1.ReshowDelay = 500;
            toolTip1.SetToolTip(button1, "Compile code");
        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 10;
            toolTip1.ReshowDelay = 500;
            toolTip1.SetToolTip(button2, "Browse a file");
        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void richTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //the following function controls the shown recommendation list upon pressing specif selected button
            try
            {
                if (listShow == true && keyword != null)
                {
                    keyword += e.KeyChar;
                    count++;
                    Point point = this.richTextBox1.GetPositionFromCharIndex(richTextBox1.SelectionStart);
                    point.Y += (int)Math.Ceiling(this.richTextBox1.Font.GetHeight()) + richTextBox1.Location.Y; //set y pos of list box
                    point.X += richTextBox1.Location.X; //set x pos of list box
                    listBox2.Location = point;
                    listBox2.Show();

                    listBox2.SelectedIndex = 0;

                    listBox2.SelectedIndex = listBox2.FindString(keyword);// search in the list for the closest word to that written
                    richTextBox1.Focus();//sets the focus back to the richtextbox so we can type more if we would like.
                }

                //************************B************************
                if (e.KeyChar == 'B')
                {
                    if (BpressedFlag == 0)
                    {
                        keyword = "B";
                        listBox2.Items.Add("BreakFromThis");
                    }
                    BpressedFlag = 1;
                    listShow = true;
                    Point point = this.richTextBox1.GetPositionFromCharIndex(richTextBox1.SelectionStart);
                    point.Y += (int)Math.Ceiling(this.richTextBox1.Font.GetHeight()) + richTextBox1.Location.Y; //set y pos of list box
                    point.X += richTextBox1.Location.X; //set x pos of list box
                    listBox2.Location = point;
                    count++;
                    listBox2.Show();
                    listBox2.SelectedIndex = 0;
                    listBox2.SelectedIndex = listBox2.FindString(keyword);
                    richTextBox1.Focus();
                }
                else if (e.KeyChar == ' ' && BpressedFlag == 1 &&
                     SpressedFlag == 0 &&
                     CpressedFlag == 0 &&
                     DpressedFlag == 0 &&
                     IpressedFlag == 0 &&
                     LpressedFlag == 0 &&
                     PpressedFlag == 0 &&
                     RpressedFlag == 0 &&
                     TpressedFlag == 0 &&
                     VpressedFlag == 0 &&
                     WpressedFlag == 0 &&
                     EpressedFlag == 0)
                {
                    listBox2.Items.Clear();
                    BpressedFlag = 0;
                }

                //************************C************************
                if (e.KeyChar == 'C')
                {
                    if (CpressedFlag == 0)
                    {
                        keyword = "C";
                        listBox2.Items.Add("Cwq");
                        listBox2.Items.Add("CwqSequence");
                        listBox2.Items.Add("Conditionof");
                    }
                    CpressedFlag = 1;
                    listShow = true;
                    Point point = this.richTextBox1.GetPositionFromCharIndex(richTextBox1.SelectionStart);
                    point.Y += (int)Math.Ceiling(this.richTextBox1.Font.GetHeight()) + richTextBox1.Location.Y; //set y pos of list box
                    point.X += richTextBox1.Location.X; //set x pos of list box
                    listBox2.Location = point;
                    count++;
                    listBox2.Show();
                    listBox2.SelectedIndex = 0;
                    listBox2.SelectedIndex = listBox2.FindString(keyword);
                    richTextBox1.Focus();
                }
                else if (e.KeyChar == ' ' && CpressedFlag == 1 &&
                     SpressedFlag == 0 &&
                     BpressedFlag == 0 &&
                     DpressedFlag == 0 &&
                     IpressedFlag == 0 &&
                     LpressedFlag == 0 &&
                     PpressedFlag == 0 &&
                     RpressedFlag == 0 &&
                     TpressedFlag == 0 &&
                     VpressedFlag == 0 &&
                     WpressedFlag == 0 &&
                     EpressedFlag == 0)
                {
                    listBox2.Items.Clear();
                    CpressedFlag = 0;
                }
                //************************D************************
                if (e.KeyChar == 'D')
                {
                    if (DpressedFlag == 0)
                    {
                        keyword = "D";
                        listBox2.Items.Add("DerivedFrom");
                    }
                    DpressedFlag = 1;
                    listShow = true;
                    Point point = this.richTextBox1.GetPositionFromCharIndex(richTextBox1.SelectionStart);
                    point.Y += (int)Math.Ceiling(this.richTextBox1.Font.GetHeight()) + richTextBox1.Location.Y; //set y pos of list box
                    point.X += richTextBox1.Location.X; //set x pos of list box
                    listBox2.Location = point;
                    count++;
                    listBox2.Show();
                    listBox2.SelectedIndex = 0;
                    listBox2.SelectedIndex = listBox2.FindString(keyword);
                    richTextBox1.Focus();
                }
                else if (e.KeyChar == ' ' && DpressedFlag == 1 &&
                     SpressedFlag == 0 &&
                     BpressedFlag == 0 &&
                     CpressedFlag == 0 &&
                     IpressedFlag == 0 &&
                     LpressedFlag == 0 &&
                     PpressedFlag == 0 &&
                     RpressedFlag == 0 &&
                     TpressedFlag == 0 &&
                     VpressedFlag == 0 &&
                     WpressedFlag == 0 &&
                     EpressedFlag == 0)
                {
                    listBox2.Items.Clear();
                    DpressedFlag = 0;
                }

                //************************E************************
                if (e.KeyChar == 'E')
                {
                    if (EpressedFlag == 0)
                    {
                        keyword = "E";
                        listBox2.Items.Add("Else");
                    }
                    EpressedFlag = 1;
                    listShow = true;
                    Point point = this.richTextBox1.GetPositionFromCharIndex(richTextBox1.SelectionStart);
                    point.Y += (int)Math.Ceiling(this.richTextBox1.Font.GetHeight()) + richTextBox1.Location.Y; //set y pos of list box
                    point.X += richTextBox1.Location.X; //set x pos of list box
                    listBox2.Location = point;
                    count++;
                    listBox2.Show();
                    listBox2.SelectedIndex = 0;
                    listBox2.SelectedIndex = listBox2.FindString(keyword);
                    richTextBox1.Focus();
                }
                else if (e.KeyChar == ' ' && EpressedFlag == 1 &&
                     SpressedFlag == 0 &&
                     BpressedFlag == 0 &&
                     CpressedFlag == 0 &&
                     IpressedFlag == 0 &&
                     LpressedFlag == 0 &&
                     PpressedFlag == 0 &&
                     RpressedFlag == 0 &&
                     TpressedFlag == 0 &&
                     VpressedFlag == 0 &&
                     WpressedFlag == 0 &&
                     DpressedFlag == 0)
                {
                    listBox2.Items.Clear();
                    EpressedFlag = 0;
                }

                //************************I************************
                if (e.KeyChar == 'I')
                {
                    if (IpressedFlag == 0)
                    {
                        keyword = "I";
                        listBox2.Items.Add("Ifity");
                        listBox2.Items.Add("Ity");
                    }
                    IpressedFlag = 1;
                    listShow = true;
                    Point point = this.richTextBox1.GetPositionFromCharIndex(richTextBox1.SelectionStart);
                    point.Y += (int)Math.Ceiling(this.richTextBox1.Font.GetHeight()) + richTextBox1.Location.Y; //set y pos of list box
                    point.X += richTextBox1.Location.X; //set x pos of list box
                    listBox2.Location = point;
                    count++;
                    listBox2.Show();
                    listBox2.SelectedIndex = 0;
                    listBox2.SelectedIndex = listBox2.FindString(keyword);
                    richTextBox1.Focus();
                }
                else if (e.KeyChar == ' ' && IpressedFlag == 1 &&
                     SpressedFlag == 0 &&
                     BpressedFlag == 0 &&
                     DpressedFlag == 0 &&
                     CpressedFlag == 0 &&
                     LpressedFlag == 0 &&
                     PpressedFlag == 0 &&
                     RpressedFlag == 0 &&
                     TpressedFlag == 0 &&
                     VpressedFlag == 0 &&
                     WpressedFlag == 0 &&
                     EpressedFlag == 0)
                {
                    listBox2.Items.Clear();
                    IpressedFlag = 0;
                }

                //************************L************************
                if (e.KeyChar == 'L')
                {
                    if (LpressedFlag == 0)
                    {
                        keyword = "L";
                        listBox2.Items.Add("Logical");
                    }
                    LpressedFlag = 1;
                    listShow = true;
                    Point point = this.richTextBox1.GetPositionFromCharIndex(richTextBox1.SelectionStart);
                    point.Y += (int)Math.Ceiling(this.richTextBox1.Font.GetHeight()) + richTextBox1.Location.Y; //set y pos of list box
                    point.X += richTextBox1.Location.X; //set x pos of list box
                    listBox2.Location = point;
                    count++;
                    listBox2.Show();
                    listBox2.SelectedIndex = 0;
                    listBox2.SelectedIndex = listBox2.FindString(keyword);
                    richTextBox1.Focus();
                }
                else if (e.KeyChar == ' ' && LpressedFlag == 1 &&
                     SpressedFlag == 0 &&
                     BpressedFlag == 0 &&
                     DpressedFlag == 0 &&
                     IpressedFlag == 0 &&
                     CpressedFlag == 0 &&
                     PpressedFlag == 0 &&
                     RpressedFlag == 0 &&
                     TpressedFlag == 0 &&
                     VpressedFlag == 0 &&
                     WpressedFlag == 0 &&
                     EpressedFlag == 0)
                {
                    listBox2.Items.Clear();
                    LpressedFlag = 0;
                }

                //************************P************************
                if (e.KeyChar == 'P')
                {
                    if (PpressedFlag == 0)
                    {
                        keyword = "P";
                        listBox2.Items.Add("Pattern");
                    }
                    PpressedFlag = 1;
                    listShow = true;
                    Point point = this.richTextBox1.GetPositionFromCharIndex(richTextBox1.SelectionStart);
                    point.Y += (int)Math.Ceiling(this.richTextBox1.Font.GetHeight()) + richTextBox1.Location.Y; //set y pos of list box
                    point.X += richTextBox1.Location.X; //set x pos of list box
                    listBox2.Location = point;
                    count++;
                    listBox2.Show();
                    listBox2.SelectedIndex = 0;
                    listBox2.SelectedIndex = listBox2.FindString(keyword);
                    richTextBox1.Focus();
                }
                else if (e.KeyChar == ' ' && PpressedFlag == 1 &&
                     SpressedFlag == 0 &&
                     BpressedFlag == 0 &&
                     DpressedFlag == 0 &&
                     IpressedFlag == 0 &&
                     CpressedFlag == 0 &&
                     LpressedFlag == 0 &&
                     RpressedFlag == 0 &&
                     TpressedFlag == 0 &&
                     VpressedFlag == 0 &&
                     WpressedFlag == 0 &&
                     EpressedFlag == 0)
                {
                    listBox2.Items.Clear();
                    PpressedFlag = 0;
                }

                //************************R************************
                if (e.KeyChar == 'R')
                {
                    if (RpressedFlag == 0)
                    {
                        keyword = "R";
                        listBox2.Items.Add("Require");
                        listBox2.Items.Add("Respondwith");
                    }
                    RpressedFlag = 1;
                    listShow = true;
                    Point point = this.richTextBox1.GetPositionFromCharIndex(richTextBox1.SelectionStart);
                    point.Y += (int)Math.Ceiling(this.richTextBox1.Font.GetHeight()) + richTextBox1.Location.Y; //set y pos of list box
                    point.X += richTextBox1.Location.X; //set x pos of list box
                    listBox2.Location = point;
                    count++;
                    listBox2.Show();
                    listBox2.SelectedIndex = 0;
                    listBox2.SelectedIndex = listBox2.FindString(keyword);
                    richTextBox1.Focus();
                }
                else if (e.KeyChar == ' ' && RpressedFlag == 1 &&
                     SpressedFlag == 0 &&
                     BpressedFlag == 0 &&
                     DpressedFlag == 0 &&
                     IpressedFlag == 0 &&
                     CpressedFlag == 0 &&
                     LpressedFlag == 0 &&
                     PpressedFlag == 0 &&
                     TpressedFlag == 0 &&
                     VpressedFlag == 0 &&
                     WpressedFlag == 0 &&
                     EpressedFlag == 0)
                {
                    listBox2.Items.Clear();
                    RpressedFlag = 0;
                }

                //************************S************************
                if (e.KeyChar == 'S')
                {

                    if (SpressedFlag == 0)
                    {
                        keyword = "S";
                        listBox2.Items.Add("Scan");
                        listBox2.Items.Add("Sifity");
                        listBox2.Items.Add("Sity");
                        listBox2.Items.Add("Srap");
                    }
                    SpressedFlag = 1;
                    listShow = true;
                    Point point = this.richTextBox1.GetPositionFromCharIndex(richTextBox1.SelectionStart);
                    point.Y += (int)Math.Ceiling(this.richTextBox1.Font.GetHeight()) + richTextBox1.Location.Y; //set y pos of list box
                    point.X += richTextBox1.Location.X; //set x pos of list box
                    listBox2.Location = point;
                    count++;
                    listBox2.Show();
                    listBox2.SelectedIndex = 0;
                    listBox2.SelectedIndex = listBox2.FindString(keyword);
                    richTextBox1.Focus();
                }
                else if (e.KeyChar == ' ' && SpressedFlag == 1 &&
                       BpressedFlag == 0 &&
                       CpressedFlag == 0 &&
                       DpressedFlag == 0 &&
                       IpressedFlag == 0 &&
                       LpressedFlag == 0 &&
                       PpressedFlag == 0 &&
                       RpressedFlag == 0 &&
                       TpressedFlag == 0 &&
                       VpressedFlag == 0 &&
                       WpressedFlag == 0 &&
                     EpressedFlag == 0)
                {
                    listBox2.Items.Clear();
                    SpressedFlag = 0;
                }


                //************************T************************
                if (e.KeyChar == 'T')
                {

                    if (TpressedFlag == 0)
                    {
                        keyword = "T";
                        listBox2.Items.Add("TrueFor");
                    }
                    TpressedFlag = 1;
                    listShow = true;
                    Point point = this.richTextBox1.GetPositionFromCharIndex(richTextBox1.SelectionStart);
                    point.Y += (int)Math.Ceiling(this.richTextBox1.Font.GetHeight()) + richTextBox1.Location.Y; //set y pos of list box
                    point.X += richTextBox1.Location.X; //set x pos of list box
                    listBox2.Location = point;
                    count++;
                    listBox2.Show();
                    listBox2.SelectedIndex = 0;
                    listBox2.SelectedIndex = listBox2.FindString(keyword);
                    richTextBox1.Focus();
                }
                else if (e.KeyChar == ' ' && TpressedFlag == 1 &&
                       BpressedFlag == 0 &&
                       CpressedFlag == 0 &&
                       DpressedFlag == 0 &&
                       IpressedFlag == 0 &&
                       LpressedFlag == 0 &&
                       PpressedFlag == 0 &&
                       RpressedFlag == 0 &&
                       SpressedFlag == 0 &&
                       VpressedFlag == 0 &&
                       WpressedFlag == 0 &&
                       EpressedFlag == 0)
                {
                    listBox2.Items.Clear();
                    TpressedFlag = 0;
                }


                //************************V************************
                if (e.KeyChar == 'V')
                {

                    if (VpressedFlag == 0)
                    {
                        keyword = "V";
                        listBox2.Items.Add("ValueLess");
                    }
                    VpressedFlag = 1;
                    listShow = true;
                    Point point = this.richTextBox1.GetPositionFromCharIndex(richTextBox1.SelectionStart);
                    point.Y += (int)Math.Ceiling(this.richTextBox1.Font.GetHeight()) + richTextBox1.Location.Y; //set y pos of list box
                    point.X += richTextBox1.Location.X; //set x pos of list box
                    listBox2.Location = point;
                    count++;
                    listBox2.Show();
                    listBox2.SelectedIndex = 0;
                    listBox2.SelectedIndex = listBox2.FindString(keyword);
                    richTextBox1.Focus();
                }
                else if (e.KeyChar == ' ' && VpressedFlag == 1 &&
                       BpressedFlag == 0 &&
                       CpressedFlag == 0 &&
                       DpressedFlag == 0 &&
                       IpressedFlag == 0 &&
                       LpressedFlag == 0 &&
                       PpressedFlag == 0 &&
                       RpressedFlag == 0 &&
                       SpressedFlag == 0 &&
                       TpressedFlag == 0 &&
                       WpressedFlag == 0 &&
                       EpressedFlag == 0)
                {
                    listBox2.Items.Clear();
                    VpressedFlag = 0;
                }

                //************************W************************
                if (e.KeyChar == 'W')
                {

                    if (WpressedFlag == 0)
                    {
                        keyword = "W";
                        listBox2.Items.Add("Whatever");
                    }
                    WpressedFlag = 1;
                    listShow = true;
                    Point point = this.richTextBox1.GetPositionFromCharIndex(richTextBox1.SelectionStart);
                    point.Y += (int)Math.Ceiling(this.richTextBox1.Font.GetHeight()) + richTextBox1.Location.Y; //set y pos of list box
                    point.X += richTextBox1.Location.X; //set x pos of list box
                    listBox2.Location = point;
                    count++;
                    listBox2.Show();
                    listBox2.SelectedIndex = 0;
                    listBox2.SelectedIndex = listBox2.FindString(keyword);
                    richTextBox1.Focus();
                }
                else if (e.KeyChar == ' ' && WpressedFlag == 1 &&
                       BpressedFlag == 0 &&
                       CpressedFlag == 0 &&
                       DpressedFlag == 0 &&
                       IpressedFlag == 0 &&
                       LpressedFlag == 0 &&
                       PpressedFlag == 0 &&
                       RpressedFlag == 0 &&
                       SpressedFlag == 0 &&
                       TpressedFlag == 0 &&
                       VpressedFlag == 0 &&
                       EpressedFlag == 0)
                {
                    listBox2.Items.Clear();
                    WpressedFlag = 0;
                }



                //**********************END OF LIST INTIALLIZING**************************



            }
            catch { }


        }

        private void listBox2_DoubleClick(object sender, EventArgs e)
        {
            //the following function control the double click of mouth to select item from list box
            try
            {

                string autoText = listBox2.SelectedItem.ToString();
                int beginPlace = richTextBox1.SelectionStart - count;
                richTextBox1.Select(beginPlace, count);
                richTextBox1.SelectedText = "";
                richTextBox1.Text += autoText;
                richTextBox1.Focus();
                listShow = false;
                listBox2.Hide();
                int endPlace = autoText.Length + beginPlace;
                richTextBox1.SelectionStart = endPlace;
                count = 0;

            }
            catch { }
        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            //the following code controls the movement in the list up or down and also selection (tab) key and (enter,space) keys which close the list
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    count = 0;
                    keyword = "";
                    listShow = false;
                    listBox2.Hide();
                    listBox2.Items.Clear();

                    BpressedFlag = 0;
                    CpressedFlag = 0;
                    DpressedFlag = 0;
                    EpressedFlag = 0;
                    IpressedFlag = 0;
                    LpressedFlag = 0;
                    PpressedFlag = 0;
                    RpressedFlag = 0;
                    SpressedFlag = 0;
                    TpressedFlag = 0;
                    VpressedFlag = 0;
                    WpressedFlag = 0;

                }
                if (e.KeyCode == Keys.Space)
                {
                    count = 0;
                    keyword = "";
                    listShow = false;
                    listBox2.Hide();
                }

                if (listShow == true)
                {
                    if (e.KeyCode == Keys.Up)
                    {
                        listBox2.Focus();
                        if (listBox2.SelectedIndex != 0)
                        {
                            listBox2.SelectedIndex -= 1;
                        }
                        else
                        {
                            listBox2.SelectedIndex = 0;
                        }
                        richTextBox1.Focus();

                    }
                    else if (e.KeyCode == Keys.Down)
                    {
                        listBox2.Focus();
                        listBox2.SelectedIndex += 1;

                        richTextBox1.Focus();
                    }

                    if (e.KeyCode == Keys.Tab)
                    {

                        string autoText = listBox2.SelectedItem.ToString();

                        int beginPlace = richTextBox1.SelectionStart - count;
                        richTextBox1.Select(beginPlace, count);
                        richTextBox1.SelectedText = "";
                        richTextBox1.Text += autoText;
                        richTextBox1.Focus();
                        listShow = false;
                        listBox2.Hide();
                        listBox2.Items.Clear();

                        BpressedFlag = 0;
                        CpressedFlag = 0;
                        DpressedFlag = 0;
                        EpressedFlag = 0;
                        IpressedFlag = 0;
                        LpressedFlag = 0;
                        PpressedFlag = 0;
                        RpressedFlag = 0;
                        SpressedFlag = 0;
                        TpressedFlag = 0;
                        VpressedFlag = 0;
                        WpressedFlag = 0;



                        int endPlace = autoText.Length + beginPlace;
                        richTextBox1.SelectionStart = endPlace;
                        count = 0;

                    }
                }
            }
            catch (ArgumentOutOfRangeException z) { }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            richTextBox1.SelectionBackColor = Color.White;
        }

        private void richTextBox1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void richTextBox1_SelectionChanged(object sender, EventArgs e)
        {
            //the following code get the cursor position in line and columns
            int index = richTextBox1.SelectionStart;
            int line = richTextBox1.GetLineFromCharIndex(index)+1;
            label4.Text = "Ln:" + line.ToString();
            int column = richTextBox1.SelectionStart - richTextBox1.GetFirstCharIndexFromLine(line-1);
            label2.Text = "Col:" + column.ToString();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|RTF Files (*.rtf)|*.rtf";
            saveFileDialog.AddExtension = true;
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var extension = System.IO.Path.GetExtension(saveFileDialog.FileName);
                if (extension.ToLower() == ".txt") /*saveFileDialog.FilterIndex==1*/
                    richTextBox1.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.PlainText);
                else
                    richTextBox1.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.RichText);
            }
        }

        private void button9_MouseHover(object sender, EventArgs e)
        {
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 10;
            toolTip1.ReshowDelay = 500;
            toolTip1.SetToolTip(button9, "Save file");
        }
    }
}
