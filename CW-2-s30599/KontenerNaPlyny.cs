namespace CW_2_s30599;

public class KontenerNaPlyny(
    bool maNiebezpiecznyLadunek,
    uint maksLadownoscKg,
    uint masaTaraKg,
    uint wysokoscCm,
    uint glebokoscCm
) : Kontener(
    'L',
    maksLadownoscKg,
    masaTaraKg,
    wysokoscCm,
    glebokoscCm
),
IHazardNotifier
{
    public bool MaNiebezpiecznyLadunek { get; } = maNiebezpiecznyLadunek;

    public override void ZaladujLadunek(uint masaLadunkuKg)
    {
        uint bezpiecznaPojemnosc = MaNiebezpiecznyLadunek
            ? masaLadunkuKg / 2
            : masaLadunkuKg * 9 / 10;
        
        if (MasaNettoKg + masaLadunkuKg > bezpiecznaPojemnosc)
        {
            PowiadomONiebezpiecznejSytuacji();
        }
        
        base.ZaladujLadunek(masaLadunkuKg);
    }
    
    public void PowiadomONiebezpiecznejSytuacji()
    {
        Console.WriteLine(
            $"Wykonujesz niebezpieczną czynność z kontenerem na płyny {NumerSeryjny()}!"
        );
    }
}
