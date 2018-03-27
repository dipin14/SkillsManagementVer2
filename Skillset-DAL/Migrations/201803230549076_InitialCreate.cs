namespace Skillset_DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "public.Skills",
                c => new
                    {
                        skillId = c.Int(nullable: false, identity: true),
                        skillName = c.String(),
                        skillDescription = c.String(),
                        status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.skillId)
                .Index(t => t.skillName, unique: true, name: "SK_Name");
            
        }
        
        public override void Down()
        {
            DropIndex("public.Skills", "SK_Name");
            DropTable("public.Skills");
        }
    }
}
