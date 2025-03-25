namespace CW_2_s30599;

public class KontenerChlodniczy(
    string rodzajProduktu,
    float minTempProduktuCelsjusz,
    float tempKonteneraCelsjusz,
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
    public float MinTempProduktuCelsjusz { get; set; } = minTempProduktuCelsjusz;
    public float TemperaturaKonteneraCelsjusz { get; set; }
        = Math.Max(minTempProduktuCelsjusz, tempKonteneraCelsjusz);
}
