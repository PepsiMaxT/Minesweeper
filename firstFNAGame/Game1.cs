using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

/*
 Single press keys:
ESC
F1-12
ENTER
 */

namespace firstFNAGame
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Player player;

        //Init variables
        int level = 0;
        List<Platforms> platforms = new List<Platforms>();

        public Game1() //This is the constructor, this function is called whenever the game class is created.
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "C:/Users/Max Taunton/source/repos/firstFNAGame/Content/";

            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            player = new Player(Content.Load<Texture2D>("Player/Player.png"), new Vector2(0, 720 - 96), 48, 96);
        }

        protected override void Update(GameTime gameTime)
        {
            //Update the things FNA handles for us underneath the hood:
            base.Update(gameTime);

            //Reset area

            recieveInputs();

            if (player.inAir)
            {
                player.gravity();
            }

            switch (level)
            {
                case 0:
                    platforms.Add(new Platforms());
                    break;
            }

            //if goal is reached clear platforms and enemies

        }

        protected override void Draw(GameTime gameTime)
        {
            //This will clear what's on the screen each frame, if we don't clear the screen will look like a mess:
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null);
            spriteBatch.Draw(player.sprite, player.hitBox, Color.White);
            
            spriteBatch.End();

            //Draw the things FNA handles for us underneath the hood:
            base.Draw(gameTime);
        }

        void recieveInputs()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                player.move((int)(0.1 * player.speed));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                player.move((int)(-0.1 * player.speed));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && !player.inAir)
            {
                player.jumpSpeed -= 30;
                player.inAir = true;
            }
        }
    }
}
