namespace GeeksForLessForum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtableComment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(maxLength: 150),
                        CommentedDate = c.DateTime(nullable: false),
                        Topics_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Topics", t => t.Topics_Id)
                .Index(t => t.Topics_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "Topics_Id", "dbo.Topics");
            DropIndex("dbo.Comments", new[] { "Topics_Id" });
            DropTable("dbo.Comments");
        }
    }
}
