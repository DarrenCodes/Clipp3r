using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Clipp3r.Infrastructure.EntityFramework.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VideoMetadatas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    VideoFileName = table.Column<string>(maxLength: 255, nullable: false),
                    LastClippedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoMetadatas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VideoMoments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    VideoMomentName = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoMoments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VideoMomentCaptures",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    VideoMetadataGuid = table.Column<Guid>(nullable: false),
                    VideoMomentGuid = table.Column<Guid>(nullable: false),
                    VideoMomentCaptureTime = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoMomentCaptures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VideoMomentCaptures_VideoMetadatas_VideoMetadataGuid",
                        column: x => x.VideoMetadataGuid,
                        principalTable: "VideoMetadatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VideoMomentCaptures_VideoMoments_VideoMomentGuid",
                        column: x => x.VideoMomentGuid,
                        principalTable: "VideoMoments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VideoMomentCaptures_VideoMetadataGuid",
                table: "VideoMomentCaptures",
                column: "VideoMetadataGuid");

            migrationBuilder.CreateIndex(
                name: "IX_VideoMomentCaptures_VideoMomentGuid",
                table: "VideoMomentCaptures",
                column: "VideoMomentGuid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VideoMomentCaptures");

            migrationBuilder.DropTable(
                name: "VideoMetadatas");

            migrationBuilder.DropTable(
                name: "VideoMoments");
        }
    }
}
