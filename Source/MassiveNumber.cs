using System;
using System.Diagnostics;

namespace Source
{
    public class MassiveNumber
    {
        // Instantiate values
        public int echelon { get; set; }
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
            double decimalShift = 0;
            string evalString = ((uint)value).ToString();
            double power = 0;

            // If the one-thousands place is not empty:
            if (evalString.Length >= 4)
            {
                power = (evalString.Length - 1) / 3;

                // Decrement the decimal place in multiples of 3.
                decimalShift = Math.Pow(1000, power);
                value /= decimalShift;

                // Increment the echelon by 1 for each shift of 3 in the decimal place.
                echelon += (int)(power % 3);
            }

            // If the ones place is empty:
            while (evalString[0] == '0' && evalString.Length == 1)
            {
                power += 1;

                // Increment the decimal place in multiples of 3.
                if (power == 3)
                {
                    power = 0;
                    value *= 1000;
                    echelon--;
                }

                // Update evalString for loop condition.
                evalString = ((uint)value).ToString();
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
                    // Generic abbreviation for quintillion onwards.
                    returnStr = " e" + (echelon * 3).ToString();
                    break;
            }

            // Do not show decimal places on the first echelon.
            if (echelon == 1)
            {
                returnStr = ((uint)value).ToString() + returnStr;
            }
            else
            {
                returnStr = (Math.Truncate(value * 1000) / 1000).ToString() + returnStr;
            }

            return returnStr;
        }

        // Math operations:
        #region

        // Addition operation:
        public double Add(double argDouble, int argEchelon)
        {
            return value + (argDouble * Math.Pow(1000, argEchelon - this.echelon));
        }

        // Subtraction operation:
        public double Sub(double argDouble, int argEchelon)
        {
            return value - (argDouble * Math.Pow(1000, argEchelon - this.echelon));
        }

        // Multiplication operation:
        public double Mult(double argDouble, int argEchelon)
        {
            return value * (argDouble * Math.Pow(1000, argEchelon - this.echelon));
        }
        public double Div(double argDouble, int argEchelon)
        {
            return value / (argDouble * Math.Pow(1000, argEchelon - this.echelon));
        }

        // Exponentiation operation:
        public double Pow(double argExponent)
        {
            return Math.Pow(value, argExponent);
        }

        // Relationship operation:
        public bool IsGreater(MassiveNumber argNum)
        {
            bool returnBool;

            if ((this.echelon > argNum.echelon) || (this.echelon == argNum.echelon && this.value >= argNum.value))
            {
                returnBool = true;
            }
            else
            {
                returnBool = false;
            }

            return returnBool;
        }

        #endregion
    }
}
