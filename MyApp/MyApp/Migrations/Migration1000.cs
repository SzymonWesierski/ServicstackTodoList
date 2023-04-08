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
        //Db.DropTable<MyApp.ServiceModel.Types.Todo>();
    }
}
