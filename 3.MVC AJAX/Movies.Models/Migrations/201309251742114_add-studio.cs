namespace Movies.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addstudio : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Studios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Actors", "BirthDate", c => c.DateTime(nullable: true));
            AddColumn("dbo.Movies", "Studio_Id", c => c.Int());
            AddColumn("dbo.Directors", "BirthDate", c => c.DateTime(nullable: false));
            CreateIndex("dbo.Movies", "Studio_Id");
            AddForeignKey("dbo.Movies", "Studio_Id", "dbo.Studios", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Movies", "Studio_Id", "dbo.Studios");
            DropIndex("dbo.Movies", new[] { "Studio_Id" });
            DropColumn("dbo.Directors", "BirthDate");
            DropColumn("dbo.Movies", "Studio_Id");
            DropColumn("dbo.Actors", "BirthDate");
            DropTable("dbo.Studios");
        }
    }
}
