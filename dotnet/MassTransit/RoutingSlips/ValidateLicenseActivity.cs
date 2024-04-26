using MassTransit;
using System;
using System.Threading.Tasks;

namespace MassTransitProject.RoutingSlips
{
    public class ValidateLicenseActivity : IExecuteActivity<ValidateLicenseArguments>
    {
        public async Task<ExecutionResult> Execute(ExecuteContext<ValidateLicenseArguments> context)
        {
            await Task.Yield();
            Console.WriteLine($"{nameof(ValidateLicenseActivity)}.{nameof(Execute)}");

            if (string.IsNullOrWhiteSpace(context.Arguments.LicenseNumber) ||
                context.Arguments.LicenseValidUntil < DateTime.Now)
            {
                return context.Faulted(new ArgumentException(nameof(ValidateLicenseArguments.LicenseNumber)));
            }
            else
            {
                return context.Completed(new
                {
                    LicenseGivenBy = "US Racing Federation"
                });
            }
        }
    }
    public class ValidateLicenseArguments
    {
        public string LicenseNumber { get; set; }

        public DateTime LicenseValidUntil { get; set; }

        public string Email { get; set; }
    }
}
