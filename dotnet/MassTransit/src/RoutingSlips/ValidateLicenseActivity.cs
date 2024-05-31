using MassTransit;
using System;
using System.Threading.Tasks;

namespace MassTransitProject.RoutingSlips
{
    public class ValidateLicenseActivity : IExecuteActivity<ValidateLicenseActivityArguments>
    {
        public async Task<ExecutionResult> Execute(ExecuteContext<ValidateLicenseActivityArguments> context)
        {
            await Task.Yield();
            Console.WriteLine($"{nameof(ValidateLicenseActivity)}.{nameof(Execute)}");

            if (Guid.Empty == context.Arguments.RegistrationId ||
                string.IsNullOrWhiteSpace(context.Arguments.LicenseNumber) ||
                context.Arguments.LicenseValidUntil < DateTime.Now)
            {
                return context.Faulted(new ArgumentException(nameof(ValidateLicenseActivityArguments.LicenseNumber)));
            }
            else
            {
                return context.Completed();
            }
        }
    }
    public class ValidateLicenseActivityArguments
    {
        public string LicenseNumber { get; set; }

        public DateTime LicenseValidUntil { get; set; }

        public Guid RegistrationId { get; set; }
    }
}
