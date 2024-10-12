using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceSystem.Migrations
{
    /// <inheritdoc />
    public partial class Change_discountvalue_to_Totaldiscount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DiscountValue",
                table: "AppInvoiceItems",
                newName: "TotalDiscount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalDiscount",
                table: "AppInvoiceItems",
                newName: "DiscountValue");
        }
    }
}
