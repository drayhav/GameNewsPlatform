using System.Text.Json;
using System.Text.Json.Serialization;
using GameService.Domain.ValueObjects;

namespace GameService.Application.Converters
{
    public class GenreJsonConverter : JsonConverter<Genre>
    {
        public override Genre? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var genreName = reader.GetString();
            if (string.IsNullOrEmpty(genreName))
            {
                throw new JsonException("Genre name cannot be null or empty.");
            }

            // Match the genre name with the predefined genres
            var genre = Genre.ListAll().FirstOrDefault(g => g.Name == genreName);
            if (genre == null)
            {
                throw new JsonException($"Genre '{genreName}' is not supported.");
            }

            return genre;
        }

        public override void Write(Utf8JsonWriter writer, Genre value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.Name);
        }
    }
}