
URScriptet i dette projekt er udviklet og testet direkte på robotten via robotens pendant.

Scriptet kører på robotten og styrer robotarm, gripper og conveyor belt.
Desktop-applikationen kommunikerer med robotten og starter scriptet eksternt.


Procesbeskrivelse

Robotten kører i et kontinuerligt loop og følger denne proces:

1. En sensor ved bordet registrerer en genstand (fx tallerken eller kop).
2. Robotarmen bevæger sig til opsamlingspositionen.
3. Gripperen lukker og samler genstanden op.
4. Robotarmen flytter genstanden til conveyor beltet.
5. Gripperen åbner og placerer genstanden på conveyor beltet.
6. Conveyor beltet starter.
7. En sensor ved enden af conveyor beltet registrerer genstanden.
8. Conveyor beltet stopper.
9. Systemet venter på, at genstanden fjernes.
10. Systemet venter på en ny genstand og gentager processen.

Systemet anvender flere digitale signaler til at styre processen.
En bord-sensor (digital input) bruges til at registrere, om der ligger en genstand på bordet.
En endesensor (digital input) er placeret for enden af conveyor beltet og bruges til at registrere, når genstanden har nået slutningen.
Derudover anvendes et digitalt output til conveyor beltet, som bruges til at starte og stoppe conveyor beltets bevægelse.

- Robotten skal være i **Remote Mode**
- Scriptet kører selvstændigt på robotten, når det er startet
- Sikkerhed håndteres af robotcontrolleren (sikkerhedszoner og nødstop)
