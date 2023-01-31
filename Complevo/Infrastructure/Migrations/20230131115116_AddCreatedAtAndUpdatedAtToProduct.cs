using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Complevo.Infrastructure.Migrations
{
  /// <inheritdoc />
  public partial class AddCreatedAtAndUpdatedAtToProduct : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AlterColumn<string>(
          name: "Description",
          table: "Products",
          type: "nvarchar(max)",
          nullable: true,
          oldClrType: typeof(string),
          oldType: "nvarchar(max)");

      migrationBuilder.AddColumn<DateTime>(
          name: "CreatedAt",
          table: "Products",
          type: "datetime2",
          nullable: false,
          defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

      migrationBuilder.AddColumn<DateTime>(
          name: "UpdatedAt",
          table: "Products",
          type: "datetime2",
          nullable: false,
          defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropColumn(
          name: "CreatedAt",
          table: "Products");

      migrationBuilder.DropColumn(
          name: "UpdatedAt",
          table: "Products");

      migrationBuilder.AlterColumn<string>(
          name: "Description",
          table: "Products",
          type: "nvarchar(max)",
          nullable: false,
          defaultValue: "",
          oldClrType: typeof(string),
          oldType: "nvarchar(max)",
          oldNullable: true);
    }
  }
}
