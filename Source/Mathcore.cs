using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Source
{
    // Agent object behavior
    public class Agent {
        int index;
        public double pointsRate { get; set; }      // Arbitrary rate that this agent index generates points.
        public double priceFactor { get; set; }     // Arbitrary factor for price of another agent.
        public uint agentCount { get; set; }         // Arbitrary count of this agent. Used as a multiplier for pointsRate and priceFactor.

        public Agent(int argIndex, double argPointsRate, double argPriceFactor)
        {
            index = argIndex;
            pointsRate = argPointsRate;
            priceFactor = argPriceFactor;
        }
    }

    #region
    // Calculation Methods
    /*public class MathCore
    {
        private static double ascore; // agent score init
        private double agent1; // private fields for initializing values 
        private double agent2;
        private double agent3;
        private double agent4;
        private double agent5;
        private double agent6;
        private double agent7;
        private double agent8;
        private double agent9;

        public MathCore() // init values (how many of each agent do we have?)
        {
            agent1 = 0;
            agent2 = 0;
            agent3 = 0;
            agent4 = 0;
            agent5 = 0;
            agent6 = 0;
            agent7 = 0;
            agent8 = 0;
            agent9 = 0;
        }
        public MathCore(double a1, double a2, double a3, double a4, double a5, double a6, double a7, double a8, double a9) // Allowing for a change in value (increasing agents)
        {
            agent1 = a1;
            agent2 = a2;
            agent3 = a3;
            agent4 = a4;
            agent5 = a5;
            agent6 = a6;
            agent7 = a7;
            agent8 = a8;
            agent9 = a9;

        }
        public double GetaScore()// Access scores
        {
            return ascore;
        }
        public double GetAgent1() // Allows for access of how many instances of agent 1 exist
        {
            return agent1;
        }
        public double GetAgent2()// Allows for access of how many instances of agent 2 exist
        {
            return agent2;
        }
        public double GetAgent3() // Allows for access of how many instances of agent 3 exist
        {
            return agent3;
        }
        public double GetAgent4() // Allows for access of how many instances of agent 4 exist
        {
            return agent4;
        }
        public double GetAgent5() // Allows for access of how many instances of agent 5 exist
        {
            return agent5;
        }
        public double GetAgent6() // Allows for access of how many instances of agent 6 exist
        {
            return agent6;
        }
        public double GetAgent7() // Allows for access of how many instances of agent 7 exist
        {
            return agent7;
        }
        public double GetAgent8() // Allows for access of how many instances of agent 8 exist
        {
            return agent8;
        }
        public double GetAgent9() // Allows for access of how many instances of agent 9 exist
        {
            return agent9;
        }
        public void SetAgent1(double a1) //Allows us to add agents
        {
            agent1 = a1;
        }
        public void SetAgent2(double a2) //Allows us to add agents
        {
            agent2 = a2;
        }
        public void SetAgent3(double a3) //Allows us to add agents
        {
            agent3 = a3;
        }
        public void SetAgent4(double a4) //Allows us to add agents
        {
            agent4 = a4;
        }
        public void SetAgent5(double a5) //Allows us to add agents
        {
            agent5 = a5;
        }
        public void SetAgent6(double a6) //Allows us to add agents
        {
            agent6 = a6;
        }
        public void SetAgent7(double a7) //Allows us to add agents
        {
            agent7 = a7;
        }
        public void SetAgent8(double a8) //Allows us to add agents
        {
            agent8 = a8;
        }
        public void SetAgent9(double a9) //Allows us to add agents
        {
            agent9 = a9;
        }
        public static void Agentc(double agent1, double agent2, double agent3, double agent4, double agent5, double agent6, double agent7, double agent8, double agent9)
        { // A much better variant of this score calculator, curtesy  of William
            double agent1s = agent1 * 0.1; //math for agent 1
            double agent2s = agent2 * 1;
            double agent3s = agent3 * 5;
            double agent4s = agent4 * 25;
            double agent5s = agent5 * 50;
            double agent6s = agent6 * 100;
            double agent7s = agent7 * 250;
            double agent8s = agent8 * 500;
            double agent9s = agent9 * 1000;
            ascore += (agent1s + agent2s + agent3s + agent4s + agent5s + agent6s + agent7s + agent8s + agent9s);
            Console.WriteLine(ascore);
            System.Threading.Thread.Sleep(1000);
        }
    }*/

    #endregion
}
