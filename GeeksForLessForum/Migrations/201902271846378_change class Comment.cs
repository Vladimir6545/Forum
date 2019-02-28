namespace GeeksForLessForum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeclassComment : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Topics", "Comments_Id", "dbo.Comments");
            DropIndex("dbo.Topics", new[] { "Comments_Id" });
            DropColumn("dbo.Topics", "Comments_Id");
            DropTable("dbo.Comments");
        }
        
        public override void Down()
        {
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
        }
    }
}
