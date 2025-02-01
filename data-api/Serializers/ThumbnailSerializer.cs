using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using data_api.Services;
using data_api.Models;

public class ThumbnailSerializer
{
    private readonly GitHubService _gitHubService;

    public ThumbnailSerializer(GitHubService gitHubService) => _gitHubService = gitHubService;

    public Type ValueType => typeof(Image);

    public Image? Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var bsonReader = context.Reader;

        switch (bsonReader.GetCurrentBsonType())
        {
            case BsonType.String:
                var filePath = bsonReader.ReadString();

                // Call GitHubService to get file details
                var metaData = _gitHubService.GetImageMetadataAsync(filePath).GetAwaiter().GetResult();

                return metaData ?? new Image { FilePath = filePath };

            case BsonType.Null:
                return null;

            default:
                throw new BsonSerializationException("Invalid type for Thumbnail");
        }
    }
}
