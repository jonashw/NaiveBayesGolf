using System;
using System.Collections.Generic;

namespace NaiveBayesGolf.Model
{
    public class AttributeSummary
    {
        public readonly string AttributeName;
        private readonly Dictionary<string, AttributeValueCount> _valueCounts = new Dictionary<string, AttributeValueCount>();
        public readonly Func<Day, string> GetValueFrom;

        public IReadOnlyDictionary<string, AttributeValueCount> ValueCounts
        {
            get { return _valueCounts; }
        }
        public AttributeSummary(string attributeName, Func<Day, string> getValueFrom)
        {
            AttributeName = attributeName;
            GetValueFrom = getValueFrom;
        }

        public void Digest(IEnumerable<GolfDay> data)
        {
            foreach (var row in data)
            {
                var value = GetValueFrom(row.Attributes);
                if (!_valueCounts.ContainsKey(value))
                {
                    _valueCounts[value] = new AttributeValueCount();
                }
                var valueCount = _valueCounts[value];
                if(row.Play)
                {
                    valueCount.Play++;
                } else {
                    valueCount.NotPlay++;
                }
            }
        }
    }
}