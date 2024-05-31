using MassTransit;
using MassTransit.Courier.Contracts;
using System;

namespace MassTransitProject.RoutingSlips
{
    public static class Helper
    {
        public static IEndpointNameFormatter endpointNameFormatter = new KebabCaseEndpointNameFormatter(false);

        public static RoutingSlip CreateRoutingSlip()
        {
            var trackingNumber = NewId.NextSequentialGuid();
            var builder = new RoutingSlipBuilder(trackingNumber);

            builder.AddVariable("RaceName", "Desert Rallys"); // variables can be mapped to actvity args

            var registerForRaceActivityArgs = new RegisterForRaceActivityArguments
            {
                Age = 21,
                Email = "mail@mail.com",
                FirstName = "fname",
                LastName = "lname",
                ForDate = DateTime.Now.AddDays(1),
            };
            builder.AddActivity(nameof(RegisterForRaceActivity),
                GetActivityAddress<RegisterForRaceActivity, RegisterForRaceActivityArguments>(), 
                registerForRaceActivityArgs);

            var validateLicenseArgs = new ValidateLicenseActivityArguments
            {
                LicenseNumber = "license",
                LicenseValidUntil = DateTime.Now.AddDays(1)
            };
            if (!string.IsNullOrEmpty(validateLicenseArgs.LicenseNumber)) { // conditionally add actvities
                builder.AddActivity(
                    nameof(ValidateLicenseActivity),
                    GetActivityAddress<ValidateLicenseActivity, ValidateLicenseActivityArguments>(),
                    validateLicenseArgs);
            }

            builder.AddActivity(
                nameof(ChooseRaceCarActivity),
                GetActivityAddress<ChooseRaceCarActivity, ChooseRaceCarActivityArguments>(),
                new ChooseRaceCarActivityArguments { CarId = "bmw1" });

            return builder.Build();
        }

        public static Uri GetActivityAddress<TActivty, TArguments>()
            where TActivty : class, IExecuteActivity<TArguments>
            where TArguments : class =>
            new Uri($"exchange:{endpointNameFormatter.ExecuteActivity<TActivty, TArguments>()}");
    }
}
