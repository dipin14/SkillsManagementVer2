namespace Skillset_DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "public.Designations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "public.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployeeCode = c.String(),
                        Name = c.String(),
                        DateOfJoining = c.DateTime(nullable: false),
                        DesignationId = c.Int(nullable: false),
                        QualificationId = c.Int(nullable: false),
                        Experience = c.Int(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false),
                        ManagerCode = c.Int(nullable: false),
                        Address = c.String(),
                        Email = c.String(),
                        MobileNumber = c.Double(nullable: false),
                        Gender = c.String(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "public.Ratings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.Int(nullable: false),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "public.SkillRatings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        SkillId = c.Int(nullable: false),
                        RatingId = c.Int(nullable: false),
                        RatingDate = c.DateTime(nullable: false),
                        Note = c.String(),
                        IsSpecialSkill = c.Boolean(nullable: false),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "public.Skills",
                c => new
                    {
                        skillId = c.Int(nullable: false, identity: true),
                        skillName = c.String(),
                        skillDescription = c.String(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.skillId)
                .Index(t => t.skillName, unique: true, name: "SK_Name");
            
        }
        
        public override void Down()
        {
            DropIndex("public.Skills", "SK_Name");
            DropTable("public.Skills");
            DropTable("public.SkillRatings");
            DropTable("public.Ratings");
            DropTable("public.Employees");
            DropTable("public.Designations");
        }
    }
}
