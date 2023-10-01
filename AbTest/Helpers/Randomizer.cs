namespace AbTest.Helpers
{
    public class Randomizer
    {
        public static T GetRandomCase<T>(Dictionary<T, double> cases) where T: notnull
        {     
            var random = new Random();

            var maxValue = cases.Values.Sum(x => x);

            var randNumber = random.NextDouble() * maxValue;

            Double temp = 0;

            foreach(var randomCase in cases)
            {
                temp += randomCase.Value;
                if (randNumber <= temp)
                    return randomCase.Key;
            }

            return cases.Keys.Last();
        }
    }
}
