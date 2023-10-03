using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbTest.Data.Db.Entites
{
    public class Experiment
    {
        [Key]
        public long Id { get; set; }  
        [Required]
        public ExperimentValue ExperimentValue { get; set; }
        [Required]
        public Session Session { get; set; }

        public Experiment() { }

        public Experiment(ExperimentValue experimentValue, Session session)
        {                        
            ExperimentValue = experimentValue;
            Session = session;
        }
    }
}
