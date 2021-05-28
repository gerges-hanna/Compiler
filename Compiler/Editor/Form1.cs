using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        public Form1()
        {
            InitializeComponent();
           
            }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //compile button
            progressBar1.Value = 0;
            this.timer1.Start();
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




            //   label4.Text="Ln"+richTextBox1.GetFirstCharIndexOfCurrentLine();





            int numOflines = int.Parse(richTextBox1.Lines.Length.ToString());
            try
            {
                listBox1.Items.Insert(numOflines - 1, numOflines);
                if (numOflines < listBox1.Items.Count)
                {
                    for (int i = numOflines; i < listBox1.Items.Count; i++)
                    {
                        listBox1.Items.RemoveAt(i);
                    }

                }
                if (numOflines == 1)
                {
                    listBox1.Items.RemoveAt(1);
                }
            }
            catch (ArgumentOutOfRangeException m)
            { }
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
            richTextBox1.SelectAll();
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
    }
}
