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

            // Update the game console
            static public void UpdateConsole()
            {
                // Update the renderer"s points.
                RenderWindow.displayPoints = GameOperator.gamePoints;
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
                for (int i = 0; i < 10; i++)
                {
                    GameOperator.gamePoints += agentObjsArr[i].pointsRate * agentObjsArr[i].agentCount; // Point incrementing algorithm.
                    RenderWindow.gamePoints = (uint)GameOperator.gamePoints;
                    RenderWindow.agentCount[i] = agentObjsArr[i].agentCount;
                    RenderWindow.agentPrice[i] = agentObjsArr[i].GetPrice();
                    RenderWindow.agentPointsRate[i] = (uint)(agentObjsArr[i].pointsRate);
                }

                // Prevent both threads from updating simultaneously.
                lock (_locker)
                {
                    GameOperator.UpdateConsole();
                    SharedResource++;
                }

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
                    inputIndex = (int)(Char.GetNumericValue(playerInput.KeyChar) + 9) % 10;
                    uint agentCost = (uint)agentObjsArr[inputIndex].GetPrice();

                    // If the player has sufficient points
                    if ((uint)GameOperator.gamePoints >= agentCost)
                    {
                        // Increment the agent that the user inputs.
                        agentObjsArr[inputIndex].agentCount++;

                        // Update the console values.
                        RenderWindow.agentCount[inputIndex]++;
                        RenderWindow.agentPrice[inputIndex] = agentObjsArr[inputIndex].GetPrice();

                        // Decrease the player"s points bank.
                        GameOperator.gamePoints -= agentCost;
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
                    GameOperator.UpdateConsole();
                    SharedResource--;
                }

                Thread.Sleep(100);
            }
        }

        #endregion

        static void Main()
        {
            // Initialize ten agents.
            agentObjsArr[0] = new Agent(1.1, 10);
            agentObjsArr[1] = new Agent(1.5, 15);
            agentObjsArr[2] = new Agent(2, 25);
            agentObjsArr[3] = new Agent(2.25, 40);
            agentObjsArr[4] = new Agent(2.75, 60);
            agentObjsArr[5] = new Agent(3, 85);
            agentObjsArr[6] = new Agent(5, 100);
            agentObjsArr[7] = new Agent(6, 150);
            agentObjsArr[8] = new Agent(8, 200);
            agentObjsArr[9] = new Agent(10.5, 300);

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
    class CustomDouble
    {
        // Instantiate values
        uint echelon;
        double value;

        // Constructor
        public CustomDouble()
        {
            // Initialize values
            echelon = 1;
            value = 0;
        }

        // Updating the echelon
        #region

        public void IncreaseEchelon()
        {
            double tempValue = value;
            uint increments = 0;

            // While the ones place is empty:
            while (tempValue % 1 == 0)
            {
                // Increment the decimal place.
                tempValue *= 10;
                increments++;
            }

            // Increment the echelon every three ticks.
            echelon += increments / 3;
        }

        public void DecreaseEchelon()
        {
            double tempValue = value;
            uint increments = 0;

            // While the ones place is empty:
            while (tempValue.ToString().Length > 3)
            {
                // Decrement the decimal place.
                tempValue /= 10;

                // Decrement the echelon every three ticks.
                increments++;
            }

            // Decrement the echelon every three ticks.
            echelon -= increments / 3;
        }

        public string GetAbbreviation()
        {
            string returnStr;

            switch (echelon)
            {
                case 1:
                    returnStr = "";
                    break;

                case 2:
                    // Abbreviate thousands.
                    returnStr = " K";
                    break;

                case 3:
                    // Abbreviate millions.
                    returnStr = " M";
                    break;

                case 4:
                    // Abbreviate billions.
                    returnStr = " B";
                    break;

                case 5:
                    // Abbreviate trillions.
                    returnStr = " T";
                    break;

                case 6:
                    // Abbreviate quadrillions.
                    returnStr = " Q";
                    break;

                default:
                    // Generic abbreviation from quintillion onwards.
                    returnStr = " e" + (echelon * 3).ToString();
                    break;
            }

            return value.ToString() + returnStr;
        }

        #endregion

        // Math operations:
        #region

        // Addition operation:
        public void Add(double argDouble, uint argEchelon)
        {
            value += (argDouble * 10 * (argEchelon - this.echelon));

            IncreaseEchelon();
        }

        // Subtraction operation:
        public void Sub(double argDouble, uint argEchelon)
        {
            value -= (argDouble * 10 * (argEchelon - this.echelon));

            DecreaseEchelon();
        }

        // Multiplication operation:
        public void Mult(double argDouble, uint argEchelon)
        {
            value *= (argDouble * 10 * (argEchelon - this.echelon));

            IncreaseEchelon();
        }

        // Exponentiation operation:
        public void Pow(double argExponent)
        {
            value = Math.Pow(value, argExponent);

            IncreaseEchelon();
        }

        #endregion
    }
}
