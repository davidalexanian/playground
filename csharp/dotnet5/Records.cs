using System;
using System.Diagnostics;
using System.Text;

namespace dotnet5.Records
{
    // immutable record
    public record Person(string FirstName, string LastName);
    
    public record Animal(string Name, string Color)
    {
        // do not allow the compiler to generate a public autoproperty
        // instead redefine with internal for the compiler to use
        internal string Name { get; init; } = Name;
        
        // allow public setter. Without "= Color" at the end,
        // it is an ordinary property (should be initialized by constructor
        public string Color { get; set; } = Color;

        public string[] Groups { get; set; }  // any other prop is also OK
    };

    record Student(string Name, string[] Phones);
}