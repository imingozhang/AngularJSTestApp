using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mingo.NgAppTest.Models
{
    /// <summary>
    /// Model of result
    /// </summary>
    public class PatternResult
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("subtext")]
        public string SubText { get; set; }

        /// <summary>
        /// True = found
        /// </summary>
        [JsonProperty("found")]
        public bool Found { get; set; }

        [JsonProperty("positions")]
        public string Positions { get; set; }
    }
}
