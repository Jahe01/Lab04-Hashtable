using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        string filePath = string.Empty;
        string dictionaryFile = string.Empty;
        string targetFile = string.Empty;
        string missedWordsFile = string.Empty;
        public Form1()
        {
            dictionaryFile = ConfigurationManager.AppSettings["dictionaryFile"];
            targetFile = ConfigurationManager.AppSettings["targetFile"];
            missedWordsFile = ConfigurationManager.AppSettings["missedWordsFile"];
            InitializeComponent();
        }


        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void hashtableToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
            Hashtable englishHashDictionary = new Hashtable();
            
            DateTime startDate = DateTime.Now;
            foreach (string line in System.IO.File.ReadLines(dictionaryFile))
            {
                if (!englishHashDictionary.ContainsKey(line))
                {
                    englishHashDictionary.Add(line, line);
                }
            }
            TimeSpan timeDiff = DateTime.Now - startDate;
            string result = string.Format("{0} add to Hashtable in {1} ms", dictionaryFile, timeDiff.TotalMilliseconds);
            Console.WriteLine(result);
            toolStripStatusLabel1.Text = result;
            
            IList<string> missedWords = new List<string>();
            foreach (string line in System.IO.File.ReadLines(targetFile))
            {
                string[] words = line.Replace(",", "").Replace("!", "").Split(' ');
                foreach (string word in words)
                {
                    if (!englishHashDictionary.ContainsKey(word))
                    {
                        if (!missedWords.Contains(word))
                        {
                            missedWords.Add(word);
                        }
                    }
                }
            }
            if (missedWords.Count > 0)
            {
                MessageBox.Show(string.Join(Environment.NewLine, missedWords));
                System.IO.File.WriteAllLines(missedWordsFile, missedWords);
            }
            Console.WriteLine("File processed!!");

        }

        private void dictionaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> englishDictionary = new Dictionary<string, string>();
            
            DateTime startDate = DateTime.Now;
            foreach (string line in System.IO.File.ReadLines(dictionaryFile))
            {
                if (!englishDictionary.ContainsKey(line))
                {
                    englishDictionary.Add(line, line);
                }
            }
            TimeSpan timeDiff = DateTime.Now - startDate;
            string result = string.Format("{0} added to Dictionary in {1} ms", dictionaryFile, timeDiff.TotalMilliseconds);
            Console.WriteLine(result);
            toolStripStatusLabel2.Text = result;
            
            IList<string> missedWords = new List<string>();
            foreach (string line in System.IO.File.ReadLines(targetFile))
            {
                string[] words = line.Replace(",", "").Replace("!", "").Split(' ');
                foreach (string word in words)
                {
                    if (!englishDictionary.ContainsKey(word))
                    {
                        if (!missedWords.Contains(word))
                        {
                            missedWords.Add(word);
                        }
                    }
                }
            }
            if (missedWords.Count > 0)
            {
                MessageBox.Show(string.Join(Environment.NewLine, missedWords));
                System.IO.File.WriteAllLines(missedWordsFile, missedWords);
            }
            Console.WriteLine("File processed!!");
        }

        private void sortedDictionaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SortedDictionary<string, string> englishSortedDictionary = new SortedDictionary<string, string>();
           
            DateTime startDate = DateTime.Now;
            foreach (string line in System.IO.File.ReadLines(dictionaryFile))
            {
                if (!englishSortedDictionary.ContainsKey(line))
                {
                    englishSortedDictionary.Add(line, line);
                }
            }
            TimeSpan timeDiff = DateTime.Now - startDate;
            string result = string.Format("{0} added to SortedDictionary in {1} ms", dictionaryFile, timeDiff.TotalMilliseconds);
            Console.WriteLine(result);
            toolStripStatusLabel3.Text = result;
            
            IList<string> missedWords = new List<string>();
            foreach (string line in System.IO.File.ReadLines(targetFile))
            {
                string[] words = line.Replace(",", "").Replace("!", "").Split(' ');
                foreach (string word in words)
                {
                    if (!englishSortedDictionary.ContainsKey(word))
                    {
                        if (!missedWords.Contains(word))
                        {
                            missedWords.Add(word);
                        }
                    }
                }
            }
            if (missedWords.Count > 0)
            {
                MessageBox.Show(string.Join(Environment.NewLine, missedWords));
                System.IO.File.WriteAllLines(missedWordsFile, missedWords);
            }
            Console.WriteLine("File processed!!");
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    
                    filePath = openFileDialog.FileName;
                    try
                    {
                        richTextBoxFileInitial.LoadFile(filePath, RichTextBoxStreamType.PlainText);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al cargar el archivo: " + ex.Message);
                    }
                }
            }
        }

        private void closeFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxFileInitial.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hashtable englishHashDictionary = new Hashtable();
            
            foreach (string line in System.IO.File.ReadLines(dictionaryFile))
            {
                if (!englishHashDictionary.ContainsKey(line))
                {
                    englishHashDictionary.Add(line, line);
                }
            }
            
            string[] lines = richTextBoxFileInitial.Text.Split('\n');
            richTextBoxFileFinal.Clear();
            foreach (string line in lines)
            {
                string[] words = line.Replace(",", "").Replace("!", "").Split(' ');
                foreach (string word in words)
                {
                    if (!englishHashDictionary.ContainsKey(word))
                    {
                        richTextBoxFileFinal.SelectionColor = Color.Red;
                    }
                    else
                    {
                        richTextBoxFileFinal.SelectionColor = richTextBoxFileInitial.SelectionColor;
                    }
                    richTextBoxFileFinal.AppendText(word + " ");
                }
                richTextBoxFileFinal.AppendText(Environment.NewLine);
            }
        }
    }
}
