using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Source
{
    enum RenderGridTypes
    {
        GridEmpty,
        GridString,
        GridPoints,
        GridPrice,
        GridAgentsCount,
        GridAgentLabel
    }

    class RenderWindow
    {
        // Initialize variables
        int[] columnWidth;
        uint columnCount;
        uint rowCount;
        //public static MassiveNumber displayPoints { get; set; }

        string[,] gridString;
        uint[,] gridValue;
        bool[,] gridHasVal;
        bool[,] gridIsStatic;

        RenderGridTypes[,] gridType;
        ConsoleColor[,] gridCol;

        // Passed in by Program.cs
        public static MassiveNumber gamePoints;
        public static MassiveNumber displayPoints; // = new MassiveNumber();
        public static MassiveNumber[] agentCount = new MassiveNumber[10];
        public static MassiveNumber[] agentPrice = new MassiveNumber[10];
        public static double[] agentPointsRate;
        public static bool[] agentIsLocked;

        // Constructor
        public RenderWindow(uint argRows, uint argColumns)
        {
            // Variable values:
            #region

            rowCount = argRows + 1;
            columnCount = argColumns + 1;

            #endregion

            // Declare arrays:
            #region

            gridString = new string[rowCount, columnCount];
            gridValue = new uint[rowCount, columnCount];
            gamePoints = new MassiveNumber();
            displayPoints = new MassiveNumber();
            agentPointsRate = new double[rowCount];
            gridHasVal = new bool[rowCount, columnCount];
            gridIsStatic = new bool[rowCount, columnCount];
            agentIsLocked = new bool[rowCount];
            gridType = new RenderGridTypes[rowCount, columnCount];
            gridCol = new ConsoleColor[rowCount, columnCount];

            columnWidth = new int[columnCount];

            for (uint i = 0; i < columnCount; i++)
            {
                columnWidth[i] = 25;
            }
            for (uint i = 0; i < rowCount; i++)
            {
                agentCount[i] = new MassiveNumber();
                agentPrice[i] = new MassiveNumber();

                agentPointsRate[i] = 1.0;
                agentIsLocked[i] = true;
            }

            #endregion

            // Hardcoded values:
            #region

            columnWidth[0] = 8;     // Empty
            columnWidth[1] = 30;    // Legend
            columnWidth[2] = 7;    // Agent Keys
            columnWidth[3] = 25;    // Agent Count
            columnWidth[4] = 17;    // Agent Price
                                    // Agent Label

            for (uint i = 0; i < rowCount; i++)
            {
                for (uint j = 0; j < columnCount; j++)
                {
                    // Default to blank grid.
                    gridString[i, j] = "";
                    gridType[i, j] = RenderGridTypes.GridEmpty;

                    // Write info
                    #region

                    if (j == 0 && i == 0)
                    {
                        gridType[i, j] = RenderGridTypes.GridString;
                        gridString[i, j] = "Exit: X";
                    }

                    #endregion

                    // Write leftmost column.
                    #region

                    if (j == 1)
                    {
                        // Center these rows relative to the agents' rows.
                        if (i == rowCount / 2 - 1)
                        {
                            gridType[i, j] = RenderGridTypes.GridPoints;
                            gridString[i, j] = "Dollars: ";
                        }
                        else if (i == rowCount / 2)
                        {
                            gridType[i, j] = RenderGridTypes.GridString;
                            gridString[i, j] = "Press Spacebar to dig";
                        }
                        else if (i == rowCount / 2 + 1)
                        {
                            gridType[i, j] = RenderGridTypes.GridString;
                            gridString[i, j] = "Press Shift";
                        }
                    }

                    #endregion

                    // Write key press column
                    #region

                    if (j == 2)
                    {
                        gridString[i, j] = "[" + ((int)(i + 11) % 10) + "]";
                        gridType[i, j] = RenderGridTypes.GridString;
                    }

                    #endregion

                    // Write agents price column.
                    #region

                    if (j == 3)
                    {
                        gridString[i, j] = "Price: ";
                        gridType[i, j] = RenderGridTypes.GridPrice;
                    }

                    #endregion

                    // Write agents count column.
                    #region

                    if (j == 4)
                    {
                        if (i == 0)
                        {
                            gridType[i, j] = RenderGridTypes.GridAgentsCount;
                        }
                        else if (i == 1)
                        {
                            gridType[i, j] = RenderGridTypes.GridAgentsCount;
                        }
                        else if (i == 2)
                        {
                            gridType[i, j] = RenderGridTypes.GridAgentsCount;
                        }
                        else if (i == 3)
                        {
                            gridType[i, j] = RenderGridTypes.GridAgentsCount;
                        }
                        else if (i == 4)
                        {
                            gridType[i, j] = RenderGridTypes.GridAgentsCount;
                        }
                        else if (i == 5)
                        {
                            gridType[i, j] = RenderGridTypes.GridAgentsCount;
                        }
                        else if (i == 6)
                        {
                            gridType[i, j] = RenderGridTypes.GridAgentsCount;
                        }
                        else if (i == 7)
                        {
                            gridType[i, j] = RenderGridTypes.GridAgentsCount;
                        }
                        else if (i == 8)
                        {
                            gridType[i, j] = RenderGridTypes.GridAgentsCount;
                        }
                        else if (i == 9)
                        {
                            gridType[i, j] = RenderGridTypes.GridAgentsCount;
                        }
                    }

                    #endregion

                    // Write agents label column.
                    #region

                    if (j == 5)
                    {
                        if (i == 0)
                        {
                            gridString[i, j] = ("Stone Harvester");
                            gridType[i, j] = RenderGridTypes.GridAgentLabel;
                        }
                        else if (i == 1)
                        {
                            gridString[i, j] = ("Coal Miner");
                            gridType[i, j] = RenderGridTypes.GridAgentLabel;
                        }
                        else if (i == 2)
                        {
                            gridString[i, j] = ("Iron Miner");
                            gridType[i, j] = RenderGridTypes.GridAgentLabel;
                        }
                        else if (i == 3)
                        {
                            gridString[i, j] = ("Drill Operator");
                            gridType[i, j] = RenderGridTypes.GridAgentLabel;
                        }
                        else if (i == 4)
                        {
                            gridString[i, j] = ("Steel Drill Operator");
                            gridType[i, j] = RenderGridTypes.GridAgentLabel;
                        }
                        else if (i == 5)
                        {
                            gridString[i, j] = ("Diamond Drill Operator");
                            gridType[i, j] = RenderGridTypes.GridAgentLabel;
                        }
                        else if (i == 6)
                        {
                            gridString[i, j] = ("Blast Miner");
                            gridType[i, j] = RenderGridTypes.GridAgentLabel;
                        }
                        else if (i == 7)
                        {
                            gridString[i, j] = ("Demolitionist");
                            gridType[i, j] = RenderGridTypes.GridAgentLabel;
                        }
                        else if (i == 8)
                        {
                            gridString[i, j] = ("Demolition Expert");
                            gridType[i, j] = RenderGridTypes.GridAgentLabel;
                        }
                        else if (i == 9)
                        {
                            gridString[i, j] = ("Crypto Miner");
                            gridType[i, j] = RenderGridTypes.GridAgentLabel;
                        }
                    }

                    #endregion
                }
            }

            #endregion
        }

        public void RenderLoop()
        {
            string tempStr;
            uint optimalAgent = OptimalAgent();

            // Refresh the console;
            Console.Clear();

            // Write each row.
            for (uint rowIterate = 0; rowIterate < rowCount; rowIterate++)
            {
                // Write each column of this row.
                for (uint columnIterate = 0; columnIterate < columnCount; columnIterate++)
                {
                    tempStr = gridString[rowIterate, columnIterate];

                    switch (gridType[rowIterate, columnIterate])
                    {
                        case RenderGridTypes.GridEmpty:
                            Console.Write(gridString[rowIterate, columnIterate].PadRight(columnWidth[columnIterate] - tempStr.Length, ' '));
                            break;

                        case RenderGridTypes.GridPoints:
                            Console.Write(tempStr);

                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.Write(gamePoints.GetAbbreviation().PadRight(columnWidth[columnIterate] - tempStr.Length, ' '));

                            break;

                        case RenderGridTypes.GridString:
                            Console.Write(gridString[rowIterate, columnIterate].PadRight(columnWidth[columnIterate], ' '));

                            break;

                        case RenderGridTypes.GridAgentsCount:
                            Console.Write(tempStr);

                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write(agentCount[rowIterate].GetAbbreviation().PadRight(columnWidth[columnIterate] - tempStr.Length, ' '));

                            break;

                        case RenderGridTypes.GridAgentLabel:
                            Console.ForegroundColor = ConsoleColor.DarkGray;

                            if (agentIsLocked[rowIterate])
                            {
                                string tempQStr = "";

                                foreach (char c in gridString[rowIterate, columnIterate])
                                {
                                    tempQStr += "?";
                                }

                                Console.Write(tempQStr.PadRight(columnWidth[columnIterate], ' '));
                            }
                            else
                            {
                                Console.Write(gridString[rowIterate, columnIterate].PadRight(columnWidth[columnIterate], ' '));
                            }
                            break;

                        case RenderGridTypes.GridPrice:
                            Console.ForegroundColor = ConsoleColor.White; // Reset color
                            Console.Write(tempStr);

                            if (gamePoints.IsGreaterThan(agentPrice[rowIterate]))
                            {
                                // If the player can afford this agent.
                                if (rowIterate == optimalAgent)
                                {
                                    // If this is the most cost efficient agent.
                                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkGray;
                                }
                            }
                            else
                            {
                                // If the player cannot afford this agent.
                                if (rowIterate == optimalAgent)
                                {
                                    // If this is the most cost efficient agent.
                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                }
                            }

                            Console.Write(agentPrice[rowIterate].GetAbbreviation().PadRight(columnWidth[columnIterate] - tempStr.Length, ' '));

                            break;

                            // End of switch-case block.
                    }

                    // Reset color.
                    Console.ForegroundColor = ConsoleColor.White;
                }

                // Shift to next row.
                Console.WriteLine();
            }
        }

        private uint OptimalAgent()
        {
            uint returnRow = 0;

            for (uint i = 0; i < rowCount; i++)
            {
                // Find the most cost efficient agent.
                // Divide each Agent's Price by its Income Rate.
                if (!agentIsLocked[i])
                {
                    if (agentPrice[i].Div(agentPointsRate[i], 1) <= agentPrice[returnRow].Div(agentPointsRate[returnRow], 1))
                    {
                        returnRow = i;
                    }
                }
            }

            return returnRow;
        }
    }
}
