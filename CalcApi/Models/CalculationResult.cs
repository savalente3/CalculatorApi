namespace CalcApi.Models
{
    public class CalculationResult
    {
        public int Id { get; set; }
        public string Operation { get; set; } = string.Empty;
        public double Result { get; set; }
    }
}
