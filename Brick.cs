using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickBreakerV2
{
    /// <summary>
    /// Manages the bricks and related data
    /// </summary>
    internal class Brick
    {
        // ======== FIELDS ========
        private int xPos;
        private int yPos;
        private int width;
        private int height;
        private Color color;

        // Turning the fields into properties
        // Allow other classes to access these values
        // Prevents creation of excessive variables

        public int X { get => xPos; set => xPos = value; }
        public int Y { get => yPos; set => yPos = value; }
        public int Width { get => width; }
        public int Height { get => height; }
        public Color BrickColor { get => color; }


    }
}
