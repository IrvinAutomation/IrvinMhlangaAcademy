using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace LAWTrust_APIAutomationTest
{
    public class TestDataGenerator
    {
        Random random = new Random();

        public string EmailAddress(int numberOfCharacters) // pass the number of characters for your email to be generated before '@'
        {
            var characters = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789";
            random = new Random();
            var EmailAddress = new string(Enumerable.Repeat(characters, numberOfCharacters).Select(s => s[random.Next(s.Length)]).ToArray());
            EmailAddress = EmailAddress + "@TCoETestAutomation.com";

            return EmailAddress;
        }

        public string TestNames(int numberOfCharacters)
        {
            string TestNames = null;
            var characters = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz";
            random = new Random();
            TestNames = new string(Enumerable.Repeat(characters, numberOfCharacters).Select(s => s[random.Next(s.Length)]).ToArray());
            return TestNames;

        }

        public string GenerateRandomNo()
        {
            int _min = 1000;
            int _max = 9999;

            return random.Next(_min, _max).ToString();
        }
        public string GenerateIDNumber()
        {
            StringBuilder sbIDNumber = new StringBuilder();
            Random rnd = new Random();


            // Date Of Birth
            int Year = rnd.Next(1900, DateTime.Now.Year);
            int Month = rnd.Next(1, 12);
            int Day = rnd.Next(1, DateTime.DaysInMonth(Year, Month));


            DateTime dt = new DateTime(Year, Month, Day);
            sbIDNumber.Append(dt.ToString("yyMMdd", CultureInfo.InvariantCulture));


            // Gender
            int MF = rnd.Next(0, 9);
            sbIDNumber.Append($"{MF}");


            // Sequence
            int iSequence = rnd.Next(0, 999);
            string sequence = iSequence < 100 ? iSequence < 10 ? $"00{iSequence}" : $"0{iSequence}" : $"{iSequence}";
            sbIDNumber.Append(sequence);


            // Citizenship
            sbIDNumber.Append($"0");


            // A
            sbIDNumber.Append($"8");


            // Control Digit
            int cd = -1;
            try
            {
                int a = 0;
                int b = 0;
                int c = 0;
                for (int i = 0; i < 6; i++)
                {
                    a += int.Parse(sbIDNumber.ToString()[2 * i].ToString());
                    b = b * 10 + int.Parse(sbIDNumber.ToString()[2 * i + 1].ToString());
                }
                b *= 2;


                do
                {
                    c += b % 10;
                    b = b / 10;
                }
                while (b > 0);


                c += a;
                cd = 10 - (c % 10);


                if (cd == 10)
                {
                    cd = 0;
                }
            }
            catch
            {
            }
            sbIDNumber.Append($"{cd}");

            return sbIDNumber.ToString();
        }

    }
}
