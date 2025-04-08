using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppLojaBackofficeMvc.Data.Migrations
{
    /// <inheritdoc />
    public partial class AjusteVendedorIdParaString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "VendedorId",
                table: "Produtos",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "VendedorId",
                table: "Produtos",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }
    }
}
