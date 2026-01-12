using Microsoft.ML.Data;

namespace MovieRecommenderGrpcService.Models
{
    public class MovieRatingData
    {
        [LoadColumn(0)]
        public float userId;

        [LoadColumn(1)]
        public float movieId;

        [LoadColumn(2)]
        public float Label;
    }
}
