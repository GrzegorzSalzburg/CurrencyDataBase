namespace CurrencyDataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LabUpdate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Currency_Base",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Index = c.Int(nullable: false),
                        currencyName = c.String(),
                        rate = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Currency_Base");
        }
    }
}
