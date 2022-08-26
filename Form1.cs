using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Windows.Forms;
using Emgu;
using Emgu.CV;
using Emgu.CV.Util;
using Emgu.CV.OCR;
using Emgu.CV.Structure;
using Emgu.Util;
using Emgu.CV.Features2D;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace case_4
{
    public partial class Form1 : Form
    {
        // Объявления переменной 
        private string filePath = string.Empty;
        private string lang = "rus";
        
        public Form1()
        {
            InitializeComponent();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult res = openFileDialog1.ShowDialog();
            // Пользователь выбирает файл 
            if (res == DialogResult.OK)
            {
                filePath = openFileDialog1.FileName;
                pictureBox1.Image = Image.FromFile(filePath);
            }
            else
            {
                MessageBox.Show("Документ не выбран", "Выберете сканнированый документ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

                if (String.IsNullOrEmpty(filePath) || String.IsNullOrWhiteSpace(filePath))
                {
                    MessageBox.Show("Документ не выбран");
                }
                else
                {
                    Tesseract tesseract = new Tesseract(@"C:\Users\danni\Desktop\DriveHack\case_4\rus\", lang, OcrEngineMode.TesseractLstmCombined);
                    tesseract.SetImage(new Image<Bgr, byte>(filePath));
                    tesseract.Recognize();
                    richTextBox1.Text = tesseract.GetUTF8Text();

                    string str = richTextBox1.Text;

                

                    // проверка на Счет-фактура
                    if (Regex.IsMatch(richTextBox1.Text, "\\bСчет-фактура\\b") || Regex.IsMatch(richTextBox1.Text, "\\bСЧЕТ-ФАКТУРА\\b") || Regex.IsMatch(richTextBox1.Text, "\\bсчет-фактура\\b"))
                    {
                        MessageBox.Show("Документ: Счет-фактура");
                    }

                    // Проверка на  ОРДЕР
                    else if (Regex.IsMatch(richTextBox1.Text, "\\bОрдер\\b") || Regex.IsMatch(richTextBox1.Text, "\\bОРДЕР\\b") || Regex.IsMatch(richTextBox1.Text, "\\bордер\\b"))
                {
                        MessageBox.Show("Документ: Ордер");
                    }

                    // проверка на Отчет
                    else if (Regex.IsMatch(richTextBox1.Text, "\\bОтчет\\b") || Regex.IsMatch(richTextBox1.Text, "\\bОТЧЕТ\\b") || Regex.IsMatch(richTextBox1.Text, "\\bотчет\\b"))
                {
                        MessageBox.Show("Документ: Отчет");
                    }

                    // проверка на АКТ
                    else if (Regex.IsMatch(richTextBox1.Text, "\\bАкт\\b") || Regex.IsMatch(richTextBox1.Text, "\\bАКТ\\b") || Regex.IsMatch(richTextBox1.Text, "\\bакт\\b"))
                {
                        MessageBox.Show(" Документ: Акт");
                    }

                    // проверка на Справка
                    else if (Regex.IsMatch(richTextBox1.Text, "\\bСправка\\b") || Regex.IsMatch(richTextBox1.Text, "\\bСПРАВКА\\b") || Regex.IsMatch(richTextBox1.Text, "\\bсправка\\b"))
                {
                        MessageBox.Show("Документ: Справка");
                    }

                    // проверка на Счет
                    else if (Regex.IsMatch(richTextBox1.Text, "\\bСчет\\b") || Regex.IsMatch(richTextBox1.Text, "\\bСЧЕТ\\b") || Regex.IsMatch(richTextBox1.Text, "\\bсчет\\b"))
                {
                        MessageBox.Show("Документ: Счет");
                    }

                    else
                    {
                        MessageBox.Show("ТЫ УМНИЦА");
                    }

                            tesseract.Dispose();

                }
            }

            }
        }
    

