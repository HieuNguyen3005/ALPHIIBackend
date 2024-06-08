using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ALPHII.Migrations
{
    /// <inheritdoc />
    public partial class InitDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VMTask_Images_ImageInputId",
                table: "VMTask");

            migrationBuilder.DropForeignKey(
                name: "FK_VMTask_Images_ImageMaskId",
                table: "VMTask");

            migrationBuilder.DropIndex(
                name: "IX_VMTask_ImageInputId",
                table: "VMTask");

            migrationBuilder.DropIndex(
                name: "IX_VMTask_ImageMaskId",
                table: "VMTask");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_VMTask_ImageInputId",
                table: "VMTask",
                column: "ImageInputId");

            migrationBuilder.CreateIndex(
                name: "IX_VMTask_ImageMaskId",
                table: "VMTask",
                column: "ImageMaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_VMTask_Images_ImageInputId",
                table: "VMTask",
                column: "ImageInputId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VMTask_Images_ImageMaskId",
                table: "VMTask",
                column: "ImageMaskId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
