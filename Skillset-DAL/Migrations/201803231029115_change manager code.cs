namespace Skillset_DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changemanagercode : DbMigration
    {
        public override void Up()
        {
            AlterColumn("public.Employees", "ManagerCode", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("public.Employees", "ManagerCode", c => c.Int(nullable: false));
        }
    }
}
