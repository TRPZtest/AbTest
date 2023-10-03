using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace AbTest.Model
{
    public class ExperimentRequestDto
    {
        [Required]
        [FromQuery(Name = "device-token")]
        public string   DeviceToken { get; set; }
    }
}
