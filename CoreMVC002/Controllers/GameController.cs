using Microsoft.AspNetCore.Mvc;
using CoreMVC002.Models;

namespace CoreMVC002.Controllers
{
    public class GameController : Controller
    {
        private static XAXBEngine _engine = new XAXBEngine();

        public IActionResult Index()
        {
            return View(_engine);
        }

        [HttpPost]
        public IActionResult Guess(string guessNumber)
        {
            if (!_engine.IsGameOver(guessNumber))
            {
                ViewBag.Message = _engine.Result;
            }
            else
            {
                ViewBag.Message = "恭喜你猜對了！";
                ViewBag.PromptReplay = true;
            }

            return View("Index", _engine);
        }

        [HttpPost]
        public IActionResult Replay()
        {
            _engine.ResetGame();
            return RedirectToAction("Index");
        }
    }
}
