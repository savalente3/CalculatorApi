using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CalcApi.Models;
using CalcApi.Repositories;
using CalcApi.Services;

namespace CalcApi.Controllers
{
    [Route("")]
    [ApiController]
    public class CalculationsController : ControllerBase
    {
        private readonly ICalculationRepository _repository;
        private readonly ICalculatorService _service;

        public CalculationsController(ICalculationRepository repository, ICalculatorService service)
        {
            _repository = repository;
            _service = service;
        }

        // GET: api/Calculations
        [HttpGet("GetAllCalculations")]
        public async Task<ActionResult<IEnumerable<CalculationResult>>> GetCalculations()
        {
            var calculations = await _repository.GetAllAsync();

            var calculationResults = calculations.Select(c => new CalculationResult
            {
                Id = c.Id,
                Operation = $"{c.OperandA} {c.Operation} {c.OperandB}",
                Result = PerformOperation(c.Operation, c.OperandA, c.OperandB)
            });

            return Ok(calculationResults);
        }

        // GET: api/Calculations/5
        [HttpGet("GetCalculationById/{id}")]
        public async Task<ActionResult<CalculationResult>> GetCalculation(int id)
        {
            var calculation = await _repository.GetByIdAsync(id);

            if (calculation == null)
            {
                return NotFound();
            }

            var result = PerformOperation(calculation.Operation, calculation.OperandA, calculation.OperandB);
            var calculationResult = new CalculationResult
            {
                Id = calculation.Id,
                Operation = $"{calculation.OperandA} {calculation.Operation} {calculation.OperandB}",
                Result = result
            };

            return Ok(calculationResult);
        }

        // POST: api/Calculations
        [HttpPost("PostCalculation")]
        public async Task<ActionResult<CalculationResult>> PostCalculation(Calculation calculation)
        {
            var result = PerformOperation(calculation.Operation, calculation.OperandA, calculation.OperandB);

            calculation.Result = result; // Store the result in the Calculation object

            await _repository.AddAsync(calculation);

            var calculationResult = new CalculationResult
            {
                Id = calculation.Id,
                Operation = $"{calculation.OperandA} {calculation.Operation} {calculation.OperandB}",
                Result = calculation.Result
            };

            return CreatedAtAction(nameof(GetCalculation), new { id = calculation.Id }, calculationResult);
        }

        // PUT: api/Calculations/5
        [HttpPut("UpdateCalculation/{id}")]
        public async Task<IActionResult> PutCalculation(int id, Calculation calculation)
        {
            var existingCalculation = await _repository.GetByIdAsync(id);
            if (existingCalculation == null)
            {
                return NotFound();
            }
            calculation.Id = id;
            existingCalculation.Operation = calculation.Operation;
            existingCalculation.OperandA = calculation.OperandA;
            existingCalculation.OperandB = calculation.OperandB;

            var result = PerformOperation(calculation.Operation, calculation.OperandA, calculation.OperandB);

            existingCalculation.Result = result; // Store the result in the existing calculation object

            try
            {
                await _repository.UpdateAsync(existingCalculation);

                var calculationResult = new CalculationResult
                {
                    Id = existingCalculation.Id,
                    Operation = $"{existingCalculation.OperandA} {existingCalculation.Operation} {existingCalculation.OperandB}",
                    Result = existingCalculation.Result
                };

                return Ok(calculationResult);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating the calculation");
            }
        }

        // DELETE: api/Calculations/5
        [HttpDelete("DeleteCalculation/{id}")]
        public async Task<IActionResult> DeleteCalculation(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }

        private double PerformOperation(string operation, double operandA, double operandB)
        {
            switch (operation.ToLower())
            {
                case "add":
                case "+":
                    return _service.Add(operandA, operandB);
                case "subtract":
                case "-":
                    return _service.Subtract(operandA, operandB);
                case "multiply":
                case "*":
                    return _service.Multiply(operandA, operandB);
                case "divide":
                case "/":
                    if (operandB == 0)
                    {
                        throw new DivideByZeroException("Cannot divide by zero.");
                    }
                    return _service.Divide(operandA, operandB);
                default:
                    throw new InvalidOperationException("Invalid operation");
            }
        }
    }
}
