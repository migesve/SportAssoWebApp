using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportAssoWebApp.Migrations
{
    /// <inheritdoc />
    public partial class AddDateAndHourToCreneau : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
        name: "Date",
        table: "Creneaux",
        type: "date",
        nullable: false,
        defaultValue: DateTime.Now);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Hour",
                table: "Creneaux",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "Date", table: "Creneaux");
            migrationBuilder.DropColumn(name: "Hour", table: "Creneaux");
        }
    }
}
