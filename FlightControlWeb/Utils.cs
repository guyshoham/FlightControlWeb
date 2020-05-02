using System;

namespace FlightControlWeb
{
    public class Utils
    {
        /// <summary>
        /// ID structure: X000-000
        /// X represents letter [A-Z]
        /// 0 represents a digit [0-9]
        /// </summary>
        /// <returns>ID string</returns>
        public static string IDGenerator()
        {
            Random rand = new Random();
            int letNum = rand.Next(0, 26); // [0-25]
            char letter = (char)('A' + letNum); // random letter [A-Z]

            string dig1 = (rand.Next(0, 10)).ToString(); // random digit [0-9]
            string dig2 = (rand.Next(0, 10)).ToString(); // random digit [0-9]
            string dig3 = (rand.Next(0, 10)).ToString(); // random digit [0-9]
            string dig4 = (rand.Next(0, 10)).ToString(); // random digit [0-9]
            string dig5 = (rand.Next(0, 10)).ToString(); // random digit [0-9]
            string dig6 = (rand.Next(0, 10)).ToString(); // random digit [0-9]

            return letter + dig1 + dig2 + dig3 + '-' + dig4 + dig5 + dig6;
        }
    }
}
