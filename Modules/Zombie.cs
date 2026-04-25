using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZombieShooterGame.Modules
{
    public class Zombie
    {
        public PictureBox Sprite { get; private set; }
        private int _speed;
        private Random _rnd = new Random();

        public Zombie(int speed)
        {
            _speed = speed;
            Sprite = new PictureBox
            {
                Tag = "zombie",
                Image = Properties.Resources.zdown,
                SizeMode = PictureBoxSizeMode.AutoSize,
                Left = _rnd.Next(0, 900),
                Top = _rnd.Next(0, 800)
            };
        }

        public void HuntPlayer(Point playerPos)
        {
            if (Sprite.Left > playerPos.X) { Sprite.Left -= _speed; Sprite.Image = Properties.Resources.zleft; }
            if (Sprite.Left < playerPos.X) { Sprite.Left += _speed; Sprite.Image = Properties.Resources.zright; }
            if (Sprite.Top > playerPos.Y) { Sprite.Top -= _speed; Sprite.Image = Properties.Resources.zup; }
            if (Sprite.Top < playerPos.Y) { Sprite.Top += _speed; Sprite.Image = Properties.Resources.zdown; }
        }
    }
}
