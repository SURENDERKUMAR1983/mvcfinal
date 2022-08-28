namespace mvcfinal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateCountryStateCity : DbMigration
    {
        public override void Up()
        {
            Sql("insert countries values('India')");
            Sql("insert countries values('USA')");
            Sql("insert countries values('UK')");
            Sql("insert states values('Punjab',1)");
            Sql("insert states values('Haryana',1)");
            Sql("insert states values('HP',1)");
            Sql("insert cities values('Mohali',1)");
            Sql("insert cities values('ASR',1)");
            Sql("insert cities values('LDH',1)");
            Sql("insert cities values('Ambala',2)");
            Sql("insert cities values('Hisar',2)");
            Sql("insert cities values('Kuk',2)");
        }
        
        public override void Down()
        {
        }
    }
}
