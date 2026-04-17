# C# Styleguide for 2. Semester Eksamensopgave

Dette er en kortfattet vejledning til den foretrukne kodestil for C# i eksamensprojektet. Formålet er at sikre ensartethed, læsbarhed og vedligeholdelse på tværs af hele projektet.

## 1. Navngivning
Konsekvent brug af standard C# navngivningskonventioner er essentiel for læsbarheden.

| Element | Konvention | Eksempel | Beskrivelse |
| :--- | :--- | :--- | :--- |
| **Klasser, Interfaces, Metoder** | PascalCase | `public class Customer` | Hvert ord starter med stort. |
| **Properties** | pascalCase | `public int CustomerId { get; set; }` | Offentlige properties bruger pascalCase. |
| **Felter (Fields)** | _camelCase | `private readonly string _name;` | Private felter starter med underscore (_) efterfulgt af camelCase. |
| **Lokale Variabler, Metode Parametre** | camelCase | `int itemCount = 0;` | Lokale variabler og metoder parametre bruger camelCase. |
| **Konstanter** | UPPER_CASE | `public const int MAX_AMOUNT = 100;` | Statiske readonly eller const felter. |
| **Namespaces** | PascalCase | `ProjectName.Model` | Hvert niveau starter med stort. |

## 2. Struktur og Formatering

### 2.1. Parenteser og Indrykning
* Brug **Allman-stil** (parentesen på en ny linje) for metoder og klasser.
* Brug **4 mellemrum** til indrykning.

### 2.2. Tomme Linjer
* Brug tomme linjer til at adskille logiske blokke i kode og mellem metode-definitioner for øget læsbarhed.
* En tom linje efter `using`-direktiver (Imports) er foretrukket.

### 2.3. Brug af `var`
* Brug `var`, når typen er åbenlys fra initialiseringen på samme linje.
  * *Eksempel:* `var name = "Peter";`
* Undgå `var`, når typen ikke er indlysende.
  * *Eksempel:* `List<Customer> customer = new List<Customer>();` (Selvom `var` kunne bruges her, er det bedre at angive den fulde type, hvis listen initialiseres uden elementer).

## 3. Kommentarer og Dokumentation

### 3.1. XML Dokumentation Kommentarer
* Brug XML dokumentation kommentarer (`///`) til offentlige klasser, interfaces, metoder og properties. Dette hjælper med IntelliSense og generering af dokumentation.
* Inkluder mindst et `<summary>` tag.

### 3.2. Linje Kommentarer
* Brug linje kommentarer (`//`) til at forklare kompliceret eller vigtig logik, eller tanken bag koden.
* Undgå at kommentere kode, der er åbenlys, som f.eks.: `x += 1 // Increases x by 1`

## 4. Designprincipper

### 4.1. Kortfattethed
* Metoder bør være korte og fokusere på én opgave (**Single Responsibility Principle**).

### 4.2. Undgå Magiske Tal
* Brug navngivne konstanter eller værdier fra en konfiguration i stedet for "magiske tal" (tal uden forklaring).

### 4.3. Undtagelse Håndtering (Exception Handling)
* Fang kun de undtagelser, der kan håndteres meningsfuldt.
* Undgå at "sluge" generelle `Exception` (`catch (Exception e)`), medmindre der gen-kastes (re-throw) efter logging.

## 5. C# Specifikke Anbefalinger
* Brug **string-interpolation** (`$"Tekst {variabel}"`) i stedet for `string.Format()` eller streng-sammenkædning (`+`).

## 6. Sprog
* Alle navne på klasser, metoder, variabler, kommentarer osv. skal være på **Engelsk** for bedre integration med standardbiblioteker.