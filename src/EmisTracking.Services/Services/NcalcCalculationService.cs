using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmisTracking.Services.Services
{
    public interface ICalculationService
    {
        public Task<double> CalculateAsync(string formula, Dictionary<string, double> parameterValues);
    }

    public class NcalcCalculationService : ICalculationService
    {
        public async Task<double> CalculateAsync(string formula, Dictionary<string, double> parameterValues)
        {
            var expression = new NCalc.AsyncExpression(formula);

            foreach (var kv in parameterValues)
            {
                expression.Parameters[kv.Key] = kv.Value;
            }

            var value = await expression.EvaluateAsync();
            return Convert.ToDouble(value);
        }
    }
}
