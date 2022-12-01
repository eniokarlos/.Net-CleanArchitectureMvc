using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchMvc.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {


            mb.InsertData(
                table: "Products",
                columns: new[] { 
                    "Name", "Description", 
                    "Price","Stock", "Image", "CategoryId"},
                values: new object[,]
                {
                    { "Caderno Pequeno", "Caderno 100 folhas",
                    10.5, 20, "Caderno.jpg", 1},

                    { "Calculadora", "Calculadora comum",
                    5.00, 10, "Calculadora.jpg", 2},

                    { "Lapiseira", "Lapiseira 0.7",
                    2.50, 40, "Lapiseira.jpg", 3}
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete FROM Products");
        }
    }
}
