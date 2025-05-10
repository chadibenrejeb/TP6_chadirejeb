using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SchoolAPI.Migrations
{
    /// <inheritdoc />
    public partial class SecondCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "WebSite",
                table: "Schools",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Schools",
                columns: new[] { "Id", "Director", "Name", "Rating", "Sections", "WebSite" },
                values: new object[,]
                {
                    { 1, "AC-COLL", "ENISO", 3.5, "IA, GTE, GMP", "http://www.eniso.ruu.tn" },
                    { 2, "B-RAMI", "ENTM", 4.0, "Mécanique, Aéronautique, Informatique", "http://www.entm.tn" },
                    { 3, "K-JEMAA", "ESI", 4.2000000000000002, "Informatique, Réseaux, Télécoms", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Schools",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Schools",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Schools",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AlterColumn<string>(
                name: "WebSite",
                table: "Schools",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
