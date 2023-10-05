using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbTest.Data.Db.Entites
{
    public record Experiment
    {      
        [Key]
        public long Id { get; set; }
        public string Value { get; set; }
        public double Probability { get; set; }
        public ExperimentKey ExperimentKey { get; set; }
        public List<Session> Sessions { get; set; } = new();
     
    }
}
