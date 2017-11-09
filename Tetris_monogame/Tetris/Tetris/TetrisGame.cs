using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System; 
namespace Tetris
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class TetrisGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Shapes shapeObj = new Shapes();
        Random rnd = new Random();
        List<int[,]> rotate = new List<int[,]>();
        
        private Texture2D block;
        private SpriteFont font;
        private KeyboardState oldKeyState;
        private KeyboardState currentKeyState; 

        const int pixelWidth = 32;
        const int pixelLength = 31;
        const int size = 32;
        int[,] shape = new int[4, 4];
        int posX = 200;
        int posY = 200;
        int boardX =330;
        int boardY = 200; 
        int rotateIndex = 0;
        int rnum = 0;

        int[,] gameBoard = new int[10, 18]; // 10x 18 board

        public TetrisGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 1000; //Make the game board a size 1k by 1k
            graphics.PreferredBackBufferHeight = 1000;
        }

        public void NewGame()
        {
            for (int i =0; i <10; i ++)
            {
                for (int j = 0; j < 18; j++)
                {
                    gameBoard[i, j] = 0; // Initialize each location to a zero
                }
            }
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
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            block = Content.Load<Texture2D>("block");
            font = Content.Load<SpriteFont>("Score");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            block.Dispose(); 
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>

        protected override void Update(GameTime gameTime)
        {
            oldKeyState = currentKeyState;
            currentKeyState = Keyboard.GetState(); 
            
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            // TODO: Add your update logic here
            if (rnum == 0)
                rotate = shapeObj.GetRotate_T();
            if (rnum == 1)
                rotate = shapeObj.GetRotate_Z();
            if (rnum == 2)
                rotate = shapeObj.GetRotate_S();
            if (rnum == 3)
                rotate = shapeObj.GetRotate_L();
            if (rnum == 4)
                rotate = shapeObj.GetRotate_J();
            if (rnum == 5)
                rotate = shapeObj.GetRotate_Sq();
            if (rnum == 6)
                rotate = shapeObj.GetRotate_Line();

            if (oldKeyState.IsKeyDown(Keys.Up) && currentKeyState.IsKeyUp(Keys.Up))
            {
                if(rotateIndex < 4)
                {
                    Array.Copy(rotate[rotateIndex++], shape, shape.Length);
                }
                else
                {
                    rotateIndex = 0; 
                }
            }
            if(Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                posX -= 10; 
            }
            if(Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                posX += 10; 
            }
            if(Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                posY += 10; 
            }
            if (oldKeyState.IsKeyDown(Keys.Enter) && currentKeyState.IsKeyUp(Keys.Enter))
            {
                 rnum = rnd.Next(0, 7); 

            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            List<int[,]> shapeList = shapeObj.GetShapeList();
            List<Color> Colors = shapeObj.GetColorList();
            Color boardColor = new Color();

            // TODO: Add your drawing code here
            spriteBatch.Begin();

           // int rnum = 0;
            shape = shapeList[rnum];

            for (int i = 0; i<4; i++)
            {
                for(int k = 0; k<4; k++)
                {
                    if(shape[k,i] == 1)
                    {
                        spriteBatch.Draw(block, new Vector2(posX+i*pixelWidth, posY+k*pixelLength), Colors[rnum]);
                    }
                }
            }
            spriteBatch.End();

            //Creating the board state
            spriteBatch.Begin();
            for (int i =0; i <10; i ++)
            {
                for (int j = 0; j < 18; j++)
                { 
                    if (gameBoard[i,j] == 0)
                    {
                       boardColor = Color.FromNonPremultiplied(50, 50, 50, 50);
                       spriteBatch.Draw(block, new Rectangle(boardX+i *size,boardY+j*size, size, size), new Rectangle (0,0,32,32), boardColor);
                    }
                }
            }
            spriteBatch.End();


            spriteBatch.Begin();
            spriteBatch.DrawString(font, "Score: ", new Vector2(700, 200), Color.Black);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
