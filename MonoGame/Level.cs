using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGame
{
    internal class Level
    {

        public enum tiles
        {
            EMPTY,
            BASE
        };

        public List<TileRow> tileRow = new List<TileRow>();

        List<Texture2D> tileSprite;
        public Level(List<Texture2D> _tileSprite)
        {
            tileSprite = _tileSprite;
        }

        //This function sets all the tiles to be what they should according to _level
        //_levelX and _levelY act as co-ordinates, in case the level switches onto numerous areas as IDK how to make a camera
        public void Initiate(int _level, int _levelX, int _levelY)
        {
            switch (_level)
            {
                case 0:
                    
                    using (StreamReader file = new StreamReader("C:/Users/Max Taunton/source/repos/MonoGame/Content/Levels/Level0.txt")) {
                        for (int y = 0; y < 27; y++)
                        {
                            int currentPlace = 0;
                            string text = file.ReadLine();
                            tileRow.Add(new TileRow(new List<Tile>()));
                            for (int x = 0; x < 48; x++)
                            {
                                int _spriteIndex = 0;
                                for (int j = currentPlace; j < text.Length - 1; j++)
                                {
                                    int tillNextSlash = 1;
                                    if (text[j] == '/')
                                    {
                                        bool nextSlashFound = false;
                                        while (!nextSlashFound)
                                        {
                                            if (text[j + tillNextSlash] == '/')
                                            {
                                                nextSlashFound = true;
                                            }
                                            else
                                            {
                                                _spriteIndex = _spriteIndex * 10;
                                                _spriteIndex += text[j + tillNextSlash] - '0';
                                                tillNextSlash++;
                                            }
                                        }
                                    }
                                    currentPlace = j + tillNextSlash;
                                    j = text.Length;
                                }
                                tileRow[y].tile.Add(new Tile(tileSprite, _spriteIndex));
                                tileRow[y].tile[x].area = new Rectangle(x * 40, y * 40, 40, 40);
                            }
                        }
                    }
                    /*for (int y = 0; y < 27; y++)
                    {
                        for (int x = 0; x < 48; x++)
                        {
                            tileRow[y].tile[x].area = new Rectangle(x * 40, y * 40, 40, 40);
                        }
                    }*/   

                    break;
            }
        }
    }
}
