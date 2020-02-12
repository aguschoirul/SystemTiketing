namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deletepicmodels : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TB_M_Employee", "PICVM_id", "dbo.TB_M_PIC");
            DropForeignKey("dbo.TB_M_PIC", "SolvingVM_id", "dbo.TB_M_Solving");
            DropIndex("dbo.TB_M_Employee", new[] { "PICVM_id" });
            DropIndex("dbo.TB_M_PIC", new[] { "SolvingVM_id" });
            DropColumn("dbo.TB_M_Employee", "PICVM_id");
            DropTable("dbo.TB_M_PIC");
            DropTable("dbo.TB_M_Solving");
        }
        
        public override void Down()
        {
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
            
            CreateTable(
                "dbo.TB_M_PIC",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        SolvingVM_id = c.Int(),
                    })
                .PrimaryKey(t => t.id);
            
            AddColumn("dbo.TB_M_Employee", "PICVM_id", c => c.Int());
            CreateIndex("dbo.TB_M_PIC", "SolvingVM_id");
            CreateIndex("dbo.TB_M_Employee", "PICVM_id");
            AddForeignKey("dbo.TB_M_PIC", "SolvingVM_id", "dbo.TB_M_Solving", "id");
            AddForeignKey("dbo.TB_M_Employee", "PICVM_id", "dbo.TB_M_PIC", "id");
        }
    }
}
