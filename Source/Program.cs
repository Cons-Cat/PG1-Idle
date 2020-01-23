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
        static Upgrade[] upgraObjsArr = new Upgrade[10]; // 10 upgrades
        static RenderWindow renderObj = new RenderWindow(9, 5); // 10 rows, 6 columns

        static public int SharedResource { get; set; }
        static public object raceConditionLocker = 0;
        static public bool exitCheck; // checker for killing threads;
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
            for (int i = 0; i < agentObjsArr.Length; i++)
            {
                if (agentObjsArr[i].isLocked)
                {
                    agentObjsArr[i].Unlock(gamePoints);
                    RenderWindow.agentIsLocked[i] = agentObjsArr[i].isLocked;
                }
            }
        }

        // Method for game loop thread.
        #region

        static void GameLoop()
        {
            while (!exitCheck)
            {
                // Loop through all agents to increase the player's points bank.
                for (uint i = 0; i < 10; i++)
                {
                    if (!agentObjsArr[i].isLocked)
                    {
                        if (agentObjsArr[i].count.value > 0)
                        {
                            // Evaluate agents.
                            gamePoints.value = (gamePoints.Add(agentObjsArr[i].count.value, agentObjsArr[i].count.echelon));
                            gamePoints.UpdateEchelon();

                            // Evaluate upgrades.
                            if (upgraObjsArr[i].count.value > 0)
                            {
                                gamePoints.value = gamePoints.Mult(upgraObjsArr[i].count.Mult(upgraObjsArr[i].incomeMultiplier, upgraObjsArr[i].count.echelon) + 1, 1);
                                gamePoints.UpdateEchelon();
                            }

                            RenderWindow.agentCount[i] = agentObjsArr[i].count;
                            RenderWindow.agentPrice[i] = agentObjsArr[i].GetPrice();
                            RenderWindow.agentPointsRate[i] = agentObjsArr[i].pointsRate; // Used for optimal calculation
                        }
                    }
                }

                RenderWindow.gamePoints = gamePoints;
                gamePoints.UpdateEchelon();

                lock (raceConditionLocker)
                {
                    // Attempt to update the console.
                    UpdateConsole();
                    SharedResource++;

                    // Attempt to unlock every locked agent.
                    UnlockAgents();
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
            while (!exitCheck)
            {
                int inputIndex;

                // Player controls
                ConsoleKeyInfo playerInput = Console.ReadKey();

                if (Char.IsNumber(playerInput.KeyChar))
                {
                    // Get array index from digit input.
                    inputIndex = (int)(Char.GetNumericValue(playerInput.KeyChar) + 9) % 10;
                    MassiveNumber itemCost = new MassiveNumber();

                    if (RenderWindow.currentMenuInd == 1)
                    {
                        itemCost = agentObjsArr[inputIndex].GetPrice();
                    }
                    else if (RenderWindow.currentMenuInd == 2)
                    {
                        itemCost = upgraObjsArr[inputIndex].GetPrice();
                    }

                    // If the player has sufficient points
                    if (gamePoints.IsGreaterThan(itemCost))
                    {
                        // Update the console values.
                        if (RenderWindow.currentMenuInd == 1)
                        {
                            // Increment the agent that the user inputs.
                            agentObjsArr[inputIndex].count.value = agentObjsArr[inputIndex].count.Add(1, 1);
                            agentObjsArr[inputIndex].count.UpdateEchelon();
                            agentObjsArr[inputIndex].UpdatePrice();

                            RenderWindow.agentCount[inputIndex] = agentObjsArr[inputIndex].count;
                            RenderWindow.agentPrice[inputIndex] = agentObjsArr[inputIndex].GetPrice();
                        }
                        else if (RenderWindow.currentMenuInd == 2)
                        {
                            // Increment the upgrade that the user inputs.
                            upgraObjsArr[inputIndex].count.value = upgraObjsArr[inputIndex].count.Add(1, 1);
                            upgraObjsArr[inputIndex].count.UpdateEchelon();

                            RenderWindow.upgraCount[inputIndex] = upgraObjsArr[inputIndex].count;
                            upgraObjsArr[inputIndex].UpdatePrice();
                            RenderWindow.upgraPrice[inputIndex] = upgraObjsArr[inputIndex].GetPrice();
                        }

                        // Decrease the player's points bank.
                        gamePoints.value = gamePoints.Sub(itemCost.value, itemCost.echelon);
                        gamePoints.UpdateEchelon();
                    }
                }
                else
                {
                    switch (playerInput.Key)
                    {
                        case ConsoleKey.Spacebar:
                            gamePoints.value = gamePoints.Add(1, 1);
                            gamePoints.UpdateEchelon();

                            // Attempt to unlock every locked agent.
                            UnlockAgents();

                            break;

                        case ConsoleKey.RightArrow:
                            RenderWindow.ChangeMenu(RenderWindow.currentMenuInd + 1);
                            break;

                        case ConsoleKey.LeftArrow:
                            RenderWindow.ChangeMenu(RenderWindow.currentMenuInd - 1);
                            break;

                        case ConsoleKey.X:
                            exitCheck = true;

                            break;
                    }
                }

                // Prevent both threads from updating simultaneously.
                lock (raceConditionLocker)
                {
                    UpdateConsole();
                    SharedResource--;
                }

                Thread.Sleep(110);
            }

            if (exitCheck)
            {
                Console.Clear();
                Console.WriteLine("Thank you for playing! Your final total of money earned was: " + gamePoints.GetAbbreviation());
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

            // Initialize ten upgrades.
            upgraObjsArr[0] = new Upgrade(1, 100, 2);
            upgraObjsArr[1] = new Upgrade(2.5, 300, 2);
            upgraObjsArr[2] = new Upgrade(5, 700, 2);
            upgraObjsArr[3] = new Upgrade(10, 1500, 2);
            upgraObjsArr[4] = new Upgrade(20, 3500, 2);
            upgraObjsArr[5] = new Upgrade(45, 5000, 2);
            upgraObjsArr[6] = new Upgrade(85, 10000, 2);
            upgraObjsArr[7] = new Upgrade(150, 20000, 2);
            upgraObjsArr[8] = new Upgrade(250, 35000, 1.75);
            upgraObjsArr[9] = new Upgrade(300, 50000, 1.5);

            agentObjsArr[0].count.value = 10;
            agentObjsArr[7].count.value = 1;

            gamePoints.value = 10000;
            gamePoints.UpdateEchelon();

            // Initial console draw.
            for (int i = 0; i < agentObjsArr.Length; i++)
            {
                RenderWindow.agentPrice[i] = agentObjsArr[i].GetPrice();
                RenderWindow.upgraPrice[i] = upgraObjsArr[i].GetPrice();
            }

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
