namespace GeeksForLessForum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adduserIdtotables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "UserID", c => c.String());
            AddColumn("dbo.Topics", "UserID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Topics", "UserID");
            DropColumn("dbo.Comments", "UserID");
        }
    }
}
