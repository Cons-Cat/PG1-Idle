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
        public MassiveNumber unlockScore = new MassiveNumber();
        public bool isLocked;

        public Agent(double argPointsRate, double argInitialPrice, double argPriceFactor)
        {
            // Initialize variables

            // Change these for testing:
            agentCount.value = 0;
            agentCount.echelon = 1;

            // Progression scaling:
            pointsRate = argPointsRate;
            initPrice = argInitialPrice;
            priceFactor = argPriceFactor;

            // Bank score to unlock:
            unlockScore.value = argInitialPrice * 0.745;    // Slightly under 3/4 of initial price.
            unlockScore.UpdateEchelon();

            isLocked = true;
        }

        public MassiveNumber GetPrice()
        {
            MassiveNumber tempNum = new MassiveNumber();
            tempNum.value = initPrice;
            tempNum.UpdateEchelon();

            // Do not scale the first agent to purchase.
            if (agentCount.value > 0)
            {
                tempNum.value = tempNum.Mult(agentCount.value + (1d / (agentCount.echelon * 1000)), agentCount.echelon);
                tempNum.UpdateEchelon();

                tempNum = tempNum.Pow(priceFactor);
            }

            return tempNum;
        }

        public void Unlock(MassiveNumber argPoints)
        {
            if (argPoints.IsGreaterThan(this.unlockScore))
            {
                isLocked = false;
            }
        }
    }
}
