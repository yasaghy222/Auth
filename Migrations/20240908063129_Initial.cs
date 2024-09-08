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
                    Method = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false),
                    RequirePermission = table.Column<bool>(type: "bit", nullable: false),
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
                values: new object[] { new byte[] { 1, 145, 106, 136, 242, 8, 4, 73, 225, 232, 220, 197, 197, 104, 46, 232 }, new DateTime(2024, 9, 8, 6, 31, 29, 224, DateTimeKind.Utc).AddTicks(8454), null, null, (byte)1, null, "Auth.Service" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccountLockedUntil", "CreateAt", "Email", "FailedLoginAttempts", "Family", "IsEmailValid", "IsPhoneValid", "ModifyAt", "Name", "Password", "Phone", "Status", "StatusDescription", "Username" },
                values: new object[] { new byte[] { 1, 145, 110, 104, 183, 60, 4, 137, 1, 231, 28, 132, 171, 255, 73, 237 }, null, new DateTime(2024, 9, 8, 6, 31, 29, 224, DateTimeKind.Utc).AddTicks(9997), null, 0, "", false, false, null, "Admin.Auth.Service", "tVl4uV2vRUGOIF1FUsJBsCW8w4c5WsqFjnS3mTORDyHwEPeEYysMTrPOjTH9HjuW", null, (byte)1, null, "AdminAuthService1" });

            migrationBuilder.InsertData(
                table: "Organizations",
                columns: new[] { "Id", "CreateAt", "ModifyAt", "ParentId", "Status", "StatusDescription", "Title" },
                values: new object[,]
                {
                    { new byte[] { 1, 145, 106, 139, 32, 123, 205, 111, 137, 86, 72, 123, 76, 224, 81, 14 }, new DateTime(2024, 9, 8, 6, 31, 29, 224, DateTimeKind.Utc).AddTicks(8475), null, new byte[] { 1, 145, 106, 136, 242, 8, 4, 73, 225, 232, 220, 197, 197, 104, 46, 232 }, (byte)1, null, "Accounting.Service" },
                    { new byte[] { 1, 145, 106, 139, 183, 77, 220, 76, 77, 22, 99, 90, 112, 204, 45, 8 }, new DateTime(2024, 9, 8, 6, 31, 29, 224, DateTimeKind.Utc).AddTicks(8480), null, new byte[] { 1, 145, 106, 136, 242, 8, 4, 73, 225, 232, 220, 197, 197, 104, 46, 232 }, (byte)1, null, "RedSense.Service" },
                    { new byte[] { 1, 145, 106, 53, 249, 102, 223, 110, 72, 121, 238, 195, 21, 7, 23, 232 }, new DateTime(2024, 9, 8, 6, 31, 29, 224, DateTimeKind.Utc).AddTicks(8483), null, new byte[] { 1, 145, 106, 136, 242, 8, 4, 73, 225, 232, 220, 197, 197, 104, 46, 232 }, (byte)1, null, "RedGuard.Update.Service" }
                });

            migrationBuilder.InsertData(
                table: "ResourceGroups",
                columns: new[] { "Id", "CreateAt", "ModifyAt", "Order", "OrganizationId", "ParentId", "Title" },
                values: new object[] { new byte[] { 1, 145, 194, 42, 195, 53, 198, 72, 83, 119, 87, 191, 13, 30, 74, 191 }, new DateTime(2024, 9, 8, 6, 31, 29, 268, DateTimeKind.Utc).AddTicks(1659), null, 1, new byte[] { 1, 145, 106, 136, 242, 8, 4, 73, 225, 232, 220, 197, 197, 104, 46, 232 }, null, "Auth.User" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreateAt", "ModifyAt", "OrganizationId", "Status", "StatusDescription", "Title" },
                values: new object[] { new byte[] { 1, 145, 106, 169, 155, 50, 131, 103, 80, 86, 64, 7, 186, 122, 115, 182 }, new DateTime(2024, 9, 8, 6, 31, 29, 224, DateTimeKind.Utc).AddTicks(9381), null, new byte[] { 1, 145, 106, 136, 242, 8, 4, 73, 225, 232, 220, 197, 197, 104, 46, 232 }, (byte)1, null, "Admin.Auth.Service" });

            migrationBuilder.InsertData(
                table: "ResourceGroups",
                columns: new[] { "Id", "CreateAt", "ModifyAt", "Order", "OrganizationId", "ParentId", "Title" },
                values: new object[] { new byte[] { 1, 145, 208, 31, 165, 2, 171, 227, 176, 45, 84, 44, 249, 118, 7, 186 }, new DateTime(2024, 9, 8, 6, 31, 29, 268, DateTimeKind.Utc).AddTicks(1667), null, 2, new byte[] { 1, 145, 106, 139, 32, 123, 205, 111, 137, 86, 72, 123, 76, 224, 81, 14 }, null, "Auth.Organization" });

            migrationBuilder.InsertData(
                table: "Resources",
                columns: new[] { "Id", "CreateAt", "GroupId", "IsPublic", "Method", "ModifyAt", "OrganizationId", "RequirePermission", "Title", "Url" },
                values: new object[,]
                {
                    { new byte[] { 1, 145, 203, 214, 139, 112, 40, 246, 89, 83, 179, 208, 14, 133, 229, 148 }, new DateTime(2024, 9, 8, 6, 31, 29, 268, DateTimeKind.Utc).AddTicks(3092), new byte[] { 1, 145, 194, 42, 195, 53, 198, 72, 83, 119, 87, 191, 13, 30, 74, 191 }, false, "POST", null, new byte[] { 1, 145, 106, 136, 242, 8, 4, 73, 225, 232, 220, 197, 197, 104, 46, 232 }, true, "Auth.User.Create", "/user" },
                    { new byte[] { 1, 145, 204, 104, 59, 239, 89, 149, 197, 113, 106, 71, 146, 116, 105, 173 }, new DateTime(2024, 9, 8, 6, 31, 29, 268, DateTimeKind.Utc).AddTicks(3110), new byte[] { 1, 145, 194, 42, 195, 53, 198, 72, 83, 119, 87, 191, 13, 30, 74, 191 }, false, "PUT", null, new byte[] { 1, 145, 106, 136, 242, 8, 4, 73, 225, 232, 220, 197, 197, 104, 46, 232 }, true, "Auth.User.Create", "/user" },
                    { new byte[] { 1, 145, 194, 19, 100, 213, 195, 167, 224, 76, 129, 159, 122, 140, 74, 9 }, new DateTime(2024, 9, 8, 6, 31, 29, 268, DateTimeKind.Utc).AddTicks(3117), new byte[] { 1, 145, 194, 42, 195, 53, 198, 72, 83, 119, 87, 191, 13, 30, 74, 191 }, false, "DELETE", null, new byte[] { 1, 145, 106, 136, 242, 8, 4, 73, 225, 232, 220, 197, 197, 104, 46, 232 }, true, "Auth.User.Delete", "/user" },
                    { new byte[] { 1, 145, 204, 113, 69, 43, 84, 249, 125, 55, 60, 132, 175, 127, 19, 107 }, new DateTime(2024, 9, 8, 6, 31, 29, 268, DateTimeKind.Utc).AddTicks(3120), new byte[] { 1, 145, 194, 42, 195, 53, 198, 72, 83, 119, 87, 191, 13, 30, 74, 191 }, false, "PATCH", null, new byte[] { 1, 145, 106, 136, 242, 8, 4, 73, 225, 232, 220, 197, 197, 104, 46, 232 }, true, "Auth.User.ChangePassword", "/user/change-password" },
                    { new byte[] { 1, 145, 204, 121, 186, 100, 163, 46, 156, 160, 49, 160, 100, 99, 222, 204 }, new DateTime(2024, 9, 8, 6, 31, 29, 268, DateTimeKind.Utc).AddTicks(3125), new byte[] { 1, 145, 194, 42, 195, 53, 198, 72, 83, 119, 87, 191, 13, 30, 74, 191 }, true, "PATCH", null, new byte[] { 1, 145, 106, 136, 242, 8, 4, 73, 225, 232, 220, 197, 197, 104, 46, 232 }, false, "Auth.User.ResetPassword", "/user/reset-password" },
                    { new byte[] { 1, 145, 204, 124, 54, 99, 52, 242, 64, 198, 0, 248, 113, 159, 236, 46 }, new DateTime(2024, 9, 8, 6, 31, 29, 268, DateTimeKind.Utc).AddTicks(3128), new byte[] { 1, 145, 194, 42, 195, 53, 198, 72, 83, 119, 87, 191, 13, 30, 74, 191 }, false, "GET", null, new byte[] { 1, 145, 106, 136, 242, 8, 4, 73, 225, 232, 220, 197, 197, 104, 46, 232 }, true, "Auth.User.Get.Id", "/user" },
                    { new byte[] { 1, 145, 194, 46, 218, 213, 189, 253, 172, 150, 128, 62, 74, 166, 112, 183 }, new DateTime(2024, 9, 8, 6, 31, 29, 268, DateTimeKind.Utc).AddTicks(3132), new byte[] { 1, 145, 194, 42, 195, 53, 198, 72, 83, 119, 87, 191, 13, 30, 74, 191 }, false, "GET", null, new byte[] { 1, 145, 106, 136, 242, 8, 4, 73, 225, 232, 220, 197, 197, 104, 46, 232 }, true, "Auth.User.List.Filter", "/user/list/filter" }
                });

            migrationBuilder.InsertData(
                table: "UserOrganizations",
                columns: new[] { "Id", "CreateAt", "ModifyAt", "OrganizationId", "RoleId", "UserId" },
                values: new object[] { new byte[] { 1, 145, 110, 124, 211, 13, 172, 121, 123, 233, 248, 119, 101, 211, 216, 0 }, new DateTime(2024, 9, 8, 6, 31, 29, 268, DateTimeKind.Utc).AddTicks(738), null, new byte[] { 1, 145, 106, 139, 32, 123, 205, 111, 137, 86, 72, 123, 76, 224, 81, 14 }, new byte[] { 1, 145, 106, 169, 155, 50, 131, 103, 80, 86, 64, 7, 186, 122, 115, 182 }, new byte[] { 1, 145, 110, 104, 183, 60, 4, 137, 1, 231, 28, 132, 171, 255, 73, 237 } });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "CreateAt", "ModifyAt", "ResourceId", "RoleId" },
                values: new object[,]
                {
                    { new byte[] { 1, 145, 204, 150, 84, 240, 42, 228, 210, 77, 104, 4, 133, 180, 25, 184 }, new DateTime(2024, 9, 8, 6, 31, 29, 268, DateTimeKind.Utc).AddTicks(4237), null, new byte[] { 1, 145, 203, 214, 139, 112, 40, 246, 89, 83, 179, 208, 14, 133, 229, 148 }, new byte[] { 1, 145, 106, 169, 155, 50, 131, 103, 80, 86, 64, 7, 186, 122, 115, 182 } },
                    { new byte[] { 1, 145, 204, 150, 107, 127, 150, 207, 20, 119, 52, 110, 7, 61, 68, 202 }, new DateTime(2024, 9, 8, 6, 31, 29, 268, DateTimeKind.Utc).AddTicks(4246), null, new byte[] { 1, 145, 204, 104, 59, 239, 89, 149, 197, 113, 106, 71, 146, 116, 105, 173 }, new byte[] { 1, 145, 106, 169, 155, 50, 131, 103, 80, 86, 64, 7, 186, 122, 115, 182 } },
                    { new byte[] { 1, 145, 208, 13, 36, 216, 81, 194, 36, 35, 85, 25, 108, 59, 73, 83 }, new DateTime(2024, 9, 8, 6, 31, 29, 268, DateTimeKind.Utc).AddTicks(4249), null, new byte[] { 1, 145, 194, 19, 100, 213, 195, 167, 224, 76, 129, 159, 122, 140, 74, 9 }, new byte[] { 1, 145, 106, 169, 155, 50, 131, 103, 80, 86, 64, 7, 186, 122, 115, 182 } },
                    { new byte[] { 1, 145, 208, 13, 243, 255, 43, 0, 161, 232, 166, 128, 90, 226, 38, 133 }, new DateTime(2024, 9, 8, 6, 31, 29, 268, DateTimeKind.Utc).AddTicks(4251), null, new byte[] { 1, 145, 204, 124, 54, 99, 52, 242, 64, 198, 0, 248, 113, 159, 236, 46 }, new byte[] { 1, 145, 106, 169, 155, 50, 131, 103, 80, 86, 64, 7, 186, 122, 115, 182 } },
                    { new byte[] { 1, 145, 208, 14, 63, 53, 205, 210, 151, 168, 232, 220, 223, 155, 152, 77 }, new DateTime(2024, 9, 8, 6, 31, 29, 268, DateTimeKind.Utc).AddTicks(4254), null, new byte[] { 1, 145, 194, 46, 218, 213, 189, 253, 172, 150, 128, 62, 74, 166, 112, 183 }, new byte[] { 1, 145, 106, 169, 155, 50, 131, 103, 80, 86, 64, 7, 186, 122, 115, 182 } }
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
