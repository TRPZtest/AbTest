﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbTest.Data.Db.Entites
{
    public record Session
    {
        [Key]
        public long Id { get; set; } 
        public string DeviceToken { get; set; }
        [Required]
        public DateTime Created { get; set; }
        public List<Experiment> Experiments { get; set; } = new();

    }
}
