namespace CW_2_s30599;

public class Statek(
    uint maksPredkoscWezly,
    uint maksLiczbaKontenerow,
    uint maksWagaKontenerowTony
)
{
    private static uint _ostatniIdentyfikator;

    public uint Identyfikator { get; } = ++_ostatniIdentyfikator;    
    public List<Kontener> Kontenery { get; set; }
    public uint MaksPredkoscWezly { get; set; } = maksPredkoscWezly;
    public uint MaksLiczbaKontenerow { get; set; } = maksLiczbaKontenerow;
    public uint MaksWagaKontenerowTony { get; set; } = maksWagaKontenerowTony;

    public uint MaksWagaKontenerowKg()
    {
        return MaksWagaKontenerowTony * 1000;
    }

    public override string ToString()
    {
        return $"Statek {Identyfikator} (speed={MaksPredkoscWezly}, maxContainerNum={MaksLiczbaKontenerow}, maxWeight={MaksWagaKontenerowKg()})";
    }
}
