using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using British_Kingdom_back.Models; // Assurez-vous d'importer correctement votre modèle

namespace British_Kingdom_back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BannerController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;

        public BannerController(IWebHostEnvironment environment, IConfiguration configuration)
        {
            _configuration = configuration;
            _environment = environment;
        }

        [HttpPost]
        public IActionResult CreateBannerSection(BannerSection bannerSection)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var checkCommand = new SqlCommand("SELECT COUNT(*) FROM BannerSection", connection))
                {
                    var existingBannerCount = (int)checkCommand.ExecuteScalar();

                    if (existingBannerCount > 0)
                    {
                        return BadRequest("Une confic banner existe déjà.");
                    }
                }

                var bannerImagesString = string.Join(",", bannerSection.BannerImages!);
                var menuString = bannerSection.Menu != null ? string.Join(",", bannerSection.Menu) : null;

                using (var command = new SqlCommand("INSERT INTO BannerSection ( BannerImages, MaleDescription, KittenDescription, FemaleDescription, Title, Subtitle, MaleImg, KittenImg, FemaleImg, TitleFontSize, TitleFontStyle, TitleFontFamily, SubtitleFontSize, SubtitleFontStyle, SubtitleFontFamily, ColorHeader, TitleColor, SubtitleColor, TitleCard1, TitleCard2, TitleCard3, Menu, ColorMenu, HoverColorMenu, FontStyleMenu, ColorFooter, Favicon, TitlePageMales, TitleFontStylePageMales, TitleColorPageMales, TextPageMales, TextFontStylePageMales, TextColorPageMales, BordureColorPageMales, TitlePageFemelles, TitleFontStylePageFemelles, TitleColorPageFemelles, TextPageFemelles, TextFontStylePageFemelles, TextColorPageFemelles, BordureColorPageFemelles ) VALUES (@BannerImages, @MaleDescription, @KittenDescription, @FemaleDescription, @Title, @Subtitle, @MaleImg, @KittenImg, @FemaleImg, @TitleFontSize, @TitleFontStyle, @TitleFontFamily, @SubtitleFontSize, @SubtitleFontStyle, @SubtitleFontFamily, @ColorHeader, @TitleColor, @SubtitleColor,@TitleCard1, @TitleCard2, @TitleCard3, @Menu, @ColorMenu, @HoverColorMenu, @FontStyleMenu, @ColorFooter, @Favicon, @TitlePageMales, @TitleFontStylePageMales, @TitleColorPageMales, @TextPageMales, @TextFontStylePageMales, @TextColorPageMales, @BordureColorPageMales, @TitlePageFemelles, @TitleFontStylePageFemelles, @TitleColorPageFemelles, @TextPageFemelles, @TextFontStylePageFemelles, @TextColorPageFemelles, @BordureColorPageFemelles, @TitlePageConditions, @TitleFontStylePageConditions, @TitleColorPageConditions, @SousTitlePageConditions, @SousTitleFontStylePageConditions, @SousTitleColorPageConditions, @TextPageConditions, @TextFontStylePageConditions, @TextColorPageConditions, @BordureColorPageConditions )", connection))
                {
                    command.Parameters.AddWithValue("@BannerImages", bannerImagesString);
                    command.Parameters.AddWithValue("@MaleDescription", bannerSection.MaleDescription);
                    command.Parameters.AddWithValue("@KittenDescription", bannerSection.KittenDescription);
                    command.Parameters.AddWithValue("@FemaleDescription", bannerSection.FemaleDescription);

                    command.Parameters.AddWithValue("@TitleCard1", bannerSection.TitleCard1);
                    command.Parameters.AddWithValue("@TitleCard2", bannerSection.TitleCard2);
                    command.Parameters.AddWithValue("@TitleCard3", bannerSection.TitleCard3);
                    command.Parameters.AddWithValue("@Title", bannerSection.Title);
                    command.Parameters.AddWithValue("@Subtitle", bannerSection.Subtitle);
                    command.Parameters.AddWithValue("@MaleImg", bannerSection.MaleImg);
                    command.Parameters.AddWithValue("@KittenImg", bannerSection.KittenImg);
                    command.Parameters.AddWithValue("@FemaleImg", bannerSection.FemaleImg);
                    command.Parameters.AddWithValue("@TitleFontSize", bannerSection.TitleFontSize);
                    command.Parameters.AddWithValue("@TitleFontStyle", bannerSection.TitleFontStyle);
                    command.Parameters.AddWithValue("@TitleFontFamily", bannerSection.TitleFontFamily);
                    command.Parameters.AddWithValue("@SubtitleFontSize", bannerSection.SubtitleFontSize);
                    command.Parameters.AddWithValue("@SubtitleFontStyle", bannerSection.SubtitleFontStyle);
                    command.Parameters.AddWithValue("@SubtitleFontFamily", bannerSection.SubtitleFontFamily);
                    command.Parameters.AddWithValue("@ColorHeader", bannerSection.ColorHeader);
                    command.Parameters.AddWithValue("@TitleColor", bannerSection.TitleColor);
                    command.Parameters.AddWithValue("@SubtitleColor", bannerSection.SubtitleColor);
                    command.Parameters.AddWithValue("@ColorMenu", bannerSection.ColorMenu);
                    command.Parameters.AddWithValue("@HoverColorMenu", bannerSection.HoverColorMenu);
                    command.Parameters.AddWithValue("@FontStyleMenu", bannerSection.FontStyleMenu);
                    command.Parameters.AddWithValue("@Menu", menuString);
                    command.Parameters.AddWithValue("@ColorFooter", bannerSection.ColorFooter);
                    command.Parameters.AddWithValue("@Favicon", bannerSection.Favicon);

                    command.Parameters.AddWithValue("@TitlePageMales", bannerSection.TitlePageMales);
                    command.Parameters.AddWithValue("@TitleFontStylePageMales", bannerSection.TitleFontStylePageMales);
                    command.Parameters.AddWithValue("@TitleColorPageMales", bannerSection.TitleColorPageMales);

                    command.Parameters.AddWithValue("@TextPageMales", bannerSection.TextPageMales);
                    command.Parameters.AddWithValue("@TextFontStylePageMales", bannerSection.TextFontStylePageMales);
                    command.Parameters.AddWithValue("@TextColorPageMales", bannerSection.TextColorPageMales);

                    command.Parameters.AddWithValue("@BordureColorPageMales", bannerSection.BordureColorPageMales);

                    command.Parameters.AddWithValue("@TitlePageFemelles", bannerSection.TitlePageFemelles);
                    command.Parameters.AddWithValue("@TitleFontStylePageFemelles", bannerSection.TitleFontStylePageFemelles);
                    command.Parameters.AddWithValue("@TitleColorPageFemelles", bannerSection.TitleColorPageFemelles);

                    command.Parameters.AddWithValue("@TextPageFemelles", bannerSection.TextPageFemelles);
                    command.Parameters.AddWithValue("@TextFontStylePageFemelles", bannerSection.TextFontStylePageFemelles);
                    command.Parameters.AddWithValue("@TextColorPageFemelles", bannerSection.TextColorPageFemelles);

                    command.Parameters.AddWithValue("@BordureColorPageFemelles", bannerSection.BordureColorPageFemelles);

                    command.Parameters.AddWithValue("@TitlePageConditions", bannerSection.TitlePageConditions);
                    command.Parameters.AddWithValue("@TitleFontStylePageConditions", bannerSection.TitleFontStylePageConditions);
                    command.Parameters.AddWithValue("@TitleColorPageConditions", bannerSection.TitleColorPageConditions);

                    command.Parameters.AddWithValue("@SousTitlePageConditions", string.Join(",", bannerSection.SousTitlePageConditions));
                    command.Parameters.AddWithValue("@SousTitleFontStylePageConditions", bannerSection.SousTitleFontStylePageConditions);
                    command.Parameters.AddWithValue("@SousTitleColorPageConditions", bannerSection.SousTitleColorPageConditions);

                    command.Parameters.AddWithValue("@TextPageConditions", string.Join(",", bannerSection.TextPageConditions));
                    command.Parameters.AddWithValue("@TextFontStylePageConditions", bannerSection.TextFontStylePageConditions);
                    command.Parameters.AddWithValue("@TextColorPageConditions", bannerSection.TextColorPageConditions);

                    command.Parameters.AddWithValue("@BordureColorPageConditions", bannerSection.BordureColorPageConditions);



                    command.ExecuteNonQuery();
                }
            }
            return CreatedAtAction(nameof(GetAllBannerSection), bannerSection);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBannerSection()
        {

            var id = "";

            if (HttpContext.Request.Query.ContainsKey("id"))
            {
                id = HttpContext.Request.Query["id"].ToString();
            }
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            var bannerSections = new List<BannerSection>();

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("SELECT * FROM BannerSection WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            var bannerImagesArray = reader.GetString(reader.GetOrdinal("BannerImages")).Split(',');

                            var bannerSection = new BannerSection
                            {
                                BannerImages = bannerImagesArray,
                                MaleDescription = GetStringOrNull(reader, "MaleDescription"),
                                KittenDescription = GetStringOrNull(reader, "KittenDescription"),
                                FemaleDescription = GetStringOrNull(reader, "FemaleDescription"),
                                Title = GetStringOrNull(reader, "Title"),
                                Subtitle = GetStringOrNull(reader, "Subtitle"),
                                MaleImg = GetStringOrNull(reader, "MaleImg"),
                                KittenImg = GetStringOrNull(reader, "KittenImg"),
                                FemaleImg = GetStringOrNull(reader, "FemaleImg"),
                                TitleFontSize = reader.IsDBNull(reader.GetOrdinal("TitleFontSize")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("TitleFontSize")),
                                TitleFontStyle = GetStringOrNull(reader, "TitleFontStyle"),
                                TitleFontFamily = GetStringOrNull(reader, "TitleFontFamily"),
                                SubtitleFontSize = reader.IsDBNull(reader.GetOrdinal("SubtitleFontSize")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("SubtitleFontSize")),
                                SubtitleFontStyle = GetStringOrNull(reader, "SubtitleFontStyle"),
                                SubtitleFontFamily = GetStringOrNull(reader, "SubtitleFontFamily"),
                                ColorHeader = GetStringOrNull(reader, "ColorHeader"),
                                TitleColor = GetStringOrNull(reader, "TitleColor"),
                                SubtitleColor = GetStringOrNull(reader, "SubtitleColor"),
                                TitleCard1 = GetStringOrNull(reader, "TitleCard1"),
                                TitleCard2 = GetStringOrNull(reader, "TitleCard2"),
                                TitleCard3 = GetStringOrNull(reader, "TitleCard3"),
                                Menu = GetStringArrayOrNull(reader, "Menu"),
                                ColorMenu = GetStringOrNull(reader, "ColorMenu"),
                                HoverColorMenu = GetStringOrNull(reader, "HoverColorMenu"),
                                FontStyleMenu = GetStringOrNull(reader, "FontStyleMenu"),
                                ColorFooter = GetStringOrNull(reader, "ColorFooter"),
                                Favicon = GetStringOrNull(reader, "Favicon"),
                                TitlePageMales = GetStringOrNull(reader, "TitlePageMales"),
                                TitleFontStylePageMales = GetStringOrNull(reader, "TitleFontStylePageMales"),
                                TitleColorPageMales = GetStringOrNull(reader, "TitleColorPageMales"),
                                TextPageMales = GetStringOrNull(reader, "TextPageMales"),
                                TextFontStylePageMales = GetStringOrNull(reader, "TextFontStylePageMales"),
                                TextColorPageMales = GetStringOrNull(reader, "TextColorPageMales"),
                                BordureColorPageMales = GetStringOrNull(reader, "BordureColorPageMales"),
                                TitlePageFemelles = GetStringOrNull(reader, "TitlePageFemelles"),
                                TitleFontStylePageFemelles = GetStringOrNull(reader, "TitleFontStylePageFemelles"),
                                TitleColorPageFemelles = GetStringOrNull(reader, "TitleColorPageFemelles"),
                                TextPageFemelles = GetStringOrNull(reader, "TextPageFemelles"),
                                TextFontStylePageFemelles = GetStringOrNull(reader, "TextFontStylePageFemelles"),
                                TextColorPageFemelles = GetStringOrNull(reader, "TextColorPageFemelles"),
                                BordureColorPageFemelles = GetStringOrNull(reader, "BordureColorPageFemelles"),
                                TitlePageConditions = GetStringOrNull(reader, "TitlePageConditions"),
                                TitleFontStylePageConditions = GetStringOrNull(reader, "TitleFontStylePageConditions"),
                                TitleColorPageConditions = GetStringOrNull(reader, "TitleColorPageConditions"),

                                SousTitlePageConditions = GetStringArrayOrNull(reader, "SousTitlePageConditions"),
                                SousTitleFontStylePageConditions = GetStringOrNull(reader, "SousTitleFontStylePageConditions"),
                                SousTitleColorPageConditions = GetStringOrNull(reader, "SousTitleColorPageConditions"),

                                TextPageConditions = GetStringArrayOrNull(reader, "TextPageConditions"),
                                TextFontStylePageConditions = GetStringOrNull(reader, "TextFontStylePageConditions"),
                                TextColorPageConditions = GetStringOrNull(reader, "TextColorPageConditions"),

                                BordureColorPageConditions = GetStringOrNull(reader, "BordureColorPageConditions")
                            };

                            bannerSections.Add(bannerSection);
                        }
                    }
                }
            }

            return Ok(bannerSections);
        }
        private string[] GetStringArrayOrNull(SqlDataReader reader, string columnName)
        {
            return reader.IsDBNull(reader.GetOrdinal(columnName)) ? null : reader.GetString(reader.GetOrdinal(columnName)).Split(',');
        }
        private string GetStringOrNull(SqlDataReader reader, string columnName)
        {
            return reader.IsDBNull(reader.GetOrdinal(columnName)) ? null : reader.GetString(reader.GetOrdinal(columnName));
        }


        [HttpPut]
        public IActionResult UpdateBannerSection([FromQuery] int id, [FromBody] BannerSection updatedBannerSection)
        {

            Console.WriteLine($"idddddd: {id}");

            if (updatedBannerSection == null)
            {
                return BadRequest("Updated banner section data is missing.");
            }

            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("UPDATE BannerSection SET BannerImages = @BannerImages, MaleDescription = @MaleDescription, KittenDescription = @KittenDescription, FemaleDescription = @FemaleDescription, TitleCard1 = @TitleCard1, TitleCard2 = @TitleCard2, TitleCard3 = @TitleCard3, Title = @Title, Subtitle = @Subtitle, MaleImg = @MaleImg, KittenImg = @KittenImg, FemaleImg = @FemaleImg, TitleFontSize = @TitleFontSize, TitleFontStyle = @TitleFontStyle, TitleFontFamily = @TitleFontFamily, SubtitleFontSize = @SubtitleFontSize, SubtitleFontStyle = @SubtitleFontStyle, SubtitleFontFamily = @SubtitleFontFamily, ColorHeader=@ColorHeader, TitleColor = @TitleColor, SubtitleColor=@SubtitleColor, Menu = @Menu, ColorMenu = @ColorMenu, HoverColorMenu = @HoverColorMenu, FontStyleMenu = @FontStyleMenu, ColorFooter = @ColorFooter, Favicon = @Favicon, TitlePageMales = @TitlePageMales, TitleFontStylePageMales = @TitleFontStylePageMales, TitleColorPageMales = @TitleColorPageMales, TextPageMales = @TextPageMales, TextFontStylePageMales = @TextFontStylePageMales, TextColorPageMales = @TextColorPageMales, BordureColorPageMales = @BordureColorPageMales, TitlePageFemelles = @TitlePageFemelles, TitleFontStylePageFemelles = @TitleFontStylePageFemelles, TitleColorPageFemelles = @TitleColorPageFemelles, TextPageFemelles = @TextPageFemelles, TextFontStylePageFemelles = @TextFontStylePageFemelles, TextColorPageFemelles = @TextColorPageFemelles, BordureColorPageFemelles = @BordureColorPageFemelles, TitlePageConditions = @TitlePageConditions, TitleFontStylePageConditions = @TitleFontStylePageConditions, TitleColorPageConditions = @TitleColorPageConditions, SousTitlePageConditions = @SousTitlePageConditions, SousTitleFontStylePageConditions = @SousTitleFontStylePageConditions, SousTitleColorPageConditions = @SousTitleColorPageConditions, TextPageConditions = @TextPageConditions, TextFontStylePageConditions = @TextFontStylePageConditions, TextColorPageConditions = @TextColorPageConditions, BordureColorPageConditions = @BordureColorPageConditions WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@BannerImages", string.Join(",", updatedBannerSection.BannerImages!));
                    command.Parameters.AddWithValue("@MaleDescription", updatedBannerSection.MaleDescription);
                    command.Parameters.AddWithValue("@KittenDescription", updatedBannerSection.KittenDescription);
                    command.Parameters.AddWithValue("@FemaleDescription", updatedBannerSection.FemaleDescription);
                    command.Parameters.AddWithValue("@TitleCard1", updatedBannerSection.TitleCard1);
                    command.Parameters.AddWithValue("@TitleCard2", updatedBannerSection.TitleCard2);
                    command.Parameters.AddWithValue("@TitleCard3", updatedBannerSection.TitleCard3);
                    command.Parameters.AddWithValue("@Title", updatedBannerSection.Title);
                    command.Parameters.AddWithValue("@Subtitle", updatedBannerSection.Subtitle);
                    command.Parameters.AddWithValue("@MaleImg", updatedBannerSection.MaleImg);
                    command.Parameters.AddWithValue("@KittenImg", updatedBannerSection.KittenImg);
                    command.Parameters.AddWithValue("@FemaleImg", updatedBannerSection.FemaleImg);
                    command.Parameters.AddWithValue("@TitleFontSize", updatedBannerSection.TitleFontSize);
                    command.Parameters.AddWithValue("@TitleFontStyle", updatedBannerSection.TitleFontStyle);
                    command.Parameters.AddWithValue("@TitleFontFamily", updatedBannerSection.TitleFontFamily);
                    command.Parameters.AddWithValue("@SubtitleFontSize", updatedBannerSection.SubtitleFontSize);
                    command.Parameters.AddWithValue("@SubtitleFontStyle", updatedBannerSection.SubtitleFontStyle);
                    command.Parameters.AddWithValue("@SubtitleFontFamily", updatedBannerSection.SubtitleFontFamily);
                    command.Parameters.AddWithValue("@ColorHeader", updatedBannerSection.ColorHeader);
                    command.Parameters.AddWithValue("@TitleColor", updatedBannerSection.TitleColor);
                    command.Parameters.AddWithValue("@SubtitleColor", updatedBannerSection.SubtitleColor);
                    command.Parameters.AddWithValue("@Menu", updatedBannerSection.Menu != null ? string.Join(",", updatedBannerSection.Menu) : null);
                    command.Parameters.AddWithValue("@ColorMenu", updatedBannerSection.ColorMenu);
                    command.Parameters.AddWithValue("@HoverColorMenu", updatedBannerSection.HoverColorMenu);
                    command.Parameters.AddWithValue("@FontStyleMenu", updatedBannerSection.FontStyleMenu);
                    command.Parameters.AddWithValue("@ColorFooter", updatedBannerSection.ColorFooter);
                    command.Parameters.AddWithValue("@Favicon", updatedBannerSection.Favicon);
                    command.Parameters.AddWithValue("@TitlePageMales", updatedBannerSection.TitlePageMales);
                    command.Parameters.AddWithValue("@TitleFontStylePageMales", updatedBannerSection.TitleFontStylePageMales);
                    command.Parameters.AddWithValue("@TitleColorPageMales", updatedBannerSection.TitleColorPageMales);
                    command.Parameters.AddWithValue("@TextPageMales", updatedBannerSection.TextPageMales);
                    command.Parameters.AddWithValue("@TextFontStylePageMales", updatedBannerSection.TextFontStylePageMales);
                    command.Parameters.AddWithValue("@TextColorPageMales", updatedBannerSection.TextColorPageMales);
                    command.Parameters.AddWithValue("@BordureColorPageMales", updatedBannerSection.BordureColorPageMales);
                    command.Parameters.AddWithValue("@TitlePageFemelles", updatedBannerSection.TitlePageFemelles);
                    command.Parameters.AddWithValue("@TitleFontStylePageFemelles", updatedBannerSection.TitleFontStylePageFemelles);
                    command.Parameters.AddWithValue("@TitleColorPageFemelles", updatedBannerSection.TitleColorPageFemelles);
                    command.Parameters.AddWithValue("@TextPageFemelles", updatedBannerSection.TextPageFemelles);
                    command.Parameters.AddWithValue("@TextFontStylePageFemelles", updatedBannerSection.TextFontStylePageFemelles);
                    command.Parameters.AddWithValue("@TextColorPageFemelles", updatedBannerSection.TextColorPageFemelles);
                    command.Parameters.AddWithValue("@BordureColorPageFemelles", updatedBannerSection.BordureColorPageFemelles);
                    command.Parameters.AddWithValue("@TitlePageConditions", updatedBannerSection.TitlePageConditions);
                    command.Parameters.AddWithValue("@TitleFontStylePageConditions", updatedBannerSection.TitleFontStylePageConditions);
                    command.Parameters.AddWithValue("@TitleColorPageConditions", updatedBannerSection.TitleColorPageConditions);

                    command.Parameters.AddWithValue("@SousTitlePageConditions", updatedBannerSection.SousTitlePageConditions != null ? string.Join(",", updatedBannerSection.SousTitlePageConditions) : null);
                    command.Parameters.AddWithValue("@SousTitleFontStylePageConditions", updatedBannerSection.SousTitleFontStylePageConditions);
                    command.Parameters.AddWithValue("@SousTitleColorPageConditions", updatedBannerSection.SousTitleColorPageConditions);

                    command.Parameters.AddWithValue("@TextPageConditions", updatedBannerSection.TextPageConditions != null ? string.Join(",", updatedBannerSection.TextPageConditions) : null);
                    command.Parameters.AddWithValue("@TextFontStylePageConditions", updatedBannerSection.TextFontStylePageConditions);
                    command.Parameters.AddWithValue("@TextColorPageConditions", updatedBannerSection.TextColorPageConditions);

                    command.Parameters.AddWithValue("@BordureColorPageConditions", updatedBannerSection.BordureColorPageConditions);




                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return Ok(updatedBannerSection);
                    }
                    else
                    {
                        return NotFound("Banner section not found.");
                    }
                }
            }
        }



        [HttpDelete("deleteMissingImages")]
        public async Task<IActionResult> DeleteImages([FromQuery] List<string> imagePaths, [FromQuery] string directory)
        {
            try
            {
                if (imagePaths == null || imagePaths.Count == 0)
                {
                    return BadRequest("No image paths provided.");
                }

                // Utiliser la variable 'directory' pour personnaliser le chemin du dossier
                string customFolderPath = Path.Combine(_environment.WebRootPath, $"assets/{directory}");

                foreach (var imagePath in imagePaths)
                {
                    string fullPath = Path.Combine(customFolderPath, imagePath);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                }

                return Ok("Images deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


    }
}
