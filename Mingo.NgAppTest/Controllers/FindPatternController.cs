using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mingo.NgAppTest.Functions;
using Mingo.NgAppTest.Models;

namespace Mingo.NgAppTest.Controllers
{
    /// <summary>
    /// Back-end API
    /// </summary>
    [Route("/api/findpattern")]
    [ApiController]
    public class FindPatternController : Controller
    {
        /// <summary>
        /// HTTP Post method for checking the pattern matching results
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        [Route("check")]
        [HttpPost]
        public IActionResult Check([FromBody] Pattern pattern)
        {
            try
            {
                var result = MyFunc.MatchString(pattern);

                return Ok(result);
            }
            catch(Exception ee)
            {
                return new BadRequestObjectResult(ee.Message);
            }
        }
    }
}
