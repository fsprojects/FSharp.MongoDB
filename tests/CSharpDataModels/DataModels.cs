namespace CSharpDataModels;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Bson.Serialization.Serializers;

public sealed class GuidStringDictionarySerializer : DictionaryInterfaceImplementerSerializer<Dictionary<Guid, string>, Guid, string>
{
    public GuidStringDictionarySerializer()
        : base(
            DictionaryRepresentation.ArrayOfDocuments,
            new GuidSerializer(GuidRepresentation.Standard),
            new StringSerializer())
    {
    }
}

public record Pair
{
    public required int First { get; init; }
    public required string? Second { get; init; }
}


public record Value
{
    public record IntValue(int Value) : Value;
    public record StringValue(string Value) : Value;
    public record PairValue(Pair Value) : Value;
}


public record RecordDataModel
{
    public ObjectId Id { get; init; }

    public required int Int { get; init; }
    public int? IntOpt { get; init; }

    public required string String { get; init; }
    public string? StringOpt { get; init; }

    public required int[] Array { get; init; }
    public int[]? ArrayOpt { get; init; }

    public required Value Value { get; init; }
    public Value? ValueOpt { get; init; }

    public required Value[] ValueArray { get; init; }
    public Value[]? ValueArrayOpt { get; init; }

    public required Pair Record { get; init; }
    public Pair? RecordOpt { get; init; }

    public required Dictionary<string, int> Map { get; init; }
    [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfDocuments)]
    public required Dictionary<int, int> IntKeyMap { get; init; }
    [BsonSerializer(typeof(GuidStringDictionarySerializer))]
    public required Dictionary<Guid, string> GuidKeyMap { get; init; }
}

public record UserId([property: BsonGuidRepresentation(GuidRepresentation.Standard)] Guid Value);

public record UserMapDocument
{
    [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfDocuments)]
    public required Dictionary<UserId, string> Users { get; init; }
}
