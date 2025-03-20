namespace CW_2_s30599;

public class KontenerNaPlynyKontener(
    bool maNiebezpiecznyLadunek,
    uint maksLadownoscKg,
    uint masaTaraKg,
    uint masaNettoKg,
    uint wysokoscCm,
    uint glebokoscCm
) : Kontener(
    'L',
    maksLadownoscKg,
    masaTaraKg,
    masaNettoKg,
    wysokoscCm,
    glebokoscCm
),
IHazardNotifier
{
    public bool MaNiebezpiecznyLadunek { get; set; } = maNiebezpiecznyLadunek;

    public override void ZaladujKontener(uint masaLadunkuKg)
    {
        uint bezpiecznaPojemnosc = MaNiebezpiecznyLadunek
            ? masaLadunkuKg / 2
            : masaLadunkuKg * 9 / 10;
        
        if (MasaNettoKg + masaLadunkuKg > bezpiecznaPojemnosc)
        {
            PowiadomONiebezpiecznejSytuacji();
        }
        
        base.ZaladujKontener(masaLadunkuKg);
    }
    
    public void PowiadomONiebezpiecznejSytuacji()
    {
        Console.WriteLine(
            $"Wykonujesz niebezpieczną czynność z kontenerem na płyny {NumerSeryjny()}!"
        );
    }
}
