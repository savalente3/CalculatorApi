namespace CalcApi.Services
{
    public interface ICalculatorService
    {
        double Add(double operandA, double operandB);
        double Subtract(double operandA, double operandB);
        double Multiply(double operandA, double operandB);
        double Divide(double operandA, double operandB);
    }
}
