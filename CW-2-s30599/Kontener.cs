namespace CW_2_s30599;

public abstract class Kontener(
    char znacznikTypu,
    uint masaTaraKg,
    uint masaNettoKg,
    uint maksLadownoscKg,
    uint wysokoscCm,
    uint glebokoscCm,
    bool ladunekBezpieczny
)
{
    private static uint _ostatniIdentyfikator;
    public uint Identyfikator { get; } = ++_ostatniIdentyfikator;
    public char ZnacznikTypu { get; } = znacznikTypu;
    // tara - masa kontenera
    public uint MasaTaraKg { get; set; } = masaTaraKg;
    // netto - masa ładunku
    public uint MasaNettoKg { get; set; } = masaNettoKg;
    public uint MaksLadownoscKg { get; set; } = maksLadownoscKg;
    public uint WysokoscCm { get; set; } = wysokoscCm;
    public uint GlebokoscCm { get; set; } = glebokoscCm;
    public bool LadunekBezpieczny { get; set; } = ladunekBezpieczny;

    public void OproznijLadunek()
    {
        MasaNettoKg = 0;
    }

    public void ZaladujKontener(uint masaLadunkuKg)
    {
        if (masaLadunkuKg > MaksLadownoscKg)
        {
            throw new OverfillException();
        }
        
        MasaNettoKg = masaLadunkuKg;
    }

    public string NumerSeryjny()
    {
        return "KON-" + ZnacznikTypu + "-" + Identyfikator;
    }
}
