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
        public static bool exitcheck = false; // checker for killing threads
        // Instantiation
        static Agent[] agentObjsArr = new Agent[10]; // 10 agents
        static RenderWindow renderObj = new RenderWindow(9, 3); // 10 rows, 4 columns

        static public int SharedResource { get; set; }
        static public object _locker = 0;

        static class GameOperator
        {
            static public MassiveNumber gamePoints = new MassiveNumber(); // { get; set; }

            // Update the game console
            static public void UpdateConsole()
            {
                // Update the renderer"s points.
                RenderWindow.gamePoints = GameOperator.gamePoints;
                renderObj.RenderLoop();
            }
        }

        // Method for game loop thread.
        #region

        static void GameLoop()
        {
            while (true)
            {
                // Loop through all agents
                for (uint i = 0; i < 10; i++)
                {
                    GameOperator.gamePoints.value = GameOperator.gamePoints.Add(agentObjsArr[i].agentCount.value, agentObjsArr[i].agentCount.echelon); // Point incrementing algorithm.

                    RenderWindow.agentCount[i] = agentObjsArr[i].agentCount;
                    RenderWindow.agentPrice[i] = agentObjsArr[i].GetPrice();
                    RenderWindow.agentPointsRate[i] = (agentObjsArr[i].pointsRate);
                }

                RenderWindow.gamePoints = GameOperator.gamePoints;
                GameOperator.gamePoints.UpdateEchelon();

                // Prevent both threads from updating simultaneously.
                lock (_locker)
                {
                    GameOperator.UpdateConsole();
                    SharedResource++;
                }

                // Loop every second.
                Thread.Sleep(1000);

                if (Program.exitcheck == true)
                {
                    break;
                }
            }
        }

        #endregion

        // Method for player input thread.
        #region

        static void PlayerInput()
        {
            while (true)
            {
                int inputIndex;

                // Player controls
                ConsoleKeyInfo playerInput = Console.ReadKey();

                if (Char.IsNumber(playerInput.KeyChar))
                {
                    // Get array index from digit input.
                    inputIndex = (int)(Char.GetNumericValue(playerInput.KeyChar) + 9) % 10;

                    MassiveNumber agentCost = agentObjsArr[inputIndex].GetPrice();

                    // If the player has sufficient points
                    if (GameOperator.gamePoints.IsGreater(agentCost))
                    {
                        // Increment the agent that the user inputs.
                        agentObjsArr[inputIndex].agentCount.value = agentObjsArr[inputIndex].agentCount.Add(1, 1);
                        agentObjsArr[inputIndex].agentCount.UpdateEchelon();

                        // Update the console values.
                        RenderWindow.agentCount[inputIndex] = agentObjsArr[inputIndex].agentCount;
                        RenderWindow.agentPrice[inputIndex] = agentObjsArr[inputIndex].GetPrice();

                        // Decrease the player's points bank.
                        GameOperator.gamePoints.value = GameOperator.gamePoints.Sub(agentCost.value, agentCost.echelon);
                        GameOperator.gamePoints.UpdateEchelon();
                    }
                    else
                    {
                        // Clear the user input line.
                        Console.SetCursorPosition(0, Console.CursorTop);

                        // Don"t bother executing UpdateConsole(), since no value changed.
                        continue;
                    }
                }
                else
                {
                    switch (playerInput.Key)
                    {
                        case ConsoleKey.Spacebar:
                            GameOperator.gamePoints.value = GameOperator.gamePoints.Add(1, 1);
                            GameOperator.gamePoints.UpdateEchelon();

                            RenderWindow.gamePoints = GameOperator.gamePoints;

                            break;

                        case ConsoleKey.X:
                            Console.Clear();
                            exitcheck = true;
                           // Console.WriteLine("Thanks for playing! Your final score is: " + GameOperator.gamePoints);

                            break;
                    }
                    if (Program.exitcheck == true)
                    {
                        break;
                    }
                }

                // Prevent both threads from updating simultaneously.
                lock (_locker)
                {
                    GameOperator.UpdateConsole();
                    SharedResource--;
                }

                Thread.Sleep(100);
            }
            if (Program.exitcheck == true)
            {
                Console.WriteLine("Thank you for playing! Your final total of money earned was: " + GameOperator.gamePoints.GetAbbreviation());
                Console.ReadKey();
            }
        }
        public static void Gamekill()
        {
         
        }

        #endregion

        static void Main()
        {
            // Initialize ten agents.
            agentObjsArr[0] = new Agent(1, 10, 1.0275);
            agentObjsArr[1] = new Agent(2.5, 30, 1.03);
            agentObjsArr[2] = new Agent(5, 70, 1.035);
            agentObjsArr[3] = new Agent(10, 150, 1.04);
            agentObjsArr[4] = new Agent(20, 350, 1.0425);
            agentObjsArr[5] = new Agent(45, 500, 1.044);
            agentObjsArr[6] = new Agent(85, 1000, 1.05);
            agentObjsArr[7] = new Agent(150, 2000, 1.055);
            agentObjsArr[8] = new Agent(250, 3500, 1.06);
            agentObjsArr[9] = new Agent(300, 5000, 1.65);

            // Initial console draw.
            GameOperator.UpdateConsole();

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
