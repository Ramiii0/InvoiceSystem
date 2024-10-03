using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceSystem.Migrations
{
    /// <inheritdoc />
    public partial class Remove_Disc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AppProductsDiscount_ProductId",
                table: "AppProductsDiscount");

            migrationBuilder.CreateIndex(
                name: "IX_AppProductsDiscount_ProductId",
                table: "AppProductsDiscount",
                column: "ProductId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AppProductsDiscount_ProductId",
                table: "AppProductsDiscount");

            migrationBuilder.CreateIndex(
                name: "IX_AppProductsDiscount_ProductId",
                table: "AppProductsDiscount",
                column: "ProductId");
        }
    }
}
