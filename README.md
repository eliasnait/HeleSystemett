# HeleSystemet

HeleSystemet er et project, der styrer en robot med gripper og conveyor belt.
Systemet kombinerer robotkommunikation, database og brugerhåndtering.

Projektet består af tre hoveddele:

1. Desktop-applikation (C# / Avalonia)
   - Brugerinterface (GUI)
   - Brugerlogin og security
   - Styring af robotfunktioner

2. Robotkommunikatio
   - TCP-forbindelse til robot (Dashboard + URScript)
   - Tænde/slukke robot
   - Sende URScript-programmer

3. Database (SQLite)
   - Brugeroplysninger
   - Rollehåndtering (admin / bruger)
   - Sikker password-hashing (Sikkerhed for passwort)

Projektet :

- `Program.cs` – Applikationens entry point  
- `App.axaml` – Avalonia app-konfiguration  
- `MainWindow.axaml` – Brugerinterface  
- `Robot.cs` – Kommunikation med robotten  
- `Models.cs` – Database- og loginlogik  
- `database.sqlite` – Lokal database  

Robot-logikken (Script) i URScript ligger separat og er dokumenteret i sin egen README. 

Sikkerhed:

- Passwords hashes med salt (PBKDF2)
- Database bruges lokalt

Hvordan køres det?

1. Åbn projektet i Rider
2. Sætter robotten i remote mode på pendanten
3. runner program
4. Log ind
5. Forbind til robot og start sekvensen

Lavet af 

Elias Nait, Ismael Haiba, Omar Adham


