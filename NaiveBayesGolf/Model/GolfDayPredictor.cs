using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.SolverFoundation.Common;

namespace NaiveBayesGolf.Model
{
    public class GolfDayPredictor
    {
        private readonly AttributeValueCount _totalPerCategory = new AttributeValueCount();
        private readonly int _total;
        private readonly AttributeSummary _outlook = new AttributeSummary("Outlook", attr => attr.Outlook);
        private readonly AttributeSummary _temp = new AttributeSummary("Temp", attr => attr.Temp);
        private readonly AttributeSummary _humidity = new AttributeSummary("Humidity", attr => attr.Humidity);
        private readonly AttributeSummary _windy = new AttributeSummary("Windy", attr => attr.Windy.ToString());

        private readonly IReadOnlyList<AttributeSummary> _attributes;

        public GolfDayPredictor(IEnumerable<GolfDay> data)
        {
            var d = data.ToList().AsReadOnly();
            _attributes = new List<AttributeSummary>
            {
                _outlook,
                _temp,
                _humidity,
                _windy
            };

            foreach (var row in d)
            {
                _total++;
                if (row.Play)
                {
                    _totalPerCategory.Play++;
                }
                else
                {
                    _totalPerCategory.NotPlay++;
                }
            }

            foreach (var attribute in _attributes)
            {
                attribute.Digest(d);
            }
        }

        public PlayPrediction Predict(Day data)
        {
            var playLikelihood    = predictLikelihood(avc => avc.Play,    data);
            var notPlayLikelihood = predictLikelihood(avc => avc.NotPlay, data);

            var combinedLiklihood = playLikelihood + notPlayLikelihood;

            return playLikelihood > notPlayLikelihood
                ? new PlayPrediction(true,     playLikelihood,    playLikelihood / combinedLiklihood)
                : new PlayPrediction(false, notPlayLikelihood, notPlayLikelihood / combinedLiklihood);
        }

        private double predictLikelihood(Func<AttributeValueCount, int> totalGetter, Day data)
        {
            BigInteger totalInCategory = totalGetter(_totalPerCategory);
            var probabilities = _attributes
                .Select(a =>
                {
                    var value = a.GetValueFrom(data);
                    var subTotal = totalGetter(a.ValueCounts[value]);
                    return Rational.Get(subTotal, totalInCategory);
                })
                .Concat(new[]
                {
                    Rational.Get(totalInCategory, _total)
                }).ToList().AsReadOnly();
            return probabilities
                .Aggregate((double)1, (a, b) => a*(b.ToDouble()));
        }
    }
}