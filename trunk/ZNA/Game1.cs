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
using FarseerGames.FarseerPhysics;
using FarseerGames.FarseerPhysics.Dynamics;
using FarseerGames.FarseerPhysics.Factories;
using FarseerGames.FarseerPhysics.Collisions;

namespace ZNA
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public static GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;

        public Texture2D box;

        //Font:
        public SpriteFont font;

        public PhysicsSimulator phsX;
        //BodyFactory.Instance.CreateRectangleBody(32,32,1);

        Body leftCorner;
        Body rightCorner;
        Body topCorner;
        Body bottomCorner;

        Body rectBody;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Frame rate is 30 fps by default for Zune.
            TargetElapsedTime = TimeSpan.FromSeconds(1 / 30.0);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            phsX = new PhysicsSimulator(); //Create physX
            //phsX.Iterations = 5;

            //phsX.Gravity = new Vector2(0, (float)9.8);

            // TODO: Add your initialization logic here
            Random r = new Random();


            for (int x = 48; x < 230; x += 64)
            {
                for (int y = 48; y < 200; y += 48)
                {
                    Body rectangleBody;
                    rectangleBody = BodyFactory.Instance.CreateRectangleBody(phsX, 32, 32, 1);
                    rectangleBody.Position = new Vector2(x, y);
                    phsX.Add(GeomFactory.Instance.CreateRectangleGeom(phsX, rectangleBody, 32, 32));
                }
            }

            
            //Body leftCorner;
            leftCorner = BodyFactory.Instance.CreateRectangleBody(phsX, 10, 480, 100000);
            leftCorner.Position = new Vector2(5, 240);

            //Body rightCorner;
            rightCorner = BodyFactory.Instance.CreateRectangleBody(phsX, 10, 480, 100000);
            rightCorner.Position = new Vector2(267, 240);

            //Body topCorner;
            topCorner = BodyFactory.Instance.CreateRectangleBody(phsX, 272, 10, 100000);
            topCorner.Position = new Vector2(136, 5);

            //Body bottomCorner;
            bottomCorner = BodyFactory.Instance.CreateRectangleBody(phsX, 272, 10, 100000);
            bottomCorner.Position = new Vector2(136, 475);

            Geom gl = GeomFactory.Instance.CreateRectangleGeom(leftCorner, 10, 480);
            Geom gr = GeomFactory.Instance.CreateRectangleGeom(rightCorner, 10, 480);
            Geom gu = GeomFactory.Instance.CreateRectangleGeom(topCorner, 272, 10);
            Geom gb = GeomFactory.Instance.CreateRectangleGeom(bottomCorner, 272, 10);

            gl.Body.IsStatic = true;
            gr.Body.IsStatic = true;
            gu.Body.IsStatic = true;
            gb.Body.IsStatic = true;

            phsX.Add(gl);
            phsX.Add(gr);
            phsX.Add(gu);
            phsX.Add(gb);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            box = Content.Load<Texture2D>("image/square");

            font = Content.Load<SpriteFont>("Font");
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            //Update the Touches BEFORE updating the Game Objects
            TouchCollection touch = TouchPanel.GetState();
            AccelerometerState accelstate = Accelerometer.GetState();
            phsX.Gravity = (float)9.8 * (new Vector2(accelstate.Acceleration.X, -accelstate.Acceleration.Y));

            foreach (TouchLocation tl in touch)
            {
                foreach (Geom b in phsX.GeomList)
                {
                    b.Body.ApplyForce((tl.Position - b.Position)/(float)2.0);
                }
            }

            phsX.Update((float)(1.0/30.0)); //Update physX
            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            // TODO: Add your drawing code here
            foreach (Geom b in phsX.GeomList)
            {
                spriteBatch.Draw(box, b.Position, null, Color.White, b.Rotation, new Vector2(16, 16), new Vector2(1, 1), 0, 0);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
