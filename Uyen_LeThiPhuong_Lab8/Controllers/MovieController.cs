using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using MovieRecommenderGrpcService;

namespace Uyen_LeThiPhuong_Lab8.Controllers
{
    public class MovieController : Controller
    {
        private readonly GrpcChannel channel;
        public MovieController()
        {
            //se va modifica portul corespunzator 
            channel = GrpcChannel.ForAddress("https://localhost:7085");
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Recommend()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Recommend(float userId, float
    movieId)
        {
            var client = new
    MovieRecommender.MovieRecommenderClient(channel);
            var request = new RecommendationRequest
            {
                UserId = userId,
                MovieId = movieId
            };

            var reply = client.Recommend(request);

            ViewBag.UserId = userId;
            ViewBag.MovieId = movieId;
            ViewBag.Score = reply.Score;
            ViewBag.Recommended = reply.Recommended;
            ViewBag.Message = reply.Message;

            return View();
        }
    }
}
