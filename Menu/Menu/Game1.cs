using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using ScreenManager;

namespace ScreenManager
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        KeyboardState keyboardState;
        KeyboardState oldKeyboardState;

        GameScreen activeScreen;
        StartScreen startScreen;
        ActionScreen actionScreen;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            string[] menuItems = { "Start Game", "High Scores", "End Game" };
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //this = objet game courant

            spriteBatch = new SpriteBatch(GraphicsDevice);

            startScreen = new StartScreen(
                this,
                spriteBatch,
                Content.Load<SpriteFont>("menufont"),
                Content.Load<Texture2D>("BG1_Moon"));
            Components.Add(startScreen);
            startScreen.Hide();

            actionScreen = new ActionScreen(
                this,
                spriteBatch,
                Content.Load<Texture2D>("BG1_Moon"));

            Components.Add(actionScreen);
            actionScreen.Hide();
            activeScreen = startScreen;
            activeScreen.Show();

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
           keyboardState = Keyboard.GetState();
           if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
               this.Exit();
           if (activeScreen == startScreen)
           {
               if (CheckKey(Keys.Enter))
               {
                   if (startScreen.SelectedIndex == 0)
                   {
                       activeScreen.Hide();
                       activeScreen = actionScreen;
                       activeScreen.Show();
                   }
                   if (startScreen.SelectedIndex == 1)
                   {
                       this.Exit();
                   }
               }
           }
           base.Update(gameTime);
           oldKeyboardState = keyboardState;
           }
        
           private bool CheckKey(Keys theKey)
           {
               return keyboardState.IsKeyUp(theKey) &&
               oldKeyboardState.IsKeyDown(theKey);
        }



        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            base.Draw(gameTime);
            spriteBatch.End();
        }
    }
}
