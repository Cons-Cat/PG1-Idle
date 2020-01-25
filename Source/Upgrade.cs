namespace Source
{
    class Upgrade : Item
    {
        // Arbitrary multiplier of agent income.
        public double incomeMultiplier { get; set; }
        public uint maxCount;

        public Upgrade(double argPointsMult, double argInitialPriceVal, int argInitialPriceEch, double argPriceFactor)
        {
            // Initialize variables
            incomeMultiplier = argPointsMult;
            maxCount = 2;

            initPrice.value = argInitialPriceVal;
            initPrice.echelon = argInitialPriceEch;

            priceFactor = argPriceFactor;

            UpdatePrice();
        }
    }
}
