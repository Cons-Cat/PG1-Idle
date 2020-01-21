using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Source
{
    // Agent object behavior
    public class Agent
    {
        public double pointsRate { get; set; }                  // Arbitrary rate that this agent index generates points.
        public double initPrice { get; set; }                   // Arbitrary price to scale by priceFactor.
        public double priceFactor { get; set; }                 // Arbitrary scaling of initPrice.
        public MassiveNumber agentCount = new MassiveNumber();  // Arbitrary count of this agent. Used as a multiplier for pointsRate and priceFactor.

        public Agent(double argPointsRate, double argInitialPrice, double argPriceFactor)
        {
            agentCount.value = 400;
            agentCount.echelon = 2;

            pointsRate = argPointsRate;
            initPrice = argInitialPrice;
            priceFactor = argPriceFactor;
        }

        public MassiveNumber GetPrice()
        {
            MassiveNumber tempNum = new MassiveNumber();
            tempNum.value = initPrice;
            tempNum.UpdateEchelon();

            // Do not scale the first agent to purchase.
            if (agentCount.value > 0)
            {
                //tempNum.value = tempNum.Mult(agentCount.value + 1, agentCount.echelon);
                if (initPrice == 5000)
                {
                    tempNum = tempNum.Pow(priceFactor, true);
                }
                else
                {
                    tempNum = tempNum.Pow(priceFactor, false);
                }
            }

            return tempNum;
        }
    }
}
