using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using WorkoutGlobal.Api.Models.Dto;

namespace WorkoutGlobal.Api.Extensions
{
    public static class DtoModelsExtensions
    {
        public static IConfiguration Configuration { get; set; }

        public static async Task<(string joinLink, string hostLink)> GetZoomLinksAsync(this CreationSportEventDto sportEventDto)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = Configuration["Zoom:ApiKey"],
                Expires = DateTime.UtcNow.AddSeconds(8000),
                SigningCredentials = new SigningCredentials(
                    key: new SymmetricSecurityKey(
                        Encoding.ASCII.GetBytes(Configuration["Zoom:ApiSecret"])),
                    algorithm: SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            var client = new RestClient($"https://api.zoom.us/v2/users/{Configuration["Zoom:Mail"]}/meetings");
            var request = new RestRequest($"https://api.zoom.us/v2/users/{Configuration["Zoom:Mail"]}/meetings", Method.Post)
            {
                RequestFormat = DataFormat.Json
            };

            request.AddJsonBody(new { agenda = sportEventDto.EventName, start_time = $"{sportEventDto.EventStartTime:s}Z", duration = "90", type = "2" });
            request.AddHeader("authorization", string.Format("Bearer {0}", tokenString));

            RestResponse restResponse = await client.ExecuteAsync(request);
            var jObject = JObject.Parse(restResponse.Content);

            return (jObject["join_url"].ToString(), jObject["start_url"].ToString());
        }
    }
}
