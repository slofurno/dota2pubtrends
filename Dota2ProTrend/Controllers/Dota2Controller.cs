using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Dota2ProTrend.Models;

namespace Dota2ProTrend.Controllers
{

    public class PlayersController : ApiController
    {
        private Dota2ProTrendContext db = new Dota2ProTrendContext();

        public IEnumerable<Player> GetPlayers()
        {


            var playerlist = db.Players.ToList();



            return playerlist;

        }

    }

    public class PlayerMatchesController : ApiController
    {
        private Dota2ProTrendContext db = new Dota2ProTrendContext();

        public IEnumerable<Match> GetMatches(int id)
        {
            List<Match> matchlist = new List<Match>();
            var playerlist = db.GamePlayers.Where(s => s.player.id == id).ToList();


            foreach (var game in playerlist)
            {

                matchlist.Add(game.match);


            }
            return matchlist;
        }

    }

    public class Dota2Controller : ApiController
    {
        private Dota2ProTrendContext db = new Dota2ProTrendContext();

        // GET api/Dota2
        public IEnumerable<Match> GetMatches()
        {
            return db.Matches.ToList();
        }
        /*
        public IEnumerable<Match> GetMatches(int id)
        {
            List<Match> matchlist = new List<Match>();
            var playerlist = db.GamePlayers.Where(s => s.player.id == id).ToList();


            foreach (var game in playerlist)
            {

                matchlist.Add(game.match);


            }
            return matchlist;
        }
        */
        // GET api/Dota2/5
        public Match GetMatch(int id)
        {

            var match = db.Matches.First(s => s.id == id);

            return match;

        }


        // PUT api/Dota2/5
        public HttpResponseMessage PutMatch(int id, Match match)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != match.id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(match).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/Dota2
        public HttpResponseMessage PostMatch(Match match)
        {
            if (ModelState.IsValid)
            {
                db.Matches.Add(match);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, match);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = match.id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Dota2/5
        public HttpResponseMessage DeleteMatch(int id)
        {
            Match match = db.Matches.Find(id);
            if (match == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Matches.Remove(match);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, match);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}