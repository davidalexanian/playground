using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace console
{
    class TaxCalculator
    {
        public static void UseExample()
        {
            var tx = new TaxCalculator("Ireland", "10.11", "40");
            tx.Calculate();
            tx.Print();
        }

        public const string IncomeTax = nameof(IncomeTax);
        public const string SocialCharge = nameof(SocialCharge);
        public const string Pension = nameof(Pension);
        public decimal NetAmount { get; private set; }

        public readonly decimal Gross;
        public readonly string Country;
        private readonly Dictionary<string, decimal> taxes = new Dictionary<string, decimal>() {
            { nameof(IncomeTax), 0M },
            { nameof(SocialCharge), 0M },
            { nameof(Pension), 0M }
        };

        public TaxCalculator(string country, string rateArg, string hoursArg)
        {
            Country = country;
            Gross = decimal.Parse(rateArg) * decimal.Parse(hoursArg);
        }

        public void Calculate()
        {
            if (NetAmount != 0)
            {
                throw new InvalidOperationException("Taxes already calculated");
            }

            foreach (var taxRange in GetTaxes(Country))
            {
                taxes[taxRange.Name] += taxRange.Apply(Gross);
            }
            NetAmount = Gross - taxes.Values.Sum();
        }

        public void Print()
        {
            Console.WriteLine($"Employee location: {Country}");
            Console.WriteLine();
            Console.WriteLine($"Gross Amount: €: {Gross}");
            Console.WriteLine();
            Console.WriteLine($"Less deductions");
            Console.WriteLine();
            Console.WriteLine($"Income Tax : €{taxes[nameof(IncomeTax)]}");
            Console.WriteLine();
            Console.WriteLine($"Universal Social Charge: €{taxes[nameof(SocialCharge)]}");
            Console.WriteLine();
            Console.WriteLine($"Pension: €{taxes[nameof(Pension)]}");
            Console.WriteLine();
            Console.WriteLine($"Net Amount: €{NetAmount}");
            Console.WriteLine();
        }

        private static TaxRange[] GetTaxes(string country)
        {
            switch (country.ToLowerInvariant())
            {
                case "ireland":
                    return GetIrelandTaxes();
                case "italy":
                    return GetItalyTaxes();
                case "germany":
                    return GetGermanyTaxes();
                default:
                    throw new ArgumentException(nameof(country));
            }
        }

        private static TaxRange[] GetIrelandTaxes() => new TaxRange[] {
            new TaxRange(0, 600, 0.25M, nameof(IncomeTax)),
            new TaxRange(601, decimal.MaxValue, 0.4M, nameof(IncomeTax)),
            new TaxRange(0, 500, 0.07M, nameof(SocialCharge)),
            new TaxRange(501, decimal.MaxValue, 0.08M, nameof(SocialCharge)),
            new TaxRange(0, decimal.MaxValue, 0.04M, nameof(Pension))
        };

        private static TaxRange[] GetItalyTaxes() => new TaxRange[] {
            new TaxRange(0, decimal.MaxValue, 0.25M, nameof(IncomeTax)),
            new TaxRange(0, decimal.MaxValue, 0.0919M, nameof(SocialCharge)),
        };

        private static TaxRange[] GetGermanyTaxes() => new TaxRange[] {
            new TaxRange(0, 400, 0.25M, nameof(IncomeTax)),
            new TaxRange(401, decimal.MaxValue, 0.32M, nameof(SocialCharge)),
            new TaxRange(0, decimal.MaxValue, 0.02M, nameof(Pension)),
        };
    }

    class TaxRange
    {
        public readonly decimal Start;
        public readonly decimal End;
        public readonly decimal Percent;
        public readonly string Name;

        public TaxRange(decimal start, decimal end, decimal percent, string name)
        {
            Start = start;
            End = end;
            Percent = percent;
            Name = name;
        }

        public decimal Apply(decimal gross)
        {
            if (gross < Start)
            {
                return 0;
            }
            else if (Start <= gross && gross <= End)
            {
                return (gross - Start) * Percent;
            }
            else if (End < gross)
            {
                return (End - Start) * Percent;
            }
            return 0;
        }
    }
}
