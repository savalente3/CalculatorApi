using System.ComponentModel.DataAnnotations;

namespace CalcApi.Models
{
    public class Calculation
    {
        public int Id { get; set; }

        [Required]
        public string Operation { get; set; } = string.Empty;

        [Required]
        public double OperandA { get; set; }

        [Required]
        public double OperandB { get; set; }
        
        public double Result { get; set; }

    }
}
