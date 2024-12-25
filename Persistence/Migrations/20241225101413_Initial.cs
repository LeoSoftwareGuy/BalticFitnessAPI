using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "foodtypes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    imageurl = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_foodtypes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "meals",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    userid = table.Column<string>(type: "text", nullable: false),
                    mealtime = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_meals", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "muscles",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    imageurl = table.Column<string>(type: "text", nullable: false),
                    type = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_muscles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "trainings",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    userid = table.Column<string>(type: "text", nullable: false),
                    trained = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trainings", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    foodtypeid = table.Column<int>(type: "integer", nullable: false),
                    title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    imageurl = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    caloriesper100 = table.Column<float>(type: "real", nullable: false),
                    fatsper100 = table.Column<float>(type: "real", nullable: false),
                    carbsper100 = table.Column<float>(type: "real", nullable: false),
                    proteinper100 = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.id);
                    table.ForeignKey(
                        name: "FK_products_foodtypes_foodtypeid",
                        column: x => x.foodtypeid,
                        principalTable: "foodtypes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "exercises",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    musclegroupid = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    imageurl = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exercises", x => x.id);
                    table.ForeignKey(
                        name: "FK_exercises_muscles_musclegroupid",
                        column: x => x.musclegroupid,
                        principalTable: "muscles",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "consumedproducts",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    mealid = table.Column<int>(type: "integer", nullable: false),
                    productid = table.Column<int>(type: "integer", nullable: false),
                    quantity = table.Column<double>(type: "double precision", nullable: false),
                    weightgrams = table.Column<double>(type: "double precision", nullable: false),
                    consumedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_consumedproducts", x => x.id);
                    table.ForeignKey(
                        name: "FK_consumedproducts_meals_mealid",
                        column: x => x.mealid,
                        principalTable: "meals",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_consumedproducts_products_productid",
                        column: x => x.productid,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "exercisesets",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    trainingid = table.Column<int>(type: "integer", nullable: false),
                    exerciseid = table.Column<int>(type: "integer", nullable: false),
                    reps = table.Column<int>(type: "integer", nullable: false),
                    weight = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exercisesets", x => x.id);
                    table.ForeignKey(
                        name: "FK_exercisesets_exercises_exerciseid",
                        column: x => x.exerciseid,
                        principalTable: "exercises",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_exercisesets_trainings_trainingid",
                        column: x => x.trainingid,
                        principalTable: "trainings",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_consumedproducts_mealid",
                table: "consumedproducts",
                column: "mealid");

            migrationBuilder.CreateIndex(
                name: "IX_consumedproducts_productid",
                table: "consumedproducts",
                column: "productid");

            migrationBuilder.CreateIndex(
                name: "IX_exercises_musclegroupid",
                table: "exercises",
                column: "musclegroupid");

            migrationBuilder.CreateIndex(
                name: "IX_exercisesets_exerciseid",
                table: "exercisesets",
                column: "exerciseid");

            migrationBuilder.CreateIndex(
                name: "IX_exercisesets_trainingid",
                table: "exercisesets",
                column: "trainingid");

            migrationBuilder.CreateIndex(
                name: "IX_products_foodtypeid",
                table: "products",
                column: "foodtypeid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "consumedproducts");

            migrationBuilder.DropTable(
                name: "exercisesets");

            migrationBuilder.DropTable(
                name: "meals");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "exercises");

            migrationBuilder.DropTable(
                name: "trainings");

            migrationBuilder.DropTable(
                name: "foodtypes");

            migrationBuilder.DropTable(
                name: "muscles");
        }
    }
}
