# Benutzerhandbuch für die Anwendung

## Inhaltsverzeichnis

1. Einleitung
2. Systemanforderungen
3. Installation
4. Konfiguration
5. Start der Anwendung
6. Vorbereitung der Anwendung
7. Anwendung nutzen
8. Fehlerbehebung
9. Häufig gestellte Fragen (FAQ)
10. Support und Kontakt

## 1. Einleitung

Willkommen zum Benutzerhandbuch für die Anwendung. Dieses Handbuch soll Ihnen helfen, die Anwendung zu installieren, zu
konfigurieren und zu nutzen.

## 2. Systemanforderungen

- Betriebssystem: Windows 10 oder höher
- Node.js: Version 18 oder höher
- .NET SDK: Version 8.0 oder höher
- Docker: Version 20.10 oder höher
- Docker Compose: Version 1.29 oder höher

## 3. Installation

### 3.1. Node.js und Angular CLI installieren

1. Laden Sie Node.js von der offiziellen Website herunter und installieren Sie es.
2. Installieren Sie Angular CLI global:
   ```sh
   npm install -g @angular/cli
   ```

### 3.2. .NET SDK installieren

1. Laden Sie das .NET SDK von der offiziellen Microsoft-Website herunter und installieren Sie es.

### 3.3. Docker und Docker Compose installieren

1. Laden Sie Docker Desktop von der offiziellen Website herunter und installieren Sie es.
2. Stellen Sie sicher, dass Docker Compose zusammen mit Docker Desktop installiert wird.

## 4. Konfiguration

### 4.1. Projekt aus dem Repository klonen

1. Klonen Sie das Projekt-Repository:
   ```sh
   git clone <repository-url>
   ```
2. Navigieren Sie in das Projektverzeichnis:
   ```sh
   cd <project-directory>
   ```

### 4.2. Umgebungsvariablen konfigurieren

1. Erstellen Sie eine `.env`-Datei im Projektverzeichnis und fügen Sie die erforderlichen Umgebungsvariablen hinzu.

## 5. Start der Anwendung

### 5.1. Anwendung starten

1. Starten Sie Docker Compose, um die Anwendung zu starten:
   ```sh
   docker-compose up --build
   ```
2. Die Anwendung sollte nun auf den konfigurierten Ports laufen.

### 5.2. Zugriff auf die Anwendung

- Server: `http://localhost:5056`
- Client: `http://localhost:8080`

## 6. Vorbereitung der Anwendung

### 6.1. Nutzer Importieren

### 6.2. Manager Anlegen

### 6.3. Richter Anlegen

### 6.4. Klassenlehrer Anlegen

### 6.5. Schüler Anlegen

### 6.6. Klassen Anlegen

### 6.7. Disziplinen Anlegen

### 6.8. Teams Anlegen

## 7. Anwendung nutzen

### 7.1. Schüler in Teams eintragen

### 7.2. Schüler in Disziplinen eintragen

### 7.3. Punkte vergeben

### 7.4. Ergebnisse einsehen

## 8. Fehlerbehebung

### 8.1. Häufige Fehler und Lösungen

- **Fehler:** Angular CLI-Befehl wird außerhalb eines Arbeitsbereichs ausgeführt.
  **Lösung:** Stellen Sie sicher, dass die `angular.json`-Datei im richtigen Verzeichnis vorhanden ist.

- **Fehler:** Anwendung wird nicht korrekt gebaut.
  **Lösung:** Überprüfen Sie die `Dockerfile`- und `docker-compose.yml`-Konfigurationen.

## 9. Häufig gestellte Fragen (FAQ)

### 9.1. Wie kann ich die Anwendung auf einem anderen Port ausführen?

- Ändern Sie die Port-Mappings in der `docker-compose.yml`-Datei.

### 9.2. Wie kann ich Abhängigkeiten aktualisieren?

- Führen Sie `npm update` im `sf.client`-Verzeichnis aus.
- Führen Sie `dotnet restore` im `sf.Server`-Verzeichnis aus.

## 10. Support und Kontakt

Für weitere Unterstützung kontaktieren Sie bitte das Support-Team
unter [support.sf.github@tsuki.wtf](mailto:support.sf.github@tsuki.wtf).
