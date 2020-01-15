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
			RenderWindow renderObj = new RenderWindow(13,3);

			renderObj.RenderLoop();

			Console.ReadLine();
		}
	}
}
