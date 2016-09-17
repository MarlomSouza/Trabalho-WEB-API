namespace Trabalho.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class teste : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tarefas", "User_Id", "dbo.Users");
            DropIndex("dbo.Tarefas", new[] { "User_Id" });
            RenameColumn(table: "dbo.Tarefas", name: "User_Id", newName: "UserId");
            AlterColumn("dbo.Tarefas", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Tarefas", "UserId");
            AddForeignKey("dbo.Tarefas", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tarefas", "UserId", "dbo.Users");
            DropIndex("dbo.Tarefas", new[] { "UserId" });
            AlterColumn("dbo.Tarefas", "UserId", c => c.Int());
            RenameColumn(table: "dbo.Tarefas", name: "UserId", newName: "User_Id");
            CreateIndex("dbo.Tarefas", "User_Id");
            AddForeignKey("dbo.Tarefas", "User_Id", "dbo.Users", "Id");
        }
    }
}
