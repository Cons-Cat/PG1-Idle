using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Source
{
    class Upgrade : Item
    {
        // Arbitrary multiplier of agent income.
        public double incomeMultiplier { get; set; }

        public Upgrade(double argPointsMult, double argInitialPriceVal, int argInitialPriceEch, double argPriceFactor)
        {
            // Initialize variables
            incomeMultiplier = argPointsMult;
            initPrice.value = argInitialPriceVal;
            initPrice.echelon = argInitialPriceEch;

            priceFactor = argPriceFactor;

            UpdatePrice();
        }
    }
}
