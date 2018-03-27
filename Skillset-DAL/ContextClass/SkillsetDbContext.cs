using Skillset_DAL.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Skillset_DAL.ContextClass
{





        public DbSet<Qualification> Qualifications { get; set; }
        public DbSet<Role> Roles { get; set; }
    }}
