using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocativeApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOwnerToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Tenants_TenantId",
                table: "Properties");

            migrationBuilder.DropIndex(
                name: "IX_Properties_TenantId",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Owners");

            migrationBuilder.AddColumn<string>(
                name: "OwnerId1",
                table: "Properties",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Owners",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Owners",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Owners",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "MaxProperties",
                table: "Owners",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaxUsers",
                table: "Owners",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Plan",
                table: "Owners",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_OwnerId1",
                table: "Properties",
                column: "OwnerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Owners_OwnerId1",
                table: "Properties",
                column: "OwnerId1",
                principalTable: "Owners",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Owners_OwnerId1",
                table: "Properties");

            migrationBuilder.DropIndex(
                name: "IX_Properties_OwnerId1",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "OwnerId1",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Owners");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Owners");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Owners");

            migrationBuilder.DropColumn(
                name: "MaxProperties",
                table: "Owners");

            migrationBuilder.DropColumn(
                name: "MaxUsers",
                table: "Owners");

            migrationBuilder.DropColumn(
                name: "Plan",
                table: "Owners");

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "Properties",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Owners",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Properties_TenantId",
                table: "Properties",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Tenants_TenantId",
                table: "Properties",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id");
        }
    }
}
