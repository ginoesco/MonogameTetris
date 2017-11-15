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
        Vector2 tetrisBlock; 
        List<int[,]> rotate = new List<int[,]>();
        
        private Texture2D block, window;
        private SpriteFont font;
        private KeyboardState oldKeyState;
        private KeyboardState currentKeyState; 

        const int pixelWidth = 32;
        const int pixelLength = 32;
        const int boardX = 330;
        const int boardY = 200;
        const int size = 32;
        int[,] shape = new int[4, 4];
        int[,] shape2 = new int[4, 4];
        int[,] rotated = new int[4, 4];
        int[,] gameBoard = new int[10, 18]; // 10x 18 board
        int[,] loadedBoard = new int[10, 18];

        int posX = 330 + pixelWidth * 4;
        int posY = 200;
       
        int boundsX = boardX + pixelWidth * 8;
        int boundsY = boardY + pixelWidth * 16; 
        int rotateIndex = 0;
        int rnum = 0;
        int count = 0; //used in timer
        int currentShape = 1;
        int nextShape;
        int moveLeftState = 0;
        int moveRightState = 0;
        int moveDownState = 0; 


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

        private void Fall()
        {
            int timer = 60;
            if (posY < boundsY)
            {
                if (count == timer)
                {
                    posY += pixelWidth;
                    count = 0;
                }
                count++;
            }
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
        public void MoveKeys()
        {
            int blocked = (int)GameBoard.BlockStates.Blocked; 
            int offGrid = (int)GameBoard.BlockStates.OffGrid;
            // Console.WriteLine("blockstate: {0}, {1}", moveLeftState, moveRightState);
            int rightWall = boundsX + pixelWidth * 9;
            if (oldKeyState.IsKeyDown(Keys.Up) && currentKeyState.IsKeyUp(Keys.Up))
            { //updates when up is pressed
                Rotate(currentShape);

                if ((currentShape == 6 || currentShape == 3 || currentShape == 4) && (moveLeftState >= 362 && moveRightState <= boundsX))
                {
                    if (rotateIndex < 4)
                        Array.Copy(rotate[rotateIndex++], shape, shape.Length);
                    else
                        rotateIndex = 0;
                }
                else if (currentShape != 6 && currentShape != 4  && currentShape != 3)
                {
                    if (rotateIndex < 4)
                    {
                        Array.Copy(rotate[rotateIndex++], shape, shape.Length);

                    }
                    else
                    {
                        rotateIndex = 0;
                    }
                }

            }
            else if (oldKeyState.IsKeyDown(Keys.Left) && currentKeyState.IsKeyUp(Keys.Left))
            {
               //int blockstate = (int)gbObj.CheckPlacement(gameBoard, shape,(int)tetrisBlock.X, (int)tetrisBlock.Y);
               // moveLeftState = blockstate; 
                if (moveLeftState >= 362)
                {
                    Console.WriteLine("moveLeft: {0}", moveLeftState); 
                    posX -= pixelWidth; 
                }
              


            }
            else if (oldKeyState.IsKeyDown(Keys.Right) && currentKeyState.IsKeyUp(Keys.Right))
            {
                //int blockstate = (int)gbObj.CheckPlacement(gameBoard, shape, (int)tetrisBlock.X, (int)tetrisBlock.Y);
                //moveRightState = blockstate; 

                  if(moveRightState <= boundsX)
                  {
                      Console.WriteLine("moveRight: {0}", moveRightState);

                      posX += pixelWidth; 
                  }

             

            }
            else if (oldKeyState.IsKeyDown(Keys.Down) && currentKeyState.IsKeyUp(Keys.Down))
            {
                if (moveDownState <= boundsY)
                    posY += pixelWidth;
            }
            else if (oldKeyState.IsKeyDown(Keys.Enter) && currentKeyState.IsKeyUp(Keys.Enter))
            { //updates when enter is pressed
                posX = 330 + pixelWidth * 4;
                posY = 200;
                rnum = rnd.Next(0, 7);

                currentShape = nextShape;
                nextShape = rnum;
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
            int blockstate = (int)gbObj.CheckPlacement(gameBoard, shape, posX, posY);
            int blocked = (int)GameBoard.BlockStates.Blocked; 
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            //Checks for what keys are pressed, Moves or rotates block
            if( blockstate != blocked ||(oldKeyState.IsKeyDown(Keys.Enter) && currentKeyState.IsKeyUp(Keys.Enter)))
                MoveKeys();
            //Console.WriteLine("blockstate: {0}",blockstate);
            Console.WriteLine("posX, posY: {0}, {1}", posX, posY);
            if(blockstate == blocked)
                 loadedBoard = gbObj.LoadBoard(loadedBoard, shape, posX, posY);

            //Fall();
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        public void drawShape(bool whichShape)
        {
            List<int[,]> shapeList = shapeObj.GetShapeList();
            List<Color> Colors = shapeObj.GetColorList();
            int leftmostX = 99;
            int rightmostX = -1;
            int lowestY = -1; 
            if (whichShape) //Drawing the current game board shape
            {
                shape = shapeList[currentShape];

                for (int i = 0; i < 4; i++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        if (shape[k, i] != 0)
                        {
                            if (i < leftmostX)
                            {
                                leftmostX = i;
                                moveLeftState = posX + i * pixelWidth;
                                //Console.WriteLine("posX, i, moveLeft: {0}, {1}, {2}", posX, i, moveLeftState);
                            }
                            if(i > rightmostX)
                            {
                                rightmostX = i; 
                                moveRightState = posX + i*pixelWidth;
                            }
                            if(k > lowestY)
                            {
                                lowestY = k;
                                moveDownState = posY + k * pixelLength;
  
                            }
                           // Console.WriteLine("lmx, rmx,lowY: {0}, {1}, {2}", leftmostX, rightmostX, lowestY);

                            spriteBatch.Draw(block, tetrisBlock = new Vector2(posX + i * pixelWidth, posY + k * pixelLength), Colors[currentShape]);

                           //spriteBatch.Draw(block,  new Rectangle(posX+i*pixelWidth, posY+k*pixelLength,pixelWidth, pixelLength),Colors[currentShape]);
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
                            //spriteBatch.Draw(block, new Vector2(750 + i * pixelWidth, 500 + k * pixelLength), Colors[nextShape]);
                            spriteBatch.Draw(block, new Rectangle(750+i*pixelWidth, 500+k*pixelLength, pixelWidth, pixelLength), Colors[nextShape]);
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
            gbObj.UpdateBoard(loadedBoard, block, spriteBatch);

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
