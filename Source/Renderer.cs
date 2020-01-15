using System;
using System.Collections.Generic;
using System.Text;

namespace Rendering
{
	class RenderWindow
	{
		// Initialize variables
		int[] columnWidth;
		int columnCount;
		int rowCount;
		int points;
		string[,] gridStrings;

		// Constructor
		public RenderWindow(int argRows, int argColumns) {
			// Variable values:
			rowCount = argRows + 1;
			columnCount = argColumns+1;

			columnWidth = new int[columnCount];

			for (int i = 0; i < columnCount; i++)
			{
				columnWidth[i] = 20;
			}

			gridStrings = new string[rowCount, columnCount];
			
			// Hardcoded values:
			columnWidth[1] = 40;

			points = 0;

			// Dummy data:
			for (int i = 0; i < rowCount; i++)
			{
				for (int j = 0; j < columnCount; j++)
				{
					// Default to blank grid.
					gridStrings[i, j] = "";

					// Write leftmost column.
					if (i == rowCount / 2 - 1 && j == 1)
					{
						gridStrings[i, j] = "Points: " +  points.ToString();
					}
					if (i == rowCount / 2 && j == 1)
					{
						gridStrings[i, j] = "Press W";
					}
					if (i == rowCount / 2 + 1 && j == 1)
					{
						gridStrings[i, j] = "Press Shift";
					}

					// Write agents column.
					if (j == 2)
					{
						gridStrings[i, j] = "Agent: " + i.ToString();
					}

					// Write agents price column.
					if (j == 3)
					{
						gridStrings[i, j] = "Price: " + ((double)i * 2.5).ToString();
					}
				}
			}
		}

		public void RenderLoop()
		{
			string tempStr;

			for (int rowIterate = 0; rowIterate < rowCount; rowIterate++)
			{
				// Write next column.
				for (int columnIterate = 0; columnIterate < columnCount; columnIterate++)
				{
					tempStr = gridStrings[rowIterate, columnIterate];

					Console.Write(tempStr.PadRight(columnWidth[columnIterate], ' '));
				}

				// Write next row.
				Console.WriteLine();
			}
		}
	}
}
