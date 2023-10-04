using AbTest.Data.Db.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbTest.UnitTests
{
    public static class TestDataHelper
    {
        public static Experiment[] GetButtonPriceExperiments()
        {
            return new Experiment[]
            {
                new Experiment { Probability = 0.33, Value = "Green" },
                new Experiment { Probability = 0.33, Value = "White" },
                new Experiment { Probability = 0.33, Value = "Yellow" }
            };
        }
    }
}
