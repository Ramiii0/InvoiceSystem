using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceSystem.Migrations
{
    /// <inheritdoc />
    public partial class edit_product_discount : Migration
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
                column: "ProductId");
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
                column: "ProductId",
                unique: true);
        }
    }
}
