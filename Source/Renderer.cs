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

        GridAgentPrice,
        GridAgentCount,
        GridAgentLabel,

        GridUpgradePrice,
        GridUpgradeCount,
        GridUpgradeLabel
    }

    class RenderWindow
    {
        // Initialize variables
        int[] columnWidth;
        uint columnCount;
        static uint rowCount;

        static string[,,] gridString;
        uint[,] gridStringCount;
        ConsoleColor[,,] gridStringColor;

        static RenderGridTypes[,] gridType;
        ConsoleColor[,] gridCol;

        // Passed in by Program.cs
        public static MassiveNumber gamePoints;
        public static MassiveNumber displayPoints;

        public static MassiveNumber[] agentCount = new MassiveNumber[10];
        public static MassiveNumber[] agentPrice = new MassiveNumber[10];
        public static MassiveNumber[] upgraCount = new MassiveNumber[10];
        public static MassiveNumber[] upgraPrice = new MassiveNumber[10];

        public static string[] agentLabel = new string[10];
        public static string[] upgraLabel = new string[10];

        public static double[] agentPointsRate;
        public static bool[] agentIsLocked;
        public static uint currentMenuInd;
        static uint menuPages;

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

            gridString = new string[rowCount, columnCount, 3];
            gridStringCount = new uint[rowCount, columnCount];
            gridStringColor = new ConsoleColor[rowCount, columnCount, 3];

            gridType = new RenderGridTypes[rowCount, columnCount];
            gridCol = new ConsoleColor[rowCount, columnCount];

            gamePoints = new MassiveNumber();
            displayPoints = new MassiveNumber();

            agentPointsRate = new double[rowCount];
            agentIsLocked = new bool[rowCount];

            agentLabel = new string[rowCount];
            upgraLabel = new string[rowCount];

            columnWidth = new int[columnCount];

            for (uint i = 0; i < columnCount; i++)
            {
                columnWidth[i] = 25;
            }

            for (uint i = 0; i < rowCount; i++)
            {
                agentCount[i] = new MassiveNumber();
                agentPrice[i] = new MassiveNumber();
                upgraCount[i] = new MassiveNumber();
                upgraPrice[i] = new MassiveNumber();

                agentPointsRate[i] = 1.0;
                agentIsLocked[i] = true;
            }

            #endregion

            // Hardcoded values:
            #region

            currentMenuInd = 1;     // 1 -> Agents
                                    // 2 -> Upgrades
            menuPages = 2;

            columnWidth[0] = 8;     // Empty
            columnWidth[1] = 30;    // Legend
            columnWidth[2] = 7;     // Agent Keys
            columnWidth[3] = 25;    // Agent Count
            columnWidth[4] = 17;    // Agent Price
                                    // Agent Label

            agentLabel[0] = "Stone Harvester";
            agentLabel[1] = "Coal Miner";
            agentLabel[2] = "Iron Miner";
            agentLabel[3] = "Drill Operator";
            agentLabel[4] = "Steel Drill Operator";
            agentLabel[5] = "Diamond Drill Operator";
            agentLabel[6] = "Blast Miner";
            agentLabel[7] = "Demolitionist";
            agentLabel[8] = "Demolition Expert";
            agentLabel[9] = "Crypto Miner";

            upgraLabel[1] = "Coalretg Miner";
            upgraLabel[2] = "Iron fdgMiner";
            upgraLabel[3] = "Ofdg4perator";
            upgraLabel[4] = "D435rill Operator";
            upgraLabel[5] = "Drill Operator";
            upgraLabel[6] = "Blast M345iner";
            upgraLabel[7] = "Demolit978ionist";
            upgraLabel[8] = "Expert";
            upgraLabel[9] = "Crypto 5534Miner";

            for (int i = 0; i < gridStringColor.GetLength(0); i++)
            {
                for (int j = 0; j < gridStringColor.GetLength(1); j++)
                {
                    for (int k = 0; k < gridStringColor.GetLength(2); k++)
                    {
                        gridStringColor[i, j, k] = ConsoleColor.White;
                    }
                }
            }

            for (uint i = 0; i < rowCount; i++)
            {
                for (uint j = 0; j < columnCount; j++)
                {
                    // Default to blank grid.
                    gridStringCount[i, j] = 0;
                    gridType[i, j] = RenderGridTypes.GridEmpty;
                    gridString[i, j, 0] = "";

                    // Write info
                    #region

                    if (j == 0 && i == 0)
                    {
                        gridType[i, j] = RenderGridTypes.GridString;
                        gridStringColor[i, j, 1] = ConsoleColor.Gray;
                        gridStringCount[i, j] = 1;

                        gridString[i, j, 0] = "Exit: ";
                        gridString[i, j, 1] = "X";
                    }

                    #endregion

                    // Write legend column.
                    #region

                    if (j == 1)
                    {
                        // Center these rows relative to the agents' rows.
                        if (i == rowCount / 2 - 1)
                        {
                            gridType[i, j] = RenderGridTypes.GridPoints;
                            gridString[i, j, 0] = "Dollars: ";
                        }
                        else if (i == rowCount / 2)
                        {
                            gridType[i, j] = RenderGridTypes.GridString;
                            gridStringColor[i, j, 1] = ConsoleColor.Cyan;
                            gridStringCount[i, j] = 2;

                            gridString[i, j, 0] = "Press ";
                            gridString[i, j, 1] = "Spacebar";
                            gridString[i, j, 2] = " to dig";
                        }
                        else if (i == rowCount / 2 + 1)
                        {
                            gridType[i, j] = RenderGridTypes.GridString;
                            gridStringColor[i, j, 1] = ConsoleColor.Cyan;
                            gridStringCount[i, j] = 1;

                            gridString[i, j, 0] = "Press ";
                            gridString[i, j, 1] = "Shift";
                        }
                    }

                    #endregion

                    // Write key press column
                    #region

                    if (j == 2)
                    {
                        gridType[i, j] = RenderGridTypes.GridString;
                        gridStringColor[i, j, 0] = ConsoleColor.Gray;
                        gridStringColor[i, j, 2] = ConsoleColor.Gray;
                        gridStringCount[i, j] = 2;

                        gridString[i, j, 0] = "[";
                        gridString[i, j, 1] = "" + ((int)(i + 11) % 10);
                        gridString[i, j, 2] = "]";
                    }

                    #endregion

                    // Write agents price column.
                    #region

                    if (j == 3)
                    {
                        gridString[i, j, 0] = "Price: ";
                        gridType[i, j] = RenderGridTypes.GridAgentPrice;
                    }

                    #endregion

                    // Write agents count column.
                    #region

                    if (j == 4)
                    {
                        if (i == 0)
                        {
                            gridType[i, j] = RenderGridTypes.GridAgentCount;
                        }
                        else if (i == 1)
                        {
                            gridType[i, j] = RenderGridTypes.GridAgentCount;
                        }
                        else if (i == 2)
                        {
                            gridType[i, j] = RenderGridTypes.GridAgentCount;
                        }
                        else if (i == 3)
                        {
                            gridType[i, j] = RenderGridTypes.GridAgentCount;
                        }
                        else if (i == 4)
                        {
                            gridType[i, j] = RenderGridTypes.GridAgentCount;
                        }
                        else if (i == 5)
                        {
                            gridType[i, j] = RenderGridTypes.GridAgentCount;
                        }
                        else if (i == 6)
                        {
                            gridType[i, j] = RenderGridTypes.GridAgentCount;
                        }
                        else if (i == 7)
                        {
                            gridType[i, j] = RenderGridTypes.GridAgentCount;
                        }
                        else if (i == 8)
                        {
                            gridType[i, j] = RenderGridTypes.GridAgentCount;
                        }
                        else if (i == 9)
                        {
                            gridType[i, j] = RenderGridTypes.GridAgentCount;
                        }
                    }

                    #endregion

                    // Write agents label column.
                    #region

                    if (j == 5)
                    {
                        gridString[i, j, 0] = (agentLabel[i]);

                        if (i == 0)
                        {
                            gridType[i, j] = RenderGridTypes.GridAgentLabel;
                        }
                        else if (i == 1)
                        {
                            gridType[i, j] = RenderGridTypes.GridAgentLabel;
                        }
                        else if (i == 2)
                        {
                            gridType[i, j] = RenderGridTypes.GridAgentLabel;
                        }
                        else if (i == 3)
                        {
                            gridType[i, j] = RenderGridTypes.GridAgentLabel;
                        }
                        else if (i == 4)
                        {
                            gridType[i, j] = RenderGridTypes.GridAgentLabel;
                        }
                        else if (i == 5)
                        {
                            gridType[i, j] = RenderGridTypes.GridAgentLabel;
                        }
                        else if (i == 6)
                        {
                            gridType[i, j] = RenderGridTypes.GridAgentLabel;
                        }
                        else if (i == 7)
                        {
                            gridType[i, j] = RenderGridTypes.GridAgentLabel;
                        }
                        else if (i == 8)
                        {
                            gridType[i, j] = RenderGridTypes.GridAgentLabel;
                        }
                        else if (i == 9)
                        {
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
                    tempStr = gridString[rowIterate, columnIterate, 0];

                    switch (gridType[rowIterate, columnIterate])
                    {
                        case RenderGridTypes.GridEmpty:
                            Console.Write(gridString[rowIterate, columnIterate, 0].PadRight(columnWidth[columnIterate] - tempStr.Length, ' '));
                            break;

                        case RenderGridTypes.GridPoints:
                            Console.Write(tempStr);

                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.Write(gamePoints.GetAbbreviation().PadRight(columnWidth[columnIterate] - tempStr.Length, ' '));

                            break;

                        case RenderGridTypes.GridString:
                            int tempStrLength = 0;

                            for (uint i = 0; i <= gridStringCount[rowIterate, columnIterate]; i++)
                            {
                                Console.ForegroundColor = gridStringColor[rowIterate, columnIterate, i];

                                if (i < gridStringCount[rowIterate, columnIterate])
                                {
                                    Console.Write(gridString[rowIterate, columnIterate, i]);
                                }
                                else
                                {
                                    Console.Write(gridString[rowIterate, columnIterate, i].PadRight(columnWidth[columnIterate] - tempStrLength, ' '));
                                }

                                tempStrLength += gridString[rowIterate, columnIterate, i].Length;
                            }

                            break;

                        case RenderGridTypes.GridAgentCount:
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write(agentCount[rowIterate].GetAbbreviation().PadRight(columnWidth[columnIterate], ' '));

                            break;

                        case RenderGridTypes.GridAgentLabel:
                            Console.ForegroundColor = ConsoleColor.DarkGray;

                            if (agentIsLocked[rowIterate])
                            {
                                string tempQStr = "";

                                foreach (char c in agentLabel[rowIterate])
                                {
                                    tempQStr += "?";
                                }

                                Console.Write(tempQStr.PadRight(columnWidth[columnIterate], ' '));
                            }
                            else
                            {
                                Console.Write(gridString[rowIterate, columnIterate, 0].PadRight(columnWidth[columnIterate], ' '));
                            }

                            break;

                        case RenderGridTypes.GridAgentPrice:
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

                        case RenderGridTypes.GridUpgradeCount:
                            Console.Write(tempStr);

                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write(upgraCount[rowIterate].GetAbbreviation().PadRight(columnWidth[columnIterate] - tempStr.Length, ' '));

                            break;

                        case RenderGridTypes.GridUpgradeLabel:
                            Console.ForegroundColor = ConsoleColor.DarkGray;

                            if (agentIsLocked[rowIterate])
                            {
                                /*string tempQStr = "";

                                foreach (char c in upgraLabel[rowIterate])
                                {
                                    tempQStr += "?";
                                }

                                Console.Write(tempQStr.PadRight(columnWidth[columnIterate], ' '));
                                */
                            }
                            else
                            {
                                //Console.Write(upgraLabel[rowIterate].PadRight(columnWidth[columnIterate], ' '));
                            }

                            break;

                        case RenderGridTypes.GridUpgradePrice:
                            Console.ForegroundColor = ConsoleColor.White; // Reset color
                            Console.Write(tempStr);

                            if (gamePoints.IsGreaterThan(upgraPrice[rowIterate]))
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

                            Console.Write(upgraPrice[rowIterate].GetAbbreviation().PadRight(columnWidth[columnIterate] - tempStr.Length, ' '));

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

        public static void ChangeMenu(uint argMenu)
        {
            // Change the menu.
            currentMenuInd = argMenu;

            // Loop through menus.
            if (currentMenuInd > menuPages)
            {
                currentMenuInd = 1;
            }
            if (currentMenuInd < 1)
            {
                currentMenuInd = menuPages;
            }

            // Update the console.
            for (int i = 0; i < rowCount; i++)
            {
                if (currentMenuInd == 1)
                {
                    {
                        gridType[i, 3] = RenderGridTypes.GridAgentPrice;
                        gridType[i, 4] = RenderGridTypes.GridAgentCount;
                        gridType[i, 5] = RenderGridTypes.GridAgentLabel;

                        gridString[i, 5, 0] = agentLabel[i];
                    }
                }

                if (currentMenuInd == 2)
                {
                    gridType[i, 3] = RenderGridTypes.GridUpgradePrice;
                    gridType[i, 4] = RenderGridTypes.GridUpgradeCount;
                    gridType[i, 5] = RenderGridTypes.GridUpgradeLabel;

                    gridString[i, 5, 0] = upgraLabel[i];

                    //Debug.WriteLine(upgraPrice[i].value);
                }
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
