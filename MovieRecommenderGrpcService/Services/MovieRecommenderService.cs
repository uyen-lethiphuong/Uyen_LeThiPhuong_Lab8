using Grpc.Core;
using Microsoft.ML;
using MovieRecommenderGrpcService.Models;

namespace MovieRecommenderGrpcService.Services
{
    public class MovieRecommenderService : MovieRecommender.MovieRecommenderBase
    {
        private readonly PredictionEngine<MovieRatingData,
 MovieRatingPrediction> _predictionEngine;

        public MovieRecommenderService()
        {
            var mlContext = new MLContext();

            string mlModelPath = "MovieRecommenderModel.mlnet";

            ITransformer mlModel = mlContext.Model.Load(mlModelPath, out
 _);

            _predictionEngine =
 mlContext.Model.CreatePredictionEngine<MovieRatingData,
 MovieRatingPrediction>(mlModel);
        }

        public override Task<RecommendationReply>
 Recommend(RecommendationRequest request, ServerCallContext context)
        {
            var input = new MovieRatingData
            {
                userId = request.UserId,
                movieId = request.MovieId
            };

            var prediction = _predictionEngine.Predict(input);
            var score = float.IsNaN(prediction.Score) ? 0 : prediction.Score;
            string msg = "";
            if (score >= 4.0f)
            {
                msg = "high recommended";
            }
            else if (score >= 3.5f)
            {
                msg = "Neutral";
            }
            else
            {
                msg = "Not recommended";
            }

            var reply = new RecommendationReply
            {
                Score = score,
                Recommended = score >= 3.5f,
                Message = msg
            };

            return Task.FromResult(reply);
        }
    }
}
