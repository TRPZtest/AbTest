using AbTest.Data.Db.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbTest.UnitTests.Helpers
{
    public static class TestDataHelper
    {
        public static Experiment[] GetButtonExperiments()
        {
            return new Experiment[]
            {
                new Experiment { Probability = 0.33, Value = "#FF0000" },
                new Experiment { Probability = 0.33, Value = "#00FF00 " },
                new Experiment { Probability = 0.33, Value = "#0000FF" }
            };
        }

        public static Experiment[] GetPriceExperiments()
        {
            return new Experiment[]
            {
                new Experiment { Probability =  0.75, Value = "10" },
                new Experiment { Probability = 0.1, Value = "20" },
                new Experiment { Probability = 0.05, Value = "50" },
                new Experiment { Probability = 0.1, Value = "5" }
            };
        }
    }
}
