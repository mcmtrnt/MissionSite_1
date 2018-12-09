using MissionSite.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MissionSite.DAL
{
    public class MissionSiteContext : DbContext
    {
        public MissionSiteContext() : base("MissionSiteContext")
        {

        }

        public DbSet<Mission> Missions { get; set; }

        public DbSet<MissionQuestion> MissionQuestions { get; set; }

        public DbSet<User> Users { get; set; }


    }
}