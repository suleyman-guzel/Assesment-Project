using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Contact_Microservice.Migrations
{
    public partial class contactpg_init : Migration
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
                values: new object[,]
                {
                    { new Guid("0cb78979-5935-4793-be77-3aae7cdc7c4a"), "Futbol a.ş", "Burak", "Yılmaz" },
                    { new Guid("24fb9dd1-9ca7-4bed-a95b-1dae06d4a4bb"), "Oyuncu a.ş", "Elçin", "Sangu" },
                    { new Guid("2b513153-af53-4257-926f-860cb14d6813"), "Oyuncu a.ş", "Ece", "Uslu" },
                    { new Guid("3874e9c0-8a55-49db-9c0f-88ce17372086"), "Oyuncu a.ş", "Mehmet", "Özgür" },
                    { new Guid("4e5a3699-ee36-444c-a203-50248a448d21"), "Şarkıcı a.ş", "Mahmut", "Tuncer" },
                    { new Guid("79a1e27a-629a-474e-bbcb-362274b65104"), "Şarkıcı a.ş", "Müslüm", "Gürses" },
                    { new Guid("9f17228d-020a-4a38-a2d3-8793625f79a6"), "Oyuncu a.ş", "Selin", "Şekerci" },
                    { new Guid("c80cbe29-d732-4fae-b6c2-b73ad8334faf"), "Şarkıcı a.ş", "İbrahim", "Tatlıses" },
                    { new Guid("e2f7b429-aa1d-4136-b412-523dce964993"), "Sanatcı a.ş", "Levent", "Yüksel" }
                });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "ContactType", "Contents", "PersonUID" },
                values: new object[,]
                {
                    { new Guid("0dd5d6e5-1c40-4eff-8ff0-f35a82d83ac5"), 1, "05417778899", new Guid("c80cbe29-d732-4fae-b6c2-b73ad8334faf") },
                    { new Guid("16551247-f3b5-4418-aa90-c41d8759ee33"), 3, "izmir", new Guid("9f17228d-020a-4a38-a2d3-8793625f79a6") },
                    { new Guid("39deb96f-c6d2-455a-9357-54084b384983"), 3, "izmir", new Guid("2b513153-af53-4257-926f-860cb14d6813") },
                    { new Guid("5a087447-cb5f-41f9-a621-29a39c499d84"), 1, "05415555555", new Guid("0cb78979-5935-4793-be77-3aae7cdc7c4a") },
                    { new Guid("64253f97-69b0-4cd3-bcc4-378fbb0f4955"), 3, "urfa", new Guid("79a1e27a-629a-474e-bbcb-362274b65104") },
                    { new Guid("7207209e-3af0-419b-813f-8508ad5c5147"), 1, "05413332211", new Guid("9f17228d-020a-4a38-a2d3-8793625f79a6") },
                    { new Guid("896233ef-6883-4bcc-8210-1d8a0454ffba"), 3, "antalya", new Guid("0cb78979-5935-4793-be77-3aae7cdc7c4a") },
                    { new Guid("a27d7023-d9a5-4b7e-b1d3-b51b855cb0f3"), 1, "05411112233", new Guid("24fb9dd1-9ca7-4bed-a95b-1dae06d4a4bb") },
                    { new Guid("a31aca8d-272f-4066-8f81-c6eb59af1705"), 3, "izmir", new Guid("24fb9dd1-9ca7-4bed-a95b-1dae06d4a4bb") },
                    { new Guid("af20dc4e-1d0c-4c56-9a97-779a25574acb"), 3, "urfa", new Guid("4e5a3699-ee36-444c-a203-50248a448d21") },
                    { new Guid("b61022c3-0ad2-45d7-88e4-dd0ec68d44bd"), 3, "urfa", new Guid("c80cbe29-d732-4fae-b6c2-b73ad8334faf") },
                    { new Guid("ca32f3b0-3fe9-40ef-939f-14c9c403e8c0"), 3, "antalya", new Guid("e2f7b429-aa1d-4136-b412-523dce964993") },
                    { new Guid("caa57e29-cde4-491f-b221-60a016e6de99"), 3, "antalya", new Guid("3874e9c0-8a55-49db-9c0f-88ce17372086") },
                    { new Guid("d4bef437-2fd6-4d8a-bf86-d2d276c68139"), 1, "05414414444", new Guid("0cb78979-5935-4793-be77-3aae7cdc7c4a") },
                    { new Guid("f7f4936f-a3d3-4c2e-8dd9-27e18d471218"), 1, "05414414444", new Guid("e2f7b429-aa1d-4136-b412-523dce964993") },
                    { new Guid("fd5df557-6d12-48c1-9a93-fa195e14548a"), 1, "05415552233", new Guid("3874e9c0-8a55-49db-9c0f-88ce17372086") }
                });

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
