using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Bacen.Domain.Entities;

public abstract class Entity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; private set; }
    public Guid CorrelationId { get; set; }

    public Entity()
    {
        CorrelationId = Guid.NewGuid();
    }
}