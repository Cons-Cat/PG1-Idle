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
        public MassiveNumber pointsRate = new MassiveNumber();
        public MassiveNumber unlockScore = new MassiveNumber();
        public bool isLocked;

        public Agent(double argPointsRate, double argInitialPriceVal, int argInitialPriceEch, double argPriceFactor)
        {
            // Initialize variables

            // Change these for testing:
            count.value = 0;
            count.echelon = 1;

            // Progression scaling:
            pointsRate.value = argPointsRate;
            initPrice.value = argInitialPriceVal;
            initPrice.echelon = argInitialPriceEch;

            priceFactor = argPriceFactor;
            UpdatePrice();

            // Bank score to unlock:
            unlockScore = initPrice;
            unlockScore.value = unlockScore.Mult(0.745, unlockScore.echelon);    // Slightly under 3/4 of initial price.
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
