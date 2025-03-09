namespace ConvertisseurdeDevises.Models; 
 
public class ExchangeRateResponse 
{ 
    public string Base_Code { get; set; } 
    public Dictionary<string, decimal> Conversion_Rates { get; set; } 
}

