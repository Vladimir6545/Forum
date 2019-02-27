namespace GeeksForLessForum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addclassComment : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Topics", "TopicMessages_Id", "dbo.TopicMessages");
            DropIndex("dbo.Topics", new[] { "TopicMessages_Id" });
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(maxLength: 150),
                        CommentedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Topics", "Comments_Id", c => c.Int());
            CreateIndex("dbo.Topics", "Comments_Id");
            AddForeignKey("dbo.Topics", "Comments_Id", "dbo.Comments", "Id");
            DropColumn("dbo.Topics", "TopicMessages_Id");
            DropTable("dbo.TopicMessages");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TopicMessages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Topics", "TopicMessages_Id", c => c.Int());
            DropForeignKey("dbo.Topics", "Comments_Id", "dbo.Comments");
            DropIndex("dbo.Topics", new[] { "Comments_Id" });
            DropColumn("dbo.Topics", "Comments_Id");
            DropTable("dbo.Comments");
            CreateIndex("dbo.Topics", "TopicMessages_Id");
            AddForeignKey("dbo.Topics", "TopicMessages_Id", "dbo.TopicMessages", "Id");
        }
    }
}
