using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserServiceAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixLoginResponseTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Token",
                table: "LoginResponse",
                type: "character varying(1255)",
                maxLength: 1255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Token",
                table: "LoginResponse",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(1255)",
                oldMaxLength: 1255);
        }
    }
}
