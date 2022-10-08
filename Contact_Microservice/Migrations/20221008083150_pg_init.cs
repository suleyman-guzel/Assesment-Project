using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Contact_Microservice.Migrations
{
    public partial class pg_init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    SurName = table.Column<string>(type: "text", nullable: false),
                    Company = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ContactType = table.Column<int>(type: "integer", nullable: false),
                    Contents = table.Column<string>(type: "text", nullable: false),
                    PersonUID = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contacts_Persons_PersonUID",
                        column: x => x.PersonUID,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "Company", "Name", "SurName" },
                values: new object[] { new Guid("dc661f81-ddcb-473f-9658-bdac1db4b6fb"), "gozili a.ş1", "süleyman", "güzel" });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "ContactType", "Contents", "PersonUID" },
                values: new object[] { new Guid("747c94fa-61c4-4524-9b7e-3e180b44f961"), 1, "12312312312", new Guid("dc661f81-ddcb-473f-9658-bdac1db4b6fb") });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_PersonUID",
                table: "Contacts",
                column: "PersonUID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
