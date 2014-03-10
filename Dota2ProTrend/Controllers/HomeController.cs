using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dota2ProTrend.Models;
using System.Diagnostics;

namespace Dota2ProTrend.Controllers
{
    public class HomeController : Controller
    {
        private Dota2ProTrendContext db = new Dota2ProTrendContext();

        //
        // GET: /Home/

        public ActionResult Index()
        {
            //int[] herocount = new int[108];
            Dictionary<string, int> herodict = new Dictionary<string, int>();

            var herolist = db.Heroes.ToList();

            foreach (var heroid in herolist)
            {
                
                var count = db.GamePlayers.Where(s => s.hero.heronumber == heroid.heronumber).Count();

                if (count > 30)
                {
                    herodict.Add(heroid.heroname, count);
                }

            }

            

            ViewBag.herolist = herodict;


            return View(db.Matches.ToList());
        }

        public ActionResult Index2()
        {
            return View(db.Matches.ToList());
        }

        //
        // GET: /Home/Details/5

        public ActionResult Hero(int id = 0)
        {

            List<Match> matchlist = new List<Match>();
            var herolist = db.GamePlayers.Where(s => s.hero.heronumber == id).ToList();


            foreach (var game in herolist)
            {

                matchlist.Add(game.match);


            }

            ViewBag.Title = "Matches with " + db.Heroes.FirstOrDefault(s => s.heronumber == id).heroname;

            return View(matchlist);
        }

        public ActionResult Player(int id = 0)
        {

            List<Match> matchlist = new List<Match>();
            var playerlist = db.GamePlayers.Where(s => s.player.playerident == id).ToList();


            foreach (var game in playerlist)
            {

                matchlist.Add(game.match);


            }
            ViewBag.Title = db.Players.FirstOrDefault(s => s.playerident == id).name + "'s Matches";

             

            return View(matchlist);
        }

        public ActionResult Details(int id = 0)
        {
            var match = db.GamePlayers.Where(s => s.matchid == id).OrderBy(s=>s.playerslot);
            if (match == null)
            {
                return HttpNotFound();
            }
            

            return View(match.ToList());
        }

        //
        // GET: /Home/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Home/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Match match)
        {
            if (ModelState.IsValid)
            {
                //db.Matches.Add(match);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(match);
        }

        //
        // GET: /Home/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Match match = db.Matches.Find(id);
            if (match == null)
            {
                return HttpNotFound();
            }
            return View(match);
        }

        //
        // POST: /Home/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Match match)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(match).State = EntityState.Modified;
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(match);
        }

        //
        // GET: /Home/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Match match = db.Matches.Find(id);
            if (match == null)
            {
                return HttpNotFound();
            }
            return View(match);
        }

        //
        // POST: /Home/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Match match = db.Matches.Find(id);
            //db.Matches.Remove(match);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }

   
}