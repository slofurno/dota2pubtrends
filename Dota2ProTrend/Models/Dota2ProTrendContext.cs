using System.Data.Entity;

namespace Dota2ProTrend.Models
{
    public class Dota2ProTrendContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<Dota2ProTrend.Models.Dota2ProTrendContext>());

        public Dota2ProTrendContext() : base("name=Dota2ProTrendContext")
        {
            

        }

        public DbSet<Match> Matches { get; set; }
        public DbSet<GamePlayerModel> GamePlayers { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Hero> Heroes { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<SkillTimings> SkillTiming { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<ItemNames> ItemNames { get; set; }
    }
}
