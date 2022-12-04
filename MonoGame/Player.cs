using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace MonoGame
{
    internal class Player
    {

        public Texture2D sprite;
        public Rectangle hitBox;
        public Vector2 position;
        public Color color = Color.White;
        int width = 40;
        int height = 80;

        public float speed = 0f;
        float acceleration =16f;
        const float maxSpeed = 8f;
        const float deceleration = 50f;
        float fallSpeed = 0f;
        float gravity = 40f;
        const float maxFallSpeed = 20f;
        bool movingLeft = false;
        bool movingRight = false;

        bool inAir = false;


        public Player(Texture2D _sprite, Vector2 _position)
        {
            sprite = _sprite;
            hitBox = new Rectangle((int)_position.X, (int)_position.Y, width, height);
            position = _position;
        }

        public void update(GameTime gameTime, Level _level)
        {
            
            //Movement
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                speed += (float)(acceleration * gameTime.ElapsedGameTime.TotalSeconds);
                if (speed > maxSpeed) speed = maxSpeed;
                movingRight = true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                speed -= (float)(acceleration * gameTime.ElapsedGameTime.TotalSeconds);
                if (speed < -maxSpeed) speed = -maxSpeed;
                movingLeft = true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                if (!inAir) { fallSpeed = -20f; inAir = true; }

            }
            if (inAir)
            {
                fallSpeed += (float)(gravity * gameTime.ElapsedGameTime.TotalSeconds);
            }
            //Deceleration
            if (speed != 0)
            {
                if (!movingRight && speed > 0.5f) speed -= (float)(deceleration * gameTime.ElapsedGameTime.TotalSeconds);
                else if (!movingLeft && speed < -0.5f) speed += (float)(deceleration * gameTime.ElapsedGameTime.TotalSeconds);
                else if (!movingLeft && !movingRight && speed > -0.5f && speed < 0.5f) speed = 0;
            }
            //Test for collisions
            {
                Rectangle nextHitBox = new Rectangle((int)(hitBox.X + speed), (int)(hitBox.Y + fallSpeed), hitBox.Width, hitBox.Height);
                for (int y = (int)(position.Y - height); y < position.Y + (2 * height); y += 40)
                {
                    if (y < 0) y = 0;
                    if (y >= 1080) break;
                    for (int x = (int)(position.X - (2 * width)); x < position.X + (3 * width); x += 40)
                    {
                        if (x < 0) x = 0;
                        if (x >= 1920) break;
                        if (_level.tileRow[y / 40].tile[x / 40].area.Intersects(nextHitBox) && _level.tileRow[y / 40].tile[x / 40].spriteIndex != 0)
                        {
                            /* (Above code may need removing/changing)
                            Check which are the first box(es) collided with along the path to the new position
                            Check which corners are colliding with anything to determine which sides are colliding
                            (e.g both left = sideways collision, both bottom = falling collision)
                            speed = 0 or fallSpeed = 0 depending on that
                            Find which corners are colliding and adjust to those tiles area.Left/Bottom/...
                            If top left, bottom left, bottom right all collide with something ignore bottom left
                             */ 
                        }
                    }
                }
                //Position update
                position.X += speed;
                position.Y += fallSpeed;
                //Update hitBox (immediately after movement)
                hitBox = new Rectangle((int)position.X, (int)position.Y, width, height);
            }

            //Reset variables
            movingRight = false;
            movingLeft = false;
        }
    }
}
