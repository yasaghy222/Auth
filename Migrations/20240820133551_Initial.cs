﻿using System;
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
                name: "ResourceGroups",
                columns: table => new
                {
                    Id = table.Column<byte[]>(type: "varbinary(200)", maxLength: 200, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Order = table.Column<int>(type: "int", nullable: true),
                    ParentId = table.Column<byte[]>(type: "varbinary(200)", maxLength: 200, nullable: true),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResourceGroups_ResourceGroups_ParentId",
                        column: x => x.ParentId,
                        principalTable: "ResourceGroups",
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
                    Email = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Id = table.Column<byte[]>(type: "varbinary(200)", maxLength: 200, nullable: false),
                    UserId = table.Column<byte[]>(type: "varbinary(200)", maxLength: 200, nullable: false),
                    Platform = table.Column<byte>(type: "tinyint", nullable: false),
                    UniqueId = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    IP = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    OrganizationId = table.Column<byte[]>(type: "varbinary(200)", maxLength: 200, nullable: false),
                    ExpireAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sessions_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sessions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                values: new object[,]
                {
                    { new byte[] { 1, 145, 106, 136, 242, 8, 4, 73, 225, 232, 220, 197, 197, 104, 46, 232 }, new DateTime(2024, 8, 20, 13, 35, 50, 598, DateTimeKind.Utc).AddTicks(7899), null, null, (byte)1, null, "Auth.Service" },
                    { new byte[] { 1, 145, 106, 139, 32, 123, 205, 111, 137, 86, 72, 123, 76, 224, 81, 14 }, new DateTime(2024, 8, 20, 13, 35, 50, 598, DateTimeKind.Utc).AddTicks(7915), null, null, (byte)1, null, "Accounting.Service" },
                    { new byte[] { 1, 145, 106, 139, 183, 77, 220, 76, 77, 22, 99, 90, 112, 204, 45, 8 }, new DateTime(2024, 8, 20, 13, 35, 50, 598, DateTimeKind.Utc).AddTicks(7918), null, null, (byte)1, null, "RedSense.Service" },
                    { new byte[] { 1, 145, 106, 177, 249, 102, 223, 64, 72, 121, 238, 195, 21, 7, 23, 232 }, new DateTime(2024, 8, 20, 13, 35, 50, 598, DateTimeKind.Utc).AddTicks(7920), null, null, (byte)1, null, "RedGuard.Update.Service" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccountLockedUntil", "CreateAt", "Email", "FailedLoginAttempts", "Family", "ModifyAt", "Name", "Password", "Phone", "Status", "StatusDescription", "Username" },
                values: new object[,]
                {
                    { new byte[] { 1, 145, 110, 104, 183, 60, 4, 137, 1, 231, 28, 132, 171, 255, 73, 237 }, null, new DateTime(2024, 8, 20, 13, 35, 50, 598, DateTimeKind.Utc).AddTicks(9730), null, 0, "Auth.Service", null, "Admin", "Uni7eNoTnfa874XWsjxYwwlotZuACI18GGHPakRNtM3U2UUyuGRAu4KRbv6kbYg6", null, (byte)1, null, "AdminAuthService1" },
                    { new byte[] { 1, 145, 110, 105, 87, 195, 45, 141, 160, 102, 109, 140, 134, 105, 139, 173 }, null, new DateTime(2024, 8, 20, 13, 35, 50, 600, DateTimeKind.Utc).AddTicks(7073), null, 0, "Accounting.Service", null, "Admin", "7/ggbvDnBI05RZ/fsj5CE5Mw4xrIWHN1shXVf0iQosbBtFLa2eSZ/dtOt1tPA31P", null, (byte)1, null, "AdminAccountingService1" },
                    { new byte[] { 1, 145, 110, 130, 27, 139, 16, 168, 64, 233, 212, 85, 188, 139, 27, 111 }, null, new DateTime(2024, 8, 20, 13, 35, 50, 602, DateTimeKind.Utc).AddTicks(3811), null, 0, "RedSense.Service", null, "Admin", "9HguKa4iTkmFh++/yUxzjQbmCwlEObAhw4aRCVTPOWEV5kKEQElHfaWXxjj6kJOA", null, (byte)1, null, "AdminRedSenseService1" },
                    { new byte[] { 1, 145, 110, 131, 42, 217, 160, 182, 83, 128, 106, 136, 170, 11, 183, 126 }, null, new DateTime(2024, 8, 20, 13, 35, 50, 604, DateTimeKind.Utc).AddTicks(391), null, 0, "RedGuard.Update.Service", null, "Admin", "MUV359Nc5cWjv54Z7lPLjaLolct1Y1FwISmwfGT7HdI6dHoq5VKeu1n//7+1K42I", null, (byte)1, null, "AdminRedGuardUpdateService1" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreateAt", "ModifyAt", "OrganizationId", "Status", "StatusDescription", "Title" },
                values: new object[,]
                {
                    { new byte[] { 1, 145, 106, 169, 155, 50, 131, 103, 80, 86, 64, 7, 186, 122, 115, 182 }, new DateTime(2024, 8, 20, 13, 35, 50, 598, DateTimeKind.Utc).AddTicks(9121), null, new byte[] { 1, 145, 106, 136, 242, 8, 4, 73, 225, 232, 220, 197, 197, 104, 46, 232 }, (byte)1, null, "Admin.Auth.Service" },
                    { new byte[] { 1, 145, 106, 170, 223, 167, 73, 180, 1, 51, 77, 185, 122, 95, 78, 244 }, new DateTime(2024, 8, 20, 13, 35, 50, 598, DateTimeKind.Utc).AddTicks(9127), null, new byte[] { 1, 145, 106, 139, 32, 123, 205, 111, 137, 86, 72, 123, 76, 224, 81, 14 }, (byte)1, null, "Admin.Accounting.Service" },
                    { new byte[] { 1, 145, 106, 174, 81, 141, 200, 228, 8, 62, 170, 241, 222, 2, 170, 69 }, new DateTime(2024, 8, 20, 13, 35, 50, 598, DateTimeKind.Utc).AddTicks(9130), null, new byte[] { 1, 145, 106, 139, 183, 77, 220, 76, 77, 22, 99, 90, 112, 204, 45, 8 }, (byte)1, null, "Admin.RedSense.Service" },
                    { new byte[] { 1, 145, 110, 132, 36, 254, 143, 28, 34, 174, 239, 146, 132, 223, 29, 241 }, new DateTime(2024, 8, 20, 13, 35, 50, 598, DateTimeKind.Utc).AddTicks(9133), null, new byte[] { 1, 145, 106, 177, 249, 102, 223, 64, 72, 121, 238, 195, 21, 7, 23, 232 }, (byte)1, null, "Admin.RedGuard.Update.Service" }
                });

            migrationBuilder.InsertData(
                table: "UserOrganizations",
                columns: new[] { "Id", "CreateAt", "ModifyAt", "OrganizationId", "RoleId", "UserId" },
                values: new object[,]
                {
                    { new byte[] { 1, 145, 110, 124, 211, 13, 172, 121, 123, 233, 248, 119, 101, 211, 216, 0 }, new DateTime(2024, 8, 20, 13, 35, 50, 606, DateTimeKind.Utc).AddTicks(753), null, new byte[] { 1, 145, 106, 136, 242, 8, 4, 73, 225, 232, 220, 197, 197, 104, 46, 232 }, new byte[] { 1, 145, 106, 169, 155, 50, 131, 103, 80, 86, 64, 7, 186, 122, 115, 182 }, new byte[] { 1, 145, 110, 104, 183, 60, 4, 137, 1, 231, 28, 132, 171, 255, 73, 237 } },
                    { new byte[] { 1, 145, 110, 127, 99, 205, 140, 184, 243, 37, 41, 73, 148, 104, 34, 45 }, new DateTime(2024, 8, 20, 13, 35, 50, 606, DateTimeKind.Utc).AddTicks(762), null, new byte[] { 1, 145, 106, 139, 32, 123, 205, 111, 137, 86, 72, 123, 76, 224, 81, 14 }, new byte[] { 1, 145, 106, 170, 223, 167, 73, 180, 1, 51, 77, 185, 122, 95, 78, 244 }, new byte[] { 1, 145, 110, 105, 87, 195, 45, 141, 160, 102, 109, 140, 134, 105, 139, 173 } },
                    { new byte[] { 1, 145, 110, 129, 40, 16, 107, 5, 179, 118, 154, 72, 139, 107, 194, 33 }, new DateTime(2024, 8, 20, 13, 35, 50, 606, DateTimeKind.Utc).AddTicks(766), null, new byte[] { 1, 145, 106, 139, 183, 77, 220, 76, 77, 22, 99, 90, 112, 204, 45, 8 }, new byte[] { 1, 145, 106, 174, 81, 141, 200, 228, 8, 62, 170, 241, 222, 2, 170, 69 }, new byte[] { 1, 145, 110, 130, 27, 139, 16, 168, 64, 233, 212, 85, 188, 139, 27, 111 } },
                    { new byte[] { 1, 145, 110, 130, 62, 238, 186, 7, 87, 156, 228, 222, 14, 191, 114, 169 }, new DateTime(2024, 8, 20, 13, 35, 50, 606, DateTimeKind.Utc).AddTicks(770), null, new byte[] { 1, 145, 106, 177, 249, 102, 223, 64, 72, 121, 238, 195, 21, 7, 23, 232 }, new byte[] { 1, 145, 110, 132, 36, 254, 143, 28, 34, 174, 239, 146, 132, 223, 29, 241 }, new byte[] { 1, 145, 110, 131, 42, 217, 160, 182, 83, 128, 106, 136, 170, 11, 183, 126 } }
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
                name: "IX_Sessions_OrganizationId",
                table: "Sessions",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_UserId",
                table: "Sessions",
                column: "UserId");

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
                name: "Sessions");

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
