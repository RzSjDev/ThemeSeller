﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class MyCmsContext:DbContext
    {
        public DbSet<ThemeGroup> ThemeGroups { get; set; }
        public DbSet<Theme> Theme { get; set; }
        public DbSet<PageComment> PageComments { get; set; }
        

    }
}
