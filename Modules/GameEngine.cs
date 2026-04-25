using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZombieShooterGame.Modules
{
    public class GameEngine
    {
        private Form _form;
        private PictureBox _playerSprite;

        public List<Zombie> Enemies = new List<Zombie>();
        public List<Bullet> Bullets = new List<Bullet>();

        public int Score { get; private set; }
        public int PlayerHealth { get; set; } = 100;
        public int Ammo { get; set; } = 10;

        public GameEngine(Form form, PictureBox player)
        {
            _form = form;
            _playerSprite = player;
        }

        public void UpdateGame()
        {
            // Logica Gloante
            foreach (var b in Bullets.ToList())
            {
                b.Move();
                if (b.Sprite.Left < 0 || b.Sprite.Left > _form.Width || b.Sprite.Top < 0 || b.Sprite.Top > _form.Height)
                    RemoveBullet(b);
            }

            // Logica Zombi
            foreach (var z in Enemies.ToList())
            {
                z.HuntPlayer(_playerSprite.Location);

                if (_playerSprite.Bounds.IntersectsWith(z.Sprite.Bounds))
                    PlayerHealth -= 1;

                foreach (var b in Bullets.ToList())
                {
                    if (z.Sprite.Bounds.IntersectsWith(b.Sprite.Bounds))
                    {
                        Score++;
                        RemoveBullet(b);
                        RemoveZombie(z);
                        SpawnZombie();
                    }
                }
            }
        }

        public void Shoot(Direction dir)
        {
            if (Ammo <= 0) return;
            Ammo--;
            var bullet = new Bullet(dir, _playerSprite.Left + 20, _playerSprite.Top + 20);
            Bullets.Add(bullet);
            _form.Controls.Add(bullet.Sprite);
        }

        public void SpawnZombie()
        {
            var z = new Zombie(3);
            Enemies.Add(z);
            _form.Controls.Add(z.Sprite);
            _playerSprite.BringToFront();
        }

        private void RemoveBullet(Bullet b) 
        {
            Bullets.Remove(b); _form.Controls.Remove(b.Sprite); b.Sprite.Dispose(); 
        }
        private void RemoveZombie(Zombie z) 
        {
            Enemies.Remove(z); _form.Controls.Remove(z.Sprite); z.Sprite.Dispose(); 
        }

        public void Reset()
        {
            foreach (var z in Enemies) _form.Controls.Remove(z.Sprite);
            foreach (var b in Bullets) _form.Controls.Remove(b.Sprite);
            Enemies.Clear();
            Bullets.Clear();
            Score = 0; PlayerHealth = 100; Ammo = 10;
        }
    }
}
