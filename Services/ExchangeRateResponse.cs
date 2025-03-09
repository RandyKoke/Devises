namespace ConvertisseurdeDevises.Services;

public class ExchangeRateResponse
{
    public required string Base_Code { get; set; }
    public required Dictionary<string, decimal> Conversion_Rates { get; set; }
    public required string Time_Last_Update_Utc { get; set; }
}

