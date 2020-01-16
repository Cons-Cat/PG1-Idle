using System;
using System.Collections.Generic;
using System.Text;

namespace Rendering
{
	class RenderWindow
	{
		// Initialize variables
		int[] columnWidth;
		uint columnCount;
		uint rowCount;
		uint points;

		string[,] gridString;
		bool[,] gridHasVal;
		uint[,] gridValue;
		ConsoleColor[,] gridCol;

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
			gridHasVal = new bool[rowCount, columnCount];
			gridValue = new uint[rowCount, columnCount];
			gridCol = new ConsoleColor[rowCount, columnCount];

            #endregion

            // Hardcoded values:
            #region

            columnWidth[0] = 8;
			columnWidth[1] = 32;

			points = 0;

            #endregion

            // Dummy data:
            for (uint i = 0; i < rowCount; i++)
			{
				for (uint j = 0; j < columnCount; j++)
				{
					// Default to blank grid.
					gridString[i, j] = "";
					gridHasVal[i, j] = false;

                    // Write leftmost column.
                    #region

                    if (j == 1)
					{
						// Center these rows relative to the agents' rows.
						if (i == rowCount / 2 - 1)
						{
							gridHasVal[i, j] = true;
							gridValue[i, j] = points;
							gridCol[i, j] = ConsoleColor.Magenta;

							gridString[i, j] = "Points: ";
						}

						if (i == rowCount / 2)
						{
							gridString[i, j] = "Press W";
						}

						if (i == rowCount / 2 + 1)
						{
							gridString[i, j] = "Press Shift";
						}
					}

                    #endregion

                    // Write agents column.
                    #region

                    if (j == 2)
					{
						gridHasVal[i, j] = true;
						gridValue[i, j] = i;
						gridCol[i, j] = ConsoleColor.Cyan;

						gridString[i, j] = "Agent: ";
					}

                    #endregion

                    // Write agents price column.
                    #region

                    if (j == 3)
					{
						gridHasVal[i, j] = true;
						gridValue[i, j] = (uint)(Convert.ToDouble(i) * 2.5);
						gridCol[i, j] = ConsoleColor.DarkCyan;

						gridString[i, j] = "Price: ";
					}

                    #endregion
                }
            }
		}

		public void RenderLoop()
		{
			string tempStr;

			// Write each row.
			for (uint rowIterate = 0; rowIterate < rowCount; rowIterate++)
			{
				// Write each column of this row.
				for (uint columnIterate = 0; columnIterate < columnCount; columnIterate++)
				{
					tempStr = gridString[rowIterate, columnIterate];
					
					if (gridHasVal[rowIterate, columnIterate])
					{
						// Colorize the grid's value
						Console.Write(tempStr);

						Console.ForegroundColor = gridCol[rowIterate, columnIterate];
						Console.Write(gridValue[rowIterate, columnIterate].ToString().PadRight(columnWidth[columnIterate] - tempStr.Length, ' '));
						Console.ForegroundColor = ConsoleColor.White; // Reset color
					}
					else
					{
						// Write a grid without a value
						Console.Write(tempStr.PadRight(columnWidth[columnIterate], ' '));
					}
				}

				// Shift to next row.
				Console.WriteLine();
			}
		}
	}
}
