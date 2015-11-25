using System;
using System.Collections.Generic;
using System.Linq;
using NaiveBayesGolf.Model;

namespace NaiveBayesGolf
{
    public static class Writer
    {
        const int Padding = 1;
        const int OutlookWidth = 8 + Padding + Padding;
        const int TempWidth = 4 + Padding + Padding;
        const int HumidityWidth = 8 + Padding + Padding;
        const int BoolWidth = 7 + Padding + Padding;
        const int TotalWidth = OutlookWidth + TempWidth + HumidityWidth + BoolWidth + BoolWidth + 6;
        const int PartialWidth = OutlookWidth + TempWidth + HumidityWidth + BoolWidth + 5;
        private static readonly string FullHorizontalDivider = string.Join("", Enumerable.Range(1, TotalWidth).Select(_ => "-"));
        private static readonly string PartialHorizontalDivider = string.Join("", Enumerable.Range(1, PartialWidth).Select(_ => "-"));

        private static void header()
        {
            Console.WriteLine("|{0}|{1}|{2}|{3}|{4}|",
                alignCenter("Outlook", OutlookWidth),
                alignCenter("Temp", TempWidth),
                alignCenter("Humidity", HumidityWidth),
                alignCenter("Windy", BoolWidth),
                alignCenter("* Play *", BoolWidth));
        }

        private static void partialHeader()
        {
            Console.WriteLine("|{0}|{1}|{2}|{3}|",
                alignCenter("Outlook", OutlookWidth),
                alignCenter("Temp", TempWidth),
                alignCenter("Humidity", HumidityWidth),
                alignCenter("Windy", BoolWidth));
        }

        public static void DataSetTable(IEnumerable<GolfDay> dataSet)
        {
            Console.WriteLine(FullHorizontalDivider);
            header();
            Console.WriteLine(FullHorizontalDivider);
            foreach (var row in dataSet)
            {
                dataRow(row.Attributes, row.Play);
            }
            Console.WriteLine(FullHorizontalDivider);
        }

        private static void dataRow(Day attr, bool play)
        {
            Console.WriteLine("|{0}|{1}|{2}|{3}|{4}|",
                alignCenter(attr.Outlook, OutlookWidth),
                alignCenter(attr.Temp, TempWidth),
                alignCenter(attr.Humidity, HumidityWidth),
                alignCenter(boolYesNoString(attr.Windy), BoolWidth),
                alignCenter(boolYesNoString(play), BoolWidth));
        }

        private static void dataRow(Day attr)
        {
            Console.WriteLine("|{0}|{1}|{2}|{3}|",
                alignCenter(attr.Outlook, OutlookWidth),
                alignCenter(attr.Temp, TempWidth),
                alignCenter(attr.Humidity, HumidityWidth),
                alignCenter(boolYesNoString(attr.Windy), BoolWidth));
        }

        public static void DataSetRow(Day attr)
        {
            Console.WriteLine(PartialHorizontalDivider);
            partialHeader();
            Console.WriteLine(PartialHorizontalDivider);
            dataRow(attr);
            Console.WriteLine(PartialHorizontalDivider);
        }

        public static void Prediction(PlayPrediction prediction)
        {
            Console.WriteLine("Play = {0}, with a likelihood of {1:F5} and a probability of {2:F5}.",
                boolYesNoString(prediction.Play),
                prediction.Likelihood,
                prediction.Probability);
        }

        private static string boolYesNoString(bool b)
        {
            return b ? "Yes" : "No";
        }

        private static string alignCenter(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            return string.IsNullOrEmpty(text)
                ? new string(' ', width)
                : text.PadRight(width - (width - text.Length)/2).PadLeft(width);
        }
    }
}