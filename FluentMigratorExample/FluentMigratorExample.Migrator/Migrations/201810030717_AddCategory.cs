using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;

namespace FluentMigratorExample.Migrator.Migrations
{
    [Migration(201810030717)]
    public class AddCategory : Migration
    {
        public override void Up()
        {
            Create.Table("Categories")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Name").AsString();

            Execute.Sql("INSERT INTO dbo.Categories SELECT DISTINCT Category FROM dbo.Products;");

            Alter.Table("Products")
                .AddColumn("CategoryId")
                .AsInt32()
                .Nullable();

            Execute.Sql("UPDATE p SET p.CategoryId = (SELECT c.Id FROM dbo.Categories c WHERE c.Name = p.Category) FROM dbo.Products p;");

            Alter.Column("CategoryId")
                .OnTable("Products")
                .AsInt32()
                .NotNullable()
                .ForeignKey("Categories", "Id")
                .Indexed();

            Delete.Column("Category")
                .FromTable("Products");
        }

        public override void Down()
        {
            Alter.Table("Products")
                .AddColumn("Category")
                .AsString()
                .Nullable();

            Execute.Sql("UPDATE p SET p.Category = (SELECT c.Name FROM dbo.Categories c WHERE c.Id = p.CategoryId) FROM dbo.Products p;");

            Alter.Column("Category")
                .OnTable("Products")
                .AsString()
                .NotNullable();

            Delete.ForeignKey()
                .FromTable("Products")
                .ForeignColumn("CategoryId")
                .ToTable("Categories")
                .PrimaryColumn("Id");

            Delete.Index()
                .OnTable("Products")
                .OnColumn("CategoryId");

            Delete.Column("CategoryId")
                .FromTable("Products");

            Delete.Table("Categories");
        }
    }
}
