namespace Skillset_DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pullfrommaster : DbMigration
    {
        public override void Up()
        {
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
            
            AddColumn("public.Employees", "RoleId", c => c.Int(nullable: false));
            AddColumn("public.Employees", "EmployeeId", c => c.Int(nullable: false));
            DropColumn("public.Employees", "ManagerCode");
        }
        
        public override void Down()
        {
            AddColumn("public.Employees", "ManagerCode", c => c.String());
            DropColumn("public.Employees", "EmployeeId");
            DropColumn("public.Employees", "RoleId");
            DropTable("public.Roles");
            DropTable("public.Qualifications");
        }
    }
}
