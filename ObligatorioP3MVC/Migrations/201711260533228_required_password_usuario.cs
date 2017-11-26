namespace ObligatorioP3MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class required_password_usuario : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Usuarios", "Pass", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Usuarios", "Pass", c => c.String());
        }
    }
}
