## Implementierte Funktionen
- **Platzreservierung:** Tabelle `KP_Booking` zur Verwaltung von Sitzplatz-Buchungen.
- **Snacks & Getränke:** Verwaltung über `Snack-Übersicht` im Backoffice, Auswahl im Frontend.
- **Feedback:** Besucher können Filme mit 1–5 Sternen bewerten und Kommentare abgeben. Daten werden in `KP_Feedback` gespeichert.
- **Migrationen:** Erstellung der Tabellen `KP_Booking`, `KP_PreorderItem`, `KP_Feedback` beim Start.
- **Frontend-Seiten:**
  - Startseite mit Übersicht (Filme, Snacks, Feedback).
  - Film-Detailseite mit Postern, Beschreibung, Bewertung und Kommentaren.
  - Snack-Übersicht.
  - Feedback-Seite mit Formular.
- **Styling:** Bootstrap 5 + eigenes CSS in `wwwroot/css/site.css`.
- **Layout:** Gemeinsames Layout in `Views/Master.cshtml`.

## Starten des Projekts
1. Repository klonen
2. Pakete wiederherstellen:
   ```bash
   dotnet restore