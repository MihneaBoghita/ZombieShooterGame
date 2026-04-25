using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZombieShooterGame.Modules;

namespace ZombieShooterGame
{
    public class Bullet
    {
        public PictureBox Sprite { get; private set; }
        public Direction Facing { get; set; }
        private int _speed = 20;

        public Bullet(Direction direction, int startX, int startY)
        {
            Facing = direction;
            Sprite = new PictureBox
            {
                Size = new Size(5, 5),
                BackColor = Color.White,
                Tag = "bullet",
                Left = startX,
                Top = startY
            };
        }

        public void Move()
        {
            if (Facing == Direction.Left) Sprite.Left -= _speed;
            if (Facing == Direction.Right) Sprite.Left += _speed;
            if (Facing == Direction.Up) Sprite.Top -= _speed;
            if (Facing == Direction.Down) Sprite.Top += _speed;
        }
    }
}
