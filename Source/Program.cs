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
			Mathcore.Agent1c(5); // function unknown, has no immediate effect in this context
			Console.WriteLine(Mathcore.Agent1c(10)); // displays the rate that agent1 would produce score, valid values are 0-10
			Console.ReadKey(); // stops the program from instantly closing before you can see the value.
		}
	}
}
