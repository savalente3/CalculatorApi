namespace CalcApi.Services
{
    public class CalculatorService : ICalculatorService
    {
        public double Add(double operandA, double operandB)
        {
            return operandA + operandB;
        }

        public double Subtract(double operandA, double operandB)
        {
            return operandA - operandB;
        }

        public double Multiply(double operandA, double operandB)
        {
            return operandA * operandB;
        }

        public double Divide(double operandA, double operandB)
        {
            if (operandB == 0)
            {
                throw new DivideByZeroException("Cannot divide by zero.");
            }
            return operandA / operandB;
        }
    }
}
