﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiDemo.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values/5
        [HttpPost, ActionName("Get")]
        public string GetWordForNumber(string number)
        {
            string numberToConvert = number;

            string isNegative = "";
            try
            {
                number = Convert.ToDouble(number).ToString();

                if (number.Contains("-"))
                {
                    isNegative = "Minus ";
                    number = number.Substring(1, number.Length - 1);
                }
                if (number == "0")
                {
                    return "Zero";
                }
                else
                {
                    return isNegative.ToString() + FinalConvertAndConbineToWords(number).ToString();
                }
            }
            catch (Exception ex)
            {

                return "Error" + ex.ToString();
            }

        }

        private static String SingleDigitNumbers(String Number)
        {
            int _Number = Convert.ToInt32(Number);
            String name = "";
            switch (_Number)
            {

                case 1:
                    name = "One";
                    break;
                case 2:
                    name = "Two";
                    break;
                case 3:
                    name = "Three";
                    break;
                case 4:
                    name = "Four";
                    break;
                case 5:
                    name = "Five";
                    break;
                case 6:
                    name = "Six";
                    break;
                case 7:
                    name = "Seven";
                    break;
                case 8:
                    name = "Eight";
                    break;
                case 9:
                    name = "Nine";
                    break;
            }
            return name;
        }

        private static String DoubleDigitNumbers(String Number)
        {
            int _Number = Convert.ToInt32(Number);
            String name = null;
            switch (_Number)
            {
                case 10:
                    name = "Ten";
                    break;
                case 11:
                    name = "Eleven";
                    break;
                case 12:
                    name = "Twelve";
                    break;
                case 13:
                    name = "Thirteen";
                    break;
                case 14:
                    name = "Fourteen";
                    break;
                case 15:
                    name = "Fifteen";
                    break;
                case 16:
                    name = "Sixteen";
                    break;
                case 17:
                    name = "Seventeen";
                    break;
                case 18:
                    name = "Eighteen";
                    break;
                case 19:
                    name = "Nineteen";
                    break;
                case 20:
                    name = "Twenty";
                    break;
                case 30:
                    name = "Thirty";
                    break;
                case 40:
                    name = "Fourty";
                    break;
                case 50:
                    name = "Fifty";
                    break;
                case 60:
                    name = "Sixty";
                    break;
                case 70:
                    name = "Seventy";
                    break;
                case 80:
                    name = "Eighty";
                    break;
                case 90:
                    name = "Ninety";
                    break;
                default:
                    if (_Number > 0)
                    {
                        name = DoubleDigitNumbers(Number.Substring(0, 1) + "0") + " " + SingleDigitNumbers(Number.Substring(1));
                    }
                    break;
            }
            return name;
        }

        private static String ConvertWholeNumberToWords(String Number)
        {
            string word = "";
            try
            {
                bool beginsZero = false;//tests for 0XX  
                bool isDone = false;//test if already translated  
                double dblAmt = (Convert.ToDouble(Number));
                //if ((dblAmt > 0) && number.StartsWith("0"))  
                if (dblAmt > 0)
                {//test for zero or digit zero in a nuemric  
                    beginsZero = Number.StartsWith("0");

                    int numDigits = Number.Length;
                    int pos = 0;//store digit grouping  
                    String place = "";//digit grouping name:hundres,thousand,etc...  
                    switch (numDigits)
                    {
                        case 1://ones' range  

                            word = SingleDigitNumbers(Number);
                            isDone = true;
                            break;
                        case 2://tens' range  
                            word = DoubleDigitNumbers(Number);
                            isDone = true;
                            break;
                        case 3://hundreds' range  
                            pos = (numDigits % 3) + 1;
                            if (dblAmt == 100){place = " Hundred ";}else {place = " Hundred And ";}
                            break;
                        case 4://thousands' range  
                        case 5:
                        case 6:
                            pos = (numDigits % 4) + 1;
                            place = " Thousand ";
                            break;
                        case 7://millions' range  
                        case 8:
                        case 9:
                            pos = (numDigits % 7) + 1;
                            place = " Million ";
                            break;
                        case 10://Billions's range  
                        case 11:
                        case 12:

                            pos = (numDigits % 10) + 1;
                            place = " Billion ";
                            break;
                        //add extra case options for anything above Billion...  
                        default:
                            isDone = true;
                            break;
                    }
                    if (!isDone)
                    {//if transalation is not done, continue...(Recursion comes in now!!)  
                        if (Number.Substring(0, pos) != "0" && Number.Substring(pos) != "0")
                        {
                            try
                            {
                                word = ConvertWholeNumberToWords(Number.Substring(0, pos)) + place + ConvertWholeNumberToWords(Number.Substring(pos));
                            }
                            catch { }
                        }
                        else
                        {
                            word = ConvertWholeNumberToWords(Number.Substring(0, pos)) + ConvertWholeNumberToWords(Number.Substring(pos));
                        }

                        //check for trailing zeros  
                        //if (beginsZero) word = " and " + word.Trim();  
                    }
                    //ignore digit grouping names  
                    if (word.Trim().Equals(place.Trim())) word = "";
                }
            }
            catch { }
            return word.Trim();
        }

        private static String ConvertDecimalsPartToWords(String number)
        {
            String cd = "", digit = "", engOne = "";
            //for (int i = 0; i < number.Length; i++)
            //{
            digit = number; //number[i].ToString();
                if (digit.Equals("0"))
                {
                    engOne = "Zero";
                }
                else
                {
                    engOne = DoubleDigitNumbers(number);
                }
                cd += " " + engOne;
           // }
            return cd;
        }

        private static String FinalConvertAndConbineToWords(String numb)
        {
            String val = "", wholeNo = numb, points = "", andStr = "", pointStr = "", unit = " Dollors ";
            String endStr = "";
            try
            {
                int decimalPlace = numb.IndexOf(".");
                if (decimalPlace > 0)
                {
                    wholeNo = numb.Substring(0, decimalPlace);
                    points = numb.Substring(decimalPlace + 1);
                    if (Convert.ToInt32(points) > 0)
                    {
                        andStr = "and";// Separate whole numbers from points part    
                        endStr = " Cents ";
                        pointStr = ConvertDecimalsPartToWords(points);
                    }
                }
                val = String.Format("{0} {1}{2} {3}{4}", ConvertWholeNumberToWords(wholeNo).Trim(), unit, andStr, pointStr, endStr);
            }
            catch { }
            return val;
        }




    }
}
