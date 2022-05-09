using Matches.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Matches.Controllers
{
    public class MatchController : Controller
    {
        static int currentID = 0;

        static List<Match> Matches = new List<Match>()
        {
        };

        // GET: MatchController
        public ActionResult Index()
        {
            IEnumerable<Match> PlayedMatches = Matches.Where(p => p.MatchDate < DateTime.Today.Date);
            ViewBag.Points = CalculatePoints(PlayedMatches);
            ViewBag.GoalDiff = PlayedMatches.Sum(p => p.GoalsFor) - Matches.Sum(p => p.GoalsAgainst);
            ViewBag.PlayedCount = PlayedMatches.Count();
            ViewBag.Won = PlayedMatches.Where(p => (p.GoalsFor > p.GoalsAgainst)).ToList().Count;
            ViewBag.Lost = PlayedMatches.Where(p => (p.GoalsFor < p.GoalsAgainst)).ToList().Count;
            ViewBag.Drew = PlayedMatches.Where(p => (p.GoalsFor == p.GoalsAgainst)).ToList().Count;
            return View(Matches);
        }

        private int CalculatePoints(IEnumerable<Match> playedM)
        {
            int pointsEarned = 0;

            foreach (Match m in playedM)
            {
                if (m.GoalsFor > m.GoalsAgainst)
                {
                    pointsEarned += 3;
                }
                if (m.GoalsFor == m.GoalsAgainst)
                {
                    pointsEarned += 1;
                }
            }
            return pointsEarned;
        }


        // GET: MatchController/Details/5
        public ActionResult Details(int id)
        {
            return View(Matches[id]);
        }

        // GET: MatchController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MatchController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                Match newMatch = new Match() { MatchID = currentID++, MatchDate = DateTime.Parse(collection["MatchDate"]), Opponent = collection["Opponent"], Venue = collection["Venue"], GoalsAgainst = 0, GoalsFor = 0 };
                Matches.Add(newMatch);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: MatchController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(Matches[id]);
        }

        // POST: MatchController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                Match editFx = Matches.FirstOrDefault(p => p.MatchID.Equals(id));
                editFx.GoalsFor = int.Parse(collection["GoalsFor"]);
                editFx.GoalsAgainst = int.Parse(collection["GoalsAgainst"]);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
