# 🎬 KinoPortal – Streaming & Kino Night

Dies ist ein Projekt im Rahmen der Vorlesung/Übung, bei dem ein kleines **Kino-Streaming-Portal** mit **ASP.NET Core MVC** und **Umbraco CMS** entwickelt wurde.  
Es bietet eine Übersicht über Filme, Snacks und ermöglicht Nutzern, Feedback zu Filmen abzugeben.

---

## ✨ Funktionen

- **Startseite (Home)**
  - Begrüßungstext („Willkommen zur Kino-/Streaming-Night!“)
  - Übersicht mit Links zu Filmen, Snacks und Feedback
  - Icons für eine ansprechendere Darstellung

- **Filme**
  - Anzeige von Filmen mit Poster, Titel und Beschreibung
  - Detailseite pro Film
  - Feedback-Übersicht (Durchschnittsbewertung + letzte Kommentare)

- **Snacks**
  - Übersicht mit Snack-Angeboten (z. B. Popcorn, Cola, Nachos)
  - Preise und Bilder
  - Möglichkeit, Menge auszuwählen

- **Feedback**
  - Nutzer können Filme mit **Bewertung (1–5 Sterne)** und Kommentar versehen
  - Feedback wird in einer SQL-Tabelle gespeichert (`KP_Feedback`)
  - Anzeige der letzten Bewertungen und Durchschnitt pro Film

---

## ⚙️ Technische Umsetzung

- **ASP.NET Core MVC** als Framework
- **Umbraco CMS** für Content-Management (Filme, Snacks, Feedback-Seiten)
- **SQL-Datenbank** zur Speicherung des Feedbacks
- **Bootstrap & CSS** für ein einfaches Layout mit Karten und Icons
- **GitHub** zur Versionsverwaltung und Abgabe

---

## 📂 Projektstruktur

- `Controllers/` → Enthält u. a. `FeedbackSurfaceController.cs` (Steuerung der Feedback-Logik)  
- `Models/` → Enthält ViewModels wie `FeedbackViewModels.cs`  
- `Views/` → Razor Views (`Home.cshtml`, `Film.cshtml`, `SnackUebersicht.cshtml`, `FeedbackUebersicht.cshtml`)  
- `Migration/` → `SchemaBootstrapper.cs` für Datenbanktabellen  
- `wwwroot/` → statische Dateien (z. B. `site.css`, Medien)  

---

## 🚀 Installation & Start

1. Repository klonen:
   ```bash
   git clone https://github.com/Yazanda1/KinoPortal.git
   cd KinoPortal
