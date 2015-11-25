namespace NaiveBayesGolf.Model
{
    public class GolfDay
    {
        public readonly bool Play;
        public readonly Day Attributes;

        public GolfDay(bool play, Day attributes)
        {
            Play = play;
            Attributes = attributes;
        }
    }
}
