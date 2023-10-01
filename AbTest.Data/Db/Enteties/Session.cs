using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbTest.Data.Db.Enteties
{
    public class Session
    {
        [Key]
        public long Id { get; set; }
        public string DeviceId { get; set; }
        public ICollection<Experiment> Experiments { get; set; }
    }
}
