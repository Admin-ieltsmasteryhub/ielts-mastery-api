using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IeltsApp.Models
{
    using System.Collections.Generic;
    public class EssayCheckerDto
    {
        public class SafetyRating
        {
            public string Category { get; set; }
            public string Probability { get; set; }
        }

        public class Candidate
        {
            public string Output { get; set; }
            public List<SafetyRating> SafetyRatings { get; set; }
        }

        public class EssayChecker
        {
            public List<Candidate> Candidates { get; set; }
        }
    }
}
