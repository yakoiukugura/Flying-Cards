using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flying_Cards
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int i = 1;
        int[] choices = new int[11];
        string path = "C:\\Users\\TheBoss\\OneDrive\\Desktop\\Atestate\\Flying Cards\\Flying Cards\\Resources\\";

        public void wait(int milliseconds)
        {
            var timer1 = new System.Windows.Forms.Timer();
            if (milliseconds == 0 || milliseconds < 0) return;

            timer1.Interval = milliseconds;
            timer1.Enabled = true;
            timer1.Start();

            timer1.Tick += (s, e) =>
            {
                timer1.Enabled = false;
                timer1.Stop();
            };

            while (timer1.Enabled)
            {
                Application.DoEvents();
            }
        }

        private void getGoodChoices()
        {
            for (int i = 1; i <= 10; i++)
            {
                choices[i] = new Random().Next(1, 5);
                pictureShow.Image = Image.FromFile(path + i + "-" + choices[i] + ".png");
                wait(3000);
            }
            pictureShow.Enabled = false;
            pictureShow.Visible = false;
            panel.Enabled = false;
            panel.Visible = false;
        }

        private void nextLevel()
        {
            i++;
            if (i > 10)
            {
                MessageBox.Show("You Win!");
                this.Close();
            }
            else
            {
                pictureBox1.Image = Image.FromFile(path + i + "-1.png");
                pictureBox2.Image = Image.FromFile(path + i + "-2.png");
                pictureBox3.Image = Image.FromFile(path + i + "-3.png");
                pictureBox4.Image = Image.FromFile(path + i + "-4.png");
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            PictureBox pic = sender as PictureBox;
            if (pic == pictureBox1)
            {
                if (choices[i] == 1)
                    pic.Image = Image.FromFile(path + "tick.png");
                else
                    pic.Image = Image.FromFile(path + "x.png");
            }
            else if (pic == pictureBox2)
            {
                if (choices[i] == 2)
                    pic.Image = Image.FromFile(path + "tick.png");
                else
                    pic.Image = Image.FromFile(path + "x.png");
            }
            else if (pic == pictureBox3)
            {
                if (choices[i] == 3)
                    pic.Image = Image.FromFile(path + "tick.png");
                else
                    pic.Image = Image.FromFile(path + "x.png");
            }
            else if (pic == pictureBox4)
            {
                if (choices[i] == 4)
                    pic.Image = Image.FromFile(path + "tick.png");
                else
                    pic.Image = Image.FromFile(path + "x.png");
            }
            wait(1000);
            nextLevel();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            getGoodChoices();
        }
    }
}
