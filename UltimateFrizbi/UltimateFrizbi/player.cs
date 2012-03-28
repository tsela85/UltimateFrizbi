using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace UltimateFrizbi
{
    public struct pivot
    {
        public Vector2 Position;
        public bool hasFrizbi;
        public Rectangle rec;
        public bool Active;
        // TODO: add atributes in the future
    }

    class player
    {
        const int numberOfPivots = 3;
        const int size = 15;

        public Texture2D PlayerTexture;
        Color playerColor;
        int playerScore;
        pivot[] pivots;

        public int Score
        {
            get { return playerScore; }
        }

        public void Initialize(Texture2D texture, Vector2 position,Color color)
        {

            PlayerTexture = texture;
            playerColor = color;
            playerScore = 0;
            pivots = new pivot[numberOfPivots];

            for (int i = 0; i < numberOfPivots; i++)
            {
                pivots[i].Position = position + new Vector2(i*30,0);
                pivots[i].hasFrizbi = false;
                pivots[i].Active = false;
                pivots[i].rec = new Rectangle((int)pivots[i].Position.X,(int)pivots[i].Position.Y, size, size);

            }
        }

        public void Update()
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < numberOfPivots; i++) {
                spriteBatch.Draw(PlayerTexture,pivots[i].rec,playerColor);
            }
        }
    }
}
