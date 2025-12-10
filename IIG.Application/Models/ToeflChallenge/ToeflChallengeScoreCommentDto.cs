using IIG.Core.Common.Enums;
using IIG.Core.Common.Models.Files;

namespace IIG.Application.Models;

public class ToeflChallengeScoreCommentDto
{
    public Guid Id { get; set; }

    public Guid RankingScoreId { get; set; }

    public int FromScore { get; set; }

    public int ToScore { get; set; }

    public string ImageUrl { get; set; }

    public DateTime? Created { get; set; }

    public DateTime? Modified { get; set; }
}