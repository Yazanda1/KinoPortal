// File: Migration/SchemaBootstrapper.cs
using System;
using Microsoft.Extensions.Logging;                           // ILogger<>
using Umbraco.Cms.Core.Composing;                             // IComposer
using Umbraco.Cms.Core.DependencyInjection;                   // IUmbracoBuilder
using Umbraco.Cms.Core.Events;                                // INotificationHandler<>
using Umbraco.Cms.Core.Notifications;                         // UmbracoApplicationStartedNotification
using Umbraco.Cms.Infrastructure.Scoping;                     // IScopeProvider

namespace KinoPortal.Migrations
{
    /// <summary>
    /// Registriert einen Startup-Handler, der die nötigen Tabellen via SQL sicherstellt.
    /// </summary>
    public class SchemaComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            // Beim Start einmalig ausführen
            builder.AddNotificationHandler<UmbracoApplicationStartedNotification, EnsureSchemaHandler>();
        }
    }

    public class EnsureSchemaHandler : INotificationHandler<UmbracoApplicationStartedNotification>
    {
        private readonly IScopeProvider _scopeProvider;
        private readonly ILogger<EnsureSchemaHandler> _logger;

        public EnsureSchemaHandler(IScopeProvider scopeProvider, ILogger<EnsureSchemaHandler> logger)
        {
            _scopeProvider = scopeProvider;
            _logger = logger;
        }

        public void Handle(UmbracoApplicationStartedNotification notification)
        {
            try
            {
                using var scope = _scopeProvider.CreateScope(autoComplete: true);
                var db = scope.Database;

                // ===== KP_Booking =====
                db.Execute(@"
CREATE TABLE IF NOT EXISTS KP_Booking (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    ScreeningKey TEXT NOT NULL,
    FilmKey TEXT NOT NULL,
    SeatRow TEXT NOT NULL,
    SeatNumber INTEGER NOT NULL,
    CustomerName TEXT NULL,
    CustomerEmail TEXT NULL,
    Status TEXT NOT NULL,
    CreatedUtc TEXT NOT NULL
);");

                db.Execute(@"
CREATE UNIQUE INDEX IF NOT EXISTS IX_KP_Booking_UniqueSeat
ON KP_Booking (ScreeningKey, SeatRow, SeatNumber);");

                // ===== KP_PreorderItem =====
                db.Execute(@"
CREATE TABLE IF NOT EXISTS KP_PreorderItem (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    BookingId INTEGER NOT NULL,
    SnackKey TEXT NOT NULL,
    Quantity INTEGER NOT NULL
);");

                db.Execute(@"
CREATE INDEX IF NOT EXISTS IX_KP_PreorderItem_Booking
ON KP_PreorderItem (BookingId);");

                // ===== KP_Feedback =====
                db.Execute(@"
CREATE TABLE IF NOT EXISTS KP_Feedback (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    FilmKey TEXT NOT NULL,
    Rating INTEGER NOT NULL,
    Comment TEXT NULL,
    UserName TEXT NULL,
    CreatedUtc TEXT NOT NULL
);");

                db.Execute(@"
CREATE INDEX IF NOT EXISTS IX_KP_Feedback_Film
ON KP_Feedback (FilmKey);");

                _logger.LogInformation("Schema ensured: KP_Booking, KP_PreorderItem, KP_Feedback.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Schema creation failed.");
                throw;
            }
        }
    }
}
