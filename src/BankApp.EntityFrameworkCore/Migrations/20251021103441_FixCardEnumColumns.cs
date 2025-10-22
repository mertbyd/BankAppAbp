using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankApp.Migrations
{
    /// <inheritdoc />
    public partial class FixCardEnumColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "OpenIddictTokens",
                schema: "openiddict",
                newName: "OpenIddictTokens");

            migrationBuilder.RenameTable(
                name: "OpenIddictScopes",
                schema: "openiddict",
                newName: "OpenIddictScopes");

            migrationBuilder.RenameTable(
                name: "OpenIddictAuthorizations",
                schema: "openiddict",
                newName: "OpenIddictAuthorizations");

            migrationBuilder.RenameTable(
                name: "OpenIddictApplications",
                schema: "openiddict",
                newName: "OpenIddictApplications");

            migrationBuilder.RenameTable(
                name: "AbpUserTokens",
                schema: "abp",
                newName: "AbpUserTokens");

            migrationBuilder.RenameTable(
                name: "AbpUsers",
                schema: "abp",
                newName: "AbpUsers");

            migrationBuilder.RenameTable(
                name: "AbpUserRoles",
                schema: "abp",
                newName: "AbpUserRoles");

            migrationBuilder.RenameTable(
                name: "AbpUserOrganizationUnits",
                schema: "abp",
                newName: "AbpUserOrganizationUnits");

            migrationBuilder.RenameTable(
                name: "AbpUserLogins",
                schema: "abp",
                newName: "AbpUserLogins");

            migrationBuilder.RenameTable(
                name: "AbpUserClaims",
                schema: "abp",
                newName: "AbpUserClaims");

            migrationBuilder.RenameTable(
                name: "AbpTenants",
                schema: "abp",
                newName: "AbpTenants");

            migrationBuilder.RenameTable(
                name: "AbpTenantConnectionStrings",
                schema: "abp",
                newName: "AbpTenantConnectionStrings");

            migrationBuilder.RenameTable(
                name: "AbpSettings",
                schema: "abp",
                newName: "AbpSettings");

            migrationBuilder.RenameTable(
                name: "AbpSecurityLogs",
                schema: "abp",
                newName: "AbpSecurityLogs");

            migrationBuilder.RenameTable(
                name: "AbpRoles",
                schema: "abp",
                newName: "AbpRoles");

            migrationBuilder.RenameTable(
                name: "AbpRoleClaims",
                schema: "abp",
                newName: "AbpRoleClaims");

            migrationBuilder.RenameTable(
                name: "AbpPermissions",
                schema: "abp",
                newName: "AbpPermissions");

            migrationBuilder.RenameTable(
                name: "AbpPermissionGroups",
                schema: "abp",
                newName: "AbpPermissionGroups");

            migrationBuilder.RenameTable(
                name: "AbpPermissionGrants",
                schema: "abp",
                newName: "AbpPermissionGrants");

            migrationBuilder.RenameTable(
                name: "AbpOrganizationUnits",
                schema: "abp",
                newName: "AbpOrganizationUnits");

            migrationBuilder.RenameTable(
                name: "AbpOrganizationUnitRoles",
                schema: "abp",
                newName: "AbpOrganizationUnitRoles");

            migrationBuilder.RenameTable(
                name: "AbpLinkUsers",
                schema: "abp",
                newName: "AbpLinkUsers");

            migrationBuilder.RenameTable(
                name: "AbpFeatureValues",
                schema: "abp",
                newName: "AbpFeatureValues");

            migrationBuilder.RenameTable(
                name: "AbpFeatures",
                schema: "abp",
                newName: "AbpFeatures");

            migrationBuilder.RenameTable(
                name: "AbpFeatureGroups",
                schema: "abp",
                newName: "AbpFeatureGroups");

            migrationBuilder.RenameTable(
                name: "AbpEntityPropertyChanges",
                schema: "abp",
                newName: "AbpEntityPropertyChanges");

            migrationBuilder.RenameTable(
                name: "AbpEntityChanges",
                schema: "abp",
                newName: "AbpEntityChanges");

            migrationBuilder.RenameTable(
                name: "AbpClaimTypes",
                schema: "abp",
                newName: "AbpClaimTypes");

            migrationBuilder.RenameTable(
                name: "AbpBackgroundJobs",
                schema: "abp",
                newName: "AbpBackgroundJobs");

            migrationBuilder.RenameTable(
                name: "AbpAuditLogs",
                schema: "abp",
                newName: "AbpAuditLogs");

            migrationBuilder.RenameTable(
                name: "AbpAuditLogActions",
                schema: "abp",
                newName: "AbpAuditLogActions");

            migrationBuilder.RenameColumn(
                name: "TransactionTypesId",
                schema: "bankapp",
                table: "Transactions",
                newName: "TransactionTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_TransactionTypesId",
                schema: "bankapp",
                table: "Transactions",
                newName: "IX_Transactions_TransactionTypeId");

            migrationBuilder.RenameColumn(
                name: "Status",
                schema: "bankapp",
                table: "Cards",
                newName: "StatusId");

            migrationBuilder.RenameColumn(
                name: "CardType",
                schema: "bankapp",
                table: "Cards",
                newName: "CardTypeId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModificationTime",
                schema: "bankapp",
                table: "TransactionTypeLookups",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletionTime",
                schema: "bankapp",
                table: "TransactionTypeLookups",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTime",
                schema: "bankapp",
                table: "TransactionTypeLookups",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TransactionDate",
                schema: "bankapp",
                table: "Transactions",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModificationTime",
                schema: "bankapp",
                table: "Transactions",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletionTime",
                schema: "bankapp",
                table: "Transactions",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTime",
                schema: "bankapp",
                table: "Transactions",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddColumn<int>(
                name: "TransactionTypeId1",
                schema: "bankapp",
                table: "Transactions",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModificationTime",
                schema: "bankapp",
                table: "GeneralLogs",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletionTime",
                schema: "bankapp",
                table: "GeneralLogs",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTime",
                schema: "bankapp",
                table: "GeneralLogs",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModificationTime",
                schema: "bankapp",
                table: "Customers",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletionTime",
                schema: "bankapp",
                table: "Customers",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTime",
                schema: "bankapp",
                table: "Customers",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                schema: "bankapp",
                table: "Customers",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModificationTime",
                schema: "bankapp",
                table: "CardTypeLookups",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletionTime",
                schema: "bankapp",
                table: "CardTypeLookups",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTime",
                schema: "bankapp",
                table: "CardTypeLookups",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModificationTime",
                schema: "bankapp",
                table: "CardStatusLookups",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletionTime",
                schema: "bankapp",
                table: "CardStatusLookups",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTime",
                schema: "bankapp",
                table: "CardStatusLookups",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModificationTime",
                schema: "bankapp",
                table: "Cards",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletionTime",
                schema: "bankapp",
                table: "Cards",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTime",
                schema: "bankapp",
                table: "Cards",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddColumn<int>(
                name: "CardTypeId1",
                schema: "bankapp",
                table: "Cards",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusId1",
                schema: "bankapp",
                table: "Cards",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "OpenedAt",
                schema: "bankapp",
                table: "Accounts",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModificationTime",
                schema: "bankapp",
                table: "Accounts",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletionTime",
                schema: "bankapp",
                table: "Accounts",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTime",
                schema: "bankapp",
                table: "Accounts",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ClosedAt",
                schema: "bankapp",
                table: "Accounts",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "RedemptionDate",
                table: "OpenIddictTokens",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModificationTime",
                table: "OpenIddictTokens",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpirationDate",
                table: "OpenIddictTokens",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletionTime",
                table: "OpenIddictTokens",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTime",
                table: "OpenIddictTokens",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "OpenIddictTokens",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModificationTime",
                table: "OpenIddictScopes",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletionTime",
                table: "OpenIddictScopes",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTime",
                table: "OpenIddictScopes",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModificationTime",
                table: "OpenIddictAuthorizations",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletionTime",
                table: "OpenIddictAuthorizations",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTime",
                table: "OpenIddictAuthorizations",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "OpenIddictAuthorizations",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModificationTime",
                table: "OpenIddictApplications",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletionTime",
                table: "OpenIddictApplications",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTime",
                table: "OpenIddictApplications",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModificationTime",
                table: "AbpUsers",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletionTime",
                table: "AbpUsers",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTime",
                table: "AbpUsers",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTime",
                table: "AbpUserOrganizationUnits",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModificationTime",
                table: "AbpTenants",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletionTime",
                table: "AbpTenants",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTime",
                table: "AbpTenants",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTime",
                table: "AbpSecurityLogs",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModificationTime",
                table: "AbpOrganizationUnits",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletionTime",
                table: "AbpOrganizationUnits",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTime",
                table: "AbpOrganizationUnits",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTime",
                table: "AbpOrganizationUnitRoles",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangeTime",
                table: "AbpEntityChanges",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "NextTryTime",
                table: "AbpBackgroundJobs",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastTryTime",
                table: "AbpBackgroundJobs",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTime",
                table: "AbpBackgroundJobs",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExecutionTime",
                table: "AbpAuditLogs",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExecutionTime",
                table: "AbpAuditLogActions",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_TransactionTypeId1",
                schema: "bankapp",
                table: "Transactions",
                column: "TransactionTypeId1");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_CardTypeId1",
                schema: "bankapp",
                table: "Cards",
                column: "CardTypeId1");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_StatusId1",
                schema: "bankapp",
                table: "Cards",
                column: "StatusId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_CardStatusLookups_StatusId1",
                schema: "bankapp",
                table: "Cards",
                column: "StatusId1",
                principalSchema: "bankapp",
                principalTable: "CardStatusLookups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_CardTypeLookups_CardTypeId1",
                schema: "bankapp",
                table: "Cards",
                column: "CardTypeId1",
                principalSchema: "bankapp",
                principalTable: "CardTypeLookups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_TransactionTypeLookups_TransactionTypeId1",
                schema: "bankapp",
                table: "Transactions",
                column: "TransactionTypeId1",
                principalSchema: "bankapp",
                principalTable: "TransactionTypeLookups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_CardStatusLookups_StatusId1",
                schema: "bankapp",
                table: "Cards");

            migrationBuilder.DropForeignKey(
                name: "FK_Cards_CardTypeLookups_CardTypeId1",
                schema: "bankapp",
                table: "Cards");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_TransactionTypeLookups_TransactionTypeId1",
                schema: "bankapp",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_TransactionTypeId1",
                schema: "bankapp",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Cards_CardTypeId1",
                schema: "bankapp",
                table: "Cards");

            migrationBuilder.DropIndex(
                name: "IX_Cards_StatusId1",
                schema: "bankapp",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "TransactionTypeId1",
                schema: "bankapp",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "CardTypeId1",
                schema: "bankapp",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "StatusId1",
                schema: "bankapp",
                table: "Cards");

            migrationBuilder.EnsureSchema(
                name: "abp");

            migrationBuilder.EnsureSchema(
                name: "openiddict");

            migrationBuilder.RenameTable(
                name: "OpenIddictTokens",
                newName: "OpenIddictTokens",
                newSchema: "openiddict");

            migrationBuilder.RenameTable(
                name: "OpenIddictScopes",
                newName: "OpenIddictScopes",
                newSchema: "openiddict");

            migrationBuilder.RenameTable(
                name: "OpenIddictAuthorizations",
                newName: "OpenIddictAuthorizations",
                newSchema: "openiddict");

            migrationBuilder.RenameTable(
                name: "OpenIddictApplications",
                newName: "OpenIddictApplications",
                newSchema: "openiddict");

            migrationBuilder.RenameTable(
                name: "AbpUserTokens",
                newName: "AbpUserTokens",
                newSchema: "abp");

            migrationBuilder.RenameTable(
                name: "AbpUsers",
                newName: "AbpUsers",
                newSchema: "abp");

            migrationBuilder.RenameTable(
                name: "AbpUserRoles",
                newName: "AbpUserRoles",
                newSchema: "abp");

            migrationBuilder.RenameTable(
                name: "AbpUserOrganizationUnits",
                newName: "AbpUserOrganizationUnits",
                newSchema: "abp");

            migrationBuilder.RenameTable(
                name: "AbpUserLogins",
                newName: "AbpUserLogins",
                newSchema: "abp");

            migrationBuilder.RenameTable(
                name: "AbpUserClaims",
                newName: "AbpUserClaims",
                newSchema: "abp");

            migrationBuilder.RenameTable(
                name: "AbpTenants",
                newName: "AbpTenants",
                newSchema: "abp");

            migrationBuilder.RenameTable(
                name: "AbpTenantConnectionStrings",
                newName: "AbpTenantConnectionStrings",
                newSchema: "abp");

            migrationBuilder.RenameTable(
                name: "AbpSettings",
                newName: "AbpSettings",
                newSchema: "abp");

            migrationBuilder.RenameTable(
                name: "AbpSecurityLogs",
                newName: "AbpSecurityLogs",
                newSchema: "abp");

            migrationBuilder.RenameTable(
                name: "AbpRoles",
                newName: "AbpRoles",
                newSchema: "abp");

            migrationBuilder.RenameTable(
                name: "AbpRoleClaims",
                newName: "AbpRoleClaims",
                newSchema: "abp");

            migrationBuilder.RenameTable(
                name: "AbpPermissions",
                newName: "AbpPermissions",
                newSchema: "abp");

            migrationBuilder.RenameTable(
                name: "AbpPermissionGroups",
                newName: "AbpPermissionGroups",
                newSchema: "abp");

            migrationBuilder.RenameTable(
                name: "AbpPermissionGrants",
                newName: "AbpPermissionGrants",
                newSchema: "abp");

            migrationBuilder.RenameTable(
                name: "AbpOrganizationUnits",
                newName: "AbpOrganizationUnits",
                newSchema: "abp");

            migrationBuilder.RenameTable(
                name: "AbpOrganizationUnitRoles",
                newName: "AbpOrganizationUnitRoles",
                newSchema: "abp");

            migrationBuilder.RenameTable(
                name: "AbpLinkUsers",
                newName: "AbpLinkUsers",
                newSchema: "abp");

            migrationBuilder.RenameTable(
                name: "AbpFeatureValues",
                newName: "AbpFeatureValues",
                newSchema: "abp");

            migrationBuilder.RenameTable(
                name: "AbpFeatures",
                newName: "AbpFeatures",
                newSchema: "abp");

            migrationBuilder.RenameTable(
                name: "AbpFeatureGroups",
                newName: "AbpFeatureGroups",
                newSchema: "abp");

            migrationBuilder.RenameTable(
                name: "AbpEntityPropertyChanges",
                newName: "AbpEntityPropertyChanges",
                newSchema: "abp");

            migrationBuilder.RenameTable(
                name: "AbpEntityChanges",
                newName: "AbpEntityChanges",
                newSchema: "abp");

            migrationBuilder.RenameTable(
                name: "AbpClaimTypes",
                newName: "AbpClaimTypes",
                newSchema: "abp");

            migrationBuilder.RenameTable(
                name: "AbpBackgroundJobs",
                newName: "AbpBackgroundJobs",
                newSchema: "abp");

            migrationBuilder.RenameTable(
                name: "AbpAuditLogs",
                newName: "AbpAuditLogs",
                newSchema: "abp");

            migrationBuilder.RenameTable(
                name: "AbpAuditLogActions",
                newName: "AbpAuditLogActions",
                newSchema: "abp");

            migrationBuilder.RenameColumn(
                name: "TransactionTypeId",
                schema: "bankapp",
                table: "Transactions",
                newName: "TransactionTypesId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_TransactionTypeId",
                schema: "bankapp",
                table: "Transactions",
                newName: "IX_Transactions_TransactionTypesId");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                schema: "bankapp",
                table: "Cards",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "CardTypeId",
                schema: "bankapp",
                table: "Cards",
                newName: "CardType");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModificationTime",
                schema: "bankapp",
                table: "TransactionTypeLookups",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletionTime",
                schema: "bankapp",
                table: "TransactionTypeLookups",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTime",
                schema: "bankapp",
                table: "TransactionTypeLookups",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TransactionDate",
                schema: "bankapp",
                table: "Transactions",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModificationTime",
                schema: "bankapp",
                table: "Transactions",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletionTime",
                schema: "bankapp",
                table: "Transactions",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTime",
                schema: "bankapp",
                table: "Transactions",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModificationTime",
                schema: "bankapp",
                table: "GeneralLogs",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletionTime",
                schema: "bankapp",
                table: "GeneralLogs",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTime",
                schema: "bankapp",
                table: "GeneralLogs",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModificationTime",
                schema: "bankapp",
                table: "Customers",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletionTime",
                schema: "bankapp",
                table: "Customers",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTime",
                schema: "bankapp",
                table: "Customers",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                schema: "bankapp",
                table: "Customers",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModificationTime",
                schema: "bankapp",
                table: "CardTypeLookups",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletionTime",
                schema: "bankapp",
                table: "CardTypeLookups",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTime",
                schema: "bankapp",
                table: "CardTypeLookups",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModificationTime",
                schema: "bankapp",
                table: "CardStatusLookups",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletionTime",
                schema: "bankapp",
                table: "CardStatusLookups",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTime",
                schema: "bankapp",
                table: "CardStatusLookups",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModificationTime",
                schema: "bankapp",
                table: "Cards",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletionTime",
                schema: "bankapp",
                table: "Cards",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTime",
                schema: "bankapp",
                table: "Cards",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OpenedAt",
                schema: "bankapp",
                table: "Accounts",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModificationTime",
                schema: "bankapp",
                table: "Accounts",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletionTime",
                schema: "bankapp",
                table: "Accounts",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTime",
                schema: "bankapp",
                table: "Accounts",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ClosedAt",
                schema: "bankapp",
                table: "Accounts",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "RedemptionDate",
                schema: "openiddict",
                table: "OpenIddictTokens",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModificationTime",
                schema: "openiddict",
                table: "OpenIddictTokens",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpirationDate",
                schema: "openiddict",
                table: "OpenIddictTokens",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletionTime",
                schema: "openiddict",
                table: "OpenIddictTokens",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTime",
                schema: "openiddict",
                table: "OpenIddictTokens",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                schema: "openiddict",
                table: "OpenIddictTokens",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModificationTime",
                schema: "openiddict",
                table: "OpenIddictScopes",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletionTime",
                schema: "openiddict",
                table: "OpenIddictScopes",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTime",
                schema: "openiddict",
                table: "OpenIddictScopes",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModificationTime",
                schema: "openiddict",
                table: "OpenIddictAuthorizations",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletionTime",
                schema: "openiddict",
                table: "OpenIddictAuthorizations",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTime",
                schema: "openiddict",
                table: "OpenIddictAuthorizations",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                schema: "openiddict",
                table: "OpenIddictAuthorizations",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModificationTime",
                schema: "openiddict",
                table: "OpenIddictApplications",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletionTime",
                schema: "openiddict",
                table: "OpenIddictApplications",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTime",
                schema: "openiddict",
                table: "OpenIddictApplications",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModificationTime",
                schema: "abp",
                table: "AbpUsers",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletionTime",
                schema: "abp",
                table: "AbpUsers",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTime",
                schema: "abp",
                table: "AbpUsers",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTime",
                schema: "abp",
                table: "AbpUserOrganizationUnits",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModificationTime",
                schema: "abp",
                table: "AbpTenants",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletionTime",
                schema: "abp",
                table: "AbpTenants",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTime",
                schema: "abp",
                table: "AbpTenants",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTime",
                schema: "abp",
                table: "AbpSecurityLogs",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModificationTime",
                schema: "abp",
                table: "AbpOrganizationUnits",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletionTime",
                schema: "abp",
                table: "AbpOrganizationUnits",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTime",
                schema: "abp",
                table: "AbpOrganizationUnits",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTime",
                schema: "abp",
                table: "AbpOrganizationUnitRoles",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangeTime",
                schema: "abp",
                table: "AbpEntityChanges",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "NextTryTime",
                schema: "abp",
                table: "AbpBackgroundJobs",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastTryTime",
                schema: "abp",
                table: "AbpBackgroundJobs",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTime",
                schema: "abp",
                table: "AbpBackgroundJobs",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExecutionTime",
                schema: "abp",
                table: "AbpAuditLogs",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExecutionTime",
                schema: "abp",
                table: "AbpAuditLogActions",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }
    }
}
