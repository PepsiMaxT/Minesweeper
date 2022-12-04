using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LevelBuilder
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        List<Texture2D> tileSprites = new List<Texture2D>();
        List<Tile> tiles = new List<Tile>();

        bool[] tileMade = new bool[1296];

        int tileSpriteIndex = 0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {

            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.ApplyChanges();
            // TODO: Add your initialization logic here
            base.Initialize();
            for (int x = 0; x < 1296; x++)
            {
                tiles.Add(new Tile(tileSprites[0], (x - ((x / 27) * 27)), (x / 27), -1));
            }
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            tileSprites.Add(Content.Load<Texture2D>("tempTile"));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Back)) tileSpriteIndex = -1;
            if (Keyboard.GetState().IsKeyDown(Keys.D0)) tileSpriteIndex = 0;
            if (Keyboard.GetState().IsKeyDown(Keys.D1)) tileSpriteIndex = 1;
            if (Keyboard.GetState().IsKeyDown(Keys.D2)) tileSpriteIndex = 2;
            if (Keyboard.GetState().IsKeyDown(Keys.D3)) tileSpriteIndex = 3;
            if (Keyboard.GetState().IsKeyDown(Keys.D4)) tileSpriteIndex = 4;
            if (Keyboard.GetState().IsKeyDown(Keys.D5)) tileSpriteIndex = 5;
            if (Keyboard.GetState().IsKeyDown(Keys.D6)) tileSpriteIndex = 6;
            if (Keyboard.GetState().IsKeyDown(Keys.D7)) tileSpriteIndex = 7;
            if (Keyboard.GetState().IsKeyDown(Keys.D8)) tileSpriteIndex = 8;
            if (Keyboard.GetState().IsKeyDown(Keys.D9)) tileSpriteIndex = 9;

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                if (Mouse.GetState().X >= 0 && Mouse.GetState().X < 1920 && Mouse.GetState().Y >= 0 && Mouse.GetState().Y < 1080)
                {
                    int tileNumber = (((Mouse.GetState().Y / 40) * 48) + (Mouse.GetState().X / 40));
                    if (tileSpriteIndex >= 0) tiles[tileNumber] = new Tile(tileSprites[tileSpriteIndex], (Mouse.GetState().X / 40) * 40, (Mouse.GetState().Y / 40) * 40, tileSpriteIndex);
                    else tiles[tileNumber] = new Tile(tileSprites[0], (Mouse.GetState().X / 40) * 40, (Mouse.GetState().Y / 40) * 40, tileSpriteIndex);
                    tileMade[tileNumber] = true;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.NumPad9))
            {
                int tileNumber = 0;
                string[] Row = new string[27];
                for (int y = 0; y < 27; y++)
                {
                    Row[y] = "";
                    for (int x = 0; x < 48; x++)
                    {
                        Row[y] += "/" + (tiles[tileNumber].spriteIndex + 1).ToString();
                        tileNumber++;
                    }
                    Row[y] += "/";
                }
                File.WriteAllLines("C:/Users/Max Taunton/source/repos/MonoGame/Content/Levels/Level0.txt", Row);
            }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            // TODO: Add your drawing code here
            for (int x = 0; x < tiles.Count; x++)
            {
                if (tileMade[x] && tiles[x].spriteIndex >= 0) spriteBatch.Draw(tiles[x].sprite, tiles[x].area, Color.White);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}