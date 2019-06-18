namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNumberAvalibleToMovie : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "NumberAvalible", c => c.Short(nullable: false));

            Sql("UPDATE [Movies] SET [NumberAvalible] = [NumberInStock]");
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movies", "NumberAvalible");
        }
    }
}
