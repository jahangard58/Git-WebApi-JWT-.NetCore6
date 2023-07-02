using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecendWebAppi.Migrations
{
    /// <inheritdoc />
    public partial class addTblTruck : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Trucks",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Barcode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    PelakPart1 = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true),
                    PelakPart2 = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    PelakPart3 = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: true),
                    PelakPart4 = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true),
                    SmartCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ShasiNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IRCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OwnerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OwnerMobile = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OwnerMelliCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BlackListFlag = table.Column<bool>(type: "bit", nullable: false),
                    BlackListDescr = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trucks", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trucks");
        }
    }
}
