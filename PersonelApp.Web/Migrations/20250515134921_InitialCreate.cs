using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonelApp.Web.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Abds",
                columns: table => new
                {
                    AbdId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AbdName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abds", x => x.AbdId);
                });

            migrationBuilder.CreateTable(
                name: "Bolums",
                columns: table => new
                {
                    BolumId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BolumName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bolums", x => x.BolumId);
                });

            migrationBuilder.CreateTable(
                name: "Fakultes",
                columns: table => new
                {
                    FakulteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FakulteName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fakultes", x => x.FakulteId);
                });

            migrationBuilder.CreateTable(
                name: "Izinler",
                columns: table => new
                {
                    IzinBilgisiId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BaslangicTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BitisTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Kullanilanizin = table.Column<int>(type: "int", nullable: false),
                    PersonelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Izinler", x => x.IzinBilgisiId);
                });

            migrationBuilder.CreateTable(
                name: "AbdBolum",
                columns: table => new
                {
                    AbdListAbdId = table.Column<int>(type: "int", nullable: false),
                    BolumListBolumId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbdBolum", x => new { x.AbdListAbdId, x.BolumListBolumId });
                    table.ForeignKey(
                        name: "FK_AbdBolum_Abds_AbdListAbdId",
                        column: x => x.AbdListAbdId,
                        principalTable: "Abds",
                        principalColumn: "AbdId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AbdBolum_Bolums_BolumListBolumId",
                        column: x => x.BolumListBolumId,
                        principalTable: "Bolums",
                        principalColumn: "BolumId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BolumFakulte",
                columns: table => new
                {
                    BolumListBolumId = table.Column<int>(type: "int", nullable: false),
                    FakulteListFakulteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BolumFakulte", x => new { x.BolumListBolumId, x.FakulteListFakulteId });
                    table.ForeignKey(
                        name: "FK_BolumFakulte_Bolums_BolumListBolumId",
                        column: x => x.BolumListBolumId,
                        principalTable: "Bolums",
                        principalColumn: "BolumId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BolumFakulte_Fakultes_FakulteListFakulteId",
                        column: x => x.FakulteListFakulteId,
                        principalTable: "Fakultes",
                        principalColumn: "FakulteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Personels",
                columns: table => new
                {
                    PersonelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KimlikNo = table.Column<long>(type: "bigint", nullable: false),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Soyad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Zaman = table.Column<DateOnly>(type: "date", nullable: false),
                    GecenYıl = table.Column<int>(type: "int", nullable: false),
                    BuYıl = table.Column<int>(type: "int", nullable: false),
                    FakulteId = table.Column<int>(type: "int", nullable: false),
                    BolumId = table.Column<int>(type: "int", nullable: false),
                    AbdId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personels", x => x.PersonelId);
                    table.ForeignKey(
                        name: "FK_Personels_Abds_AbdId",
                        column: x => x.AbdId,
                        principalTable: "Abds",
                        principalColumn: "AbdId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Personels_Bolums_BolumId",
                        column: x => x.BolumId,
                        principalTable: "Bolums",
                        principalColumn: "BolumId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Personels_Fakultes_FakulteId",
                        column: x => x.FakulteId,
                        principalTable: "Fakultes",
                        principalColumn: "FakulteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AbdBolum_BolumListBolumId",
                table: "AbdBolum",
                column: "BolumListBolumId");

            migrationBuilder.CreateIndex(
                name: "IX_BolumFakulte_FakulteListFakulteId",
                table: "BolumFakulte",
                column: "FakulteListFakulteId");

            migrationBuilder.CreateIndex(
                name: "IX_Personels_AbdId",
                table: "Personels",
                column: "AbdId");

            migrationBuilder.CreateIndex(
                name: "IX_Personels_BolumId",
                table: "Personels",
                column: "BolumId");

            migrationBuilder.CreateIndex(
                name: "IX_Personels_FakulteId",
                table: "Personels",
                column: "FakulteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AbdBolum");

            migrationBuilder.DropTable(
                name: "BolumFakulte");

            migrationBuilder.DropTable(
                name: "Izinler");

            migrationBuilder.DropTable(
                name: "Personels");

            migrationBuilder.DropTable(
                name: "Abds");

            migrationBuilder.DropTable(
                name: "Bolums");

            migrationBuilder.DropTable(
                name: "Fakultes");
        }
    }
}
