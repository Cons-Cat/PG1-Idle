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
}
