using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dota2ProTrend.Models;
using System.Timers;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Data.Entity;
using System.Net;

namespace Dota2ProTrend
{
    public static class ApiCaller
    {


        public static int teamid = 40000;
        public static int dupremove = 0;
        public static List<int> matchestoupdate = new List<int>();
        public static List<int> teamstoupdate = new List<int>();
        public static List<int> playerids = new List<int>()
        {
            67601693, 86715129, 109778511, 115975133, 67760037, 40547474, 30237211, 86726887, 88719902, 5448108, 85805514, 21604967, 87276347, 86745912, 121847953, 
            118073569, 132291754

        };

        //public static void OnCallTimedEvent(object source, ElapsedEventArgs e)
        /*
        public static void populateNameData()
        {

            Int64[] idarray = new Int64[playerids.Count];

            for (var i = 0; i < playerids.Count; i++)
            {

                idarray[i] += 76561197960265728;

            }



            string ids = string.Join(",", idarray);

            HttpClient client = new HttpClient();

            string baseurl = "http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key=65C5ADADF141DB0495C3FBBCA6D65689&steamids=";

            Int64 steamid;
            Int64 baseid = 76561197960265728;

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage result;
            JObject content;
            JObject resultobject;

           
              
                result = client.GetAsync(baseurl + ids + " ").Result;
                content = result.Content.ReadAsAsync<JObject>().Result;
                resultobject = content.Value<JObject>("response");


                JArray Players = resultobject.Value<JArray>("players");


            Dota2ProTrendContext db = new Dota2ProTrendContext();


            var playerlist = Players.ToList();

            Player newplayer = new Player();

            

            foreach (var thisplayer in playerlist)
            {

                newplayer = new Player();

                newplayer.playerident

                newhero.heronumber = thishero.Value<int>("id");
                newhero.heroname = thishero.Value<string>("name").Substring(14);



                db.Heroes.Add(newhero);
                db.SaveChanges();

            }

        }
        */

        public static void testCase()
        {

            getHero(5);
        }

        public static void getSomeMatches()
        {

            HttpClient client = new HttpClient();
            string baseurl = "https://api.steampowered.com/IDOTA2Match_570/GetMatchHistory/V001/?key=65C5ADADF141DB0495C3FBBCA6D65689&matches_requested=2&account_id=";

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage result;
            JObject content;
            JObject resultobject;
            JArray matches;

            for (var i = 0; i < playerids.Count; i++)
            {

                result = client.GetAsync(baseurl + playerids[i] + "").Result;
                content = result.Content.ReadAsAsync<JObject>().Result;
                resultobject = content.Value<JObject>("result");
                matches = resultobject.Value<JArray>("matches");

                var matchlist = matches.ToList();

                foreach (var match in matchlist)
                {

                    addMatch(match.Value<int>("match_id"));

                }

            }

            Debug.WriteLine(string.Join(",", matchestoupdate));
            processMatches();

        }

        public static void addMatch(int id)
        {

            if (matchestoupdate.Contains(id))
            {


            }
            else
            {
                matchestoupdate.Add(id);
            }

        }

        public static void processMatches()
        {
            

            foreach (var match in matchestoupdate)
            {

                using (var db = new Dota2ProTrendContext())
                {
                    var exist = db.Matches.SingleOrDefault(x => x.matchnumber == match);

                    if (exist == null)
                    {
                        processMatch(match);
                    }
                    else
                    {
                        Debug.WriteLine("match " + match + " already exists");
                    }
                }
            }

            //processMatch(541595171);

        }

        public static void processMatch(int matchnumber)
        {

            Debug.WriteLine("processing match id : " + matchnumber);

            Dota2ProTrendContext db = new Dota2ProTrendContext();

            var matchresult = db.Matches.FirstOrDefault(x => x.matchnumber == 4);


            if (matchresult == null)
            {

                HttpClient client = new HttpClient();

                string baseurl = "https://api.steampowered.com/IDOTA2Match_570/GetMatchDetails/V001/?key=65C5ADADF141DB0495C3FBBCA6D65689&match_id=";

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage result;
                JObject content;
                JObject resultobject;
                JArray players;
                bool radiantwon;
                int starttime;
                int gamemode;
                int lobbytype;

                result = client.GetAsync(baseurl + matchnumber + "").Result;
                content = result.Content.ReadAsAsync<JObject>().Result;
                resultobject = content.Value<JObject>("result");
                players = resultobject.Value<JArray>("players");
                radiantwon = resultobject.Value<bool>("radiant_win");
                starttime = resultobject.Value<int>("start_time");
                gamemode = resultobject.Value<int>("game_mode");
                lobbytype = resultobject.Value<int>("lobby_type");

                Debug.WriteLine("match lobby type : " + lobbytype);

                if (lobbytype.Equals(7))
                {

                    Match newmatch = new Match();

                    newmatch.gamemode = resultobject.Value<int>("game_mode");
                    newmatch.radiantwon = resultobject.Value<bool>("radiant_win");
                    newmatch.starttime = resultobject.Value<int>("start_time");
                    newmatch.matchnumber = matchnumber;

                    db.Matches.Add(newmatch);
                    db.SaveChanges();


                    List<GamePlayerModel> playerlist = new List<GamePlayerModel>();
                    var playerinfolist = players.ToList();



                    foreach (var playerinfo in playerinfolist)
                    {

                        GamePlayerModel matchpi = new GamePlayerModel();

                        long accountid = playerinfo.Value<long>("account_id");

                        

                        

                        var heroid = playerinfo.Value<int>("hero_id");

                        matchpi.playerid = getPlayer(accountid).id;
                        //matchpi.playerid = db.Players.FirstOrDefault(x => x.playerident == accountid).id;
                        matchpi.matchid = db.Matches.FirstOrDefault(s => s.matchnumber == newmatch.matchnumber).id;
                        matchpi.hero = db.Heroes.FirstOrDefault(x=> x.heronumber == heroid);
                        matchpi.kills = playerinfo.Value<int>("kills");
                        matchpi.deaths = playerinfo.Value<int>("deaths");
                        matchpi.assists = playerinfo.Value<int>("assists");
                        matchpi.gpm = playerinfo.Value<int>("gold_per_min");
                        matchpi.xpm = playerinfo.Value<int>("xp_per_min");
                        matchpi.creepscore = playerinfo.Value<int>("last_hits");
                        matchpi.level = playerinfo.Value<int>("level");
                        matchpi.playerslot = playerinfo.Value<int>("player_slot");

                        Debug.WriteLine("adding matchplayer : " + matchpi.ToString());

                       

                        List<Item> itemlist = new List<Item>();

                        Item tempitem = new Item();

                        tempitem.itemnumber = playerinfo.Value<int>("item_0");

                        itemlist.Add(tempitem);


                        tempitem = new Item();

                        tempitem.itemnumber = playerinfo.Value<int>("item_1");

                        itemlist.Add(tempitem);


                        tempitem = new Item();

                        tempitem.itemnumber = playerinfo.Value<int>("item_2");

                        itemlist.Add(tempitem);

                        tempitem = new Item();

                        tempitem.itemnumber = playerinfo.Value<int>("item_3");

                        itemlist.Add(tempitem);


                        tempitem = new Item();

                        tempitem.itemnumber = playerinfo.Value<int>("item_4");

                        itemlist.Add(tempitem);


                        tempitem = new Item();

                        tempitem.itemnumber = playerinfo.Value<int>("item_5");

                        itemlist.Add(tempitem);

                        matchpi.items = itemlist;

                        /*
                        var itemid = playerinfo.Value<int>("item_0");
                        
                        matchpi.items.Add(db.Items.FirstOrDefault(x=> x.itemnumber==itemid));

                        itemid = playerinfo.Value<int>("item_1");
                        matchpi.items.Add(db.Items.FirstOrDefault(x => x.itemnumber == itemid));

                        itemid = playerinfo.Value<int>("item_2");
                        matchpi.items.Add(db.Items.FirstOrDefault(x => x.itemnumber == itemid));

                        itemid = playerinfo.Value<int>("item_3");
                        matchpi.items.Add(db.Items.FirstOrDefault(x => x.itemnumber == itemid));

                        itemid = playerinfo.Value<int>("item_4");
                        matchpi.items.Add(db.Items.FirstOrDefault(x => x.itemnumber == itemid));

                        itemid = playerinfo.Value<int>("item_5");
                        matchpi.items.Add(db.Items.FirstOrDefault(x => x.itemnumber == itemid));
                        */


                        db.GamePlayers.Add(matchpi);
                        db.SaveChanges();

                        /*
                        matchpi.skills = new List<SkillTimings>();

                        JArray skilltimes = playerinfo.Value<JArray>("ability_upgrades");

                        var skilltimelist = skilltimes.ToList();

                        foreach (var st in skilltimelist)
                        {
                            var temp = new SkillTimings(getSkill(st.Value<int>("ability")), st.Value<int>("time"), matchpi);
                            db.SkillTiming.Add(temp);
                            db.SaveChanges();
                            matchpi.skills.Add(temp);


                        }

                       */

                        playerlist.Add(matchpi);

                    }


                    var addedmatch = db.Matches.FirstOrDefault(x => x.matchnumber == matchnumber);

                    addedmatch.gameplayers = playerlist;

                    db.SaveChanges();

                    

                    

                  


                }
                else
                {
                    Debug.WriteLine("not doing unranked games right now");

                }

            }
            else
            {
                Debug.WriteLine("match already found");
                Debug.WriteLine("match info : " + matchresult.ToString());
            }

        }

        

        public static Skill getSkill(int id)
        {
            Dota2ProTrendContext db = new Dota2ProTrendContext();

            var matchresult = db.Skills.FirstOrDefault(x => x.skillnumber == id);

            if (matchresult == null)
            {
                Skill newskill = new Skill();
                newskill.skillname = "undefined";
                newskill.skillnumber = id;

                db.Skills.Add(newskill);
                db.SaveChanges();

                matchresult = newskill;

            }

            return matchresult;
            
        }

        public static Item getItem(int id)
        {

            Dota2ProTrendContext db = new Dota2ProTrendContext();

            var matchresult = db.Items.FirstOrDefault(x => x.itemnumber == id);

            if (matchresult == null)
            {
                Item newskill = new Item();
                //newskill.itemname = "unknown item";
                //newskill.itemnumber = id;

                db.Items.Add(newskill);
                db.SaveChanges();

                matchresult = newskill;

            }

            return matchresult;
        }

        public static Hero getHero(int id)
        {

            Dota2ProTrendContext db = new Dota2ProTrendContext();

            var matchresult = db.Heroes.FirstOrDefault(x => x.heronumber == id);

            if (matchresult == null)
            {
                Hero newskill = new Hero();
                newskill.heroname = "unknown hero";
                newskill.heronumber = id;

                db.Heroes.Add(newskill);
                db.SaveChanges();

                matchresult = newskill;

            }

            return matchresult;
        }

        public static Player getPlayer(long id)
        {
            Dota2ProTrendContext db = new Dota2ProTrendContext();

            var player = db.Players.FirstOrDefault(x => x.playerident == id);

            if (player == null)
            {
                Player newplayer = new Player();
                newplayer.name = getName(get64bitID(id));
                newplayer.playerident = id;
                db.Players.Add(newplayer);
                db.SaveChanges();

                player = newplayer;
            }

            return player;

        }



        public static string getName(Int64 id)
        {
            HttpClient client = new HttpClient();

            string baseurl = "http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key=65C5ADADF141DB0495C3FBBCA6D65689&steamids=";

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage result;
            JObject content;
            JObject resultobject;
            JArray players;


            result = client.GetAsync(baseurl + id + "").Result;
            content = result.Content.ReadAsAsync<JObject>().Result;
            resultobject = content.Value<JObject>("response");
            players = resultobject.Value<JArray>("players");

            var playerslist = players.ToList();
            string playername;
            if (playerslist.Count > 0)
            {
                playername = playerslist[0].Value<string>("personaname");
            }
            else
            {
                playername = "private account";

            }
            return playername;


        }

        public static Int64 get64bitID(long id)
        {

            Int64 newid = (76561197960265728 + id);

            return newid;
        }

        public static void populateItemandHeroData()
        {


            HttpClient client = new HttpClient();


            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage result = client.GetAsync(("https://api.steampowered.com/IEconDOTA2_570/GetHeroes/v0001/?key=65C5ADADF141DB0495C3FBBCA6D65689")).Result;

            JObject content = result.Content.ReadAsAsync<JObject>().Result;

            JObject resultobject = content.Value<JObject>("result");

            JArray Heroes = resultobject.Value<JArray>("heroes");


            Dota2ProTrendContext db = new Dota2ProTrendContext();

            var herolist = Heroes.ToList();

            Hero newhero = new Hero();


            foreach (var thishero in herolist)
            {

                newhero = new Hero();
                newhero.heronumber = thishero.Value<int>("id");
                newhero.heroname = thishero.Value<string>("name").Substring(14);

             

                db.Heroes.Add(newhero);
                db.SaveChanges();

            }

            string itemdataurl = "http://dota2pubtrends.azurewebsites.net/items.js";
            WebClient wc = new WebClient();
            string data = wc.DownloadString(itemdataurl);



            var itemcontent = JObject.Parse(data);



            JObject Items = itemcontent.Value<JObject>("items");



            ItemNames newitem;


            string indc;

            for (int itemid = 0; itemid <= 215; itemid++)
            {
                indc = ("" + itemid + "");
                newitem = new ItemNames();
                newitem.itemnumber = itemid;
                newitem.itemname = Items.Value<string>(indc);

                db.ItemNames.Add(newitem);
                db.SaveChanges();

            }



        }





    }


}