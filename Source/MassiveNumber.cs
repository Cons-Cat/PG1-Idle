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

        public void UpdateEchelon()
        {
            double tempValue = value;

            // If the one-thousands place is not empty:
            if ((uint)tempValue.ToString().Length >= 4)
            {
                // Decrement the decimal place.
                tempValue /= Math.Pow(1000, ((uint)value.ToString().Length - 1) % 3);

                // Increment the echelon
                echelon += ((uint)value.ToString().Length - 1) % 3;

                // Update value
                value = tempValue;
            }

            // If the ones place is empty:
            if ((uint)tempValue.ToString().Length == 0)
            {
                // UNFINISHED
            }
        }

        #endregion

        // Abbreviated string for Render.cs
        public string GetAbbreviation()
        {
            string returnStr;

            switch (echelon)
            {
                case 1:
                    returnStr = "!";
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
            return value + (argDouble * Math.Pow(10, argEchelon - this.echelon));
        }

        // Subtraction operation:
        public double Sub(double argDouble, uint argEchelon)
        {
            return value - (argDouble * Math.Pow(10, argEchelon - this.echelon));
        }

        // Multiplication operation:
        public double Mult(double argDouble, uint argEchelon)
        {
            return value * (argDouble * Math.Pow(10, argEchelon - this.echelon));
        }
        public double Div(double argDouble, uint argEchelon)
        {
            return value / (argDouble * Math.Pow(10, argEchelon - this.echelon));
        }

        // Exponentiation operation:
        public double Pow(double argExponent)
        {
            return Math.Pow(value, argExponent);
        }

        #endregion
    }
}
