//Made by AnonimKisi

//Add the necessary libraries
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Threading;

namespace Snake
{
    public partial class Form1 : Form
    {
        //Declare booleans for the timer
        bool up_ispressed = false;
        bool down_ispressed = false;
        bool left_ispressed = false;
        bool right_ispressed = false;

        //Declare variables for score, size and speed
        int points = 0;
        int i = -5;
        int j = 5;
        int k = 10;

        //Make a restart function
        void GameOver()
        {
            void Restart()
            {
                var applicationPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                Process.Start(applicationPath);
                Environment.Exit(1);
            }
            timer1.Stop();
            MessageBox.Show("GAME OVER\nYour final score: " + points);
            Restart();
        }

        //Make a function to spawn an 'apple'
        void GenerateApple()
        {
            Random random = new Random();
            int random_x = random.Next(10, 380);
            int random_y = random.Next(10, 380);
            apple.Location = new Point(random_x, random_y);
            apple.Show();
        }
        //Start the program
        public Form1()
        {
            InitializeComponent();
        }
        //Generate an 'apple', give 'snake' a size and change title to "Score: 0"
        private void Form1_Load(object sender, EventArgs e)
        {
            GenerateApple();
            snake.Size = new Size(k, k);
            this.Text = "Score: 0";
            timer1.Start();
        }
        //detect arrow keys and move the 'snake'
        void snake_PreviewKeyDown_1(object sender, PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = true;

            if (e.KeyCode == Keys.Up)
            {
                up_ispressed = true;
                down_ispressed = false;
                left_ispressed = false;
                right_ispressed = false;

                snake.Top = snake.Top + i;
            }

            if (e.KeyCode == Keys.Down)
            {
                up_ispressed = false;
                down_ispressed = true;
                left_ispressed = false;
                right_ispressed = false;

                snake.Top = snake.Top + j;
            }

            if (e.KeyCode == Keys.Left)
            {
                up_ispressed = false;
                down_ispressed = false;
                left_ispressed = true;
                right_ispressed = false;

                snake.Left = snake.Left + i;
            }

            if (e.KeyCode == Keys.Right)
            {
                up_ispressed = false;
                down_ispressed = false;
                left_ispressed = false;
                right_ispressed = true;

                snake.Left = snake.Left + j;
            }

            //if 'snake' touches any label, trigger the Restart() function
            foreach (Label lbl in this.Controls.OfType<Label>())
            {
                if (snake.Bounds.IntersectsWith(lbl.Bounds))
                {
                    GameOver();
                }
            }

            //if 'snake' touches the 'apple', change its size, speed and give the player 100 points
            if (snake.Bounds.IntersectsWith(apple.Bounds))
            {
                GenerateApple();
                points = points + 100;
                i = i + -1;
                j = j + 1;
                k = k + 10;
                snake.Size = new Size(k, k);
                this.Text = "Score: " + points;
            }

        }

        //Use timer to constantly move the 'snake' in the direction of the last arrow button that is pressed
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (up_ispressed == true)
            {
                snake.Top = snake.Top + i;
            }

            if (down_ispressed == true)
            {
                snake.Top = snake.Top + j;
            }

            if (left_ispressed == true)
            {
                snake.Left = snake.Left + i;
            }

            if (right_ispressed == true)
            {
                snake.Left = snake.Left + j;
            }

            //if 'snake' touches the 'apple', change its size, speed and give the player 100 points
            if (snake.Bounds.IntersectsWith(apple.Bounds))
            {
                GenerateApple();
                points = points + 100;
                i = i + -1;
                j = j + 1;
                k = k + 10;
                snake.Size = new Size(k, k);
                this.Text = "Score: " + points;
            }

            //if 'snake' touches any label, trigger the Restart() function
            foreach (Label lbl in this.Controls.OfType<Label>())
            {
                if (snake.Bounds.IntersectsWith(lbl.Bounds))
                {
                    GameOver();
                }
            }

        }
    }
}
