using MassTransit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MassTransitProject.RoutingSlips
{
    public class RegisterForRaceActivity : IActivity<RegisterForRaceActivityArguments, RegisterForRaceActivityLog>
    {
        public static HashSet<string> registrations = new HashSet<string>();

        public async Task<ExecutionResult> Execute(ExecuteContext<RegisterForRaceActivityArguments> context)
        {
            await Task.Yield();
            var raceName = context.GetVariable<string>("RaceName");
            Console.WriteLine($"{nameof(RegisterForRaceActivity)}.{nameof(Execute)}");

            var id = NewId.Next().ToString();
            registrations.Add(id.ToString());

            // this is required when it needs to be compensatetad and we need data to revert the change done
            var log = new RegisterForRaceActivityLog { RegistrationId = id };

            // additional variables set by activity that can be used at later activities
            var variables = new { RegistrationId = id };
            
            return context.CompletedWithVariables(log, variables);
        }

        public async Task<CompensationResult> Compensate(CompensateContext<RegisterForRaceActivityLog> context)
        {
            await Task.Yield();
            Console.WriteLine($"{nameof(RegisterForRaceActivity)}.{nameof(Compensate)}");

            if (registrations.Contains(context.Log.RegistrationId.ToString()))
            {
                registrations.Remove(context.Log.RegistrationId.ToString());
            }
            return context.Compensated();
        }
    }

    public class RegisterForRaceActivityArguments
    {
        public DateTime ForDate { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        
        public int Age { get; set; }

        public string Email { get; set; }
    }

    public class RegisterForRaceActivityLog
    {
        public string RegistrationId { get; set; }
    }
}
