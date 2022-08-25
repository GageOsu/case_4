using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Emgu.CV.CvEnum;

using Emgu;
using Emgu.CV;
using Emgu.CV.Util;
using Emgu.CV.OCR;
using Emgu.CV.Structure;
using Emgu.Util;
using Emgu.CV.Features2D;
using static System.Net.Mime.MediaTypeNames;

namespace case_4
{
    public partial class Form1 : Form
    {

        private string filePath = string.Empty;
        private string lang = "rus";
        
        public Form1()
        {
            InitializeComponent();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult res = openFileDialog1.ShowDialog();

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
                if(String.IsNullOrEmpty(filePath) || String.IsNullOrWhiteSpace(filePath))
                {
                    throw new Exception("Документ не выбран");
                }
                else
                {
                    Tesseract tesseract = new Tesseract(@"C:\Users\danni\Desktop", lang, OcrEngineMode.TesseractLstmCombined);
                    tesseract.SetImage(new Image<Bgr, byte>(filePath));
                    tesseract.Recognize();
                    richTextBox1.Text = tesseract.GetUTF8Text();
                    if (richTextBox1.Text.ToLower.Contains("Лес"))
                    {

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
