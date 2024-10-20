using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Prototype.Migrations
{
    /// <inheritdoc />
    public partial class Messages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entry_Threads_MessageThreadId",
                table: "Entry");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Entry",
                table: "Entry");

            migrationBuilder.RenameTable(
                name: "Entry",
                newName: "Messages");

            migrationBuilder.RenameIndex(
                name: "IX_Entry_MessageThreadId",
                table: "Messages",
                newName: "IX_Messages_MessageThreadId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Messages",
                table: "Messages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Threads_MessageThreadId",
                table: "Messages",
                column: "MessageThreadId",
                principalTable: "Threads",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Threads_MessageThreadId",
                table: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Messages",
                table: "Messages");

            migrationBuilder.RenameTable(
                name: "Messages",
                newName: "Entry");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_MessageThreadId",
                table: "Entry",
                newName: "IX_Entry_MessageThreadId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Entry",
                table: "Entry",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Entry_Threads_MessageThreadId",
                table: "Entry",
                column: "MessageThreadId",
                principalTable: "Threads",
                principalColumn: "Id");
        }
    }
}
