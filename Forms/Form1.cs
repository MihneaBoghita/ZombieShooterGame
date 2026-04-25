using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ZombieShooterGame.Modules;

namespace ZombieShooterGame
{
    public partial class Form1 : Form
    {
        bool goLeft, goRight, goUp, goDown, gameOver;
        Direction facing = Direction.Up;
        GameEngine engine;

        public Form1()
        {
            InitializeComponent();
            engine = new GameEngine(this, Player); 
            RestartGame();
        }

        private void MainTimerEvent(object sender, EventArgs e)
        {
            if (engine.PlayerHealth > 1)
            {
                HealthBar.Value = engine.PlayerHealth;
                txtAmmo.Text = "Ammo: " + engine.Ammo;
                txtScore.Text = "Score: " + engine.Score;

                MovePlayer();
                engine.UpdateGame();
            }
            else
            {
                gameOver = true;
                Player.Image = Properties.Resources.dead;
                GameTimer.Stop();
            }
        }

        private void MovePlayer()
        {
            if (goLeft && Player.Left > 0) Player.Left -= 10;
            if (goRight && Player.Right < ClientSize.Width) Player.Left += 10;
            if (goUp && Player.Top > 40) Player.Top -= 10;
            if (goDown && Player.Bottom < ClientSize.Height) Player.Top += 10;
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (gameOver) return;
            if (e.KeyCode == Keys.Left) { goLeft = true; facing = Direction.Left; Player.Image = Properties.Resources.left; }
            if (e.KeyCode == Keys.Right) { goRight = true; facing = Direction.Right; Player.Image = Properties.Resources.right; }
            if (e.KeyCode == Keys.Up) { goUp = true; facing = Direction.Up; Player.Image = Properties.Resources.up; }
            if (e.KeyCode == Keys.Down) { goDown = true; facing = Direction.Down; Player.Image = Properties.Resources.down; }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) goLeft = false;
            if (e.KeyCode == Keys.Right) goRight = false;
            if (e.KeyCode == Keys.Up) goUp = false;
            if (e.KeyCode == Keys.Down) goDown = false;

            if (e.KeyCode == Keys.Space && !gameOver) engine.Shoot(facing);
            if (e.KeyCode == Keys.Enter && gameOver) RestartGame();
        }

        private void RestartGame()
        {
            engine.Reset();
            for (int i = 0; i < 3; i++) engine.SpawnZombie();
            gameOver = false;
            GameTimer.Start();
        }
    }
}