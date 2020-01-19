using System;

namespace Source
{
    public class MassiveNumber
    {
        // Instantiate values
        public uint echelon { get; set; }
        public double value { get; set; }

        // Constructor
        public MassiveNumber()
        {
            // Initialize values
            echelon = 1;
            value = 0;
        }

        // Updating the echelon
        #region

        public void IncreaseEchelon()
        {
            double tempValue = value;
            uint increments = 0;

            // While the ones place is empty:
            while (tempValue % 1 == 0)
            {
                // Increment the decimal place.
                tempValue *= 10;
                increments++;
            }

            // Increment the echelon every three ticks.
            echelon += increments / 3;
        }

        public void DecreaseEchelon()
        {
            double tempValue = value;
            uint increments = 0;

            // While the ones place is empty:
            while (tempValue.ToString().Length > 3)
            {
                // Decrement the decimal place.
                tempValue /= 10;

                // Decrement the echelon every three ticks.
                increments++;
            }

            // Decrement the echelon every three ticks.
            echelon -= increments / 3;
        }

        #endregion

        // Abbreviated string for Render.cs
        public string GetAbbreviation()
        {
            string returnStr;

            switch (echelon)
            {
                case 1:
                    returnStr = "";
                    break;

                case 2:
                    // Abbreviate thousands.
                    returnStr = " K";
                    break;

                case 3:
                    // Abbreviate millions.
                    returnStr = " M";
                    break;

                case 4:
                    // Abbreviate billions.
                    returnStr = " B";
                    break;

                case 5:
                    // Abbreviate trillions.
                    returnStr = " T";
                    break;

                case 6:
                    // Abbreviate quadrillions.
                    returnStr = " Q";
                    break;

                default:
                    // Generic abbreviation from quintillion onwards.
                    returnStr = " e" + (echelon * 3).ToString();
                    break;
            }

            return value.ToString() + returnStr;
        }

        // Math operations:
        #region

        // Addition operation:
        public double Add(double argDouble, uint argEchelon)
        {
            return value + (argDouble * 10 * (argEchelon - this.echelon));
        }

        // Subtraction operation:
        public double Sub(double argDouble, uint argEchelon)
        {
            return value - (argDouble * 10 * (argEchelon - this.echelon));
        }

        // Multiplication operation:
        public double Mult(double argDouble, uint argEchelon)
        {
            return value * (argDouble * 10 * (argEchelon - this.echelon));
        }
        public double Div(double argDouble, uint argEchelon)
        {
            return value / (argDouble * 10 * (argEchelon - this.echelon));
        }

        // Exponentiation operation:
        public double Pow(double argExponent)
        {
            return Math.Pow(value, argExponent);
        }

        #endregion
    }
}
