namespace Skillset_DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
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
                        Id = c.Int(nullable: false),
                        EmployeeCode = c.String(),
                        Name = c.String(),
                        DateOfJoining = c.DateTime(nullable: false),
                        DesignationId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                        QualificationId = c.Int(nullable: false),
                        Experience = c.Int(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                        Address = c.String(),
                        Email = c.String(),
                        MobileNumber = c.Double(nullable: false),
                        Gender = c.String(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.Designations", t => t.DesignationId, cascadeDelete: true)
                .ForeignKey("public.Employees", t => t.EmployeeId)
                .ForeignKey("public.Qualifications", t => t.QualificationId, cascadeDelete: true)
                .ForeignKey("public.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.EmployeeCode, unique: true)
                .Index(t => t.DesignationId)
                .Index(t => t.RoleId)
                .Index(t => t.QualificationId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "public.Qualifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "public.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
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
                        SkillId = c.Int(nullable: false, identity: true),
                        SkillName = c.String(),
                        SkillDescription = c.String(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.SkillId)
                .Index(t => t.SkillName, unique: true, name: "SK_Name");
            
        }
        
        public override void Down()
        {
            DropForeignKey("public.Employees", "RoleId", "public.Roles");
            DropForeignKey("public.Employees", "QualificationId", "public.Qualifications");
            DropForeignKey("public.Employees", "EmployeeId", "public.Employees");
            DropForeignKey("public.Employees", "DesignationId", "public.Designations");
            DropIndex("public.Skills", "SK_Name");
            DropIndex("public.Employees", new[] { "EmployeeId" });
            DropIndex("public.Employees", new[] { "QualificationId" });
            DropIndex("public.Employees", new[] { "RoleId" });
            DropIndex("public.Employees", new[] { "DesignationId" });
            DropIndex("public.Employees", new[] { "EmployeeCode" });
            DropTable("public.Skills");
            DropTable("public.SkillRatings");
            DropTable("public.Ratings");
            DropTable("public.Roles");
            DropTable("public.Qualifications");
            DropTable("public.Employees");
            DropTable("public.Designations");
        }
    }
}
