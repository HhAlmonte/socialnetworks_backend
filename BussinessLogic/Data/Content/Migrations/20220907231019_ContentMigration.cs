using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BussinessLogic.Data.Content.Migrations
{
    public partial class ContentMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PublicationsEntities",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    VideoUrl = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicationsEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CommentsEntities",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    VideoUrl = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    PublicationId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentsEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentsEntities_PublicationsEntities_PublicationId",
                        column: x => x.PublicationId,
                        principalTable: "PublicationsEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnswersEntities",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    VideoUrl = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CommentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CommentsEntitiesId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswersEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnswersEntities_CommentsEntities_CommentId",
                        column: x => x.CommentId,
                        principalTable: "CommentsEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnswersEntities_CommentsEntities_CommentsEntitiesId",
                        column: x => x.CommentsEntitiesId,
                        principalTable: "CommentsEntities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnswersEntities_CommentId",
                table: "AnswersEntities",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswersEntities_CommentsEntitiesId",
                table: "AnswersEntities",
                column: "CommentsEntitiesId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentsEntities_PublicationId",
                table: "CommentsEntities",
                column: "PublicationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnswersEntities");

            migrationBuilder.DropTable(
                name: "CommentsEntities");

            migrationBuilder.DropTable(
                name: "PublicationsEntities");
        }
    }
}
