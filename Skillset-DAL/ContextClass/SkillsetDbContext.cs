using Skillset_DAL.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Skillset_DAL.ContextClass
{


		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.HasDefaultSchema("public");
			base.OnModelCreating(modelBuilder);




            modelBuilder.Entity<Skill>().Property(c => c.skillName).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("SK_Name") { IsUnique = true }));
        public DbSet<Role> Roles { get; set; }
}
