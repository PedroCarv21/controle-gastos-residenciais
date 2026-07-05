using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResidentialExpenseControl.Api.Migrations
{
    /// <inheritdoc />
    public partial class RenameAmountToValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Transactions",
                newName: "Value");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Value",
                table: "Transactions",
                newName: "Amount");
        }
    }
}
