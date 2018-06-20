using System;
using System.Collections.Generic;
namespace IntToFlo
{
    //Megan Owens assignment 2: programming languages
    class Program
    {
        static void Main(string[] args)
        {
            //variables to be used later
            Boolean wholeNumberIsZero;
            String integerBinaryString = "";
            String decimalBinaryString = ".0";
            String total = "";
            String mantissa = "";
            int isNegative;
            String signBit;
            double input = 0;

            //Prompts the user to input a number
            Console.Write("Enter decimal number:");
            //reads input
            try
            {
                input = double.Parse(Console.ReadLine());
            }
            catch (System.FormatException e)
            {
                Console.WriteLine("This is not the correct format, or is NaN. Please restart and input a single number with no spaces in between.");
            }
            //find non special values binary code.
            if(input !=0){
                //sets the sign bit
            if (input < 0)
            {
                isNegative = -1;
                signBit = "1";
            }
            else
            {
                isNegative = 1;
                signBit = "0";
            }
                //checks to see if the string fits the format 0.XX
            if (input < 1 && input > 0)
                wholeNumberIsZero = true;
            else
                wholeNumberIsZero = false;
                
            //casts number to int to get the whole number    
            int wholeNumber = (int)input;

                // gets the binary string of the whole number
            integerBinaryString = intToBinary(wholeNumber * isNegative);
                //gets the binary string of the decimal part
            getDecimal(23);
                //gets the mantissa and strings everything together
            findMantissa(127);
            Console.WriteLine("\nIEEE 754 single precision is:\n" + total);

                //clears values
            integerBinaryString = "";

            decimalBinaryString = ".0";
            total = "";
            mantissa = "";

            integerBinaryString = intToBinary(wholeNumber * isNegative);
            getDecimal(52);
            findMantissa(1023);
                Console.WriteLine("\nIEEE 754 double precision is:\n" + total);
        }
            //prints correct response in case of denormalized value 0.
            if(input ==0)
            {
                Console.WriteLine("IEEE 754 single precision is:\n00000000000000000000000000000000");
                Console.WriteLine("\nIEEE 754 double precision is:\n0000000000000000000000000000000000000000000000000000000000000000");
            }
          




            void findMantissa(int expBias)
            {
               
                String exponentField;
                String mantissaTemp = "";
                String mantissaTemp2="";
                //cuts off the first item
               
                if (integerBinaryString.Length != 0)
                {
                    //we can cut off the first number in the case of X.XX because it will always be a 1
                    mantissaTemp = integerBinaryString.Substring(1);
                    mantissaTemp2 = decimalBinaryString;
                    // finds exponent value
                    int expVal = (integerBinaryString.Length - 1) + expBias;
                    //calculates correct exponent length and calls calculateExponent to get the exponent string  
                    if (expBias == 127)
                        exponentField = calculateExponent(expVal, 8);
                    else
                        exponentField = calculateExponent(expVal, 11);
                        
                }
                //if the number is in the format 0.XX we need to find a negative exponent (move backward through the string)
                else
                {
                    //finds the first one in the decimal string
                    int indexOfFirstOne = decimalBinaryString.IndexOf("1");
                    //substring for mantissa
                    mantissaTemp2 = decimalBinaryString.Substring(indexOfFirstOne+1);
                    //finds correct exponent value
                    int expVal = ((indexOfFirstOne+1) *( - 1)) + expBias;
                    if (expBias == 127)
                        exponentField = calculateExponent(expVal, 8);
                    else
                        exponentField = calculateExponent(expVal, 11);
                }
                //strings the mantissa together
                mantissa = mantissaTemp + mantissaTemp2;
               

                //sets the final string to be displayed
                total = signBit + exponentField + mantissa;
                
            }

           //gets decimal binary string 
            void getDecimal(int matissaSize)
            {
                
                String helper = input.ToString();
                int Size;
                //if there is a decimal part create a substring with just the decimal part
                if (helper.Contains("."))
                {
                    int index = helper.IndexOf(".");

                    decimalBinaryString = helper.Substring(index);
                }
                //if the number is not in the format 0.XX then the size is 1- the length (since we will cut off the first 1)
                if (integerBinaryString.Length != 0)
                    Size = integerBinaryString.Length - 1;
                else
                    Size = 0;



                String hel = "";
                double tempDouble = double.Parse(decimalBinaryString);
                Boolean isFirstOne = true;

                //for loop that runs until we hit the mantissa size (23 or 52 respectively) minus the part of the integerbinarystring that will be added to the mantissa
                for (int j = 1; j <= matissaSize-Size;j++)
                {
                    tempDouble = tempDouble * 2;
                    //if the number is 0.XX
                    if(tempDouble <1)
                    {
                        hel = hel + "0";

                    }
                    //if the number is 1.XX
                    else
                    {
                        //checks to see if the number is in the format 0.XX and if the number is the first one
                        if (wholeNumberIsZero && isFirstOne)
                        {
                            //j must go down in value because all the 0s prior to this one will be cut off, and in order to have the correct length mantissa we must handle for that 
                            j = j - (hel.Length+1);
                            isFirstOne = false;
                        }
                        hel = hel + "1";
                        tempDouble = tempDouble - 1;

                    }
                }
                decimalBinaryString = hel;


            }
            //gives the binary string for a number 
             String intToBinary(int i)
            {
                String tmpStr ="";
                if (i != 0)
                {
                    //stops when i hits 0 or below
                    while (i > 0)
                    {
                        int temp = i % 2;
                        tmpStr = temp + tmpStr;
                        i = i / 2;

                    }
                }
                return tmpStr;
            }
            //calculates the exponent value
            String calculateExponent(int i,int lim)
            {
                String tmpStr = "";
                int counter = 0;
                    //stops when either 8 or 11 is hit
                    while (counter < lim) 
                    {
                        int temp = i % 2;
                        tmpStr = temp + tmpStr;
                        i = i / 2;
                        counter++;

                    }

                return tmpStr;
            }

           


        }
    }
}
