namespace GeeksForLessForum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changetableComment : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "Topics_Id", "dbo.Topics");
            DropIndex("dbo.Comments", new[] { "Topics_Id" });
            AddColumn("dbo.Comments", "TopicId", c => c.Int(nullable: false));
            DropColumn("dbo.Comments", "Topics_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comments", "Topics_Id", c => c.Int());
            DropColumn("dbo.Comments", "TopicId");
            CreateIndex("dbo.Comments", "Topics_Id");
            AddForeignKey("dbo.Comments", "Topics_Id", "dbo.Topics", "Id");
        }
    }
}
