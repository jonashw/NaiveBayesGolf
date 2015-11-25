namespace NaiveBayesGolf.Model
{
    public class PlayPrediction
    {
        public readonly bool Play;
        public readonly double Likelihood;
        public readonly double Probability;

        public PlayPrediction(bool play, double likelihood, double probability)
        {
            Play = play;
            Likelihood = likelihood;
            Probability = probability;
        }
    }
}