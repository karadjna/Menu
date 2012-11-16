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
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace ScreenManager
{
    // DrawableGameComponent : pour dessiner soi meme le menu
    public class MenuComponent : Microsoft.Xna.Framework.DrawableGameComponent
    {
        string[] menuItems;
        int selectedIndex;
        Color normal = Color.White;
        // lorsqu'un item est selectionne - hilite
        Color hilite = Color.Blue;
        KeyboardState keyboardState;
        // etat du keyboard lors de la frame (60 sec) precedente - oldKeyboardState
        KeyboardState oldKeyboardState;

        // Pour dessiner le texte du menu - spriteBatch/spriteFont
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;
        Vector2 position;
        float width = 0f;
        float height = 0f;

        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                // SI <0, ALORS 0 ; SI >0, ALORS = taille array
                selectedIndex = value;
                if (selectedIndex < 0)
                    selectedIndex = 0;
                if (selectedIndex >= menuItems.Length)
                    selectedIndex = menuItems.Length - 1;
            }
        }

        // base(object) : GameComponents & DrawableGameComponents ont besoin d'un game.
        public MenuComponent(Game game, SpriteBatch spriteBatch, SpriteFont spriteFont, string[] menuItems)
            : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.spriteFont = spriteFont;
            this.menuItems = menuItems;
            // methode pour calculer height & width du screen && centrer le menu
            MeasureMenu();
        }

        private void MeasureMenu()
        {
            height = 0;
            width = 0;
            foreach (string item in menuItems)
            {
                Vector2 size = spriteFont.MeasureString(item);
                if (size.X > width)
                    width = size.X;
                height += spriteFont.LineSpacing + 5;
            }
            position = new Vector2(
                // Centrage du menu
                (Game.Window.ClientBounds.Width - width) / 2,
                (Game.Window.ClientBounds.Height - height) / 2);
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
        }

        private bool CheckKey(Keys theKey)
        {
            return keyboardState.IsKeyDown(theKey) &&
            oldKeyboardState.IsKeyUp(theKey);
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            keyboardState = Keyboard.GetState();

            if (CheckKey(Keys.Down))
            {
            selectedIndex++;

            if (selectedIndex == menuItems.Length)
            selectedIndex = 0;
            }

            if (CheckKey(Keys.Up))
            {
                selectedIndex--;

                if (selectedIndex < 0)
                    selectedIndex = menuItems.Length - 1;
            }

            base.Update(gameTime);
            oldKeyboardState = keyboardState;
        }


        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            // endroit ou dessiner la prochaine ligne du menu
            Vector2 location = position;
            Color tint;
            for (int i = 0; i < menuItems.Length; i++)
            {
                if (i == selectedIndex)
                    tint = hilite;
                else
                    tint = normal;
                spriteBatch.DrawString(
                    spriteFont,
                    menuItems[i],
                    location,
                    tint);
                // Affichage des lignes du menu
                location.Y += spriteFont.LineSpacing + 5;
            }
        }

    }
}
