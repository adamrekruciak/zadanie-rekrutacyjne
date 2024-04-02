
# Zadanie rekrutacyjne dla firmy Profisys

## Opis projektu

Aplikacja webowa oparta na platformie .NET, stworzona w celu importowania danych z plików CSV do bazy danych oraz wyświetlania ich w formie listowanej lub tabelarycznej z możliwością przejrzenia konkretnych  zamówień.
## Wykorzystane technologie
- Frontend: Vue.js
- Backend: ASP.NET 7.0 Web API
- Baza danych: Microsoft SQL Server

## Uruchomienie projektu
1. Sklonuj repozytorium z GitHub.
2. Uruchom backend aplikacji w środowisku Visual Studio.
3. Po uruchomieniu projektu w Visual Studio  wpisz komendę dotnet ef database update w celu utworzenia bazy danych.
3. Uruchom frontend aplikacji poprzez wpisanie poleceń w terminalu w folderze głównym projektu:
    ```
    cd frontend
    npm install
    npm run serve
    ```
4. Aplikacja będzie dostępna pod adresem http://localhost:8080/.
