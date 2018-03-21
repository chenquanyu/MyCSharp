namespace MyCSharp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPlayer01 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Player", "Birthday", c => c.DateTime());
            AlterColumn("dbo.Player", "IsSingle", c => c.Boolean());
            AlterColumn("dbo.Player", "RoleType", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Player", "RoleType", c => c.Int(nullable: false));
            AlterColumn("dbo.Player", "IsSingle", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Player", "Birthday", c => c.DateTime(nullable: false));
        }
    }
}
