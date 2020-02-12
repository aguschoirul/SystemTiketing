namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addforeignkey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TB_M_Division", "EmployeeVM_id", "dbo.TB_M_Employee");
            DropForeignKey("dbo.TB_M_Division", "SubmissionVM_id", "dbo.TB_M_Submission");
            DropIndex("dbo.TB_M_Division", new[] { "EmployeeVM_id" });
            DropIndex("dbo.TB_M_Division", new[] { "SubmissionVM_id" });
            CreateTable(
                "dbo.EmployeeVMDivisionVMs",
                c => new
                    {
                        EmployeeVM_id = c.Int(nullable: false),
                        DivisionVM_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.EmployeeVM_id, t.DivisionVM_id })
                .ForeignKey("dbo.TB_M_Employee", t => t.EmployeeVM_id, cascadeDelete: true)
                .ForeignKey("dbo.TB_M_Division", t => t.DivisionVM_id, cascadeDelete: true)
                .Index(t => t.EmployeeVM_id)
                .Index(t => t.DivisionVM_id);
            
            DropColumn("dbo.TB_M_Division", "EmployeeVM_id");
            DropColumn("dbo.TB_M_Division", "SubmissionVM_id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TB_M_Division", "SubmissionVM_id", c => c.Int());
            AddColumn("dbo.TB_M_Division", "EmployeeVM_id", c => c.Int());
            DropForeignKey("dbo.EmployeeVMDivisionVMs", "DivisionVM_id", "dbo.TB_M_Division");
            DropForeignKey("dbo.EmployeeVMDivisionVMs", "EmployeeVM_id", "dbo.TB_M_Employee");
            DropIndex("dbo.EmployeeVMDivisionVMs", new[] { "DivisionVM_id" });
            DropIndex("dbo.EmployeeVMDivisionVMs", new[] { "EmployeeVM_id" });
            DropTable("dbo.EmployeeVMDivisionVMs");
            CreateIndex("dbo.TB_M_Division", "SubmissionVM_id");
            CreateIndex("dbo.TB_M_Division", "EmployeeVM_id");
            AddForeignKey("dbo.TB_M_Division", "SubmissionVM_id", "dbo.TB_M_Submission", "id");
            AddForeignKey("dbo.TB_M_Division", "EmployeeVM_id", "dbo.TB_M_Employee", "id");
        }
    }
}
