﻿using Skillset_DAL.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Skillset_DAL.ContextClass
{
    public class SkillsetDbContext : DbContext
    {
        public SkillsetDbContext() : base(nameOrConnectionString: "Default") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);

            //Fluent API to be written here


            modelBuilder.Entity<Skill>().Property(c => c.skillName).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("SK_Name") { IsUnique = true }));
        }
        public DbSet<Skill> Skills { get; set; }
    }
}
