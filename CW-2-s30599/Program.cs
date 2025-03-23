using System.Numerics;
using CW_2_s30599;

var statki = new List<Statek>();
var kontenery = new List<Kontener>();

while (true)
{
    Console.WriteLine();
    Console.WriteLine("Lista kontenerowców:");

    if (statki.Count == 0) Console.WriteLine("Brak");
    else foreach (var statek in statki) Console.WriteLine(statek);
    
    Console.WriteLine();
    Console.WriteLine("Lista kontenerów:");

    if (kontenery.Count == 0) Console.WriteLine("Brak");
    else foreach (var kontener in kontenery) Console.WriteLine(kontener);
    
    Console.WriteLine();
    Console.WriteLine("Możliwe akcje:");
    Console.WriteLine("0. Wyjdź z aplikacji");
    Console.WriteLine("1. Dodaj kontenerowiec");

    if (statki.Count > 0)
    {
        Console.WriteLine("2. Usuń kontenerowiec");
        Console.WriteLine("3. Dodaj kontener");

        if (kontenery.Count > 0)
        {
            Console.WriteLine("4. Przenieś kontener");
            Console.WriteLine("5. Usuń kontener");
        }
    }
    
    Console.WriteLine();
    Console.Write("Podaj numer akcji: ");

    byte akcja;

    if (byte.TryParse(Console.ReadLine(), out akcja))
    {
        if (akcja == 0) break;
        
        if (akcja == 1)
        {
            Console.Clear();
            WypiszNaglowek("Nowy kontenerowiec");

            var maksPredkoscWezly = OdczytajLiczbeCalkowita<uint>(
                "Podaj maksymalną prędkość w węzłach"
            );
            var maksLiczbaKontenerow = OdczytajLiczbeCalkowita<uint>(
                "Podaj maksymalną liczbę kontenerów"
            );
            var maksWagaKontenerowTony = OdczytajLiczbeCalkowita<uint>(
                "Podaj maksymalną wagę brutto kontenerów w tonach"
            );
            var nowyStatek = new Statek(
                maksPredkoscWezly,
                maksLiczbaKontenerow,
                maksWagaKontenerowTony
            );
            
            statki.Add(nowyStatek);
            Console.Clear();
            Console.WriteLine("Kontenerowiec został dodany!");
        }
        else if (akcja == 2 && statki.Count > 0)
        {
            var idStatku = statki.Count == 1
                ? statki[0].Identyfikator
                : OdczytajLiczbeCalkowita<uint>(
                    "Wprowadź numer statku do usunięcia"
                );
            var indeksStatku = statki.FindIndex(
                s => s.Identyfikator == idStatku
            );

            if (indeksStatku == -1)
            {
                Console.Clear();
                Console.Error.WriteLine("Błąd: statek o podanym numerze nie istnieje!");
            }
            else
            {
                var frazaPotwierdzenia = "POTWIERDZAM";

                Console.WriteLine("Czy na pewno chcesz usunąć ten kontenerowiec?");
                Console.Write(
                    $"Wpisz {frazaPotwierdzenia}, aby potwierdzić lub cokolwiek innego, by anulować: "
                );

                var odpowiedz = Console.ReadLine();

                if (odpowiedz == frazaPotwierdzenia)
                {
                    statki.RemoveAt(indeksStatku);
                    Console.Clear();
                    Console.WriteLine("Kontenerowiec został usunięty!");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Anulowano usunięcie kontenerowca!");
                }   
            }
        }
        else
        {
            Console.Clear();
            Console.Error.WriteLine("Błąd: nieprawidłowy numer akcji!");
        }
    }
    else
    {
        Console.Clear();
        Console.Error.WriteLine("Błąd: nieprawidłowy numer akcji!");
    }
}

static void WypiszNaglowek(string tytul)
{
    var myslniki = new String('-', tytul.Length + 2);
    
    Console.WriteLine(myslniki);
    Console.WriteLine($" {tytul.ToUpper()} ");
    Console.WriteLine(myslniki);
    Console.WriteLine();
}

// https://stackoverflow.com/a/34186
static T OdczytajLiczbeCalkowita<T>(string polecenie) where T : IBinaryInteger<T>
{
    T? wynik;

    while (true)
    {
        Console.Write($"{polecenie}: ");
        
        if (T.TryParse(Console.ReadLine(), null, out wynik)) break;
        
        Console.Error.WriteLine("Błąd: nieprawidłowa liczba!");
    }

    return wynik;
}
