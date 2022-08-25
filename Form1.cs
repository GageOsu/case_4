using System;
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
            // Обработка ошибки
            if (res == DialogResult.OK)
            {
                filePath = openFileDialog1.FileName;
                pictureBox1.Image = Image.FromFile(filePath);
            }
            else
            {
                MessageBox.Show("Скан не выбран", "Выберете сканнированый документ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(filePath) || String.IsNullOrWhiteSpace(filePath))
                {
                    throw new Exception("Документ не выбран");
                }
                else
                {
                    Tesseract tesseract = new Tesseract(@"C:\Users\danni\Desktop", lang, OcrEngineMode.TesseractLstmCombined);
                    tesseract.SetImage(new Image<Bgr, byte>(filePath));
                    tesseract.Recognize();
                    richTextBox1.Text = tesseract.GetUTF8Text();

                    string str = richTextBox1.Text;
                    // проверка на Счет-фактура
                    if (((str.IndexOf("СЧЕТ-ФАКТУРА") != -1) || (str.IndexOf("Счет-фактура") != -1)) || ((str.IndexOf("счет-фактура") != -1)))
                    {
                        MessageBox.Show("Документ: Счет-фактура");
                    }

                    // проверка на Отчет
                    else if (((str.IndexOf(" ОТЧЕТ ") != -1) || (str.IndexOf(" отчет ") != -1)) || ((str.IndexOf(" Отчет ") != -1)))
                    {
                        MessageBox.Show("Документ: Отчет");
                    }

                    // Проверка на счет на оплату 
                    else if (((str.IndexOf("СЧЕТ") != -1) && (str.IndexOf("НА ОПЛАТУ") != -1)) || ((str.IndexOf("счет") != -1) && (str.IndexOf("на оплату") != -1)) || ((str.IndexOf("Счет") != -1) && (str.IndexOf("на оплату") != -1)))
                    {
                        MessageBox.Show("Документ: Счет на оплату");
                    }

                    // проверка на Счет
                    else if (((str.IndexOf(" счет ") != -1) || (str.IndexOf(" Счет ") != -1)) || ((str.IndexOf(" СЧЕТ ") != -1)))
                    {
                        MessageBox.Show("Документ: Счет");
                    }

                    // Проверка на АКТ СДАЧИ ПРИЕМКИ 
                    else if (((str.IndexOf("сдачи-приемки") != -1) && (str.IndexOf("Акт") != -1)) || ((str.IndexOf("сдачи-приемки") != -1) && (str.IndexOf("АКТ") != -1)) || ((str.IndexOf("СДАЧИ-ПРИЕМКИ") != -1) && (str.IndexOf("АКТ") != -1)))
                    {
                        MessageBox.Show("Документ: Акт сдачи-приемки");
                    }

                    // проверка на АКТ
                    else if (((str.IndexOf(" АКТ ") != -1) || (str.IndexOf(" Акт ") != -1)) || ((str.IndexOf(" акт ") != -1)))
                    {
                        MessageBox.Show(" Документ: Акт");
                    }

                    // Проверка на ПРИХОДНЫЙ КАССОВЫЙ ОРДЕР
                    else if (((str.IndexOf("ПРИХОДНЫЙ") != -1) && (str.IndexOf("КАССОВЫЙ ОРДЕР") != -1)) || ((str.IndexOf("приходный") != -1) && (str.IndexOf("кассовый ордер") != -1)) || ((str.IndexOf("Приходный") != -1) && (str.IndexOf("кассовый ордер") != -1)))
                    {
                        MessageBox.Show("Документ: Счет на оплату");
                    }

                    // проверка на Справка
                    else if (((str.IndexOf(" справка ") != -1) || (str.IndexOf(" Справка ") != -1)) || ((str.IndexOf(" СПРАВКА ") != -1)))
                    {
                        MessageBox.Show("Документ: Справка");
                    }

                    else
                    {
                        MessageBox.Show("Нет");
                    }

                            tesseract.Dispose();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Oшибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
