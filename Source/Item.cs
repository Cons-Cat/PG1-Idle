using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Source
{
    public class Item
    {
        public double initPrice { get; set; }                   // Arbitrary price to scale by priceFactor.
        public double priceFactor { get; set; }                 // Arbitrary scaling of initPrice.
        public MassiveNumber count = new MassiveNumber();

        // Constructor
        public Item()
        {
            count.value = 0;
        }

        public MassiveNumber GetPrice()
        {
            MassiveNumber tempNum = new MassiveNumber();

            tempNum.value = initPrice;
            tempNum.UpdateEchelon();

            // Do not scale the first agent to purchase.
            if (count.value > 0)
            {
                tempNum.value = tempNum.Mult(count.value + (1d / (count.echelon * 1000)), count.echelon);
                tempNum.UpdateEchelon();

                tempNum = tempNum.Pow(priceFactor);
            }

            return tempNum;
        }
    }
}
