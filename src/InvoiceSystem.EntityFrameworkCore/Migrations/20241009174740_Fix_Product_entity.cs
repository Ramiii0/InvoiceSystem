using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceSystem.Migrations
{
    /// <inheritdoc />
    public partial class Fix_Product_entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AppProductsPricing_ProductId",
                table: "AppProductsPricing");

            migrationBuilder.DropIndex(
                name: "IX_AppProductsDiscount_ProductId",
                table: "AppProductsDiscount");

            migrationBuilder.CreateIndex(
                name: "IX_AppProductsPricing_ProductId",
                table: "AppProductsPricing",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_AppProductsDiscount_ProductId",
                table: "AppProductsDiscount",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AppProductsPricing_ProductId",
                table: "AppProductsPricing");

            migrationBuilder.DropIndex(
                name: "IX_AppProductsDiscount_ProductId",
                table: "AppProductsDiscount");

            migrationBuilder.CreateIndex(
                name: "IX_AppProductsPricing_ProductId",
                table: "AppProductsPricing",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppProductsDiscount_ProductId",
                table: "AppProductsDiscount",
                column: "ProductId",
                unique: true);
        }
    }
}
