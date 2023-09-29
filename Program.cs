using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;

namespace Question1
{
    internal class Program
    {
        static string GenerateBarcode(string validExpireDate, string validExpireHour)
        {
            string chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            Random random = new Random();
            char[] barcodeArray = new char[15];
   
            for (int i = 0; i < 5; i++)
            {
                barcodeArray[i] = chars[random.Next(0, chars.Length)];
            }
            
            for (int i = 0; i < 8; i++)
            {
                barcodeArray[i + 5] = validExpireDate[i];
            }

            barcodeArray[13] = validExpireHour[0];
            barcodeArray[14] = validExpireHour[1];

            return new string(barcodeArray);

        }
        static string ShowMessage(string barcodeData) {

            string message = "";
            string systemTimeString = "";
            string barcodeString = barcodeData.ToString();
            string barcodeDateString = barcodeData.Substring(5);
            string[] productStatus = { "商品即將到期", "商品已過期" };

            DateTime now = DateTime.Now;
            DateTime barcodeDateTime;
            double durationMinute = 0;
            
            if (DateTime.TryParseExact(barcodeDateString, "yyyyMMddHH", CultureInfo.InvariantCulture, DateTimeStyles.None, out barcodeDateTime))
            {
                durationMinute = barcodeDateTime.Subtract(now).TotalMinutes;
            }

            

            systemTimeString = "系統時間: " + now.ToString("yyyy/MM/dd HH:mm");

            message = (systemTimeString.ToString()+ "\n" + barcodeString);

            if (5 < durationMinute && durationMinute < 60)
            {
                Console.Write(message + "        " + productStatus[0]);
            }
            else if (durationMinute < 5)
            {
                Console.Write(message + "        " + productStatus[1]);
            }
            else {
                Console.Write(message);
            }
                return message;
        }

        public static void Main(string[] args)
        {
            string inputExpireDate="";
            string inputExpireHour="";
            string validExpireDate="";
            string validExpireHour = "";

            Regex rgxExpireDate = new Regex(@"^(?:19|20)\d\d(?:0[1-9]|1[0-2])(?:0[1-9]|[12][0-9]|3[01])$");
            

            while (string.IsNullOrEmpty(validExpireDate) || !rgxExpireDate.IsMatch(validExpireDate))
            {
                Console.WriteLine("輸入商品保存期限( YYYYMMDD )");
                inputExpireDate = Console.ReadLine();

                if (rgxExpireDate.IsMatch(inputExpireDate))
                {
                    validExpireDate = inputExpireDate;
                }
                else
                {
                    Console.WriteLine("格式錯誤  須為YYYYMMDD ");
                    Console.WriteLine("---------------------------------------------------");
                }
            }

            Regex rgxExpireHour = new Regex(@"^(?:[0-1][0-9]|2[0-3])$");
            
            while (string.IsNullOrEmpty(validExpireHour) || !rgxExpireHour.IsMatch(validExpireHour))
            {
                Console.WriteLine("輸入商品保存時間( HH )");
                inputExpireHour = Console.ReadLine();

                if (rgxExpireHour.IsMatch(inputExpireHour))
                {
                    validExpireHour = inputExpireHour;

                    break;
                }
                else
                {
                    Console.WriteLine("格式錯誤  須為HH ");
                    Console.WriteLine("---------------------------------------------------");
                }
            }

            if (rgxExprieDate.IsMatch(inputExpireDate) && rgxExpireHour.IsMatch(inputExpireHour))
            {
                string barcode = GenerateBarcode(validExpireDate, validExpireHour);
                Console.WriteLine("15碼條碼:  " + barcode);
                Console.WriteLine("---------------------------------------------------" );
                ShowMessage(barcode);
            }
         

            Console.ReadKey();

        }
    }
}
