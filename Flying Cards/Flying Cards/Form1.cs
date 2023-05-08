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

        //Declara variabilele de nivel curent si punctaj curent
        int CURRENT_LEVEL;
        int SCORE;

        //Declara locatia imaginilor si un vector pentru a stoca alegerile corecte
        string PATH = "C:\\Users\\TheBoss\\OneDrive\\Desktop\\Atestate\\Flying Cards\\Flying Cards\\Resources\\";
        int[] choices = new int[11];

        //Declara un timer pentru timpul ramas si timpul ramas
        int timeLeft = 5;

        //Initializarea jocului
        private void start_button_Click(object sender, EventArgs e)
        {
            //Seteaza nivelul curent si punctajul la inceput
            CURRENT_LEVEL = 0;
            SCORE = 10;

            //Ascunde meniul de pornire
            menu.Enabled = false;
            menu.Visible = false;

            //Ascunde titlul jocului
            title.Enabled = false;
            title.Visible = false;

            //Ascunde butonul de pornire joc
            start_button.Enabled = false;
            start_button.Visible = false;

            //Ascunde butonul de iesire din joc
            quit_button.Enabled = false;
            quit_button.Visible = false;

            //Seteaza nivelul urmator si genereaza alegerile corecte
            nextLevel();
            getGoodChoices();
        }

        //Functie pentru butonul de iesire din joc
        private void quit_button_Click(object sender, EventArgs e)
        {
            //Afiseaza un mesaj pentru confirmarea iesirii din joc
            if (MessageBox.Show("Are you sure you want to quit?", "Quit Game", MessageBoxButtons.YesNo) == DialogResult.Yes)
                this.Close();
        }

        //Functie pentru a genera alegerile corecte
        private void getGoodChoices()
        {
            //Afiseaza o imagine si seteaza panoul pentru alegere
            pictureShow.Enabled = true;
            pictureShow.Visible = true;
            panel.Enabled = true;
            panel.Visible = true;

            //Genereaza alegerile corecte
            for (int i = 1; i <= 10; i++)
            {
                choices[i] = new Random().Next(1, 5);
                pictureShow.Image = Image.FromFile(PATH + i + "-" + choices[i] + ".png");
                wait(500);
            }
            //Ascunde imaginea si panoul de alegere
            pictureShow.Enabled = false;
            pictureShow.Visible = false;
            panel.Enabled = false;
            panel.Visible = false;
            //Porneste timer-ul pentru timpul ramas si afiseaza timpul ramas
            timer1.Enabled = true;
            txtTime.Visible = true;
            txtTime.Enabled = true;
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // Aceasta functie este apelata atunci cand unul dintre cele patru PictureBox-uri este apasat.
            // Functia verifica daca imaginea selectata este corecta sau nu si modifica imaginea corespunzatoare cu marcajul "tick" sau "x".
            // Daca imaginea selectata este gresita, scorul este decrementat cu 1.
            // Apoi se asteapta 1 secunda si se trece la urmatorul nivel prin apelarea functiei nextLevel().
            PictureBox pic = sender as PictureBox;
            if (pic == pictureBox1 && choices[CURRENT_LEVEL] == 1)
                pic.Image = Image.FromFile(PATH + "tick.png");
            else if (pic == pictureBox2 && choices[CURRENT_LEVEL] == 2)
                pic.Image = Image.FromFile(PATH + "tick.png");
            else if (pic == pictureBox3 && choices[CURRENT_LEVEL] == 3)
                pic.Image = Image.FromFile(PATH + "tick.png");
            else if (pic == pictureBox4 && choices[CURRENT_LEVEL] == 4)
                pic.Image = Image.FromFile(PATH + "tick.png");
            else
            {
                pic.Image = Image.FromFile(PATH + "x.png");
                SCORE--;
            }
            wait(1000);
            nextLevel();
        }

        private void nextLevel()
        {
            // Aceasta functie incrementeaza numarul nivelului curent si verifica daca jocul s-a terminat sau nu.
            // Daca jocul s-a terminat, functia va apela functia gameOver() pentru a afisa scorul si a permite jucatorului sa reinceapa jocul.
            // Daca jocul nu s-a terminat, se actualizeaza imaginile PictureBox-urilor cu cele corespunzatoare noului nivel.
            timeLeft = 5;
            CURRENT_LEVEL++;
            if (CURRENT_LEVEL > 10)
            {
                gameOver();
            }
            else
            {
                pictureBox1.Image = Image.FromFile(PATH + CURRENT_LEVEL + "-1.png");
                pictureBox2.Image = Image.FromFile(PATH + CURRENT_LEVEL + "-2.png");
                pictureBox3.Image = Image.FromFile(PATH + CURRENT_LEVEL + "-3.png");
                pictureBox4.Image = Image.FromFile(PATH + CURRENT_LEVEL + "-4.png");
            }
        }

        private void gameOver()
        {
            // Aceasta functie afiseaza un mesaj care indica daca jucatorul a castigat sau a pierdut jocul si scorul obtinut.
            // Apoi, se afiseaza butoanele "Play Again" si "Quit" pentru a permite jucatorului sa reinceapa jocul sau sa il inchida.
            // Timerul care masoara timpul ramas pentru fiecare nivel este dezactivat si ascuns, iar label-ul care afiseaza scorul jucatorului este afisat.

            menu.Enabled = true;
            menu.Visible = true;

            if (SCORE > 4)
                title.Text = "You Win!";
            else
                title.Text = "You Lose!";

            title.Enabled = true;
            title.Visible = true;

            start_button.Text = "Play Again";
            start_button.Enabled = true;
            start_button.Visible = true;

            quit_button.Enabled = true;
            quit_button.Visible = true;

            txtScore.Visible = true;
            txtScore.Text = "Score: " + SCORE;

            timer1.Enabled = false;
            txtTime.Visible = false;
            txtTime.Enabled = false;
            txtTime.Text = "5";
        }

        // Functia "wait" asteapta un anumit numar de milisecunde inainte de a continua executia
        // https://stackoverflow.com/questions/10458118/wait-one-second-in-running-program
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

        // Aceasta functie este apelata la fiecare tick al timer-ului (la fiecare secunda).
        private void timer1_Tick(object sender, EventArgs e)
        {
            // Se actualizeaza textul din textbox-ul care indica timpul ramas.
            txtTime.Text = timeLeft.ToString();
            // Daca timpul a expirat
            if (timeLeft == 0)
            {
                // Se scade un punct din scor si se trece la urmatorul nivel.
                SCORE--;
                nextLevel();
            }
            else
                // Daca timpul nu a expirat, se scade o secunda din timpul ramas.
                timeLeft--;
        }
    }
}
