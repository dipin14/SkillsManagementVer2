namespace Skillset_DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            AlterColumn("public.Employees", "DateOfJoining", c => c.DateTime(nullable: false));
            AlterColumn("public.Employees", "DateOfBirth", c => c.DateTime(nullable: false));
            AlterColumn("public.SkillRatings", "RatingDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("public.SkillRatings", "RatingDate", c => c.DateTime(nullable: false));
            AlterColumn("public.Employees", "DateOfBirth", c => c.DateTime(nullable: false));
            AlterColumn("public.Employees", "DateOfJoining", c => c.DateTime(nullable: false));
        }
    }
}
