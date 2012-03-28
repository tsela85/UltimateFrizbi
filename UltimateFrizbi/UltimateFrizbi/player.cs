using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace UltimateFrizbi
{
    public struct pivot
    {
        public Vector2 Position;
        public bool hasFrizbi;
        public Rectangle rec;
        public int power;
        public float angle;
        // TODO: add atributes in the future
    }

    class player
    {
        const int numberOfPivots = 3;
        const int size = 30;

        public Texture2D PlayerTexture;
        SpriteFont playerFont;
        Color playerColor;
        int playerScore;
        pivot[] pivots;
        int currentPivot;
        public bool pivotChosen;
        int screenHeight, screenWidth;

        public int Score
        {
            get { return playerScore; }
        }

        public void Initialize(Texture2D texture, Vector2 position,Color color, SpriteFont font,int sHeight,int sWidth)
        {

            PlayerTexture = texture;
            playerFont = font;
            playerColor = color;
            playerScore = 0;
            currentPivot = 0;
            pivotChosen = false;
            screenHeight = sHeight;
            screenWidth = sWidth;

            pivots = new pivot[numberOfPivots];

            for (int i = 0; i < numberOfPivots; i++)
            {
                pivots[i].Position = position + new Vector2(i*40,-size/2);
                pivots[i].hasFrizbi = false;
                pivots[i].rec = new Rectangle((int)pivots[i].Position.X,(int)pivots[i].Position.Y, size, size);
                pivots[i].power = 0;
                pivots[i].angle = 0;

            }
        }

        private void ProcessKeyboard()
        {
            KeyboardState keybState = Keyboard.GetState();
            if (!pivotChosen)
            {
                if (keybState.IsKeyDown(Keys.Left))
                {
                    currentPivot = (currentPivot + 1) % 3;             
                }
                if (keybState.IsKeyDown(Keys.Right))
                    currentPivot = (currentPivot + 1) % 3;
                //choose player
                if (keybState.IsKeyDown(Keys.Enter))
                {
                    pivotChosen = true;
                }
            } else
            {
                //Angle
                if (keybState.IsKeyDown(Keys.Left))
                    pivots[currentPivot].angle = (pivots[currentPivot].angle - 0.01f) % (2 * MathHelper.Pi);
                if (keybState.IsKeyDown(Keys.Right))
                    pivots[currentPivot].angle = (pivots[currentPivot].angle + 0.01f) % (2 * MathHelper.Pi); 
                //power
                if (keybState.IsKeyDown(Keys.Down))
                    pivots[currentPivot].power -= 1;
                if (keybState.IsKeyDown(Keys.Up))
                    pivots[currentPivot].power += 1;
                //fix power
                if (pivots[currentPivot].power > 100)
                        pivots[currentPivot].power = 100;
                if (pivots[currentPivot].power < 0)
                    pivots[currentPivot].power = 0;
                //move pivot
                if (keybState.IsKeyDown(Keys.Enter))
                {
                    calcPivotPosition(currentPivot);
                    pivotChosen = false;
                }
            }
        }

        private void calcPivotPosition(int pivot)
        {
            pivots[currentPivot].Position.X += (float)Math.Cos(pivots[currentPivot].angle) * pivots[currentPivot].power/10;
            if (pivots[currentPivot].Position.X > screenWidth - 69)
                pivots[currentPivot].Position.X = screenWidth - 69;
            if (pivots[currentPivot].Position.X < 42)
                pivots[currentPivot].Position.X = 42;
            pivots[currentPivot].Position.Y += (float)Math.Sin(pivots[currentPivot].angle) * pivots[currentPivot].power/10;
            if (pivots[currentPivot].Position.Y > screenHeight - 50)
                pivots[currentPivot].Position.Y = screenHeight - 50;
            if (pivots[currentPivot].Position.Y < 20)
                pivots[currentPivot].Position.Y = 20;            
            pivots[currentPivot].rec.X = (int)pivots[currentPivot].Position.X;
            pivots[currentPivot].rec.Y = (int)pivots[currentPivot].Position.Y;
        }

        public void Update()
        {
            ProcessKeyboard();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < numberOfPivots; i++) {
                if (i != currentPivot)
                    spriteBatch.Draw(PlayerTexture, pivots[i].rec, null,playerColor);
                else
                {
                    spriteBatch.Draw(PlayerTexture, pivots[i].rec, null, Color.Tan);
                    spriteBatch.DrawString(playerFont, "Power:" + pivots[i].power + "\nAngle:" +
                        (int)MathHelper.ToDegrees(pivots[i].angle), pivots[i].Position + (pivots[i].Position.Y < screenHeight - 100 ? new Vector2(0, 50) : -new Vector2(0, 50)), Color.Black);
                }
            }
        }
    }
}
