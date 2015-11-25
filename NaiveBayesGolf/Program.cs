using System;
using System.Collections.Generic;
using System.Linq;
using NaiveBayesGolf.Model;

namespace NaiveBayesGolf
{
    public class Program
    {
        /* Implementation of Naive Bayes for the Golf/Weather dataset.
         * Principles taken from https://www.youtube.com/watch?v=IlVINQDk4o8
         * NOTE: The fellow in the video mis-counts "Sunny" (he counts 2 Yes, 3 No instead of 3 Yes, 2 No), 
         * leading to incorrect probabilities (he calculates 2/9 Yes, 3/5 No rather than 3/9 Yes, 2/5 No) and likelihoods.
         */
        public static void Main(string[] args)
        {
            var dataSet = getTrainingDataSet();
            Console.WriteLine("Raw DataSet:");
            Writer.DataSetTable(dataSet);
            Console.WriteLine();
            var summary = new GolfDayPredictor(dataSet);
            Console.WriteLine("Naive Bayes has been applied to the DataSet above, which links weather conditions to a decision to play golf or not.");
            Console.WriteLine();
            Console.WriteLine("Next, we are given a description of weather conditions on an arbitrary day: ");
            var novelGolfDay = new Day(Outlook.Sunny, Temp.Cool, Humidity.High, true);
            var prediction = summary.Predict(novelGolfDay);
            Writer.DataSetRow(novelGolfDay);
            Console.WriteLine();
            Console.WriteLine("The Naive Bayes learner predicts:");
            Console.WriteLine();
            Console.Write("\t");
            Writer.Prediction(prediction);
            Console.WriteLine();
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }

        private static IReadOnlyList<GolfDay> getTrainingDataSet()
        {
            return new[]
            {
                new GolfDay(false, new Day(Outlook.Rainy,    Temp.Hot,  Humidity.High,   false)), 
                new GolfDay(false, new Day(Outlook.Rainy,    Temp.Hot,  Humidity.High,   true)), 
                new GolfDay(true,  new Day(Outlook.Overcast, Temp.Hot,  Humidity.High,   false)), 
                new GolfDay(true,  new Day(Outlook.Sunny,    Temp.Mild, Humidity.High,   false)), 
                new GolfDay(true,  new Day(Outlook.Sunny,    Temp.Cool, Humidity.Normal, false)), 
                new GolfDay(false, new Day(Outlook.Sunny,    Temp.Cool, Humidity.Normal, true)), 
                new GolfDay(true,  new Day(Outlook.Overcast, Temp.Cool, Humidity.Normal, true)), 
                new GolfDay(false, new Day(Outlook.Rainy,    Temp.Mild, Humidity.High,   false)), 
                new GolfDay(true,  new Day(Outlook.Rainy,    Temp.Cool, Humidity.Normal, false)), 
                new GolfDay(true,  new Day(Outlook.Sunny,    Temp.Mild, Humidity.Normal, false)), 
                new GolfDay(true,  new Day(Outlook.Rainy,    Temp.Mild, Humidity.Normal, true)), 
                new GolfDay(true,  new Day(Outlook.Overcast, Temp.Mild, Humidity.High,   true)), 
                new GolfDay(true,  new Day(Outlook.Overcast, Temp.Hot,  Humidity.Normal, false)), 
                new GolfDay(false, new Day(Outlook.Sunny,    Temp.Mild, Humidity.High,   true))
            }.ToList().AsReadOnly();
        }
    }
}