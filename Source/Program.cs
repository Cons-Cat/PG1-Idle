using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Source
{
	class Program
	{
		static void Main( string[] args )
		{
			Mathcore mathcore = new Mathcore(); // creates instance of class
			while (true)
			{
				//Runs the mathcore score generator once per second
				Mathcore.Agentc(mathcore.GetAgent1(), mathcore.GetAgent2(), mathcore.GetAgent3(), mathcore.GetAgent4(), mathcore.GetAgent5(), mathcore.GetAgent6(), mathcore.GetAgent7(), mathcore.GetAgent8(), mathcore.GetAgent9());
			
			}
		}
	}
}
