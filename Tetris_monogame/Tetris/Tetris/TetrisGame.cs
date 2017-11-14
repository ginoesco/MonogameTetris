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
        Random rnd = new Random(DateTime.Now.Millisecond);
        GameBoard gbObj = new GameBoard(); 
        List<int[,]> rotate = new List<int[,]>();
        
        private Texture2D block, window;
        private SpriteFont font;
        private KeyboardState oldKeyState;
        private KeyboardState currentKeyState; 

        const int pixelWidth = 32;
        const int pixelLength = 31;
        const int boardX = 330;
        const int boardY = 200;
        const int size = 32;
        int[,] shape = new int[4, 4];
        int[,] shape2 = new int[4, 4];
        int[,] rotated = new int[4, 4];
        int[,] gameBoard = new int[10, 18]; // 10x 18 board

        int posX = 330;
        int posY = 200;
    
        int boundsX = boardX + pixelWidth * 7;
        int boundsY = boardY + pixelWidth * 16; 
        int rotateIndex = 0;
        int rnum = 0;
        int rnum2 = 2;
        int temp = 0;
        int currentShape = 1;
        int nextShape;


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
            window = Content.Load<Texture2D>("Window");

        }

        private void Rotate(int currentShape)
        {
            switch (currentShape)
            {
                case 0:
                    rotate = shapeObj.GetRotate_T();
                    break;
                case 1:
                    rotate = shapeObj.GetRotate_Z();
                    break;
                case 2:
                    rotate = shapeObj.GetRotate_S();
                    break;
                case 3:
                    rotate = shapeObj.GetRotate_L();
                    break;
                case 4:
                    rotate = shapeObj.GetRotate_J();
                    break;
                case 5:
                    rotate = shapeObj.GetRotate_Sq();
                    break;
                case 6:
                    rotate = shapeObj.GetRotate_Line();
                    break;
                default:
                    break;
            }
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
   
            //Grabs rotation list for the current block

            if (oldKeyState.IsKeyDown(Keys.Up) && currentKeyState.IsKeyUp(Keys.Up))
            { //updates when up is pressed
                Rotate(currentShape);
                if (rotateIndex < 4)
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
                if(posX>boardX)
                    posX -= pixelWidth; 
            }
            if(Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                if(posX < boundsX)
                    posX += pixelWidth; 
            }
            if(Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                if(posY < boundsY)
                    posY += pixelWidth; 
            }
            if (oldKeyState.IsKeyDown(Keys.Enter) && currentKeyState.IsKeyUp(Keys.Enter))
            { //updates when enter is pressed
                rnum = rnd.Next(0, 7);

                currentShape = nextShape;
                nextShape = rnum;
            }

            base.Update(gameTime);
        }

        public void drawShape(bool whichShape)
        {
            List<int[,]> shapeList = shapeObj.GetShapeList();
            List<Color> Colors = shapeObj.GetColorList();
            if (whichShape) //Drawing the current game board shape
            {
                shape = shapeList[currentShape];
                for (int i = 0; i < 4; i++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        if (shape[k, i] == 1)
                        {
                            spriteBatch.Draw(block, new Vector2(posX + i * pixelWidth, posY + k * pixelLength), Colors[currentShape]);
                        }
                    }
                }
            }
            else //Drawing the shape in the next shape block
            {
                shape2 = shapeList[nextShape];
                for (int i = 0; i < 4; i++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        if (shape2[k, i] == 1)
                        {
                            spriteBatch.Draw(block, new Vector2(750 + i * pixelWidth, 500 + k * pixelLength), Colors[nextShape]);
                        }
                    }
                }
            }
            
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);
            List<int[,]> GameBoardList = gbObj.GetGameBoard(); 
            Color boardColor = new Color();
            bool boardShape = true;
            bool nextShape = false;

            // TODO: Add your drawing code here

            gameBoard = GameBoardList[0];
            //Game board
            spriteBatch.Begin();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 18; j++)
                {
                    if (gameBoard[i, j] == 0)
                    {
                        boardColor = Color.FromNonPremultiplied(50, 50, 50, 50);
                        spriteBatch.Draw(block, new Rectangle(boardX + i * size, boardY + j * size, size, size), new Rectangle(0, 0, 32, 32), boardColor);
                    }
                }
            }
            spriteBatch.End();

            //Drawing the shape to go onto the board
            spriteBatch.Begin();
            drawShape(boardShape);
            spriteBatch.End();


            //display the score
            spriteBatch.Begin();
            spriteBatch.DrawString(font, "Score: ", new Vector2(700, 200), Color.Black);
            spriteBatch.End();

            //Next block square
            spriteBatch.Begin();
            spriteBatch.DrawString(font, "Next Block", new Vector2(700, 400), Color.Black);
            spriteBatch.Draw(window, new Rectangle(700,450, 200,200), Color.White );
            drawShape(nextShape);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
