using System.Reflection;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CineScope.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            LoadSql(migrationBuilder,"Users.sql");
            LoadSql(migrationBuilder,"Movies.sql");
            LoadSql(migrationBuilder,"Genres.sql");
            LoadSql(migrationBuilder,"GenreMovie.sql");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }

        private void LoadSql(MigrationBuilder migrationBuilder, string file)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = $"CineScope.Migrations.Seeds.{file}";

            using var stream = assembly.GetManifestResourceStream(resourceName);
            using var reader = new StreamReader(stream!);
    
            var sql = reader.ReadToEnd();
            migrationBuilder.Sql(sql);
        }
    }
}
