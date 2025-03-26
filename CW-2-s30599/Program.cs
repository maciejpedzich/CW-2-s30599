/*
 * Ten program został napisany z uwzględnieniem praktyk LIQUID:
 * Low In Quality, Unrivalled In Despair
 */

using System.Numerics;

namespace CW_2_s30599;

class Program
{
    static List<Statek> _statki = new List<Statek>();
    static List<Kontener> _kontenery = new List<Kontener>();
    
    static void Main(string[] _)
    {
        while (true)
        {
            var saJakiesStatki = _statki.Count > 0;
            var saWolneKontenery = _kontenery.Count > 0;
            var saZaladowaneStatki = _statki.Any(s => s.Kontenery.Count > 0);
            
            Console.WriteLine();
            Console.WriteLine("Lista kontenerowców:");

            if (saJakiesStatki)
                foreach (var statek in _statki) Console.WriteLine(statek);
            else
                Console.WriteLine("Brak");
            
            Console.WriteLine();
            Console.WriteLine("Lista wolnych kontenerów:");

            if (saWolneKontenery)
                foreach (var kontener in _kontenery) Console.WriteLine(kontener);
            else
                Console.WriteLine("Brak");
            
            Console.WriteLine();
            Console.WriteLine("Możliwe akcje:");
            Console.WriteLine("0. Wyjdź z aplikacji");
            Console.WriteLine("1. Dodaj kontenerowiec");
            
            if (saJakiesStatki)
            {
                Console.WriteLine("2. Usuń kontenerowiec");
                Console.WriteLine("3. Dodaj kontener");

                if (saWolneKontenery)
                {
                    Console.WriteLine("4. Przenieś wolny kontener na statek");
                    Console.WriteLine("5. Załaduj ładunek do kontenera");
                    Console.WriteLine("6. Rozładuj kontener");
                    Console.WriteLine("7. Usuń wolny kontener");
                }

                if (saZaladowaneStatki)
                {
                    Console.WriteLine("8. Usuń kontener ze statku");

                    if (_statki.Count > 1)
                        Console.WriteLine("9. Przenieś kontener między statkami");
                }
            }
            
            Console.WriteLine();

            var akcja = OdczytajLiczbe<byte>("Podaj numer akcji");

            if (akcja == 0) break;
            
            if (akcja == 1)
            {
                WypiszNaglowek("Nowy kontenerowiec");

                var maksPredkoscWezly = OdczytajLiczbe<uint>(
                    "Podaj maksymalną prędkość w węzłach"
                );
                var maksLiczbaKontenerow = OdczytajLiczbe<uint>(
                    "Podaj maksymalną liczbę kontenerów"
                );
                var maksWagaKontenerowTony = OdczytajLiczbe<uint>(
                    "Podaj maksymalną wagę brutto kontenerów w tonach"
                );
                var nowyStatek = new Statek(
                    maksPredkoscWezly,
                    maksLiczbaKontenerow,
                    maksWagaKontenerowTony
                );
                
                _statki.Add(nowyStatek);
                WypiszSukces("kontenerowiec został dodany");
            }
            else if (akcja == 2 && saJakiesStatki) AkcjaWymagajacaWyboruStatku(
                _statki,
                s => 
                {
                    Console.WriteLine("Czy na pewno chcesz usunąć ten kontenerowiec?");
                    AkcjaWymagajacaZatwierdzenia(() =>
                    {
                        _statki.Remove(s);
                        WypiszSukces("kontenerowiec został usunięty");
                    });
                }
            );
            else if (akcja == 3 && saJakiesStatki)
            {
                WypiszNaglowek("Nowy kontener");
                Console.WriteLine("Dostępne typy kontenerów:");
                Console.WriteLine("C - kontener chłodniczy");
                Console.WriteLine("G - kontener na gaz");
                Console.WriteLine("L - kontener na płyny");
                Console.WriteLine();
                Console.Write("Podaj literę typu kontenera: ");

                var wejscieTypKontenera = Console.ReadLine()!.ToUpper();
                var znacznikTypu = wejscieTypKontenera[0];
                char[] mozliweTypyKontenerow = ['C', 'G', 'L'];

                if (mozliweTypyKontenerow.Contains(znacznikTypu))
                {
                    var maksLadownoscKg = OdczytajLiczbe<uint>(
                        "Podaj maksymalną ładowność kontenera w kilogramach"
                    );
                    var masaTaraKg = OdczytajLiczbe<uint>(
                        "Podaj masę kontenera w kilogramach"
                    );
                    var wysokoscCm = OdczytajLiczbe<uint>(
                        "Podaj wysokość kontenera w centymetrach"
                    );
                    var glebokoscCm = OdczytajLiczbe<uint>(
                        "Podaj głębokość kontenera w centymetrach"
                    );
                    Kontener kontener;

                    if (znacznikTypu == 'C')
                    {
                        Console.Write("Podaj nazwę przechowywanego produktu: ");

                        var rodzajProduktu = Console.ReadLine()!;
                        var minTempProduktuCelsjusz = OdczytajLiczbe<float>(
                            "Podaj minimalną temperaturę produktu w stopniach Celsjusza"    
                        );
                        var tempKonteneraCelsjusz = OdczytajLiczbe<float>(
                            "Podaj temperaturę kontenera w stopniach Celsjusza"    
                        );

                        kontener = new KontenerChlodniczy(
                            rodzajProduktu,
                            minTempProduktuCelsjusz,
                            tempKonteneraCelsjusz,
                            maksLadownoscKg,
                            masaTaraKg,
                            wysokoscCm,
                            glebokoscCm
                        );
                    }
                    else if (znacznikTypu == 'G')
                    {
                        kontener = new KontenerNaGaz(
                            maksLadownoscKg,
                            masaTaraKg,
                            wysokoscCm,
                            glebokoscCm
                        );
                    }
                    else
                    {
                        Console.WriteLine("Czy ten kontener będzie przechowywał niebezpieczny ładunek?");
                        Console.Write("Jeśli tak, wpisz NIEBEZPIECZNY: ");

                        var maNiebezpiecznyLadunek = Console.ReadLine() == "NIEBEZPIECZNY";

                        kontener = new KontenerNaPlyny(
                            maNiebezpiecznyLadunek,
                            maksLadownoscKg,
                            masaTaraKg,
                            wysokoscCm,
                            glebokoscCm
                        );
                    }

                    _kontenery.Add(kontener);
                    WypiszSukces("pomyślnie dodano kontener");
                }
                else WypiszBlad("nieprawidłowy typ kontenera");
            }
            else if (akcja == 4 && saWolneKontenery) AkcjaWymagajacaWyboruKontenera(
                _kontenery,
                k =>
                {
                    var dozwoloneStatki = _statki.Where(
                            s =>
                            {
                                var nowaMasaBruttoKg =
                                    s.WagaBruttoKontenerowKg() + k.MasaBruttoKg();

                                return nowaMasaBruttoKg <= s.MaksWagaBruttoKontenerowKg();
                            })
                        .ToList();

                    if (dozwoloneStatki.Count > 0)
                    {
                        var indeksStatku = 0;
                        var numeryDozwolonychStatkow =
                            dozwoloneStatki.Select(s => s.Identyfikator);

                        if (dozwoloneStatki.Count > 1)
                        {
                            Console.WriteLine(
                                "Wybrany kontener może zostać załadowany do następujących statków:"
                            );
                            Console.WriteLine(String.Join(", ", numeryDozwolonychStatkow));
                            Console.WriteLine();

                            var idStatku = OdczytajLiczbe<uint>("Podaj numer statku");
                            indeksStatku = dozwoloneStatki.FindIndex(
                                s => s.Identyfikator == idStatku
                            );

                            if (indeksStatku == -1)
                            {
                                WypiszBlad(
                                    "statek o podanym numerze nie istnieje lub jest niedozwolony"
                                );
                                
                                return;
                            }
                        }

                        var statek = dozwoloneStatki[indeksStatku];

                        statek.ZaladujKontener(k);
                        _kontenery.Remove(k);
                        WypiszSukces("pomyślnie przeniesiono kontener na statek");
                    }
                    else WypiszBlad("nie ma statku zdolnego pomieścić ten kontener");
                }
            );
            else if (akcja == 5 && saWolneKontenery) AkcjaWymagajacaWyboruKontenera(
                _kontenery,
                k =>
                {
                    var masaLadunkuKg = OdczytajLiczbe<uint>(
                        "Podaj masę ładunku w kilogramach"
                    );

                    try
                    {
                        k.ZaladujLadunek(masaLadunkuKg);
                    }
                    catch (OverfillException e)
                    {
                        WypiszBlad(e.Message);
                    }
                }
            );
            else if (akcja == 6 && saWolneKontenery) AkcjaWymagajacaWyboruKontenera(
                _kontenery,
                k =>
                {
                    k.OproznijLadunek();
                    WypiszSukces("kontener został opróżniony");
                }
            );
            else if (akcja == 7 && saWolneKontenery) AkcjaWymagajacaWyboruKontenera(
                _kontenery,
                k =>
                {
                    AkcjaWymagajacaZatwierdzenia(() =>
                    {
                        _kontenery.Remove(k);
                        WypiszSukces("pomyślnie usunięto kontener");
                    });
                }
            );
            else if (akcja == 8 && saZaladowaneStatki) AkcjaWymagajacaWyboruStatku(
                _statki,
                s =>
                {
                    AkcjaWymagajacaWyboruKontenera(
                        s.Kontenery,
                        k =>
                        {
                            AkcjaWymagajacaZatwierdzenia(() =>
                            {
                                s.Kontenery.Remove(k);
                                WypiszSukces("pomyślnie usunięto kontener");
                            });
                        }
                    );
                }
            );
            else if (akcja == 9 && saZaladowaneStatki && _statki.Count > 1) AkcjaWymagajacaWyboruStatku(
                _statki,
                s1 =>
                {
                    AkcjaWymagajacaWyboruKontenera(
                        s1.Kontenery,
                        k =>
                        {
                            var statkiZdolnePomiescicKontener = _statki
                                .Where(s2 =>
                                {
                                    var rozneStatki =
                                        s1.Identyfikator != s2.Identyfikator;
                                    var drugiZmiesciKontener =
                                        s2.WagaBruttoKontenerowKg() + k.MasaBruttoKg()
                                        <= s2.MaksWagaBruttoKontenerowKg();

                                    return rozneStatki && drugiZmiesciKontener;
                                })
                                .ToList();
                            
                            if (statkiZdolnePomiescicKontener.Count == 0)
                                WypiszBlad(
                                    "nie ma innych statków zdolnych pomieścić ten kontener"
                                );
                            else
                            {
                                var identyfikatoryStatkow = statkiZdolnePomiescicKontener
                                    .Select(s => s.Identyfikator)
                                    .ToList();
                                
                                Console.WriteLine("Możesz przenieść kontener do tych statków:");
                                Console.WriteLine(String.Join(", ", identyfikatoryStatkow));
                                
                                AkcjaWymagajacaWyboruStatku(
                                    statkiZdolnePomiescicKontener,
                                    s2 =>
                                    {
                                        s2.Kontenery.Add(k);
                                        s1.Kontenery.Remove(k);
                                    }
                                );
                            }
                        }
                    );
                }
            );
            else WypiszBlad("nieprawidłowy numer akcji");
        }
    }
    
    static void WypiszSukces(string wiadomosc)
    {
        Console.Clear();
        Console.WriteLine($"Sukces: {wiadomosc}!");
    }

    static void WypiszBlad(string wiadomosc)
    {
        Console.Clear();
        Console.Error.WriteLine($"Błąd: {wiadomosc}!");
    }

    static void WypiszNaglowek(string tytul)
    {
        var myslniki = new string('-', tytul.Length + 4);
    
        Console.Clear();
        Console.WriteLine(myslniki);
        Console.WriteLine($"| {tytul.ToUpper()} |");
        Console.WriteLine(myslniki);
        Console.WriteLine();
    }

    static T OdczytajLiczbe<T>(string polecenie) where T : INumber<T>
    {
        T? wynik;

        while (true)
        {
            Console.Write($"{polecenie}: ");
        
            if (T.TryParse(Console.ReadLine(), null, out wynik))
                break;
        
            Console.Error.WriteLine("Błąd: nieprawidłowa liczba!");
        }

        return wynik;
    }

    static void AkcjaWymagajacaZatwierdzenia(Action wykonajPoZatwierdzeniu)
    {
        var zatwierdzam = "ZATWIERDZAM";

        Console.Write(
            $"Wpisz {zatwierdzam}, aby potwierdzić lub cokolwiek innego, by anulować: "
        );

        var odpowiedz = Console.ReadLine();

        if (odpowiedz == zatwierdzam) wykonajPoZatwierdzeniu();
        else WypiszBlad("akcja została anulowana");
    }

    static void AkcjaWymagajacaWyboruKontenera(
        List<Kontener> zrodlo,
        Action<Kontener> poWybraniuKontenera
    )
    {
        var idKontenera = zrodlo.Count == 1
            ? zrodlo[0].Identyfikator
            : OdczytajLiczbe<uint>("Podaj numer kontenera");
        var indeksKontenera = zrodlo.FindIndex(
            k => k.Identyfikator == idKontenera
        );

        if (indeksKontenera == -1)
            WypiszBlad("kontener o podanym numerze nie istnieje");
        else
            poWybraniuKontenera(zrodlo[indeksKontenera]);
    }

    static void AkcjaWymagajacaWyboruStatku(
        List<Statek> zrodlo,
        Action<Statek> poWybraniuStatku
    )
    {
        var idStatku = zrodlo.Count == 1
            ? zrodlo[0].Identyfikator
            : OdczytajLiczbe<uint>("Podaj numer statku");
        var indeksStatku = zrodlo.FindIndex(
            k => k.Identyfikator == idStatku
        );

        if (indeksStatku == -1)
            WypiszBlad("statek o podanym numerze nie istnieje");
        else
            poWybraniuStatku(zrodlo[indeksStatku]);
    }
}