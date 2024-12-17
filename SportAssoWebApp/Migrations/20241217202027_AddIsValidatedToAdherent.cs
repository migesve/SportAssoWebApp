using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportAssoWebApp.Migrations
{
    /// <inheritdoc />
    public partial class AddIsValidatedToAdherent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
        name: "IsValidated",
        table: "Adherents",
        type: "bit",
        nullable: false,
        defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
