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
    internal class TileRow
    {
        public List<Tile> tile;
        public TileRow(List<Tile> _tile)
        {
            tile = _tile;
        }
    }
}
