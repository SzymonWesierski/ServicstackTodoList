using ServiceStack.OrmLite;

namespace MyApp.Migrations;

public class Migration1000 : MigrationBase
{
    public override void Up()
    {
        Db.CreateTable<MyApp.ServiceModel.Types.Todo>();
    }

    public override void Down()
    {
        //Commented due to servicestack free licence
        //The free-quota limit on '10 ServiceStack Operations' has been reached
        //Db.DropTable<MyApp.ServiceModel.Types.Todo>();
    }
}
