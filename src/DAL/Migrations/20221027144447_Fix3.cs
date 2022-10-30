using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class Fix3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CollectionImageUrl",
                table: "Collections");

            migrationBuilder.AddColumn<int>(
                name: "CollectionImageId",
                table: "Collections",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CollectionImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublicId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CollectionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectionImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CollectionImages_Collections_CollectionId",
                        column: x => x.CollectionId,
                        principalTable: "Collections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollectionImages_CollectionId",
                table: "CollectionImages",
                column: "CollectionId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollectionImages");

            migrationBuilder.DropColumn(
                name: "CollectionImageId",
                table: "Collections");

            migrationBuilder.AddColumn<string>(
                name: "CollectionImageUrl",
                table: "Collections",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
