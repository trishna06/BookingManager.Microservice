using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingManager.Infrastructure.Migrations
{
    public partial class INITIAL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BM_BOOKING",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USER_NAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ROOM_LINK_ID = table.Column<int>(type: "int", nullable: false),
                    START_DATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    END_DATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    STATUS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IS_DELETED = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CREATED_DATE_TIME = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CREATED_BY = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CREATED_BY_USERNAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MODIFIED_DATE_TIME = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MODIFIED_BY = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MODIFIED_BY_USERNAME = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BM_BOOKING", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BM_BOOKING");
        }
    }
}
