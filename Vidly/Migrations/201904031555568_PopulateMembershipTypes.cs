namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateMembershipTypes : DbMigration
    {
        public override void Up()
        {
            Sql("insert into MembershipTypes (Id, Name, SignUpFee, DurationInMonths, DiscountRate) values (1,'Pay As You Go',0,0,0);");
            Sql("insert into MembershipTypes (Id, Name, SignUpFee, DurationInMonths, DiscountRate) values (2,'Monthly',30,1,10);");
            Sql("insert into MembershipTypes (Id, Name, SignUpFee, DurationInMonths, DiscountRate) values (3,'Quarterly',90,4,15);");
            Sql("insert into MembershipTypes (Id, Name, SignUpFee, DurationInMonths, DiscountRate) values (4,'Yearly',300,12,20);");
        }
        
        public override void Down()
        {
        }
    }
}
