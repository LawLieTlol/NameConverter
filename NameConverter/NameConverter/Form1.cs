using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NameConverter
{
    public partial class Form1 : Form
    {
        string[] result;
        string[] lines;
        string pathIn;
        string pathOut;
        string idMinistry;

        int count;
        
        OpenFileDialog ofd;
        SaveFileDialog sfd;
        Dictionary<string, string> words = new Dictionary<string, string>()
        {
            {"а", "a"},
            {"б", "b"},
            {"в", "v"},
            {"г", "g"},
            {"д", "d"},
            {"е", "e"},
            {"ё", "yo"},
            {"ж", "zh"},
            {"з", "z"},
            {"и", "i"},
            {"й", "j"},
            {"к", "k"},
            {"л", "l"},
            {"м", "m"},
            {"н", "n"},
            {"о", "o"},
            {"п", "p"},
            {"р", "r"},
            {"с", "s"},
            {"т", "t"},
            {"у", "u"},
            {"ф", "f"},
            {"х", "h"},
            {"ц", "c"},
            {"ч", "ch"},
            {"ш", "sh"},
            {"щ", "sch"},
            {"ъ", "j"},
            {"ы", "i"},
            {"ь", "j"},
            {"э", "e"},
            {"ю", "yu"},
            {"я", "ya"},
            {"А", "A"},
            {"Б", "B"},
            {"В", "V"},
            {"Г", "G"},
            {"Д", "D"},
            {"Е", "E"},
            {"Ё", "Yo"},
            {"Ж", "Zh"},
            {"З", "Z"},
            {"И", "I"},
            {"Й", "J"},
            {"К", "K"},
            {"Л", "L"},
            {"М", "M"},
            {"Н", "N"},
            {"О", "O"},
            {"П", "P"},
            {"Р", "R"},
            {"С", "S"},
            {"Т", "T"},
            {"У", "U"},
            {"Ф", "F"},
            {"Х", "H"},
            {"Ц", "C"},
            {"Ч", "Ch"},
            {"Ш", "Sh"},
            {"Щ", "Sch"},
            {"Ъ", "J"},
            {"Ы", "I"},
            {"Ь", "J"},
            {"Э", "E"},
            {"Ю", "Yu"},
            {"Я", "Ya"},
        };
        StreamWriter sw;

        public Form1()
        {
            InitializeComponent();

            ofd = new OpenFileDialog();
            ofd.FileName = String.Empty;
            ofd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            ofd.InitialDirectory = @"C:\";
            ofd.Title = "Select txt file with names.";
            sfd = new SaveFileDialog();
            sfd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            sfd.InitialDirectory = @"C:\";
            sfd.Title = "Select folder for txt file with names.";
            
            comboBox1.SelectedIndex = 1;
        }

        private string Translit(string rus)
        {
            string source = rus;
            foreach (KeyValuePair<string, string> pair in words)
                source = source.Replace(pair.Key, pair.Value);
            return source;
        }

        private void SaveToFile(string[] input, string path)
        {
            sw = new StreamWriter(path);
            for (int i = 0; i < count; i++)
                sw.WriteLine(input[i]);
            sw.Close();
        }

        private void ReadFromFile(string path)
        {
            lines = File.ReadAllLines(path, Encoding.Default);
            count = File.ReadAllLines(path).Length;
        }

        private void Transform()
        {
            ReadFromFile(pathIn);
            string tmp;
            result = new string[count];

            for (int i = 0; i < count; i++)
            {
                tmp = lines[i].ToLower();
                if (tmp[0].Equals('0') || tmp[0].Equals('1') || tmp[0].Equals('2'))
                    result[i] = tmp;
                else
                {
                    tmp = Translit(tmp);
                    result[i] = tmp.Substring(0, tmp.IndexOf(' ')) + tmp.Substring(tmp.IndexOf(' ') + 1, 1)
                                        + tmp.Substring(tmp.LastIndexOf(' ') + 1, 1) + comboBox1.SelectedItem.ToString().Substring(0, 3);

                }
            }

            //foreach (string st in lines)
            //{
            //    tmp = st.ToLower();
            //    tmp = Translit(tmp);

            //    listBox1.Items.Add(tmp);
            //}

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                pathIn = "input.txt";
                pathOut = "output.txt";
                Transform();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveToFile(result, pathOut);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            


            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pathIn = ofd.FileName;
                label1.Text = ofd.FileName;
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            idMinistry = comboBox1.SelectedItem.ToString().Substring(0, 3);
            sfd.FileName = $"output_Names_{idMinistry}.txt";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pathOut = sfd.FileName;
                    Transform();
                    SaveToFile(result, pathOut);

                    label2.Text = sfd.FileName;
                }
                catch (Exception ex)
                {
                    label3.Text = ex.ToString();
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            label3.Text = comboBox1.SelectedItem.ToString().Substring(0, 3);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
