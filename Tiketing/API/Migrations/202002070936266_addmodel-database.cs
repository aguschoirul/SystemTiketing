namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmodeldatabase : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.TB_M_Login", newName: "TB_M_User");
            DropForeignKey("dbo.RoleVMLoginVMs", "RoleVM_id", "dbo.TB_M_Roles");
            DropForeignKey("dbo.RoleVMLoginVMs", "LoginVM_id", "dbo.TB_M_Login");
            DropIndex("dbo.RoleVMLoginVMs", new[] { "RoleVM_id" });
            DropIndex("dbo.RoleVMLoginVMs", new[] { "LoginVM_id" });
            CreateTable(
                "dbo.TB_M_Division",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        EmployeeVM_id = c.Int(),
                        SubmissionVM_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.TB_M_Employee", t => t.EmployeeVM_id)
                .ForeignKey("dbo.TB_M_Submission", t => t.SubmissionVM_id)
                .Index(t => t.EmployeeVM_id)
                .Index(t => t.SubmissionVM_id);
            
            CreateTable(
                "dbo.TB_M_PIC",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        SolvingVM_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.TB_M_Solving", t => t.SolvingVM_id)
                .Index(t => t.SolvingVM_id);
            
            CreateTable(
                "dbo.TB_R_Report",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        reportdate = c.DateTime(nullable: false),
                        details = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.TB_M_Submission",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        ReportVM_id = c.Int(),
                        SolvingVM_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.TB_R_Report", t => t.ReportVM_id)
                .ForeignKey("dbo.TB_M_Solving", t => t.SolvingVM_id)
                .Index(t => t.ReportVM_id)
                .Index(t => t.SolvingVM_id);
            
            CreateTable(
                "dbo.TB_M_Solving",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        detail = c.String(),
                        status = c.String(),
                        start_date = c.String(),
                        due_date = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            AddColumn("dbo.TB_M_Employee", "PICVM_id", c => c.Int());
            AddColumn("dbo.TB_M_User", "SubmissionVM_id", c => c.Int());
            AddColumn("dbo.TB_M_Ticket", "SubmissionVM_id", c => c.Int());
            CreateIndex("dbo.TB_M_Employee", "PICVM_id");
            CreateIndex("dbo.TB_M_Ticket", "SubmissionVM_id");
            CreateIndex("dbo.TB_M_User", "SubmissionVM_id");
            AddForeignKey("dbo.TB_M_Employee", "PICVM_id", "dbo.TB_M_PIC", "id");
            AddForeignKey("dbo.TB_M_Ticket", "SubmissionVM_id", "dbo.TB_M_Submission", "id");
            AddForeignKey("dbo.TB_M_User", "SubmissionVM_id", "dbo.TB_M_Submission", "id");
            DropColumn("dbo.TB_M_Ticket", "dateofsubmission");
            DropTable("dbo.RoleVMLoginVMs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RoleVMLoginVMs",
                c => new
                    {
                        RoleVM_id = c.Int(nullable: false),
                        LoginVM_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RoleVM_id, t.LoginVM_id });
            
            AddColumn("dbo.TB_M_Ticket", "dateofsubmission", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.TB_M_Submission", "SolvingVM_id", "dbo.TB_M_Solving");
            DropForeignKey("dbo.TB_M_PIC", "SolvingVM_id", "dbo.TB_M_Solving");
            DropForeignKey("dbo.TB_M_Submission", "ReportVM_id", "dbo.TB_R_Report");
            DropForeignKey("dbo.TB_M_User", "SubmissionVM_id", "dbo.TB_M_Submission");
            DropForeignKey("dbo.TB_M_Ticket", "SubmissionVM_id", "dbo.TB_M_Submission");
            DropForeignKey("dbo.TB_M_Division", "SubmissionVM_id", "dbo.TB_M_Submission");
            DropForeignKey("dbo.TB_M_Employee", "PICVM_id", "dbo.TB_M_PIC");
            DropForeignKey("dbo.TB_M_Division", "EmployeeVM_id", "dbo.TB_M_Employee");
            DropIndex("dbo.TB_M_User", new[] { "SubmissionVM_id" });
            DropIndex("dbo.TB_M_Ticket", new[] { "SubmissionVM_id" });
            DropIndex("dbo.TB_M_Submission", new[] { "SolvingVM_id" });
            DropIndex("dbo.TB_M_Submission", new[] { "ReportVM_id" });
            DropIndex("dbo.TB_M_PIC", new[] { "SolvingVM_id" });
            DropIndex("dbo.TB_M_Employee", new[] { "PICVM_id" });
            DropIndex("dbo.TB_M_Division", new[] { "SubmissionVM_id" });
            DropIndex("dbo.TB_M_Division", new[] { "EmployeeVM_id" });
            DropColumn("dbo.TB_M_Ticket", "SubmissionVM_id");
            DropColumn("dbo.TB_M_User", "SubmissionVM_id");
            DropColumn("dbo.TB_M_Employee", "PICVM_id");
            DropTable("dbo.TB_M_Solving");
            DropTable("dbo.TB_M_Submission");
            DropTable("dbo.TB_R_Report");
            DropTable("dbo.TB_M_PIC");
            DropTable("dbo.TB_M_Division");
            CreateIndex("dbo.RoleVMLoginVMs", "LoginVM_id");
            CreateIndex("dbo.RoleVMLoginVMs", "RoleVM_id");
            AddForeignKey("dbo.RoleVMLoginVMs", "LoginVM_id", "dbo.TB_M_Login", "id", cascadeDelete: true);
            AddForeignKey("dbo.RoleVMLoginVMs", "RoleVM_id", "dbo.TB_M_Roles", "id", cascadeDelete: true);
            RenameTable(name: "dbo.TB_M_User", newName: "TB_M_Login");
        }
    }
}
