namespace CW_2_s30599;

public class Statek(
    uint maksPredkoscWezly,
    uint maksLiczbaKontenerow,
    uint maksWagaKontenerowTony
)
{
    private static uint _ostatniIdentyfikator;

    public uint Identyfikator { get; } = ++_ostatniIdentyfikator;
    public List<Kontener> Kontenery { get; } = new List<Kontener>();
    public uint MaksPredkoscWezly { get; } = maksPredkoscWezly;
    public uint MaksLiczbaKontenerow { get; } = maksLiczbaKontenerow;
    public uint MaksWagaKontenerowTony { get; } = maksWagaKontenerowTony;

    public void ZaladujKontener(Kontener kontener)
    {
        Kontenery.Add(kontener);
    }
    
    public uint MaksWagaBruttoKontenerowKg()
    {
        return MaksWagaKontenerowTony * 1000;
    }

    public uint WagaBruttoKontenerowKg()
    {
        return Kontenery.Aggregate(
            (uint) 0,
            (waga, kontener) =>
            {
                waga += kontener.MasaBruttoKg();
                return waga;
            });
    }

    public override string ToString()
    {
        var listaKontenerow = String.Join("\n", Kontenery.Select((kontener) => $"\t- {kontener}"));
        
        return $"Statek {Identyfikator} (maksPredkoscWezly={MaksPredkoscWezly}, maksLiczbaKontenerow={MaksLiczbaKontenerow}, maksWagaBruttoKontenerowKg={MaksWagaBruttoKontenerowKg()})\n{listaKontenerow}";
    }
}
