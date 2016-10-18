namespace Trabalho.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ListaTarefas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Cor = c.String(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Tarefas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Data = c.DateTime(nullable: false),
                        Feito = c.Boolean(nullable: false),
                        ListaTarefaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ListaTarefas", t => t.ListaTarefaId, cascadeDelete: true)
                .Index(t => t.ListaTarefaId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        Email = c.String(nullable: false, maxLength: 50),
                        Senha = c.String(nullable: false),
                        facebook_token = c.String(),
                    })
                .PrimaryKey(t => t.Id);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ListaTarefas", "UserId", "dbo.Users");
            DropForeignKey("dbo.Tarefas", "ListaTarefaId", "dbo.ListaTarefas");
            DropIndex("dbo.Tarefas", new[] { "ListaTarefaId" });
            DropIndex("dbo.ListaTarefas", new[] { "UserId" });
            DropTable("dbo.Users");
            DropTable("dbo.Tarefas");
            DropTable("dbo.ListaTarefas");
        }
    }
}
