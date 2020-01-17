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

			Mathcore mathcore = new Mathcore(); // creates instance of class
			
			while (true)
			{
				//Runs the mathcore score generator once per second
				Mathcore.Agentc(mathcore.GetAgent1(), mathcore.GetAgent2(), mathcore.GetAgent3(), mathcore.GetAgent4(), mathcore.GetAgent5(), mathcore.GetAgent6(), mathcore.GetAgent7(), mathcore.GetAgent8(), mathcore.GetAgent9());
			
			}

			Console.ReadLine();
		}
	}
}
