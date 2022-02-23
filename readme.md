# Aplikacja NBP DASHBOARD

## Technologie
- backend: ASP .Net core 5.0,
  - Restsharp
  - AutoMapper
  - MediatR
  - EntityFramework Core
- baza: SQL Server (ja używam wersji 2019, ale to nie powinno robić różnicy, nie ma tu elementów które nie powinny działać na starszych wersjach bazy danych, jak również na innych dialekach SQL),
- frontend: React 

## Założenia aplikacji:

- podział aplikacji na kilka projektów (core,apiclient,web,cli,tests)
- CQRS - podzielenie Commandów (powiązanych z akcji controllerów) na pobierająca i zmieniające dane
- komunikacja Controller - Repozytorium za pomocą MediatR
- pomiędzy NBP ApiClient a bazą jest element pośredniczący - umożliwi choćby zmianę miejsca zapisu danych
- **zakładam** że dane w wyników kursów walut zmieniają się rzadko, dlatego dane będą pobierane będą pobierane w momencie włączenia aplikacji web, a w momencie zapytania przez użytownika zostaną tylko pobrane z bazy i wyświetlone.
- możliwe będzie wywołanie importu z konsoli (aplikacja nbp.cli) lub z frontendu - tylko w przypadku gdy danych nie ma jeszcze w bazie  
- spróbujemy zrobić prosty frontend w ReactJS

## Podział aplikacji
Aplikacja jest podzielona na następuje części:

- nbp.api.client - biblioteka odpowiedzialna tylko i wyłącznie za pobieranie danych z API NBP
- nbp.core - biblioteka odpowiedzialna za logikę biznesową
- nbp.cli - aplikacja consolowa za pomocą której można wywoływać niektórze polecania aplikacji, np pobranie danych z api z danego dnia. Może się przydać jeśli byśmy chcieli zautomatyzować proces pobierania danych
- nbp - aplikacja web,
  - api - api restfull służące to pobierania danych o kursach walut
  - frontend - aplikacja JS służąca jako frontend do api
- nbp.tests - projekt testów do aplikacji, zarówno jednostkowych jak i integracyjnych


## Jak wykonać migrację bazy
baza danych musi być założona,
ConnectionString należy wpisać w pliku appsettings.json w katalogu nbp (źródła)
są dwie możliwości założenia tabel w bazie:
* użycie skryptów migracji:
  w katalogu głównym aplikacji **nbp** (web api) wykonać
  dotnet ef database update --project ..\nbp.core\nbp.core.csproj
* lub wykonać zawartość pliku `database.sql`

## Jak uruchomić aplikację (web)

są trzy możliwości
- uruchomić z Visual Studio/Rider
- za pomocą polecenia dotnet run
- przekompilować projekt oraz uruchomić z poprzez wykonanie nbp.exe

## Swagger

Domyślny adres api Swagger: `http://localhost:5000/swagger/index.html`

## Frontend
Domyślny adres frontendu: `http://localhost:5000`

## CLI

aplikacja nbp.cli posiada następujące polecenia

- **currency import** - import danych o walutach (musi być wykonany w pierwszej kolejności)
- **exchange-rate import** - import danych kursów walut