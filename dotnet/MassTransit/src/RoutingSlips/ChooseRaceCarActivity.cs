using MassTransit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MassTransitProject.RoutingSlips
{
    public class ChooseRaceCarActivity : IActivity<ChooseRaceCarActivityArguments, ChooseRaceCarActivityLog>
    {
        public static List<string> availableCars = new List<string> { 
            "bmw", "mercedec", "bmw", "ford"
        };

        public async Task<ExecutionResult> Execute(ExecuteContext<ChooseRaceCarActivityArguments> context)
        {
            await Task.Yield();

            Console.WriteLine($"{nameof(ChooseRaceCarActivity)}.{nameof(Execute)}");
            if (string.IsNullOrWhiteSpace(context.Arguments.RegistrationId)) 
            {
                return context.Faulted(new ArgumentException(nameof(context.Arguments.RegistrationId)));
            }

            if (!availableCars.Contains(context.Arguments.CarId))
            {
                return context.Faulted(new ArgumentException($"Car {context.Arguments.CarId} is not available"));
            }
            else
            {
                availableCars.Remove(context.Arguments.CarId);
            }

            return context.Completed(new ChooseRaceCarActivityLog { CarId = context.Arguments.CarId });
        }

        public async Task<CompensationResult> Compensate(CompensateContext<ChooseRaceCarActivityLog> context)
        {
            await Task.Yield();
            Console.WriteLine($"{nameof(ChooseRaceCarActivity)}.{nameof(Compensate)}");
            availableCars.Add(context.Log.CarId);
            return context.Compensated(new { NumberOfAvailableCars = availableCars.Count });    // upsert variables on routing slip
        }
    }

    public class ChooseRaceCarActivityArguments
    {
        public string RegistrationId { get; set; }

        public string CarId { get; set; }
    }

    public class ChooseRaceCarActivityLog
    {
        public string CarId { get; set; }
    }
}
