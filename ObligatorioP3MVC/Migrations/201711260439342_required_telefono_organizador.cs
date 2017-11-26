namespace ObligatorioP3MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class required_telefono_organizador : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Organizadores", "Telefono", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Organizadores", "Telefono", c => c.String());
        }
    }
}
