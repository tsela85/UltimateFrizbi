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

    class Player
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
        KeyboardState lastKbs;
        Rectangle goal;


        public int Score
        {
            get { return playerScore; }
        }

        public void Initialize(Texture2D texture, Vector2 position,Color color, SpriteFont font,int sHeight,int sWidth,Rectangle oppositeGoal)
        {
           
            PlayerTexture = texture;
            playerFont = font;
            playerColor = color;
            playerScore = 0;
            currentPivot = 0;
            pivotChosen = false;
            screenHeight = sHeight;
            screenWidth = sWidth;
            goal = oppositeGoal;

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

        public int Update(Frizbi frizbi)
        {
            KeyboardState keybState = Keyboard.GetState();
            int chosen = 1;

            if (lastKbs != null)
            {
                if (!pivotChosen)
                {
                    #region switch between pivots
                    if (keybState.IsKeyDown(Keys.Left) && lastKbs.IsKeyUp(Keys.Left))
                    {
                        currentPivot = (currentPivot - 1) % numberOfPivots;
                        currentPivot += (currentPivot == -1 ? numberOfPivots : 0);

                    }
                    if (keybState.IsKeyDown(Keys.Right) && lastKbs.IsKeyUp(Keys.Right))
                        currentPivot = (currentPivot + 1) % numberOfPivots;
                    //choose player
                    if (!lastKbs.IsKeyDown(Keys.Enter) && keybState.IsKeyDown(Keys.Enter))
                    {
                        pivotChosen = true;
                    }
                    #endregion

                }
                else //pivot is chosen
                {
                    //Angle
                    if (keybState.IsKeyDown(Keys.Left))
                        pivots[currentPivot].angle = (pivots[currentPivot].angle - 0.05f);
                    if (keybState.IsKeyDown(Keys.Right))
                        pivots[currentPivot].angle = (pivots[currentPivot].angle + 0.05f);
                    if (pivots[currentPivot].angle < 0)
                        pivots[currentPivot].angle += (2 * MathHelper.Pi);
                    //power
                    if (keybState.IsKeyDown(Keys.Down))
                        pivots[currentPivot].power -= 1;
                    if (keybState.IsKeyDown(Keys.Up))
                        pivots[currentPivot].power += 1;
                    //fix power
                    if (pivots[currentPivot].power > 300)
                        pivots[currentPivot].power = 300;
                    if (pivots[currentPivot].power < 0)
                        pivots[currentPivot].power = 0;
                    //move pivot
                    if (!lastKbs.IsKeyDown(Keys.Enter) && keybState.IsKeyDown(Keys.Enter))
                    {
                        if (pivots[currentPivot].hasFrizbi)
                            throwFrizbi(frizbi);
                        else
                            calcPivotPosition();
                        pivotChosen = false;
                        chosen = -1;
                    }
                }
            }
            lastKbs = keybState;
            return chosen;
        }

        private void throwFrizbi(Frizbi frizbi)
        {
            Vector2 newFrizbiPos = frizbi.getPosition;
            int i;

            pivots[currentPivot].hasFrizbi = false;
            newFrizbiPos.X -= (float)Math.Sin(pivots[currentPivot].angle) * pivots[currentPivot].power;
            if (newFrizbiPos.X > screenWidth - 69)
                newFrizbiPos.X = screenWidth - 69;
            if (newFrizbiPos.X < 42)
                newFrizbiPos.X = 42;
            newFrizbiPos.Y += (float)Math.Cos(pivots[currentPivot].angle) * pivots[currentPivot].power;
            if (newFrizbiPos.Y > screenHeight - 50)
                newFrizbiPos.Y = screenHeight - 50;
            if (newFrizbiPos.Y < 20)
                newFrizbiPos.Y = 20;
            frizbi.setPosition(newFrizbiPos);

            i = checkGoodPass(newFrizbiPos);
            if (i != -1)
            {
                currentPivot = i;
                moveChosenToFrizbi(frizbi);
                checkIfGoal(frizbi);
            }
            else
                frizbi.frizbeLandedOnTheGround();
        }

        private void checkIfGoal(Frizbi frizbi)
        {
            if (pivots[currentPivot].rec.Intersects(goal))
            {
                playerScore++;
                pivots[currentPivot].hasFrizbi = false;
                frizbi.frizbeLandedOnTheGround();
            }
        }

        private int checkGoodPass(Vector2 pos)
        {
            for (int i = 0; i < numberOfPivots; i++)
                if ((i != currentPivot) && (Math.Abs(pos.X - pivots[i].Position.X) < 60) && (Math.Abs(pos.Y - pivots[i].Position.Y) < 60))
                    return i;
            return -1;
        }


        public void moveChosenToFrizbi(Frizbi frizbi)
        {
            pivots[currentPivot].Position = frizbi.getPosition;
            pivots[currentPivot].rec.X = (int)pivots[currentPivot].Position.X;
            pivots[currentPivot].rec.Y = (int)pivots[currentPivot].Position.Y;

            pivots[currentPivot].hasFrizbi = true;
            frizbi.atPlayerHand = true;
        }

        private void calcPivotPosition()
        {
            pivots[currentPivot].Position.X -= (float)Math.Sin(pivots[currentPivot].angle) * pivots[currentPivot].power;
            if (pivots[currentPivot].Position.X > screenWidth - 69)
                pivots[currentPivot].Position.X = screenWidth - 69;
            if (pivots[currentPivot].Position.X < 42)
                pivots[currentPivot].Position.X = 42;
            pivots[currentPivot].Position.Y += (float)Math.Cos(pivots[currentPivot].angle) * pivots[currentPivot].power;
            if (pivots[currentPivot].Position.Y > screenHeight - 50)
                pivots[currentPivot].Position.Y = screenHeight - 50;
            if (pivots[currentPivot].Position.Y < 20)
                pivots[currentPivot].Position.Y = 20;
            pivots[currentPivot].rec.X = (int)pivots[currentPivot].Position.X;
            pivots[currentPivot].rec.Y = (int)pivots[currentPivot].Position.Y;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < numberOfPivots; i++)
            {
                if (i != currentPivot)
                    spriteBatch.Draw(PlayerTexture, pivots[i].rec, null, playerColor);
                else
                {
                    spriteBatch.Draw(PlayerTexture, pivots[i].rec, null, playerColor);
                    spriteBatch.DrawString(playerFont, "Power:" + pivots[i].power + "\nAngle:" +
                        (int)MathHelper.ToDegrees((pivots[i].angle) % (2 * MathHelper.Pi))
                        , pivots[i].Position + (pivots[i].Position.Y < screenHeight - 100 ? new Vector2(0, 50) : -new Vector2(0, 50)), Color.Black);
                    spriteBatch.Draw(PlayerTexture, new Rectangle((int)pivots[i].Position.X,(int)pivots[i].Position.Y,5,pivots[i].power), null, playerColor
                        , pivots[i].angle , new Vector2(0, 0), SpriteEffects.None, 0);
                }
            }
        }
    }
}
