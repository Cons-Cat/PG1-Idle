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
            string evalString = ((uint)value).ToString();
            double power = 0;

            // If the one-thousands place is not empty:
            if (evalString.Length >= 4)
            {
                //double decimalShift = 0;
                power = (evalString.Length - 1) / 3;

                // Decrement the decimal place in multiples of 3.
                value /= Math.Pow(1000, power);

                // Increment the echelon by 1 for each shift of 3 in the decimal place.
                echelon += (int)(power % 3);
            }

            // If the ones place is empty AND the MassiveNumber is not equivalent to 0:
            while (evalString[0] == '0' && evalString.Length == 1 && value != 0)
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

                case 0:
                    // Erratic case when value > 0 && value < 1.
                    returnStr = "";
                    value = 0;
                    break;

                default:
                    // Generic abbreviation for quintillion onwards.
                    returnStr = " e" + (echelon * 3).ToString();

                    break;
            }

            // Do not show decimal places on the first echelon.
            if (returnStr == "") // echelon = 0 OR echelon = 1
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
            double returnVal = value - (argDouble * Math.Pow(1000, argEchelon - this.echelon));

            if (returnVal < 0)
            {
                returnVal = 0;
            }

            return returnVal;
        }

        // Multiplication operation:
        public double Mult(double argDouble, double argEchelon)
        {
            return value * (argDouble * Math.Pow(1000, argEchelon - this.echelon));
        }

        // Division operation:
        public double Div(double argDouble, int argEchelon)
        {
            return value / (argDouble * Math.Pow(1000, argEchelon - this.echelon));
        }

        // Exponentiation operation:
        public MassiveNumber Pow(double argExponent)
        {
            // Instantiate and initialize variables.
            MassiveNumber returnNumber = new MassiveNumber();
            returnNumber.value = 1;
            returnNumber.echelon = 1;

            MassiveNumber fractionNumber = new MassiveNumber();
            fractionNumber.value = 1;
            fractionNumber.echelon = 1;

            double fractionExpo = argExponent % 1d;

            // If the exponent is not an integer.
            if (fractionExpo > 0)
            {
                fractionNumber.value = this.value;
                fractionNumber.echelon = this.echelon;

                // (x*1,000) ^ y = (x)^y * (10^3)^y
                fractionNumber.value = Math.Pow(fractionNumber.value, fractionExpo);
                fractionNumber.UpdateEchelon();

                for (int i = 0; i < fractionNumber.echelon + 1; i++)
                {
                    fractionNumber.value *= Math.Pow(10, fractionExpo);
                    fractionNumber.UpdateEchelon();
                }
            }

            returnNumber.value = this.value;
            returnNumber.echelon = this.echelon;

            // x^4 = ( x*x*x*x )
            if ((uint)argExponent > 1)
            {
                for (uint i = 1; i < (uint)argExponent; i++)
                {
                    returnNumber.value = returnNumber.Mult(this.value, this.echelon);
                    returnNumber.UpdateEchelon();

                    if (i < (uint)argExponent - 1)
                    {
                        returnNumber.echelon *= returnNumber.echelon;
                    }
                }
            }
            else
            {
                if ((uint)argExponent > 1)
                {
                    // Mult at least once
                    returnNumber.value = returnNumber.Mult(returnNumber.value, 1);
                    returnNumber.UpdateEchelon();
                }
            }

            // Mult whole exponentiated value to fractional exponentiated value.
            // This method is equivalent to:
            // x^4.5 = ( x*x*x*x ) * ( x^0.5 )

            if (argExponent >= 1)
            {
                returnNumber.value = returnNumber.Mult(fractionNumber.value, fractionNumber.echelon);
            }
            else
            {
                returnNumber.value = fractionNumber.value;
            }

            returnNumber.UpdateEchelon();

            return returnNumber;
        }

        // Relationship operation:
        public bool IsGreaterThan(MassiveNumber argNum)
        {
            bool returnBool;

            if ((this.echelon > argNum.echelon) || (this.echelon == argNum.echelon && this.value >= argNum.value) || (this.echelon == 1 && argNum.echelon == 1 && (uint)this.value == (uint)argNum.value))
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
