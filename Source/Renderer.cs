﻿using System;
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
        GridAgents,
        GridPrice
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
            }

            #endregion

            // Hardcoded values:
            #region

            columnWidth[0] = 8;
            columnWidth[1] = 32;

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
                            gridString[i, j] = "Points: ";
                        }
                        else if (i == rowCount / 2)
                        {
                            gridType[i, j] = RenderGridTypes.GridString;
                            gridString[i, j] = "Press Spacebar";
                        }
                        else if (i == rowCount / 2 + 1)
                        {
                            gridType[i, j] = RenderGridTypes.GridString;
                            gridString[i, j] = "Press Shift";
                        }
                    }

                    #endregion

                    // Write agents column.
                    #region

                    if (j == 2)
                    {
                        gridString[i, j] = "Agent " + ((i + 1) % 10).ToString() + ": ";
                        gridType[i, j] = RenderGridTypes.GridAgents;
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
                        case RenderGridTypes.GridAgents:
                            Console.Write(tempStr);

                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write(agentCount[rowIterate].GetAbbreviation().PadRight(columnWidth[columnIterate] - tempStr.Length, ' '));

                            break;
                        case RenderGridTypes.GridPrice:
                            Console.ForegroundColor = ConsoleColor.White; // Reset color
                            Console.Write(tempStr);

                            if (gamePoints.IsGreater(agentPrice[rowIterate]))
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
                    }

                    Console.ForegroundColor = ConsoleColor.White; // Reset color
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
                if (agentPrice[i].Div(agentPointsRate[i], 1) <= agentPrice[returnRow].Div(agentPointsRate[returnRow], 1))
                {
                    returnRow = i;
                }
            }

            return returnRow;
        }
    }
}
