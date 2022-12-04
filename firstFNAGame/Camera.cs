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
    internal class Camera
    {
        public Viewport centre = new Viewport();

        public void align(int _playerX, int _playerY, int _X, int _Y)
        {
            centre.X = (_playerX + (_X / 2));
            centre.Y = (_playerY + (_Y / 2));
        }
    }
}
