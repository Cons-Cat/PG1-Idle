using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Source
{
    class Program
    {
        // Instantiation
        static Agent[] agentObjsArr = new Agent[10]; // 10 agents
        static RenderWindow renderObj = new RenderWindow(9, 3); // 10 rows, 4 columns

        static public int SharedResource { get; set; }
        static public object _locker = 0;

        static class GameOperator
        {
            static public double gamePoints { get; set; }
        }

        static void UpdateConsole()
        {
            // Update the renderer's points.
            RenderWindow.displayPoints = GameOperator.gamePoints;
            renderObj.RenderLoop();
        }

        // Method for game loop thread.
        static void GameLoop()
        {
            while (true)
            {
                // Loop through all agents
                for (int i = 0; i < 10; i++)
                {
                    GameOperator.gamePoints += agentObjsArr[i].pointsRate * agentObjsArr[i].agentCount; // Point incrementing algorithm.
                    RenderWindow.agentCount[i] = agentObjsArr[i].agentCount;
                    RenderWindow.agentPrice[i] = (uint)(agentObjsArr[i].priceFactor * (agentObjsArr[i].agentCount + 1));
                }

                // Prevent both threads from updating simultaneously.
                lock (_locker)
                {
                    UpdateConsole();
                    SharedResource++;
                }

                Thread.Sleep(1000);
            }
        }

        // Method for player input thread.
        #region

        static void PlayerInput()
        {
            while (true)
            {
                // Player controls
                ConsoleKeyInfo playerInput = Console.ReadKey();

                if (Char.IsNumber(playerInput.KeyChar))
                {
                    // Increment the agent that the user inputs
                    agentObjsArr[(int)(Char.GetNumericValue(playerInput.KeyChar) + 9) % 10].agentCount++;
                }
                else
                {
                    switch (playerInput.Key)
                    {
                        case ConsoleKey.Spacebar:
                            GameOperator.gamePoints += 1;

                            break;

                        case ConsoleKey.X:
                            Console.Clear();
                            Console.WriteLine("Thanks for playing! Your final score is: " + GameOperator.gamePoints);

                            break;
                    }
                }

                // Prevent both threads from updating simultaneously.
                lock (_locker)
                {
                    UpdateConsole();
                    SharedResource--;
                }

                Thread.Sleep(100);
            }
        }

        #endregion

        static void Main()
        {
            // Initialize ten agents.
            agentObjsArr[0] = new Agent(0,1.1,10);
            agentObjsArr[1] = new Agent(1,1.2,50);
            agentObjsArr[2] = new Agent(2,1.4,150);
            agentObjsArr[3] = new Agent(3,1.75,200);
            agentObjsArr[4] = new Agent(4,1,300);
            agentObjsArr[5] = new Agent(5,1.5,400);
            agentObjsArr[6] = new Agent(6,2,550);
            agentObjsArr[7] = new Agent(7,4,700);
            agentObjsArr[8] = new Agent(8,7,1000);
            agentObjsArr[9] = new Agent(9,10,2000);

            // Initial console draw.
            UpdateConsole();

            // Instantiate threads.
            ThreadStart gameLoop = new ThreadStart(GameLoop);
            Thread myGameLoop = new Thread(gameLoop);
            myGameLoop.Start();
            

            ThreadStart inputLoop = new ThreadStart(PlayerInput);
            Thread myInputLoop = new Thread(inputLoop);
            myInputLoop.Start();
        }
    }
}
