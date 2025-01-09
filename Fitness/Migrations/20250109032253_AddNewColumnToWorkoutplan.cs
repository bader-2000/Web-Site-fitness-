using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fitness.Migrations
{
    /// <inheritdoc />
    public partial class AddNewColumnToWorkoutplan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "C##FITNESS");

            migrationBuilder.CreateTable(
                name: "PROFILE",
                schema: "C##FITNESS",
                columns: table => new
                {
                    PROFILEID = table.Column<decimal>(type: "NUMBER", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NAME = table.Column<string>(type: "VARCHAR2(50)", unicode: false, maxLength: 50, nullable: true),
                    USERNAME = table.Column<string>(type: "VARCHAR2(50)", unicode: false, maxLength: 50, nullable: false),
                    USERPASSWORD = table.Column<string>(type: "VARCHAR2(50)", unicode: false, maxLength: 50, nullable: false),
                    PHOTO = table.Column<string>(type: "VARCHAR2(150)", unicode: false, maxLength: 150, nullable: true),
                    DATE_OF_BIRTH = table.Column<DateTime>(type: "DATE", nullable: true),
                    EMAIL = table.Column<string>(type: "VARCHAR2(30)", unicode: false, maxLength: 30, nullable: true),
                    LNAME = table.Column<string>(type: "VARCHAR2(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C008427", x => x.PROFILEID);
                });

            migrationBuilder.CreateTable(
                name: "WORKOUTPLANS",
                schema: "C##FITNESS",
                columns: table => new
                {
                    IDWOP = table.Column<decimal>(type: "NUMBER", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NUMBEROFWEEK = table.Column<decimal>(type: "NUMBER", nullable: true),
                    GOALS = table.Column<string>(type: "VARCHAR2(500)", unicode: false, maxLength: 500, nullable: true),
                    DAY1 = table.Column<string>(type: "VARCHAR2(300)", unicode: false, maxLength: 300, nullable: true),
                    DAY2 = table.Column<string>(type: "VARCHAR2(300)", unicode: false, maxLength: 300, nullable: true),
                    DAY3 = table.Column<string>(type: "VARCHAR2(300)", unicode: false, maxLength: 300, nullable: true),
                    DAY4 = table.Column<string>(type: "VARCHAR2(300)", unicode: false, maxLength: 300, nullable: true),
                    DAY5 = table.Column<string>(type: "VARCHAR2(300)", unicode: false, maxLength: 300, nullable: true),
                    DAY6 = table.Column<string>(type: "VARCHAR2(300)", unicode: false, maxLength: 300, nullable: true),
                    DAY7 = table.Column<string>(type: "VARCHAR2(300)", unicode: false, maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C008439", x => x.IDWOP);
                });

            migrationBuilder.CreateTable(
                name: "INFOFITNESS",
                schema: "C##FITNESS",
                columns: table => new
                {
                    IDIF = table.Column<decimal>(type: "NUMBER", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    EMAIL = table.Column<string>(type: "VARCHAR2(500)", unicode: false, maxLength: 500, nullable: true),
                    PHONE = table.Column<string>(type: "VARCHAR2(300)", unicode: false, maxLength: 300, nullable: true),
                    LOCATION = table.Column<string>(type: "VARCHAR2(300)", unicode: false, maxLength: 300, nullable: true),
                    ABOUTUS = table.Column<string>(type: "VARCHAR2(300)", unicode: false, maxLength: 300, nullable: true),
                    FACEBOOK = table.Column<string>(type: "VARCHAR2(300)", unicode: false, maxLength: 300, nullable: true),
                    LINKEDIN = table.Column<string>(type: "VARCHAR2(300)", unicode: false, maxLength: 300, nullable: true),
                    PHOTOABOUTUS = table.Column<string>(type: "VARCHAR2(300)", unicode: false, maxLength: 300, nullable: true),
                    INPROFILEID = table.Column<decimal>(type: "NUMBER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C008447", x => x.IDIF);
                    table.ForeignKey(
                        name: "FK_INFO",
                        column: x => x.INPROFILEID,
                        principalSchema: "C##FITNESS",
                        principalTable: "PROFILE",
                        principalColumn: "PROFILEID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ROLE",
                schema: "C##FITNESS",
                columns: table => new
                {
                    ROLEID = table.Column<decimal>(type: "NUMBER", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    RNAME = table.Column<string>(type: "VARCHAR2(50)", unicode: false, maxLength: 50, nullable: false),
                    RPROFILEID = table.Column<decimal>(type: "NUMBER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C008436", x => x.ROLEID);
                    table.ForeignKey(
                        name: "FK_ROLE",
                        column: x => x.RPROFILEID,
                        principalSchema: "C##FITNESS",
                        principalTable: "PROFILE",
                        principalColumn: "PROFILEID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "TESTIMONIAL",
                schema: "C##FITNESS",
                columns: table => new
                {
                    TESTIMOID = table.Column<decimal>(type: "NUMBER", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    FEEDBACK = table.Column<string>(type: "VARCHAR2(1000)", unicode: false, maxLength: 1000, nullable: true),
                    STATUS = table.Column<string>(type: "VARCHAR2(50)", unicode: false, maxLength: 50, nullable: true),
                    TPROFILEID = table.Column<decimal>(type: "NUMBER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C008429", x => x.TESTIMOID);
                    table.ForeignKey(
                        name: "FK_PROFILE",
                        column: x => x.TPROFILEID,
                        principalSchema: "C##FITNESS",
                        principalTable: "PROFILE",
                        principalColumn: "PROFILEID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "SUBSCRIPTIONS",
                schema: "C##FITNESS",
                columns: table => new
                {
                    SUBSCRID = table.Column<decimal>(type: "NUMBER", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    COUNTWEEKS = table.Column<decimal>(type: "NUMBER", nullable: true),
                    NAMEPLAN = table.Column<string>(type: "VARCHAR2(50)", unicode: false, maxLength: 50, nullable: true),
                    PRICE = table.Column<decimal>(type: "NUMBER", nullable: true),
                    SIDWOP = table.Column<decimal>(type: "NUMBER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C008441", x => x.SUBSCRID);
                    table.ForeignKey(
                        name: "FK_SUBSC",
                        column: x => x.SIDWOP,
                        principalSchema: "C##FITNESS",
                        principalTable: "WORKOUTPLANS",
                        principalColumn: "IDWOP",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TYPEPERSON",
                schema: "C##FITNESS",
                columns: table => new
                {
                    ID = table.Column<decimal>(type: "NUMBER", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    TPROFILEID = table.Column<decimal>(type: "NUMBER", nullable: true),
                    TSUBSCRID = table.Column<decimal>(type: "NUMBER", nullable: true),
                    STARTDATE = table.Column<DateTime>(type: "DATE", nullable: true),
                    ENDDATE = table.Column<DateTime>(type: "DATE", nullable: true),
                    STATUS = table.Column<string>(type: "VARCHAR2(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C008450", x => x.ID);
                    table.ForeignKey(
                        name: "FKT_PROFILE",
                        column: x => x.TPROFILEID,
                        principalSchema: "C##FITNESS",
                        principalTable: "PROFILE",
                        principalColumn: "PROFILEID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FKT_SUBSCR",
                        column: x => x.TSUBSCRID,
                        principalSchema: "C##FITNESS",
                        principalTable: "SUBSCRIPTIONS",
                        principalColumn: "SUBSCRID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_INFOFITNESS_INPROFILEID",
                schema: "C##FITNESS",
                table: "INFOFITNESS",
                column: "INPROFILEID");

            migrationBuilder.CreateIndex(
                name: "IX_ROLE_RPROFILEID",
                schema: "C##FITNESS",
                table: "ROLE",
                column: "RPROFILEID");

            migrationBuilder.CreateIndex(
                name: "IX_SUBSCRIPTIONS_SIDWOP",
                schema: "C##FITNESS",
                table: "SUBSCRIPTIONS",
                column: "SIDWOP");

            migrationBuilder.CreateIndex(
                name: "IX_TESTIMONIAL_TPROFILEID",
                schema: "C##FITNESS",
                table: "TESTIMONIAL",
                column: "TPROFILEID");

            migrationBuilder.CreateIndex(
                name: "IX_TYPEPERSON_TPROFILEID",
                schema: "C##FITNESS",
                table: "TYPEPERSON",
                column: "TPROFILEID");

            migrationBuilder.CreateIndex(
                name: "IX_TYPEPERSON_TSUBSCRID",
                schema: "C##FITNESS",
                table: "TYPEPERSON",
                column: "TSUBSCRID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "INFOFITNESS",
                schema: "C##FITNESS");

            migrationBuilder.DropTable(
                name: "ROLE",
                schema: "C##FITNESS");

            migrationBuilder.DropTable(
                name: "TESTIMONIAL",
                schema: "C##FITNESS");

            migrationBuilder.DropTable(
                name: "TYPEPERSON",
                schema: "C##FITNESS");

            migrationBuilder.DropTable(
                name: "PROFILE",
                schema: "C##FITNESS");

            migrationBuilder.DropTable(
                name: "SUBSCRIPTIONS",
                schema: "C##FITNESS");

            migrationBuilder.DropTable(
                name: "WORKOUTPLANS",
                schema: "C##FITNESS");
        }
    }
}
