using System;
using System.Collections.Generic;
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
		public static double displayPoints { get; set; }

		string[,] gridString;
		uint[,] gridValue;
		bool[,] gridHasVal;
		bool[,] gridIsStatic;

		RenderGridTypes[,] gridType;
		ConsoleColor[,] gridCol;

		// Passed in by Main()
		public static uint[] agentCount;
		public static uint[] agentPrice;

		// Constructor
		public RenderWindow(uint argRows, uint argColumns) {
			// Variable values:
			#region

			rowCount = argRows + 1;
			columnCount = argColumns+1;

            #endregion

            // Declare arrays:
            #region

            columnWidth = new int[columnCount];

			for (uint i = 0; i < columnCount; i++)
			{
				columnWidth[i] = 25;
			}

			gridString = new string[rowCount, columnCount];
			gridValue = new uint[rowCount, columnCount];
			agentCount = new uint[10];
			agentPrice = new uint[10];
			gridHasVal = new bool[rowCount, columnCount];
			gridIsStatic = new bool[rowCount, columnCount];
			gridType = new RenderGridTypes[rowCount, columnCount];
			gridCol = new ConsoleColor[rowCount, columnCount];

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
						gridType[i,j] = RenderGridTypes.GridString;
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
							gridType[i,j] = RenderGridTypes.GridPoints;
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

			// Refresh the console;
			Console.Clear();

			// Write each row.
			for (uint rowIterate = 0; rowIterate < rowCount; rowIterate++)
			{
				// Write each column of this row.
				for (uint columnIterate = 0; columnIterate < columnCount; columnIterate++)
				{
					tempStr = gridString[rowIterate, columnIterate];

					switch (gridType[rowIterate,columnIterate])
					{
						case RenderGridTypes.GridEmpty:
							Console.Write(gridString[rowIterate, columnIterate].PadRight(columnWidth[columnIterate] - tempStr.Length, ' '));
							break;
						case RenderGridTypes.GridPoints:
							Console.Write(tempStr);

							Console.ForegroundColor = ConsoleColor.Magenta;
							Console.Write(((uint)displayPoints).ToString().PadRight(columnWidth[columnIterate] - tempStr.Length, ' '));

							break;
						case RenderGridTypes.GridString:
							Console.Write(gridString[rowIterate, columnIterate].PadRight(columnWidth[columnIterate], ' '));

							break;
						case RenderGridTypes.GridAgents:
							Console.Write(tempStr);

							Console.ForegroundColor = ConsoleColor.DarkGray;
							Console.Write((agentCount[rowIterate]).ToString().PadRight(columnWidth[columnIterate] - tempStr.Length, ' '));

							break;
						case RenderGridTypes.GridPrice:
							Console.ForegroundColor = ConsoleColor.White; // Reset color
							Console.Write(tempStr);

							Console.ForegroundColor = ConsoleColor.DarkGray;
							Console.Write((agentPrice[rowIterate]).ToString().PadRight(columnWidth[columnIterate] - tempStr.Length, ' '));

							break;
					}

					Console.ForegroundColor = ConsoleColor.White; // Reset color
				}

				// Shift to next row.
				Console.WriteLine();
			}
		}
	}
}
