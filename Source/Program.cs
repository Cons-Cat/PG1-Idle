using System;
using System.Collections.Generic;
using System.Text;

namespace Source
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize agents
            Agent[] agentObjsArr = new Agent[10];
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

            // Render console window
            RenderWindow renderObj = new RenderWindow(9, 3);
            renderObj.RenderLoop();

            while (true)
            {
                // Loop through all agents
                for (int i = 0; i < 10; i++) {
                    GameOperator.gamePoints += agentObjsArr[i].pointsRate * agentObjsArr[i].agentCount; // Increment points by the agents.
                    RenderWindow.agentCount[i] = agentObjsArr[i].agentCount;
                    RenderWindow.agentPrice[i] = (uint)(agentObjsArr[i].priceFactor * (agentObjsArr[i].agentCount+1));
                }

                RenderWindow.displayPoints = GameOperator.gamePoints;

                // Render pertinent values in a console.
                renderObj.RenderLoop();

                // Player controls
                ConsoleKeyInfo playerInput = Console.ReadKey();

                if (Char.IsNumber(playerInput.KeyChar))
                {
                    // Increment the agent that the user inputs
                    agentObjsArr[(int)(Char.GetNumericValue(playerInput.KeyChar)+9) % 10].agentCount++;
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
            }
        }
    }

    static class GameOperator
    {
        static public double gamePoints { get; set; }
    }
}
