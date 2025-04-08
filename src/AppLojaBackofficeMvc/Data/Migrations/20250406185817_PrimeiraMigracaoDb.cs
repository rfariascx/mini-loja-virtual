using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppLojaBackofficeMvc.Data.Migrations
{
    /// <inheritdoc />
    public partial class PrimeiraMigracaoDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    CategoriaId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CategoriaDescricao = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.CategoriaId);
                });

            migrationBuilder.CreateTable(
                name: "Vendedores",
                columns: table => new
                {
                    VendedorId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VendedorNomeCompleto = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    VendedorEmail = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendedores", x => x.VendedorId);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    ProdutoId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProdutoDescricao = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false),
                    ProdutoImagem = table.Column<string>(type: "TEXT", nullable: true),
                    ProdutoPreco = table.Column<decimal>(type: "TEXT", nullable: false),
                    ProdutoEstoque = table.Column<int>(type: "INTEGER", nullable: false),
                    CategoriaId = table.Column<int>(type: "INTEGER", nullable: false),
                    VendedorId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.ProdutoId);
                    table.ForeignKey(
                        name: "FK_Produtos_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "CategoriaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_CategoriaId",
                table: "Produtos",
                column: "CategoriaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "Vendedores");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
