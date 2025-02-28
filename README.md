# Benutzerhandbuch für die Anwendung

---
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

---
## 1. Einleitung

Willkommen zum Benutzerhandbuch für die Anwendung. Dieses Handbuch soll Ihnen helfen, die Anwendung zu installieren, zu
konfigurieren und zu nutzen.

---
## 2. Systemanforderungen

- Betriebssystem: Windows 10 oder höher
- Node.js: Version 18 oder höher
- .NET SDK: Version 8.0 oder höher
- Docker: Version 20.10 oder höher
- Docker Compose: Version 1.29 oder höher

---
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

---
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

---
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

---
## 6. Vorbereitung der Anwendung


### 6.1. Manager Anlegen

Manager können über das Admin-Panel angelegt werden. Gehen Sie zum Abschnitt "Nutzerverwaltung" und wählen Sie "Neuen Nutzer hinzufügen". Füllen Sie das Formular aus und wählen Sie die Rolle "Manager" aus der Dropdown-Liste.
![img.png](img.png)


### 6.3. Richter Anlegen

Richter können ebenfalls über das Admin-Panel angelegt werden. Gehen Sie zum Abschnitt "Nutzerverwaltung" und wählen Sie "Neuen Nutzer hinzufügen". Füllen Sie das Formular aus und wählen Sie die Rolle "Judge" aus der Dropdown-Liste.


### 6.4. Klassenlehrer Anlegen

Um Klassenlehrer anzulegen, gehen Sie zum Abschnitt "Nutzerverwaltung" und wählen Sie "Neuen Nutzer hinzufügen". Füllen Sie das Formular aus und wählen Sie die Rolle "Klassenlehrer" aus der Dropdown-Liste. Weisen Sie dem Klassenlehrer eine oder mehrere Klassen zu.


### 6.5. Schüler Anlegen

Schüler können über das Admin-Panel angelegt werden. Gehen Sie zum Abschnitt "Nutzerverwaltung" und wählen Sie "Neuen Nutzer hinzufügen". Füllen Sie das Formular aus und wählen Sie die Rolle "Schüler" aus der Dropdown-Liste. Weisen Sie dem Schüler eine Klasse und ein Team zu.


### 6.6. Klassen Anlegen

Klassen können über das Admin-Panel angelegt werden. Gehen Sie zum Abschnitt "Klassenverwaltung" und wählen Sie "Neue Klasse hinzufügen". Füllen Sie das Formular aus und geben Sie den Namen der Klasse an. Die zugehörigen Schüler und Lehrer können durch den Abschnitt "Nutzerverwaltung" aus der Klass-Dropdown-Liste zugeordnet werden.


### 6.7. Disziplinen Anlegen

Disziplinen können über das Admin-Panel angelegt werden. Gehen Sie zum Abschnitt "Disziplinenverwaltung" und wählen Sie "Neue Disziplin hinzufügen". Füllen Sie das Formular aus und geben Sie den Namen der Disziplin an. Die zugehörigen Teams und Richter können durch den Abschnitt "Teamverwaltung" aus der Disziplinen-Dropdown-Liste zugeordnet werden.



### 6.8. Teams Anlegen

Teams können über das Admin-Panel angelegt werden. Gehen Sie zum Abschnitt "Teamverwaltung" und wählen Sie "Neues Team hinzufügen". Füllen Sie das Formular aus und geben Sie den Namen des Teams und Disziplinen an. Die zugehörigen Schüler können durch den Abschnitt "Nutzerverwaltung" zugeordnet werden.


---
## 7. Anwendung nutzen

### 7.1. Schüler in Teams eintragen

Um Schüler in Teams einzutragen, gehen Sie zum Abschnitt "Nutzerverwaltung" und wählen Sie den gewünschten Schüler aus. Bearbeiten Sie das Profil des Schülers und wählen Sie das Team aus der Dropdown-Liste aus. Speichern Sie die Änderungen, um den Schüler dem Team zuzuweisen.

### 7.2. Disziplinen verwalten

Um Disziplinen zu verwalten, gehen Sie zum Abschnitt "Disziplinenverwaltung". Hier können Sie Disziplinen hinzufügen, bearbeiten oder löschen. Sie können aber Teams und Richter den Disziplinen durch abschnitt "Teams" zuweisen.

### 7.3. Punkte vergeben

Um Punkte zu vergeben, gehen Sie zum Abschnitt "NutzerverWaltung". Hier können Sie Punkte für Schülern hinzufügen. 

### 7.4. Ergebnisse anzeigen

Um die Ergebnisse anzuzeigen, gehen Sie zum Abschnitt "Ergebnisse". Hier können Sie die Top 10 Teams nach Disziplin oder insgesamt anzeigen. Verwenden Sie die Dropdown-Menüs, um die gewünschten Filter auszuwählen.

### 7.5. Benutzerrollen verwalten

Um Benutzerrollen zu verwalten, gehen Sie zum Abschnitt "Nutzerverwaltung". Hier können Sie die Rollen der Benutzer ändern, neue Benutzer hinzufügen oder bestehende Benutzer löschen. Verwenden Sie die Dropdown-Menüs, um die gewünschten Rollen auszuwählen.

### 7.6. Benutzerprofile bearbeiten

Um Benutzerprofile zu bearbeiten, gehen Sie zum Abschnitt "Nutzerverwaltung" und wählen Sie den gewünschten Benutzer aus. Klicken Sie auf "Profil bearbeiten" und aktualisieren Sie die gewünschten Informationen. Speichern Sie die Änderungen, um das Profil zu aktualisieren.

### 7.7. Kontaktinformationen anzeigen

Um die Kontaktinformationen anzuzeigen, gehen Sie zum Abschnitt "Kontakt". Hier finden Sie die Kontaktdaten und Social-Media-Links des Veranstalters. Verwenden Sie die bereitgestellten Links, um den Veranstalter zu kontaktieren.

---
## 8. Fehlerbehebung

### 8.1. Häufige Fehler und Lösungen

- **Fehler:** Angular CLI-Befehl wird außerhalb eines Arbeitsbereichs ausgeführt.
- **Lösung:** Stellen Sie sicher, dass die `angular.json`-Datei im richtigen Verzeichnis vorhanden ist.
---
- **Fehler:** Anwendung wird nicht korrekt gebaut.
- **Lösung:** Überprüfen Sie die `Dockerfile`- und `docker-compose.yml`-Konfigurationen.

---
## 9. Häufig gestellte Fragen (FAQ)

### 9.1. Wie kann ich die Anwendung auf einem anderen Port ausführen?

- Ändern Sie die Port-Mappings in der `docker-compose.yml`-Datei.

### 9.2. Wie kann ich Abhängigkeiten aktualisieren?

- Führen Sie `npm update` im `sf.client`-Verzeichnis aus.
- Führen Sie `dotnet restore` im `sf.Server`-Verzeichnis aus.

---
## 10. Support und Kontakt

Für weitere Unterstützung kontaktieren Sie bitte das Support-Team
unter [support.sf.github@tsuki.wtf](mailto:support.sf.github@tsuki.wtf).
