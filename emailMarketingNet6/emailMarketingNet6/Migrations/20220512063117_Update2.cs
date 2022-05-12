using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace emailMarketingNet6.Migrations
{
    public partial class Update2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Campaigns_Emails_EmailSenderId",
                table: "Campaigns");

            migrationBuilder.DropIndex(
                name: "IX_Campaigns_EmailSenderId",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "EmailSenderId",
                table: "Campaigns");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Contacts",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_EmailSendId",
                table: "Campaigns",
                column: "EmailSendId");

            migrationBuilder.AddForeignKey(
                name: "FK_Campaigns_Emails_EmailSendId",
                table: "Campaigns",
                column: "EmailSendId",
                principalTable: "Emails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Campaigns_Emails_EmailSendId",
                table: "Campaigns");

            migrationBuilder.DropIndex(
                name: "IX_Campaigns_EmailSendId",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Contacts");

            migrationBuilder.AddColumn<int>(
                name: "EmailSenderId",
                table: "Campaigns",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_EmailSenderId",
                table: "Campaigns",
                column: "EmailSenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Campaigns_Emails_EmailSenderId",
                table: "Campaigns",
                column: "EmailSenderId",
                principalTable: "Emails",
                principalColumn: "Id");
        }
    }
}
