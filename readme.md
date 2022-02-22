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

