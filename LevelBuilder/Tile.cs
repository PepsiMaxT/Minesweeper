using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LevelBuilder
{
    internal class Tile
    {
        public Texture2D sprite;
        public Rectangle area;
        public int spriteIndex;
        public int width = 40;
        public int height = 40;

        public Tile(Texture2D _sprite, int _positionX, int _positionY, int _spriteIndex) 
        {
            sprite = _sprite;
            area = new Rectangle(_positionX, _positionY, width, height);
            spriteIndex = _spriteIndex;
        }
    }
}
