namespace Source
{
    public class Item
    {
        public MassiveNumber initPrice = new MassiveNumber();   // Arbitrary price to scale by priceFactor.
        public double priceFactor { get; set; }                 // Arbitrary scaling of initPrice.
        public MassiveNumber count = new MassiveNumber();
        public MassiveNumber price = new MassiveNumber();

        public void UpdatePrice()
        {
            MassiveNumber tempNum = new MassiveNumber();

            tempNum.value = initPrice.value;
            tempNum.echelon = initPrice.echelon;

            if (count.value > 0)
            {
                tempNum.value = tempNum.Mult(count.value + 1, tempNum.echelon);
                tempNum.UpdateEchelon();

                tempNum = tempNum.Pow(priceFactor);
            }

            price = tempNum;
            tempNum = null;
        }

        public MassiveNumber GetPrice()
        {
            return price;
        }
    }
}
