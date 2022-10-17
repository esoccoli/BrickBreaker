using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickBreakerV2
{
    /// <summary>
    /// Manages the ball and related data
    /// </summary>
    internal class Ball
    {
        private int xPos;
        private int yPos;
        private int radius;
        private Color color;

        // Turns fields into properties
        // Allows other classes to access the values
        // Reduces creation of extra variables
        public int X { get => xPos; set => xPos = value; }
        public int Y { get => yPos; set => yPos = value; }
        private int Radius { get => radius; }
        private Color BallColor { get => color; set => color = value; }

    }
}
