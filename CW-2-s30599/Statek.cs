namespace CW_2_s30599;

public class Statek(
    uint maksPredkoscWezly,
    uint maksLiczbaKontenerow,
    uint maksWagaKontenerowTony
)
{
    private static uint _ostatniIdentyfikator;

    public uint Identyfikator { get; } = ++_ostatniIdentyfikator;
    public List<Kontener> Kontenery { get; set; } = new List<Kontener>();
    public uint MaksPredkoscWezly { get; } = maksPredkoscWezly;
    public uint MaksLiczbaKontenerow { get; } = maksLiczbaKontenerow;
    public uint MaksWagaKontenerowTony { get; } = maksWagaKontenerowTony;

    public uint MaksWagaKontenerowKg()
    {
        return MaksWagaKontenerowTony * 1000;
    }

    public override string ToString()
    {
        return $"Statek {Identyfikator} (speed={MaksPredkoscWezly}, maxContainerNum={MaksLiczbaKontenerow}, maxWeight={MaksWagaKontenerowKg()})";
    }
}
