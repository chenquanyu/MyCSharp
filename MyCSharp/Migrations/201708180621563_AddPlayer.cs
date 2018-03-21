namespace MyCSharp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPlayer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Player",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Gender = c.String(),
                        Birthday = c.DateTime(nullable: false),
                        Comments = c.String(),
                        IsSingle = c.Boolean(nullable: false),
                        RoleType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Player");
        }
    }
}
