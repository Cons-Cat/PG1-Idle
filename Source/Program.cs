using System;
using System.Collections.Generic;
using System.Text;
using Rendering;

namespace Source
{
	class Program
	{
		static void Main( string[] args )
		{
			// Render console window
			RenderWindow renderObj = new RenderWindow(9,3);

			renderObj.RenderLoop();

			Console.ReadLine();
		}
	}
}
