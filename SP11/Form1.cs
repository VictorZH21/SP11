using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace SP11
{
    public partial class Form1 : Form
    {
        private int keystrokesCount = 0;
        private string FolderPath = @"C:\Users\CoFFey\Desktop\Screen";
        public static Bitmap screen = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
        public Form1()
        {
            InitializeComponent();
            timer1.Enabled = true;
            timer1.Interval = 10000;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Graphics graphics = Graphics.FromImage(screen as Image);
            graphics.CopyFromScreen(0, 0, 0, 0, screen.Size);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Image = Form1.screen;
            SaveScreenshot();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }
        private void SaveScreenshot()
        {
            string fileName = $"Screenshot_{DateTime.Now:yyyyMMdd_HHmmss}.png";
            string filePath = Path.Combine(FolderPath, fileName);
            try
            {
                screen.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
                using (StreamWriter sw = File.AppendText(Path.Combine(FolderPath, "log.txt")))
                {
                    sw.WriteLine(DateTime.Now.ToShortTimeString());
                    sw.WriteLine(fileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            keystrokesCount++;
            label1.Text = keystrokesCount.ToString();
            SaveScreenshot();
        }
    }
}
