using Microsoft.Xna.Framework;

namespace BrickBreaker
{
    /// <summary>
    /// Manages the bricks and related data
    /// </summary>
    public class Brick
    {
        /// <summary>
        /// Color of the brick
        /// </summary>
        private Color color;
        
        /// <summary>
        /// Bounds of the brick, which is also used as the hitbox for collision detection
        /// </summary>
        public Rectangle Hitbox { get; private set; }

        /// <summary>
        /// Whether the brick is broken or not
        /// </summary>
        public bool Broken { get; set; }

        /// <summary>
        /// Sets up a brick with a specified hitbox and color
        /// </summary>
        /// <param name="hitbox">The hitbox (position) of the brick</param>
        /// <param name="color">The color of the brick</param>
        public Brick(Rectangle hitbox, Color color)
        {
            Hitbox = hitbox;
            this.color = color;
            Broken = false;
        }

        /// <summary>
        /// Sets up a brick with a specified hitbox and default color
        /// </summary>
        /// <param name="hitbox">The hitbox (position) of the brick</param>
        public Brick(Rectangle hitbox)
        {
            Hitbox = hitbox;
            color = new Color(217, 15, 42);
            Broken = false;
        }
    }
}
