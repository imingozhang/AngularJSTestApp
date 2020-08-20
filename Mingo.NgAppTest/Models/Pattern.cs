using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mingo.NgAppTest.Models
{
    /// <summary>
    /// Model of request
    /// </summary>
    public class Pattern
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("subtext")]
        public string SubText { get; set; }

    }
}
