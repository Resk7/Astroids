using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Asteroids
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D Asteroid1;
        Texture2D PlayerSprite;
        SpriteFont Score;

        Vector2 PlayerXY;
        Vector2 Asteroid1V2;
        Vector2 Asteroid2V2;
        Vector2 Asteroid3V2;

        float x;
        float y;
        float yVel;
        float Ax, Bx, Cx;
        float Ay, By, Cy;
        float AVel;

        int score;
        int scoreTimer;
        int HighScore;

        bool GameRun = true;

        Rectangle AA;
        Rectangle AB;
        Rectangle AC;
        Rectangle Player;

        Random random = new Random();

        List<Vector2> AsteroidsPos = new List<Vector2>();

        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            x = 100;
            y = 200;
            Ax = 600;
            Ay = 200;
            AVel = 3;
            score = 0;
            scoreTimer = 0;
            Cx = 1000;
            PlayerXY = new Vector2(x, y);
            Asteroid1V2 = new Vector2(Ax, Ay);
            AsteroidsPos.Add(Asteroid1V2);
            GameRun = true;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Asteroid1 = Content.Load<Texture2D>("Asteroid1");
            PlayerSprite = Content.Load<Texture2D>("PlayerSprite");
            Score = Content.Load<SpriteFont>("Score");

            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if(GameRun == true)
            {
                PlayerXY = new Vector2(x, y);
                if (Keyboard.GetState().IsKeyDown(Keys.W) && y > 0)
                {
                    if (yVel < 15)
                        yVel += 2;
                    y -= yVel;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.S) && y < 450)
                {
                    if (yVel < 15)
                        yVel += 2;
                    y += yVel;
                }
                else
                {
                    yVel = 0f;
                }
                AsteroidMovement();
                ScoreMethod();
                Rectangles();

            }
            // TODO: Add your update logic here
            if (GameRun == false && Keyboard.GetState().IsKeyDown(Keys.R))
                Initialize();

            base.Update(gameTime);
        }
        void AsteroidMovement()
        {
            //Asteroid 1
            Asteroid1V2 = new Vector2(Ax, Ay);
            AVel += 0.015f;
            Ax -= AVel;
            if (Ax < -40)
            {
                Ax = 800;
                Ay = random.Next(20, 430);
            }

            //Asteroid 2
            Asteroid2V2 = new Vector2(Bx, By);
            Bx -= AVel;
            if(Bx < -40)
            {
                Bx = 800;
                By = random.Next(20, 430);
            }

            //Asteroid 3
            Asteroid3V2 = new Vector2(Cx, Cy);
            Cx -= AVel;
            if(Cx < -40)
            {
                Cx = 800;
                Cy = random.Next(20, 430);
            }
                
        }
        void Loss()
        {
            GameRun = false;
        }

        void ScoreMethod()
        {
            scoreTimer++;
            if (scoreTimer == 60)
            {
                scoreTimer = 0;
                score++;
            }
            if (score > HighScore)
                HighScore = score;
        }


        void Rectangles()
        {
            //Asteroid 1
            AA = new Rectangle((int)Ax, (int)Ay, Asteroid1.Width, Asteroid1.Height);
            if (Player.Intersects(AA))
                Loss();

            //Asteroid 2
            AB = new Rectangle((int)Bx, (int)By, Asteroid1.Width, Asteroid1.Height);
            if (Player.Intersects(AB))
                Loss();

            //Asteroid 3
            AC = new Rectangle((int)Cx, (int)Cy, Asteroid1.Width, Asteroid1.Height);
            if (Player.Intersects(AC))
                Loss();
            //Player
            Player = new Rectangle((int)x, (int)y, PlayerSprite.Width/2, PlayerSprite.Height/2);


        }



        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            spriteBatch.Draw(Asteroid1, Asteroid1V2, Color.Red);
            spriteBatch.Draw(Asteroid1, Asteroid2V2, Color.Red);
            spriteBatch.Draw(Asteroid1, Asteroid3V2, Color.Red);
            spriteBatch.Draw(PlayerSprite, PlayerXY, Color.White);
            spriteBatch.DrawString(Score, "Highscore: " + HighScore, Vector2.One, Color.White);
            spriteBatch.DrawString(Score, "Score: "+score, new Vector2(1, 19), Color.White);
            if(!GameRun)
                spriteBatch.DrawString(Score, "Press R to restart", new Vector2(340, 200), Color.White);



            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
