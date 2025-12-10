using IIG.Core.Providers.MongoDbProvider.Infrastructure;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IIG.Core.Common.MongoDataModels.ScoringByAI
{
    [BsonCollection("scoringByAI")]
    public class MgScoringAIModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public Guid chooseId { get; set; }
        public List<ListWord> listWord { get; set; }
    }

    public class ListWord
    {
        public string Words { get; set; }
        public List<Phonemes> Phonemes { get; set; }
    }
    public class Phonemes
    {
        public string Phoneme { get; set; }
        public PronunciationAssessment PronunciationAssessment { get; set; }
        public double Offset { get; set; }
        public double Duration { get; set; }
    }
    public class PronunciationAssessment
    {
        public double AccuracyScore { get; set; }
    }
}
