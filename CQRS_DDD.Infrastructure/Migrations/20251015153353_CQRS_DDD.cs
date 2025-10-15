using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CQRS_DDD.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CQRS_DDD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CiLojaEntity",
                columns: table => new
                {
                    CiLojaEntityId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LojaEntityId = table.Column<int>(type: "integer", nullable: false),
                    CiMsg = table.Column<string>(type: "text", nullable: false),
                    Ativa = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CiLojaEntity", x => x.CiLojaEntityId);
                });

            migrationBuilder.CreateTable(
                name: "FrutasEntity",
                columns: table => new
                {
                    FrutasEntityId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Qtde = table.Column<int>(type: "integer", nullable: false),
                    Ativa = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FrutasEntity", x => x.FrutasEntityId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CiLojaEntity");

            migrationBuilder.DropTable(
                name: "FrutasEntity");
        }
    }
}
