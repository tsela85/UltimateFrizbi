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


namespace UltimateFrizbi
{
    public struct playerData
    {
        public Vector2 Position;
        public Color Color;
        public bool hasFrizbi;
        public Rectangle rec;
        // TODO: add atributes in the future
    }
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class bluePlayer : Microsoft.Xna.Framework.GameComponent
    {
        playerData[] players;
       // int numberOfPlayers = 4;

        public bluePlayer(Game game)
            : base(game)
        {
            players = new playerData[3];
            for (int i = 0; i < 3; i++)
            {
                players[i].hasFrizbi = false;
                players[i].Color = Color.Blue;
                players[i].Position = new Vector2(0, 0);
                players[i].rec = new Rectangle((int)players[i].Position.X,(int)players[i].Position.X, 16, 16);
            }
        }

        /// <summary>s
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            base.Update(gameTime);
        }
    }
}
