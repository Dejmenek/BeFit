# BeFit - Aplikacja do Śledzenia Treningów

### Spis Treści

1. [Informacje Ogólne](#informacje-ogólne)
2. [Użyte Technologie](#użyte-technologie)
3. [Funkcjonalności](#funkcjonalności)
4. [Instalacja i Konfiguracja](#instalacja-i-konfiguracja)
5. [Przykłady](#przykłady)

### Informacje Ogólne

**BeFit** to kompleksowa aplikacja webowa do śledzenia treningów, zaprojektowana w celu pomocy użytkownikom w monitorowaniu treningów, śledzeniu postępów i osiąganiu celów fitness. Jest to **projekt akademicki** opracowany w ramach zajęć, demonstrujący nowoczesne praktyki tworzenia aplikacji webowych przy użyciu ASP.NET Core.

Aplikacja oferuje przyjazny interfejs do zarządzania ćwiczeniami, tworzenia szablonów treningowych, logowania sesji treningowych i analizowania statystyk fitness. BeFit pomaga entuzjastom fitness utrzymać regularność treningów, oferując funkcje takie jak szablony treningowe, śledzenie sesji i analityka postępów.

### Użyte Technologie

- **.NET 9.0** - Nowoczesna platforma .NET do budowania aplikacji webowych
- **ASP.NET Core MVC** - Framework webowy do budowania dynamicznych aplikacji internetowych
- **Entity Framework Core 9.0** - ORM do operacji na bazie danych
- **SQL Server** - System zarządzania relacyjną bazą danych
- **ASP.NET Core Identity** - System uwierzytelniania i autoryzacji
- **Bootstrap 5** - Framework CSS do responsywnego projektowania
- **jQuery** - Biblioteka JavaScript do manipulacji DOM
- **jQuery Validation** - Walidacja formularzy po stronie klienta
- **Razor Pages** - Silnik renderowania stron po stronie serwera

### Funkcjonalności

#### Zarządzanie Użytkownikami
- Rejestracja i uwierzytelnianie użytkowników
- Bezpieczne logowanie z potwierdzeniem e-mail
- Kontrola dostępu oparta na rolach
- Zarządzanie profilem użytkownika

#### Zarządzanie Ćwiczeniami
- Przeglądanie i wyszukiwanie ćwiczeń
- Szczegóły ćwiczeń obejmujące:
  - Typ ćwiczenia (Kardio, Siła, Elastyczność, itp.)
  - Grupy mięśni docelowych
  - Poziomy trudności
  - Wymagany sprzęt
  - Szczegółowe instrukcje

#### Szablony Treningowe
- Tworzenie niestandardowych szablonów treningowych
- Definiowanie preferowanych dni treningowych
- Dodawanie wielu ćwiczeń do szablonów
- Ustawianie celów i opisów dla każdego szablonu
- Edycja i usuwanie szablonów
- Przeglądanie kalendarza szablonów

#### Sesje Treningowe
- Logowanie sesji treningowych z czasem rozpoczęcia i zakończenia
- Śledzenie szczegółowej wydajności ćwiczeń:
  - Serie i powtórzenia
  - Użyty ciężar
  - Czas odpoczynku między seriami
  - Tempo (dla ćwiczeń czasowych)
  - Czas trwania (dla ćwiczeń kardio)
  - Dystans (dla biegania/jazdy na rowerze)
- Dodawanie notatek do sesji treningowych
- Przeglądanie historii treningów

#### Panel i Statystyki
- **Statystyki Treningów:**
  - Całkowita liczba ukończonych treningów
  - Aktualna seria treningowa
  - Całkowity czas spędzony na treningu
- **Statystyki Ćwiczeń:**
  - Liczba sesji treningowych na ćwiczenie
  - Całkowita liczba wykonanych powtórzeń
  - Średni i maksymalny podnoszony ciężar
- **Kalendarz Treningowy:**
  - Widok tygodniowy zaplanowanych szablonów treningowych
  - Wizualna reprezentacja harmonogramu treningów

### Instalacja i Konfiguracja

#### Wymagania

- **.NET 9.0 SDK** - [Pobierz tutaj](https://dotnet.microsoft.com/download/dotnet/9.0)
- **SQL Server** - SQL Server Express lub LocalDB (zawarty w Visual Studio)
- **Visual Studio 2022** lub **Visual Studio Code** (zalecane)
- **Git** (opcjonalnie, do klonowania repozytorium)

#### Krok 1: Sklonuj Repozytorium

```bash
git clone <adres-repozytorium>
cd BeFit
```

#### Krok 2: Skonfiguruj Połączenie z Bazą Danych

1. Otwórz plik `BeFit/appsettings.json`
2. Zaktualizuj connection string, aby pasował do Twojej instancji SQL Server:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=BeFit;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

Dla SQL Server Express użyj:
```json
"DefaultConnection": "Server=.\\SQLEXPRESS;Database=BeFit;Trusted_Connection=True;MultipleActiveResultSets=true"
```

#### Krok 3: Przywróć Zależności

```bash
cd BeFit
dotnet restore
```

#### Krok 4: Uruchom Migracje Bazy Danych

```bash
dotnet ef database update
```

To utworzy schemat bazy danych i zastosuje wszystkie migracje.

#### Krok 5: Uruchom Aplikację

```bash
dotnet run
```

#### Krok 6: Konfiguracja Początkowa

1. Przejdź na stronę rejestracji
2. Utwórz nowe konto użytkownika
3. Potwierdź swój e-mail (jeśli włączone jest potwierdzenie e-mail)
4. Zaloguj się i zacznij korzystać z aplikacji

#### Uwagi dla Deweloperów

- Aplikacja używa migracji Entity Framework Core do zarządzania schematem bazy danych
- Wstępne wypełnianie danych jest wykonywane automatycznie przy starcie aplikacji
- W trybie deweloperskim baza danych jest tworzona automatycznie, jeśli nie istnieje

### Przykłady

#### Strona Główna
<img width="1868" height="892" alt="image" src="https://github.com/user-attachments/assets/3d42cc9d-6618-42be-bbce-cd9342157d48" />

#### Panel
<img width="1868" height="892" alt="image" src="https://github.com/user-attachments/assets/3e3f15b8-b8f4-408a-944b-6c2c4fb63c53" />

#### Szablony Treningowe
<img width="1868" height="892" alt="image" src="https://github.com/user-attachments/assets/64bb7d88-8a04-4b94-829c-8258ade9289b" />

#### Sesja Treningowa
<img width="1868" height="892" alt="image" src="https://github.com/user-attachments/assets/eca32261-5736-4f4e-a330-783a068e01ab" />
