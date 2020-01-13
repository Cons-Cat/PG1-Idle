using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Source
{
    class Mathcore
    {
        private double agent1; // private fields for initializing values

        public Mathcore() // init values (how many agents do we have?)
        {
            agent1 = 5;
        }
        public Mathcore(double a1) // Allowing for a change in value (increasing agents)
        {
            agent1 = a1;
        }
        public double GetAgent1() // Allows for access of private variable
        {
            return agent1;
        }
        public void SetAgent1(double a1) // Allows for editing of private variables
        {
            agent1 = a1;
        }
        public static double Agent1c(int agent1) // the long ugly chain of math statements for 11 possible outcomes. Agent#s is the rate of which an agent is generating points (score) per game calculation.
        {
            if (agent1 == 0) 
            {
                double agent1s = (0 * 0.1);
                return agent1s;
            }
            else if (agent1 == 1)
            {
                double agent1s = (1 * 0.1);
                return agent1s;
            }
            else if (agent1 == 2)
            {
                double agent1s = (2 * 0.1);
                return agent1s;
            }
            else if (agent1 == 3)
            {
                double agent1s = (3 * 0.1);
                return agent1s;
            }
            else if (agent1 == 4)
            {
                double agent1s = (4 * 0.1);
                return agent1s;
            }
            else if (agent1 == 5)
            {
                double agent1s = (5 * 0.1);
                return agent1s;
            }
            else if (agent1 == 6)
            {
                double agent1s = (6 * 0.1);
                return agent1s;
            }
            else if (agent1 == 7)
            {
                double agent1s = (7 * 0.1);
                return agent1s;
            }
            else if (agent1 == 8)
            {
                double agent1s = (8 * 0.1);
                return agent1s;
            }
            else if (agent1 == 9)
            {
                double agent1s = (9 * 0.1);
                return agent1s;
            }
            else if (agent1 == 10)
            {
                double agent1s = (10 * 0.1);
                return agent1s;
            }
            return agent1;
        }
    }
}
