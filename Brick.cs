using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickBreaker
{
    /// <summary>
    /// Manages the bricks and related data
    /// </summary>
    internal class Brick
    {
        // ======== FIELDS ========
        private Rectangle hitbox;
        private Color color;
        private bool isBroken;

        // Turning the fields into properties
        // Allow other classes to access these values
        // Prevents creation of excessive variables
        public Rectangle Hitbox { get { return hitbox; } }
        public Color BrickColor { get => color; }
        public bool Broken { get => isBroken; set => isBroken = value; }

        // ======== CONSTRUCTORS ========
        public Brick(Rectangle hitbox, Color color)
        {
            this.hitbox = hitbox;
            this.color = color;
            isBroken = false;
        }

        public Brick(Rectangle hitbox)
        {
            this.hitbox = hitbox;
            color = Color.Red;
            isBroken = false;
        }

        public void Update(GameTime gametime, Ball ball)
        {
            Vector2 currVel = ball.Velocity;
            Vector2 currPos = ball.Position;
            Rectangle ballBounds = ball.Bounds;
        }
    }
}
