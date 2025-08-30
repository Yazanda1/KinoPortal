using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Infrastructure.Scoping;

namespace KinoPortal.Controllers
{
    // kein SurfaceController nötig
    public class FeedbackSurfaceController : Controller
    {
        private readonly IScopeProvider _scope;
        private readonly ILogger<FeedbackSurfaceController> _logger;

        public FeedbackSurfaceController(IScopeProvider scope, ILogger<FeedbackSurfaceController> logger)
        {
            _scope = scope;
            _logger = logger;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Submit(Guid filmKey, int rating, string? comment, string? userName)
        {
            try
            {
                if (rating < 1 || rating > 5)
                {
                    TempData["error"] = "Bitte eine Bewertung zwischen 1 und 5 wählen.";
                    return RedirectBack();
                }

                using var scope = _scope.CreateScope(autoComplete: true);
                var db = scope.Database;

                db.Execute(
                    "INSERT INTO KP_Feedback (FilmKey, Rating, Comment, UserName, CreatedUtc) VALUES (@0,@1,@2,@3,@4)",
                    filmKey, rating, comment, userName, DateTime.UtcNow);

                TempData["success"] = "Danke für dein Feedback!";
                return RedirectBack();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Feedback speichern fehlgeschlagen");
                TempData["error"] = "Fehler beim Speichern. Bitte später erneut versuchen.";
                return RedirectBack();
            }
        }

        // Hilfs-Redirect: zurück auf die aufrufende Seite
        private IActionResult RedirectBack()
        {
            var referer = Request.Headers["Referer"].ToString();
            return string.IsNullOrWhiteSpace(referer) ? Redirect("/") : Redirect(referer);
        }
    }
}
