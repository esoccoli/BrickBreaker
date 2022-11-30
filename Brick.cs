using Microsoft.Xna.Framework;

namespace BrickBreaker
{
    /// <summary>
    /// Manages the bricks and related data
    /// </summary>
    internal class Brick
    {
        #region Fields
        private Rectangle hitbox;   // Size and position of brick
        private Color color;        // Color of brick
        private bool isBroken;      // Whether brick is broken
        #endregion

        #region Properties
        /// <summary>
        /// Accesses the hitbox of the brick
        /// </summary>
        public Rectangle Hitbox { get { return hitbox; } }

        /// <summary>
        /// Accesses the color of the brick
        /// </summary>
        public Color BrickColor { get => color; }

        /// <summary>
        /// Determines and/or updates the state of the brick (broken or not)
        /// </summary>
        public bool Broken { get => isBroken; set => isBroken = value; }
        #endregion

        #region Constructors
        /// <summary>
        /// Sets up a brick with a specified hitbox and color
        /// </summary>
        /// <param name="hitbox">The hitbox (position) of the brick</param>
        /// <param name="color">The color of the brick</param>
        public Brick(Rectangle hitbox, Color color)
        {
            this.hitbox = hitbox;
            this.color = color;
            isBroken = false;
        }

        /// <summary>
        /// Sets up a brick with a specified hitbox and default color
        /// </summary>
        /// <param name="hitbox">The hitbox (position) of the brick</param>
        public Brick(Rectangle hitbox)
        {
            this.hitbox = hitbox;
            color = new Color(217, 15, 42);
            isBroken = false;
        }
        #endregion
    }
}
