using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickBreakerV2
{
    /// <summary>
    /// Manages the paddle and related data
    /// </summary>
    internal class Paddle
    {
        private int xPos;
        private int yPos;
        private int width;
        private int height;
        private Color color;

        // Turns fields into properties
        // Allows other classes to access the values
        // Reduces creation of extra variables
        public int X { get => xPos; set => xPos = value; }
        public int Y { get => yPos; set => yPos = value; }
        public int Width { get => width; }
        public int Height { get => height; }
        public Color PaddleColor { get => color; set => color = value; }

        // === CONSTRUCTORS ===

        /// <summary>
        /// Sets up the paddle with a rectangle for the position
        /// </summary>
        /// <param name="position">The position of the paddle</param>
        /// <param name="color">The color of the paddle</param>
        public Paddle(Rectangle position, Color color)
        {
            xPos= position.X;
            yPos = position.Y;
            width = position.Width;
            height = position.Height;
            this.color = color;
        }

        /// <summary>
        /// Sets up the paddle with an x, y, width, and height
        /// </summary>
        /// <param name="xPos">The x-coordinate of the top left corner</param>
        /// <param name="yPos">The y-coordinate of the top left corner</param>
        /// <param name="width">The width of the paddle</param>
        /// <param name="height">The height of the paddle</param>
        /// <param name="color">The color of the paddle</param>
        public Paddle(int xPos, int yPos, int width, int height, Color color)
        {
            this.xPos = xPos;
            this.yPos = yPos;
            this.width = width;
            this.height = height;
            this.color = color;
        }
    }
}
