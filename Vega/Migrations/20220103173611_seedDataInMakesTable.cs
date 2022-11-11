using Microsoft.EntityFrameworkCore.Migrations;

namespace Vega.Migrations
{
    public partial class seedDataInMakesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Makes",
                newName: "Makes",
                newSchema: "dbo");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "dbo",
                table: "Makes",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Makes",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Make1" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Makes",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Make2" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Makes",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Make3" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Makes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Makes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Makes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.RenameTable(
                name: "Makes",
                schema: "dbo",
                newName: "Makes");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Makes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);
        }
    }
}
