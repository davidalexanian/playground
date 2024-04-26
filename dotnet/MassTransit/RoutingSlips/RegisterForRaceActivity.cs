using Automatonymous;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MassTransitProject.RoutingSlips
{
    public class RegisterForRaceActivity : IActivity<RegisterForRaceArguments, RegisterForRaceActivityLog>
    {
        public static HashSet<string> registrations = new HashSet<string>();

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

        public async Task<ExecutionResult> Execute(ExecuteContext<RegisterForRaceArguments> context)
        {
            await Task.Yield();
            Console.WriteLine($"{nameof(RegisterForRaceActivity)}.{nameof(Execute)}");

            var id = NewId.Next();
            registrations.Add(id.ToString());
            return context.Completed(new RegisterForRaceActivityLog 
            {
                RegistrationId = id,
                RegistrationDate = DateTime.Now,
            });
        }


    }

    public class RegisterForRaceArguments
    {
        public DateTime Date { get; set; }

        public string Email { get; set; }
    }

    public class RegisterForRaceActivityLog
    {
        public DateTime RegistrationDate { get; set; }

        public NewId RegistrationId { get; set; }
    }
}
