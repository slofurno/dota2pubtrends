using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;


namespace Dota2ProTrend.Models
{
    public class Match
    {
        public int id { get; set; }
        public int matchnumber { get; set; }
        public bool radiantwon { get; set; }
        public int starttime { get; set; }
        public int gamemode { get; set; }
        public virtual ICollection<GamePlayerModel> gameplayers { get; set; }


    }

    public class GamePlayerModel
    {

        public int id { get; set; }
        public int matchid { get; set; }
        public int playerid { get; set; }
        public virtual Match match { get; set; }
        public virtual Player player { get; set; }
        public virtual ICollection<Item> items { get; set; }
        public virtual ICollection<SkillTimings> skills { get; set; }
        public virtual Hero hero { get; set; }
        public int kills { get; set; }
        public int deaths { get; set; }
        public int assists { get; set; }
        public int gpm { get; set; }
        public int xpm { get; set; }
        public int creepscore { get; set; }
        public int level { get; set; }
        public int playerslot { get; set; }




    }

    public class Player
    {

        public Player(long pid, string name)
        {
            this.name = name;
            this.playerident = pid;

        }

        public Player()
        {


        }

        public int id { get; set; }
        public long playerident { get; set; }
        public string name { get; set; }



    }

    public class Hero
    {

        public int id { get; set; }
        public int heronumber { get; set; }
        public string heroname { get; set; }

    }

    public class ItemNames
    {

        public int id { get; set; }
        public int itemnumber { get; set; }
        public string itemname { get; set; }
        public string itemurl { get; set; }
        
        

    }

    public class Item
    {
        public int id { get; set; }
        public int itemnumber { get; set; }
        public virtual ItemNames iteminfo { get; set; }
        public virtual GamePlayerModel gameplayer { get; set; }
        public int GamePlayerModel_id { get; set; }
        
    }

    public class SkillTimings
    {

        public SkillTimings(Skill sk, int time, GamePlayerModel gpm)
        {
            this.skill = sk;
            this.time = time;
            this.gameplayer = gpm;

        }
        public int id { get; set; }
        public int skillid { get; set; }
        public virtual Skill skill { get; set; }
        public int time { get; set; }
        public virtual GamePlayerModel gameplayer { get; set; }
        public int gameplayermodelid { get; set; }
  
    }

    public class Skill
    {
        public int id { get; set; }
        public int skillnumber { get; set; }
        public string skillname { get; set; }
    }

}