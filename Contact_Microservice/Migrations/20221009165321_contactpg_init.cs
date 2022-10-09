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
                    { new Guid("023e1635-8be6-45cc-ad7d-2800a3b679bc"), "Oyuncu a.ş", "Selin", "Şekerci" },
                    { new Guid("1f410483-38c3-48df-9a99-76dbb2bf0d1a"), "Şarkıcı a.ş", "Müslüm", "Gürses" },
                    { new Guid("409ba2b3-10a4-44eb-9193-f43d777b1e30"), "Şarkıcı a.ş", "Mahmut", "Tuncer" },
                    { new Guid("550ac5d8-5c79-4ab7-ad05-38f2ae7916b0"), "Oyuncu a.ş", "Ece", "Uslu" },
                    { new Guid("866c2580-7505-417e-8b2d-89be6d304335"), "Sanatcı a.ş", "Levent", "Yüksel" },
                    { new Guid("b58041ac-5714-4c93-98d5-a981f4754eed"), "Futbol a.ş", "Burak", "Yılmaz" },
                    { new Guid("bdc3f868-7ed1-4db0-ae35-9a7283bc26ce"), "Şarkıcı a.ş", "İbrahim", "Tatlıses" },
                    { new Guid("c026ed21-910a-4c55-9135-41a20e65dc07"), "Oyuncu a.ş", "Elçin", "Sangu" },
                    { new Guid("c7c24bf5-1097-4825-90c1-2f4233a3a2ce"), "Oyuncu a.ş", "Mehmet", "Özgür" }
                });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "ContactType", "Contents", "PersonUID" },
                values: new object[,]
                {
                    { new Guid("144fa168-5c07-46c1-a4ad-6890a99df997"), 3, "izmir", new Guid("c026ed21-910a-4c55-9135-41a20e65dc07") },
                    { new Guid("22304ec0-794e-439b-9550-cd2a9f5afd0d"), 3, "urfa", new Guid("bdc3f868-7ed1-4db0-ae35-9a7283bc26ce") },
                    { new Guid("2abae796-fd5b-49b7-aa19-7311f4f1c2de"), 3, "izmir", new Guid("023e1635-8be6-45cc-ad7d-2800a3b679bc") },
                    { new Guid("39192dbb-e8d1-4f61-abe1-5fa536704770"), 3, "antalya", new Guid("b58041ac-5714-4c93-98d5-a981f4754eed") },
                    { new Guid("6fab9285-ecf3-400a-8bef-f3ddf83b1f5c"), 3, "antalya", new Guid("c7c24bf5-1097-4825-90c1-2f4233a3a2ce") },
                    { new Guid("7e6fd57c-4608-4f0a-a44a-92280b2ce3be"), 3, "urfa", new Guid("409ba2b3-10a4-44eb-9193-f43d777b1e30") },
                    { new Guid("80065b7f-b588-4b0e-92f3-fe9c91af2b39"), 3, "urfa", new Guid("1f410483-38c3-48df-9a99-76dbb2bf0d1a") },
                    { new Guid("aa6da8ad-df16-4a59-a3f0-c215e1ef238a"), 3, "izmir", new Guid("550ac5d8-5c79-4ab7-ad05-38f2ae7916b0") },
                    { new Guid("ed8eebe4-c077-463a-b0a8-1a744465c065"), 3, "antalya", new Guid("866c2580-7505-417e-8b2d-89be6d304335") }
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
