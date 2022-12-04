using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGame
{
    internal class Tile
    {
        public Texture2D sprite;
        public Rectangle area;
        public int spriteIndex;
        public int width = 40;
        public int height = 40;

        public Tile(List<Texture2D> _tileSprites, int _sprite) 
        {
            spriteIndex = _sprite;
            sprite = _tileSprites[spriteIndex];
        }
    }

    public enum tiles
    {
        EMPTY,
        BASE,
    };
}
