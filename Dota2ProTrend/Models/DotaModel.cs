using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using Newtonsoft.Json;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization;


namespace Dota2ProTrend.Models
{

    [DataContract]
    public class Match
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public int matchnumber { get; set; }
        [DataMember]
        public bool radiantwon { get; set; }
        public int starttime { get; set; }
        [DataMember]
        public int gamemode { get; set; }
        
        public virtual ICollection<GamePlayerModel> gameplayers { get; set; }

        public Match()
        {

        }
    }
    [DataContract]
    public class GamePlayerModel
    {

        public int id { get; set; }
        public int matchid { get; set; }

        [DataMember]
        public int playerid { get; set; }

        [DataMember]
        public virtual Match match { get; set; }

        [DataMember]
        public virtual Player player { get; set; }

        [DataMember]
        public virtual ICollection<Item> items { get; set; }
        [JsonIgnore]
        public virtual ICollection<SkillTimings> skills { get; set; }

        [DataMember]
        public virtual Hero hero { get; set; }

        [DataMember]
        public int kills { get; set; }

        [DataMember]
        public int deaths { get; set; }

        [DataMember]
        public int assists { get; set; }
        public int gpm { get; set; }
        public int xpm { get; set; }
        public int creepscore { get; set; }
        public int level { get; set; }
        [DataMember]
        public int playerslot { get; set; }

        public GamePlayerModel()
        {


        }


    }
    [DataContract]
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
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public long playerident { get; set; }
        [DataMember]
        public string name { get; set; }



    }
    [DataContract]
    public class Hero
    {

        public int id { get; set; }
        [DataMember]
        public int heronumber { get; set; }
        [DataMember]
        public string heroname { get; set; }

        public Hero()
        {

        }
    }
    [DataContract]
    public class ItemNames
    {

        public int id { get; set; }
        public int itemnumber { get; set; }
        public string itemname { get; set; }
        [DataMember]
        public string itemurl { get; set; }

        public ItemNames()
        {

        }

    }
    [DataContract]
    public class Item
    {
        public int id { get; set; }
        [DataMember]
        public int itemnumber { get; set; }

        [DataMember]
        public virtual ItemNames iteminfo { get; set; }
        
        
        public virtual GamePlayerModel gameplayer { get; set; }
        public int GamePlayerModel_id { get; set; }

        public Item()
        {

        }
    }
    [DataContract]
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
        [JsonIgnore]
        [XmlIgnoreAttribute]
        public virtual Skill skill { get; set; }
        public int time { get; set; }
        [JsonIgnore]
        [XmlIgnoreAttribute]
        public virtual GamePlayerModel gameplayer { get; set; }
        public int gameplayermodelid { get; set; }

        public SkillTimings()
        {

        }
    }
    [DataContract]
    public class Skill
    {
        public int id { get; set; }
        public int skillnumber { get; set; }
        public string skillname { get; set; }

        public Skill()
        {

        }
    }

}