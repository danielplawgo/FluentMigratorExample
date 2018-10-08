using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;

namespace FluentMigratorExample.Migrator.Migrations
{
    [Migration(201810030605)]
    public class AddProduct : Migration
    {
        public override void Up()
        {
            Create.Table("Products")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Name").AsString()
                .WithColumn("Category").AsString();

            Insert.IntoTable("Products").Row(new {Name = "Product 1.1", Category = "Category 1" });
            Insert.IntoTable("Products").Row(new {Name = "Product 1.2", Category = "Category 1" });
            Insert.IntoTable("Products").Row(new {Name = "Product 1.3", Category = "Category 1" });
            Insert.IntoTable("Products").Row(new {Name = "Product 2.1", Category = "Category 2" });
            Insert.IntoTable("Products").Row(new {Name = "Product 2.2", Category = "Category 2" });
        }

        public override void Down()
        {
            Delete.Table("Products");
        }
    }
}
