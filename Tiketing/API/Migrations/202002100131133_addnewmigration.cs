namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addnewmigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TB_M_User", "SubmissionVM_id", "dbo.TB_M_Submission");
            DropIndex("dbo.TB_M_User", new[] { "SubmissionVM_id" });
            DropColumn("dbo.TB_M_User", "SubmissionVM_id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TB_M_User", "SubmissionVM_id", c => c.Int());
            CreateIndex("dbo.TB_M_User", "SubmissionVM_id");
            AddForeignKey("dbo.TB_M_User", "SubmissionVM_id", "dbo.TB_M_Submission", "id");
        }
    }
}
