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
    public class Game1 : Game
    {
        private Player player;
        private Level level;
        private List<Texture2D> tileSprites = new List<Texture2D>();

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private SpriteFont spriteFont;

        RenderTarget2D renderTarget;
        public float scale = 0.44444f;

        //KeyPressedAlready
        bool F11Pressed = false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.ApplyChanges();

            //Setup first level
            level = new Level(tileSprites);
            level.Initiate(0, 0, 0);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            renderTarget = new RenderTarget2D(GraphicsDevice, 1920, 1080);

            player = new Player(Content.Load<Texture2D>("Player/player"), new Vector2(0, 1080 - 290));
            
            spriteFont = Content.Load<SpriteFont>("galleryFont");
            tileSprites.Add(null);
            tileSprites.Add(Content.Load<Texture2D>("Tiles/tempTile"));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //KeyPress
            if (Keyboard.GetState().IsKeyDown(Keys.F11) && !F11Pressed) 
            { 
                ControlFullScreenMode(!graphics.IsFullScreen);
                F11Pressed = true;
            }
            else F11Pressed = false;

            //constant update
            player.update(gameTime, level);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            scale = 1f / (1080f / graphics.GraphicsDevice.Viewport.Height);
            GraphicsDevice.SetRenderTarget(renderTarget);
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);
            //Background

            //Midground

            for (int y = 0; y < level.tileRow.Count; y++)
            {
                for (int x = 0; x < level.tileRow[y].tile.Count; x++)
                {
                    if (level.tileRow[y].tile[x].spriteIndex != 0)
                    {
                        level.tileRow[y].tile[x].area = new Rectangle(x * 40, y * 40, 40, 40);
                        spriteBatch.Draw(level.tileRow[y].tile[x].sprite, level.tileRow[y].tile[x].area, Color.White);
                    }
                }
            }
            //Player
            spriteBatch.Draw(player.sprite, player.hitBox, player.color);
            //Foreground

            //Text
            //spriteBatch.DrawString(spriteFont, "Row Count: " + tileRow[0].count.ToString(), new Vector2(0, 0), Color.White);
            spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(renderTarget, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        void ControlFullScreenMode(bool becomeFullscreen)
        {
            graphics.IsFullScreen = becomeFullscreen;
            graphics.ApplyChanges();
        }
    }
}