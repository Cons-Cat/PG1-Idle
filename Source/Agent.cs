using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Source
{
    // Agent object behavior
    public class Agent {
        public double pointsRate { get; set; }      // Arbitrary rate that this agent index generates points.
        public double priceFactor { get; set; }     // Arbitrary factor for price of another agent.
        public uint agentCount { get; set; }         // Arbitrary count of this agent. Used as a multiplier for pointsRate and priceFactor.
        
        public Agent(double argPointsRate, double argPriceFactor)
        {
            pointsRate = argPointsRate;
            priceFactor = argPriceFactor;
        }

        public double GetPrice()
        {
            return Math.Pow((priceFactor * (agentCount + 1)),1.2);
        }
    }
}
