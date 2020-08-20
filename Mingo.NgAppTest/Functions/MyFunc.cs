using Mingo.NgAppTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Mingo.NgAppTest.Functions
{
    public class MyFunc
    {
        /// <summary>
        /// Function for searching a string with a specific pattern
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static PatternResult MatchString(Pattern pattern)
        {
            if (pattern == null)
            {
                throw new ArgumentNullException("The input object should not be null.");
            }

            if (pattern.Text == null)
            {
                throw new ArgumentNullException("The input text should not be null.");
            }

            if (pattern.SubText == null)
            {
                throw new ArgumentNullException("The input subtext should not be null.");
            }

            try
            {
                var regMatch = Regex.Match(pattern.Text, pattern.SubText, RegexOptions.IgnoreCase);
                var matchResults = FindNextMatch(regMatch);

                var patternResult = new PatternResult();
                patternResult.Text = pattern.Text;
                patternResult.SubText = pattern.SubText;

                if (matchResults != null && matchResults.Count > 0)
                {
                    patternResult.Found = true;
                    patternResult.Positions = String.Join(',', matchResults.Select(x => x.Index.ToString()));
                }

                return patternResult;
            }
            catch(Exception ee)
            {
                throw new Exception($"Found error: { ee.Message }");
            }
        }

        /// <summary>
        /// Do loop searching
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        private static List<Match> FindNextMatch(Match match)
        {
            var matchResult = new List<Match>();
            if (match.Success)
            {
                matchResult.Add(match);
                var next = match.NextMatch();

                if (next.Success)
                {
                    var nextMatchCollection = FindNextMatch(next);
                    matchResult.AddRange(nextMatchCollection);
                }
            }

            return matchResult;
        }
    }
}
