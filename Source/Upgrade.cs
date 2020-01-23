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
        public double incomeMultiplier { get; set; }            // Arbitrary multiplier of agent income.

        public Upgrade(double argPointsMult, double argInitialPrice, double argPriceFactor)
        {
            // Initialize variables
            incomeMultiplier = argPointsMult;
            initPrice.value = argInitialPrice;
            priceFactor = argPriceFactor;

            UpdatePrice();
        }
    }
}
