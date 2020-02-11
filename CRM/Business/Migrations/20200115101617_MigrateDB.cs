using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Business.Migrations
{
    public partial class MigrateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountTradeHistories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Account = table.Column<string>(nullable: true),
                    Time = table.Column<DateTime>(nullable: false),
                    Side = table.Column<string>(nullable: true),
                    Pair = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    DesiredQuantity = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    DollarQuantity = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    DesiredDollarQuantity = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    Profit = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    DesiredProfit = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    PercentProfit = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    DesiredPercentProfit = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    EnterTax = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    SignalStr = table.Column<string>(nullable: true),
                    Algorithm = table.Column<string>(nullable: true),
                    DecidePercent = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountTradeHistories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DailyUpdates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    dailyTrigger = table.Column<TimeSpan>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyUpdates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IgnoreIds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OrderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IgnoreIds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderBookModels",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BookType = table.Column<string>(nullable: true),
                    CurrencyName = table.Column<string>(nullable: true),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Volume = table.Column<double>(nullable: false),
                    MarketSituation = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderBookModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TradeDeltaModels",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CurrencyName = table.Column<string>(nullable: true),
                    TimeFrom = table.Column<DateTimeOffset>(nullable: false),
                    TimeTo = table.Column<DateTimeOffset>(nullable: false),
                    Delta = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeDeltaModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TradeHistoryModels",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CurrencyName = table.Column<string>(nullable: true),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    Side = table.Column<string>(nullable: true),
                    OrderTime = table.Column<DateTimeOffset>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Volume = table.Column<double>(nullable: false),
                    MarketSituation = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeHistoryModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WrongOrders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OrderId = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18, 6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WrongOrders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserModels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Login = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    RegistrationDate = table.Column<DateTime>(nullable: false),
                    LastAuthorizationDate = table.Column<DateTime>(nullable: false),
                    RoleId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserModels_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExchangeKeys",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    AccountId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExchangeKeys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExchangeKeys_UserModels_UserId",
                        column: x => x.UserId,
                        principalTable: "UserModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeKeys_UserId",
                table: "ExchangeKeys",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserModels_RoleId",
                table: "UserModels",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountTradeHistories");

            migrationBuilder.DropTable(
                name: "DailyUpdates");

            migrationBuilder.DropTable(
                name: "ExchangeKeys");

            migrationBuilder.DropTable(
                name: "IgnoreIds");

            migrationBuilder.DropTable(
                name: "OrderBookModels");

            migrationBuilder.DropTable(
                name: "TradeDeltaModels");

            migrationBuilder.DropTable(
                name: "TradeHistoryModels");

            migrationBuilder.DropTable(
                name: "WrongOrders");

            migrationBuilder.DropTable(
                name: "UserModels");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
