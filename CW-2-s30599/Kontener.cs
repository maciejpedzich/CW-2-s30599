namespace CW_2_s30599;

public abstract class Kontener(
    char znacznikTypu,
    uint maksLadownoscKg,
    uint masaTaraKg,
    uint masaNettoKg,
    uint wysokoscCm,
    uint glebokoscCm
)
{
    private static uint _ostatniIdentyfikator;
    private uint _masaNettoKg = masaNettoKg;
    
    public uint Identyfikator { get; } = ++_ostatniIdentyfikator;
    public char ZnacznikTypu { get; } = znacznikTypu;
    public uint MaksLadownoscKg { get; set; } = maksLadownoscKg;
    // Masa tara to masa kontenera.
    public uint MasaTaraKg { get; set; } = masaTaraKg;
    // Masa netto to masa ładunku.
    public uint MasaNettoKg
    {
        init => ZaladujKontener(value);
        get => _masaNettoKg;
    }
    public uint WysokoscCm { get; set; } = wysokoscCm;
    public uint GlebokoscCm { get; set; } = glebokoscCm;

    public string NumerSeryjny()
    {
        return $"KON-{ZnacznikTypu}-{Identyfikator}";
    }
    
    public virtual void OproznijLadunek()
    {
        _masaNettoKg = 0;
    }

    public virtual void ZaladujKontener(uint masaLadunkuKg)
    {
        if (masaLadunkuKg > MaksLadownoscKg)
        {
            throw new OverfillException();
        }
        
        _masaNettoKg += masaLadunkuKg;
    }
}
