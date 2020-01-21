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
        static RenderWindow renderObj = new RenderWindow(9, 5); // 10 rows, 6 columns

        static public int SharedResource { get; set; }
        static public object raceConditionLocker = 0;
        static public bool exitCheck; // checker for killing threads;

        static class GameOperator
        {
            static public MassiveNumber gamePoints = new MassiveNumber();

            // Update the game console.
            static public void UpdateConsole()
            {
                // Update the renderer's points.
                RenderWindow.gamePoints = gamePoints;
                renderObj.RenderLoop();
            }

            static public void UnlockAgents()
            {
                // Attempt to unlock every locked agent.
                foreach (Agent a in agentObjsArr)
                {
                    if (a.isLocked)
                    {
                        a.Unlock(gamePoints);
                    }
                }
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
                    if (!agentObjsArr[i].isLocked)
                    {
                        GameOperator.gamePoints.value = GameOperator.gamePoints.Add(agentObjsArr[i].agentCount.value, agentObjsArr[i].agentCount.echelon); // Point incrementing algorithm.

                        RenderWindow.agentCount[i] = agentObjsArr[i].agentCount;
                        RenderWindow.agentPrice[i] = agentObjsArr[i].GetPrice();
                        RenderWindow.agentPointsRate[i] = (agentObjsArr[i].pointsRate);
                    }
                }

                RenderWindow.gamePoints = GameOperator.gamePoints;
                GameOperator.gamePoints.UpdateEchelon();

                lock (raceConditionLocker)
                {
                    // Attempt to update the console.
                    GameOperator.UpdateConsole();
                    SharedResource++;

                    // Attempt to unlock every locked agent.
                    GameOperator.UnlockAgents();
                }

                if (Program.exitCheck)
                {
                    break;
                }

                // Loop every second.
                Thread.Sleep(1000);
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
                    if (GameOperator.gamePoints.IsGreaterThan(agentCost))
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
                }
                else
                {
                    switch (playerInput.Key)
                    {
                        case ConsoleKey.Spacebar:
                            GameOperator.gamePoints.value = GameOperator.gamePoints.Add(1, 1);
                            GameOperator.gamePoints.UpdateEchelon();

                            // Attempt to unlock every locked agent.
                            GameOperator.UnlockAgents();

                            break;

                        case ConsoleKey.X:
                            exitCheck = true;

                            break;
                    }

                    if (Program.exitCheck)
                    {
                        break;
                    }
                }

                // Prevent both threads from updating simultaneously.
                lock (raceConditionLocker)
                {
                    GameOperator.UpdateConsole();
                    SharedResource--;
                }

                Thread.Sleep(100);
            }

            if (Program.exitCheck)
            {
                Console.Clear();
                Console.WriteLine("Thank you for playing! Your final total of money earned was: " + GameOperator.gamePoints.GetAbbreviation());
                Console.ReadKey();
            }
        }

        #endregion

        static void Main()
        {
            // Initialize the exit condition.
            exitCheck = false;

            // Initialize ten agents.
            agentObjsArr[0] = new Agent(1, 10, 1.0275);
            agentObjsArr[1] = new Agent(2.5, 30, 1.03);
            agentObjsArr[2] = new Agent(5, 70, 1.035);
            agentObjsArr[3] = new Agent(10, 150, 1.04);
            agentObjsArr[4] = new Agent(20, 350, 1.0425);
            agentObjsArr[5] = new Agent(45, 500, 1.044);
            agentObjsArr[6] = new Agent(85, 1000, 1.5);
            agentObjsArr[7] = new Agent(150, 2000, 1.55);
            agentObjsArr[8] = new Agent(250, 3500, 1.6);
            agentObjsArr[9] = new Agent(300, 5000, 1.65);

            // Initial console draw.
            //GameOperator.gamePoints.value = 0;
            //GameOperator.gamePoints.echelon = 1;

            for (int i = 0; i < agentObjsArr.Length; i++)
            {
                RenderWindow.agentPrice[i] = agentObjsArr[i].GetPrice();
            }

            GameOperator.UpdateConsole();

            // Instantiate threads.
            /*ThreadStart gameLoop = new ThreadStart(GameLoop);
            Thread myGameLoop = new Thread(gameLoop);
            myGameLoop.Start();
            */
            ThreadStart inputLoop = new ThreadStart(PlayerInput);
            Thread myInputLoop = new Thread(inputLoop);
            myInputLoop.Start();
        }
    }
}
