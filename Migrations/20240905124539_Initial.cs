using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Auth.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    Id = table.Column<byte[]>(type: "varbinary(200)", maxLength: 200, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ParentId = table.Column<byte[]>(type: "varbinary(200)", maxLength: 200, nullable: true),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    StatusDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Organizations_Organizations_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Organizations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<byte[]>(type: "varbinary(200)", maxLength: 200, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Family = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    IsPhoneValid = table.Column<bool>(type: "bit", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    IsEmailValid = table.Column<bool>(type: "bit", nullable: false),
                    FailedLoginAttempts = table.Column<int>(type: "int", nullable: false),
                    AccountLockedUntil = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    StatusDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ResourceGroups",
                columns: table => new
                {
                    Id = table.Column<byte[]>(type: "varbinary(200)", maxLength: 200, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Order = table.Column<int>(type: "int", nullable: true),
                    OrganizationId = table.Column<byte[]>(type: "varbinary(200)", nullable: false),
                    ParentId = table.Column<byte[]>(type: "varbinary(200)", maxLength: 200, nullable: true),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResourceGroups_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResourceGroups_ResourceGroups_ParentId",
                        column: x => x.ParentId,
                        principalTable: "ResourceGroups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<byte[]>(type: "varbinary(200)", maxLength: 200, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    OrganizationId = table.Column<byte[]>(type: "varbinary(200)", maxLength: 200, nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    StatusDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Roles_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    Id = table.Column<byte[]>(type: "varbinary(200)", maxLength: 200, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    OrganizationId = table.Column<byte[]>(type: "varbinary(200)", maxLength: 200, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    GroupId = table.Column<byte[]>(type: "varbinary(200)", maxLength: 200, nullable: true),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resources_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Resources_ResourceGroups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "ResourceGroups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserOrganizations",
                columns: table => new
                {
                    Id = table.Column<byte[]>(type: "varbinary(200)", maxLength: 200, nullable: false),
                    UserId = table.Column<byte[]>(type: "varbinary(200)", maxLength: 200, nullable: false),
                    OrganizationId = table.Column<byte[]>(type: "varbinary(200)", maxLength: 200, nullable: false),
                    RoleId = table.Column<byte[]>(type: "varbinary(200)", maxLength: 200, nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOrganizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserOrganizations_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserOrganizations_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserOrganizations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<byte[]>(type: "varbinary(200)", maxLength: 200, nullable: false),
                    RoleId = table.Column<byte[]>(type: "varbinary(200)", maxLength: 200, nullable: false),
                    ResourceId = table.Column<byte[]>(type: "varbinary(200)", maxLength: 200, nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permissions_Resources_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Permissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Organizations",
                columns: new[] { "Id", "CreateAt", "ModifyAt", "ParentId", "Status", "StatusDescription", "Title" },
                values: new object[] { new byte[] { 1, 145, 106, 136, 242, 8, 4, 73, 225, 232, 220, 197, 197, 104, 46, 232 }, new DateTime(2024, 9, 5, 12, 45, 38, 659, DateTimeKind.Utc).AddTicks(8750), null, null, (byte)1, null, "Auth.Service" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccountLockedUntil", "CreateAt", "Email", "FailedLoginAttempts", "Family", "IsEmailValid", "IsPhoneValid", "ModifyAt", "Name", "Password", "Phone", "Status", "StatusDescription", "Username" },
                values: new object[] { new byte[] { 1, 145, 110, 104, 183, 60, 4, 137, 1, 231, 28, 132, 171, 255, 73, 237 }, null, new DateTime(2024, 9, 5, 12, 45, 38, 660, DateTimeKind.Utc).AddTicks(220), null, 0, "Auth.Service", false, false, null, "Admin", "icoMg8j2l99NC7JaDWHofIL6jJsut20+7kWSohXr6vKU1niIvwchn47i44Sr72bP", null, (byte)1, null, "AdminAuthService1" });

            migrationBuilder.InsertData(
                table: "Organizations",
                columns: new[] { "Id", "CreateAt", "ModifyAt", "ParentId", "Status", "StatusDescription", "Title" },
                values: new object[,]
                {
                    { new byte[] { 1, 145, 106, 139, 32, 123, 205, 111, 137, 86, 72, 123, 76, 224, 81, 14 }, new DateTime(2024, 9, 5, 12, 45, 38, 659, DateTimeKind.Utc).AddTicks(8770), null, new byte[] { 1, 145, 106, 136, 242, 8, 4, 73, 225, 232, 220, 197, 197, 104, 46, 232 }, (byte)1, null, "Accounting.Service" },
                    { new byte[] { 1, 145, 106, 139, 183, 77, 220, 76, 77, 22, 99, 90, 112, 204, 45, 8 }, new DateTime(2024, 9, 5, 12, 45, 38, 659, DateTimeKind.Utc).AddTicks(8770), null, new byte[] { 1, 145, 106, 136, 242, 8, 4, 73, 225, 232, 220, 197, 197, 104, 46, 232 }, (byte)1, null, "RedSense.Service" },
                    { new byte[] { 1, 145, 106, 53, 249, 102, 223, 110, 72, 121, 238, 195, 21, 7, 23, 232 }, new DateTime(2024, 9, 5, 12, 45, 38, 659, DateTimeKind.Utc).AddTicks(8810), null, new byte[] { 1, 145, 106, 136, 242, 8, 4, 73, 225, 232, 220, 197, 197, 104, 46, 232 }, (byte)1, null, "RedGuard.Update.Service" }
                });

            migrationBuilder.InsertData(
                table: "ResourceGroups",
                columns: new[] { "Id", "CreateAt", "ModifyAt", "Order", "OrganizationId", "ParentId", "Title" },
                values: new object[,]
                {
                    { new byte[] { 1, 145, 194, 42, 195, 53, 198, 72, 83, 119, 87, 191, 13, 30, 74, 191 }, new DateTime(2024, 9, 5, 12, 45, 38, 701, DateTimeKind.Utc).AddTicks(6370), null, 1, new byte[] { 1, 145, 106, 136, 242, 8, 4, 73, 225, 232, 220, 197, 197, 104, 46, 232 }, null, "Auth.Users" },
                    { new byte[] { 1, 145, 194, 45, 99, 78, 65, 54, 202, 171, 255, 86, 230, 97, 209, 207 }, new DateTime(2024, 9, 5, 12, 45, 38, 701, DateTimeKind.Utc).AddTicks(6370), null, 2, new byte[] { 1, 145, 106, 136, 242, 8, 4, 73, 225, 232, 220, 197, 197, 104, 46, 232 }, null, "Auth.Organizations" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreateAt", "ModifyAt", "OrganizationId", "Status", "StatusDescription", "Title" },
                values: new object[] { new byte[] { 1, 145, 106, 169, 155, 50, 131, 103, 80, 86, 64, 7, 186, 122, 115, 182 }, new DateTime(2024, 9, 5, 12, 45, 38, 659, DateTimeKind.Utc).AddTicks(9690), null, new byte[] { 1, 145, 106, 136, 242, 8, 4, 73, 225, 232, 220, 197, 197, 104, 46, 232 }, (byte)1, null, "Admin.Auth.Service" });

            migrationBuilder.InsertData(
                table: "Resources",
                columns: new[] { "Id", "CreateAt", "GroupId", "ModifyAt", "OrganizationId", "Title", "Url" },
                values: new object[,]
                {
                    { new byte[] { 1, 145, 194, 19, 100, 213, 195, 167, 224, 76, 129, 159, 122, 140, 74, 9 }, new DateTime(2024, 9, 5, 12, 45, 38, 701, DateTimeKind.Utc).AddTicks(7800), new byte[] { 1, 145, 194, 42, 195, 53, 198, 72, 83, 119, 87, 191, 13, 30, 74, 191 }, null, new byte[] { 1, 145, 106, 136, 242, 8, 4, 73, 225, 232, 220, 197, 197, 104, 46, 232 }, "Auth.User.Create", "/user" },
                    { new byte[] { 1, 145, 194, 46, 218, 213, 189, 253, 172, 150, 128, 62, 74, 166, 112, 183 }, new DateTime(2024, 9, 5, 12, 45, 38, 701, DateTimeKind.Utc).AddTicks(7800), new byte[] { 1, 145, 194, 42, 195, 53, 198, 72, 83, 119, 87, 191, 13, 30, 74, 191 }, null, new byte[] { 1, 145, 106, 136, 242, 8, 4, 73, 225, 232, 220, 197, 197, 104, 46, 232 }, "Auth.User.ListByFilters", "/user/list/filter" }
                });

            migrationBuilder.InsertData(
                table: "UserOrganizations",
                columns: new[] { "Id", "CreateAt", "ModifyAt", "OrganizationId", "RoleId", "UserId" },
                values: new object[] { new byte[] { 1, 145, 110, 124, 211, 13, 172, 121, 123, 233, 248, 119, 101, 211, 216, 0 }, new DateTime(2024, 9, 5, 12, 45, 38, 701, DateTimeKind.Utc).AddTicks(5300), null, new byte[] { 1, 145, 106, 136, 242, 8, 4, 73, 225, 232, 220, 197, 197, 104, 46, 232 }, new byte[] { 1, 145, 106, 169, 155, 50, 131, 103, 80, 86, 64, 7, 186, 122, 115, 182 }, new byte[] { 1, 145, 110, 104, 183, 60, 4, 137, 1, 231, 28, 132, 171, 255, 73, 237 } });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "CreateAt", "ModifyAt", "ResourceId", "RoleId" },
                values: new object[,]
                {
                    { new byte[] { 1, 145, 194, 17, 51, 104, 213, 0, 96, 96, 6, 174, 40, 102, 47, 34 }, new DateTime(2024, 9, 5, 12, 45, 38, 701, DateTimeKind.Utc).AddTicks(8930), null, new byte[] { 1, 145, 194, 19, 100, 213, 195, 167, 224, 76, 129, 159, 122, 140, 74, 9 }, new byte[] { 1, 145, 106, 169, 155, 50, 131, 103, 80, 86, 64, 7, 186, 122, 115, 182 } },
                    { new byte[] { 1, 145, 194, 48, 229, 40, 27, 168, 73, 211, 5, 201, 74, 251, 113, 198 }, new DateTime(2024, 9, 5, 12, 45, 38, 701, DateTimeKind.Utc).AddTicks(8930), null, new byte[] { 1, 145, 194, 46, 218, 213, 189, 253, 172, 150, 128, 62, 74, 166, 112, 183 }, new byte[] { 1, 145, 106, 169, 155, 50, 131, 103, 80, 86, 64, 7, 186, 122, 115, 182 } }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_ParentId",
                table: "Organizations",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_ResourceId",
                table: "Permissions",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_RoleId",
                table: "Permissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceGroups_OrganizationId",
                table: "ResourceGroups",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceGroups_ParentId",
                table: "ResourceGroups",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_GroupId",
                table: "Resources",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_OrganizationId",
                table: "Resources",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_OrganizationId",
                table: "Roles",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOrganizations_OrganizationId",
                table: "UserOrganizations",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOrganizations_RoleId",
                table: "UserOrganizations",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOrganizations_UserId",
                table: "UserOrganizations",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "UserOrganizations");

            migrationBuilder.DropTable(
                name: "Resources");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "ResourceGroups");

            migrationBuilder.DropTable(
                name: "Organizations");
        }
    }
}
