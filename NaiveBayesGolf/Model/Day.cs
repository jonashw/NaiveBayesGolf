namespace NaiveBayesGolf.Model
{
    public class Day
    {
        public readonly string Outlook;
        public readonly string Temp;
        public readonly string Humidity;
        public readonly bool Windy;

        public Day(Outlook outlook, Temp temp, Humidity humidity, bool windy)
        {
            Outlook = outlook.ToString();
            Temp = temp.ToString();
            Humidity = humidity.ToString();
            Windy = windy;
        }
    }
}