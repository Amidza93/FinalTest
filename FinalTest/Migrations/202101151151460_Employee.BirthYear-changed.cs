namespace FinalTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployeeBirthYearchanged : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "BirthYear", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "BirthYear", c => c.Int(nullable: false));
        }
    }
}
