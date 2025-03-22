namespace CW_2_s30599;

public class KontenerChlodniczy(
    string rodzajProduktu,
    float minTemperaturaProduktu,
    float temperaturaKontenera,
    uint maksLadownoscKg,
    uint masaTaraKg,
    uint wysokoscCm,
    uint glebokoscCm
) : Kontener(
    'C',
    maksLadownoscKg,
    masaTaraKg,
    wysokoscCm,
    glebokoscCm
)
{
    public string RodzajProduktu { get; set; } = rodzajProduktu;
    public float MinTemperaturaProduktu { get; set; } = minTemperaturaProduktu;
    public float TemperaturaKontenera { get; set; }
        = Math.Max(minTemperaturaProduktu, temperaturaKontenera);
}
