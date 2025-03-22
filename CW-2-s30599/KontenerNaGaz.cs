namespace CW_2_s30599;

public class KontenerNaGaz(
    uint maksLadownoscKg,
    uint masaTaraKg,
    uint wysokoscCm,
    uint glebokoscCm
) : Kontener(
    'G',
    maksLadownoscKg,
    masaTaraKg,
    wysokoscCm,
    glebokoscCm
),
IHazardNotifier
{
    public override void OproznijLadunek()
    {
        MasaNettoKg /= 20;
    }
    
    public void PowiadomONiebezpiecznejSytuacji()
    {
        Console.WriteLine(
            $"Wykonujesz niebezpieczną czynność z kontenerem na gaz {NumerSeryjny()}!"
        );
    }
}
