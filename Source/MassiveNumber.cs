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
            double tempValue = value;
            double decimalShift = 0;

            // If the one-thousands place is not empty:
            if (((uint)value).ToString().Length >= 4)
            {
                // Decrement the decimal place in multiples of 3.
                decimalShift = Math.Pow(1000, (((uint)value).ToString().Length) % 3);
                tempValue /= decimalShift;

                // Increment the echelon by 1 for each shift of 3 in the decimal place.
                echelon += (int)decimalShift % 3;

                // Update value with 6 decimal precision
                //value = Math.Truncate(tempValue * 600) / 600;
                value = tempValue;
            }

            // If the ones place is empty:
            if (((uint)tempValue).ToString().Length == 0)
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

            return ((uint)value).ToString() + returnStr;
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
                /*Debug.WriteLine(this.echelon);
                Debug.WriteLine(argNum.echelon);
                Debug.WriteLine("");*/
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
