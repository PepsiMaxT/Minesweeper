using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace firstFNAGame
{
    internal class Player
    {

        public Texture2D sprite;
        public Rectangle hitBox;
        public int width;
        public int height;
        public Vector2 position;

        Camera camera = new Camera();

        public int speed = 50;
        public float gravitySpeed = 0.7f;

        public float jumpSpeed;

        public bool inAir;

        public Player(Texture2D _sprite, Vector2 _position, int _width, int _height)
        {
            sprite = _sprite;
            hitBox = new Rectangle((int)(_position.X), (int)(_position.Y), _width, _height);
            width = _width;
            height = _height;
            position = new Vector2(_position.X, _position.Y);
        }

        public void move(int _amount)
        {
            position.X += _amount;
        }

        public void gravity()
        {
            jumpSpeed += gravitySpeed;
            hitBox.Y += (int)jumpSpeed;

            //Check each floor collision
            
        }

        public void update()
        {
            camera.align((int)position.X, (int)position.Y, width, height);
            hitBox.X = (int)camera.centre.X;
        }
    }
}
