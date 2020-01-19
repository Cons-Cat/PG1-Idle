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

		// Passed in by Program.cs
		public static uint[] agentCount;
		public static double[] agentPrice;
		public static uint[] agentPointsRate;
		public static uint gamePoints;

		// Constructor
		public RenderWindow(uint argRows, uint argColumns) {
			// Variable values:
			#region

			rowCount = argRows + 1;
			columnCount = argColumns+1;

            #endregion

            // Declare arrays:
            #region

			gridString = new string[rowCount, columnCount];
			gridValue = new uint[rowCount, columnCount];
			agentCount = new uint[10];
			agentPrice = new double[10];
			agentPointsRate = new uint[10];
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
				agentPointsRate[i] = 1;
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

					switch (gridType[rowIterate,columnIterate])
					{
						case RenderGridTypes.GridEmpty:
							Console.Write(gridString[rowIterate, columnIterate].PadRight(columnWidth[columnIterate] - tempStr.Length, ' '));
							break;
						case RenderGridTypes.GridPoints:
							Console.Write(tempStr);

							Console.ForegroundColor = ConsoleColor.Magenta;
							Console.Write(AbbreviateNumber((uint)displayPoints).PadRight(columnWidth[columnIterate] - tempStr.Length, ' '));

							break;
						case RenderGridTypes.GridString:
							Console.Write(gridString[rowIterate, columnIterate].PadRight(columnWidth[columnIterate], ' '));

							break;
						case RenderGridTypes.GridAgents:
							Console.Write(tempStr);

							Console.ForegroundColor = ConsoleColor.DarkGray;
							Console.Write(AbbreviateNumber(agentCount[rowIterate]).PadRight(columnWidth[columnIterate] - tempStr.Length, ' '));

							break;
						case RenderGridTypes.GridPrice:
							Console.ForegroundColor = ConsoleColor.White; // Reset color
							Console.Write(tempStr);

							if (rowIterate == optimalAgent)
							{
								Console.ForegroundColor = ConsoleColor.DarkGreen;
							}
							else
							{
								if (gamePoints >= agentPrice[rowIterate])
								{
									// If the player can afford this agent.
									Console.ForegroundColor = ConsoleColor.DarkGray;
								}
								else
								{
									// If the player cannot afford this agent.
									Console.ForegroundColor = ConsoleColor.DarkRed;
								}
							}

							Console.Write(AbbreviateNumber((uint)agentPrice[rowIterate]).PadRight(columnWidth[columnIterate] - tempStr.Length, ' '));

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
				// Find shortest time for an agent's income to break even with its cost.
				if ((agentPrice[i] / agentPointsRate[i]) <= (agentPrice[returnRow] / agentPointsRate[returnRow]))
				{
					returnRow = i;
				}
			}

			return returnRow;
		}

		private string AbbreviateNumber(uint argNum)
		{
			string returnStr;

			if (argNum.ToString().Length <= 5)
			{
				// Do not clip numbers that are 5 digits or fewer.
				returnStr = argNum.ToString();
			}
			else if (argNum.ToString().Length <= 6)
			{
				// Clip numbers in the range of 100,000 - 999,999.
				returnStr = argNum.ToString();
				ReturnString(returnStr[0], returnStr[1], returnStr[2], 'K');
			}
			else if (argNum.ToString().Length <= 9)
			{
				// Clip numbers in the range of 1,000,000 - 999,999,999
				returnStr = argNum.ToString();
				ReturnString(returnStr[0], returnStr[1], returnStr[2], 'K');
			}
			else if (argNum.ToString().Length <= 12)
			{
				// Clip numbers in the range of 1,000,000,000 - 999,999,999,999
				returnStr = argNum.ToString();
				ReturnString(returnStr[0], returnStr[1], returnStr[2], 'B');
			}
			else if (argNum.ToString().Length <= 15)
			{
				// Clip numbers in the range of 1,000,000,000,000 - 999,999,999,999,999
				returnStr = argNum.ToString();
				ReturnString(returnStr[0], returnStr[1], returnStr[2], 'T');
			}
			else if (argNum.ToString().Length <= 18)
			{
				// Clip numbers in the range of 1,000,000,000,000,000 - 999,999,999,999,999,999
				returnStr = argNum.ToString();
				ReturnString(returnStr[0], returnStr[1], returnStr[2], 'Q');
			}
			else
			{
				returnStr = "HUUUUUGE NUM!";
			}

			// Local function to assemble the string.
			void ReturnString(char argChar0, char argChar1, char argChar2, char argCharEnd)
			{
				if (argChar1.Equals('0'))
				{
					// If the number's second digit is 0, only print the first.
					returnStr = argChar0.ToString() + argCharEnd.ToString();
				}
				else if (argChar2.Equals('0'))
				{
					// If the number's third digit is 0, only print the first two.
					returnStr = argChar0.ToString() + "." + argChar1.ToString() + argCharEnd.ToString();
				}
				else
				{
					// If the number's first three digits are non-zero, print them all.
					returnStr = argChar0.ToString() + "." + argChar1.ToString() + argChar2.ToString() + argCharEnd.ToString();
				}
			}

			return returnStr;
		}
	}
}
