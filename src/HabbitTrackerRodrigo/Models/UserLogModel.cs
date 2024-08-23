using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HabbitTrackerRodrigo.Models
{
    public class UserLogModel
    {
        [BsonElement("userId")]
        public int UserId { get; set; }

        [BsonElement("habitId")]
        public int HabitId { get; set; }

        [BsonElement("date")]
        public string Date { get; set; }

        [BsonElement("status")]
        public bool Completed { get; set; }
    }
}
