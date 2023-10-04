using AbTest.Data.Db.Entites;
using AbTest.Helpers;
using Xunit;

namespace AbTest.UnitTests
{
    public class RandomizerTests
    {
        [Fact]
        public void CheckRandomizerForColors()
        {
            var colors = TestDataHelper.GetButtonPriceExperiments();

            var randomColors = new List<Experiment>();

            for (int i = 0; i < 1000; i++)
            {
                randomColors.Add(Randomizer.GetRandomExperimentValue(colors));
            }

            var valueCountDictionary = randomColors.GroupBy(x => x.Value).ToDictionary(x => x.Key, x => x.Count());
            
            valueCountDictionary.Values.ToList().ForEach(x => Assert.True(x > 300 && x < 360)); //Every color must repeats between 300 and 360 times
        }
    }
}