namespace GeeksForLessForum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addrequiredtoTopicmodel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Topics", "Header", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.Topics", "Body", c => c.String(nullable: false, maxLength: 1000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Topics", "Body", c => c.String(maxLength: 1000));
            AlterColumn("dbo.Topics", "Header", c => c.String(maxLength: 40));
        }
    }
}
