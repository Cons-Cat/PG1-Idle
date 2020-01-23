using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Source
{
    // Agent object behavior
    public class Agent : Item
    {
        public double pointsRate { get; set; }                  // Arbitrary rate that this agent index generates points.
        public MassiveNumber unlockScore = new MassiveNumber();
        public bool isLocked;

        public Agent(double argPointsRate, double argInitialPrice, double argPriceFactor)
        {
            // Initialize variables

            // Change these for testing:
            count.value = 0;
            count.echelon = 1;

            // Progression scaling:
            pointsRate = argPointsRate;
            initPrice.value = argInitialPrice;
            priceFactor = argPriceFactor;
            UpdatePrice();

            // Bank score to unlock:
            unlockScore.value = argInitialPrice * 0.745;    // Slightly under 3/4 of initial price.
            unlockScore.UpdateEchelon();

            isLocked = true;
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
