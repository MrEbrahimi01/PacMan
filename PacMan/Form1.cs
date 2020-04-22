using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PacMan
{
    public partial class Form1 : Form
    {
        bool CheckStart = false;

        int ghost1 = 4;
        int ghost2 = 2;
        int ghost3 = 3;
        int ghost4 = 8;

        int score = 0;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            GroupBoxVisible(false);
            groupBox2.Enabled = false;
            Pic_PacMan.Image = Properties.Resources.PacMan_Right;
        }
        private void btn_Start_Click(object sender, EventArgs e)
        {
            if (txt_Name.Text == "")
            {
                MessageBox.Show("Please Enter Name .");
            }
            else
            {
                lbl_Name.Text = "Welcome " + txt_Name.Text;
                GroupBoxVisible(true);
                btn_stop_game.Enabled = false;
            }
        }
        public void GroupBoxVisible(bool Condition)
        {
            if (Condition == true)
            {
                groupBox2.Visible = true;
                lbl_Name.Visible = true;
                lbl_Score.Visible = true;
                btn_start_game.Visible = true;
                btn_stop_game.Visible = true;
                btn_Exit.Visible = true;
            }
            else if (Condition == false)
            {
                groupBox2.Visible = false;
                lbl_Name.Visible = false;
                lbl_Score.Visible = false;
                btn_start_game.Visible = false;
                btn_stop_game.Visible = false;
                btn_Exit.Visible = false;
            }
        }

        private void Start_And_Stop(object sender, EventArgs e)
        {
            CheckStart = !CheckStart;
            if (CheckStart == true)
            {
                btn_start_game.Enabled = false;
                btn_stop_game.Enabled = true;
                groupBox2.Enabled = true;
                timer1.Start();
            }
            else
            {
                btn_start_game.Enabled = true;
                btn_stop_game.Enabled = false;
                groupBox2.Enabled = false;
                timer1.Stop();
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (btn_start_game.Enabled == false)
            {
                if (e.KeyCode == Keys.Left)
                {
                    Pic_PacMan.Image = Properties.Resources.PacMan_Left;
                    if (CheckMovePacMan())
                    {
                        Pic_PacMan.Left -= 5;
                    }
                }
                if (e.KeyCode == Keys.Right)
                {
                    Pic_PacMan.Image = Properties.Resources.PacMan_Right;
                    if (CheckMovePacMan())
                    {
                        Pic_PacMan.Left += 5;
                    }
                }
                if (e.KeyCode == Keys.Up)
                {
                    Pic_PacMan.Image = Properties.Resources.PacMan_Top;
                    if (CheckMovePacMan())
                    {
                        Pic_PacMan.Top -= 5;
                    }
                }
                if (e.KeyCode == Keys.Down)
                {
                    Pic_PacMan.Image = Properties.Resources.PacMan_Bottom;
                    if (CheckMovePacMan())
                    {
                        Pic_PacMan.Top += 5;
                    }
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pic_Ghost1.Left += ghost1;
            pic_Ghost2.Left += ghost2;
            pic_Ghost3.Left += ghost3;
            pic_Ghost4.Left += ghost4;

            if (pic_Ghost1.Bounds.IntersectsWith(pictureBox10.Bounds))
            {
                ghost1 = -ghost1;
            }
            else if (pic_Ghost1.Bounds.IntersectsWith(pictureBox3.Bounds))
            {
                ghost1 = -ghost1;
            }

            if (pic_Ghost2.Bounds.IntersectsWith(pictureBox17.Bounds))
            {
                ghost2 = -ghost2;
            }
            else if (pic_Ghost2.Bounds.IntersectsWith(pictureBox4.Bounds))
            {
                ghost2 = -ghost2;
            }

            if (pic_Ghost3.Bounds.IntersectsWith(pictureBox17.Bounds))
            {
                ghost3 = -ghost3;
            }
            else if (pic_Ghost3.Bounds.IntersectsWith(pictureBox8.Bounds))
            {
                ghost3 = -ghost3;
            }

            if (pic_Ghost4.Bounds.IntersectsWith(pictureBox8.Bounds))
            {
                ghost4 = -ghost4;
            }
            else if (pic_Ghost4.Bounds.IntersectsWith(pictureBox18.Bounds))
            {
                ghost4 = -ghost4;

            }
        }
        public bool CheckMovePacMan()
        {
            foreach (Control x in groupBox2.Controls)
            {
                if (x is PictureBox && x.Tag == "Wall" || x.Tag == "Ghost")
                {
                    if (((PictureBox)x).Bounds.IntersectsWith(Pic_PacMan.Bounds))
                    {
                        LossGame();
                        return false;
                    }
                }
                if (x is PictureBox && x.Tag == "Coin")
                {
                    if (((PictureBox)x).Bounds.IntersectsWith(Pic_PacMan.Bounds))
                    {
                        //groupBox2.Controls.Remove(x);
                        x.Location = new Point(-20, -20);
                        x.Hide();
                        score++;
                        lbl_Score.Text = "Score : " + score;
                        return true;
                    }
                }
                if (score == 7)
                {
                    PassGame();
                    return false;
                }
            }
            return true;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (btn_start_game.Enabled == true)
            {
                Application.Exit();
            }
            else
            {
                DialogResult exit = MessageBox.Show("Are You Sure ? ", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (exit == DialogResult.Yes)
                {
                    Application.Exit();
                }
            }
        }
        public void LossGame()
        {
            Pic_PacMan.Location = new Point(22, 22);
            MessageBox.Show("Game Over :(");
            score = 0;
            lbl_Score.Text = "Score : 0";
            Start_And_Stop(null, null);
            timer1.Stop();
            ShowCoin();
        }
        public void PassGame()
        {
            timer1.Stop();
            score = 0;
            lbl_Score.Text = "Score : 0";
            MessageBox.Show("Win :)");
            ShowCoin();
            Pic_PacMan.Location = new Point(22, 22);
            Start_And_Stop(null, null);
        }
        public void ShowCoin()
        {
            pictureBox21.Location = new Point(212, 17);
            pictureBox21.Show();
            pictureBox22.Location = new Point(29, 332);
            pictureBox22.Show();
            pictureBox23.Location = new Point(563, 453);
            pictureBox23.Show();
            pictureBox24.Location = new Point(510, 150);
            pictureBox24.Show();
            pictureBox25.Location = new Point(28, 150);
            pictureBox25.Show();
            pictureBox26.Location = new Point(191, 17);
            pictureBox26.Show();
            pictureBox27.Location = new Point(168, 17);
            pictureBox27.Show();
        }
    }
}
