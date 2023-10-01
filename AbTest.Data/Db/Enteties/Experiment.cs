using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbTest.Data.Db.Enteties
{
    public class Experiment
    {
        [Key]
        public long Id { get; set; }
        public long SessionId { get; set; }
        public long ExperimentValueId { get; set; }
        public string ExperimentValue { get; set; }
        public double ExperimentProbability { get; set; }
        public long ExperimentKeyId { get; set; }
        public string ExperimentKey { get; set;}
        
    }
}
