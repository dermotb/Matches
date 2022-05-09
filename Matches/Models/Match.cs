using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Matches.Models
{
    public class Match
    {
        public int MatchID { get; set; }
        [Display(Name = "Match Date")]
        [DataType(DataType.Date)]
        public DateTime MatchDate { get; set; }
        public string Opponent { get; set; }
        public string Venue { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }
    }
}