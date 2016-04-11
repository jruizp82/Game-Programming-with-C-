using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RubberChickenGame
{
    /// <summary>
    /// A rubber chicken
    /// </summary>
    class RubberChicken
    {
        #region Fields

        bool active = true;
        int damage;

        // drawing support
        Texture2D sprite;
        Rectangle drawRectangle;

        // moving support
        const float RUBBER_CHICKEN_SPEED = 0.8f;
        Vector2 velocity = Vector2.Zero;

        // click processing
        bool clickStarted = false;
        bool buttonReleased = true;
        bool moving = false;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sprite">sprite</param>
        /// <param name="location">location of center</param>
        /// <param name="damage">damage from rubber chicken</param>
        public RubberChicken(Texture2D sprite, Vector2 location,
            int damage)
        {
            this.sprite = sprite;
            this.damage = damage;

            // build draw rectangle
            drawRectangle = new Rectangle(
                (int)(location.X - sprite.Width / 2),
                (int)(location.Y - sprite.Height / 2),
                sprite.Width, sprite.Height);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets and sets whether the rubber chicken is active
        /// </summary>
        public bool Active
        {
            get { return active; }
            set { active = value; }
        }

        /// <summary>
        /// Gets the collision rectangle for the rubber chicken
        /// </summary>
        public Rectangle CollisionRectangle
        {
            get { return drawRectangle; }
        }

        /// <summary>
        /// Gets the damage the rubber chicken inflicts
        /// </summary>
        public int Damage
        {
            get { return damage; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Updates the rubber chicken, moving and launching
        /// when clicked
        /// </summary>
        /// <param name="gameTime">game time</param>
        /// <param name="mouse">mouse state</param>
        public void Update(GameTime gameTime, MouseState mouse)
        {
            // move based on velocity
            drawRectangle.X += (int)(velocity.X * gameTime.ElapsedGameTime.Milliseconds);
            drawRectangle.Y += (int)(velocity.Y * gameTime.ElapsedGameTime.Milliseconds);

            // launch on click
            // check for mouse over rubber chicken
            if (drawRectangle.Contains(mouse.X, mouse.Y))
            {
                // check for click started on rubber chicken
                if (mouse.LeftButton == ButtonState.Pressed &&
                    buttonReleased)
                {
                    clickStarted = true;
                    buttonReleased = false;
                }
                else if (mouse.LeftButton == ButtonState.Released)
                {
                    buttonReleased = true;

                    // if click finished on rubber chicken, launch as appropriate
                    if (clickStarted)
                    {
                        // launch if not already moving
                        if (!moving)
                        {
                            moving = true;
                            velocity = new Vector2(0, -1 * RUBBER_CHICKEN_SPEED);
                        }

                        clickStarted = false;
                    }
                }
            }
            else
            {
                // no clicking on this button
                clickStarted = false;
                buttonReleased = false;
            }
        }

        /// <summary>
        /// Draws the rubber chicken
        /// </summary>
        /// <param name="spriteBatch">sprite batch</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, drawRectangle, Color.White);
        }

        /// <summary>
        /// Resets the chicken at the given y location
        /// </summary>
        /// <param name="y">y location of center of rubber chicken</param>
        public void Reset(int y)
        {
            drawRectangle.Y = y - drawRectangle.Height / 2;
            velocity = Vector2.Zero;
            moving = false;
        }

        #endregion
    }
}
