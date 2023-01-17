using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _2._3.Tools
{
    public class Val : ValidationAttribute
    {
        private int Value { get; }
        public Val(int Value)
        {
            this.Value = Value;
        }

        public override bool IsValid(object? value)
        {
            return value is null || value is int v && v < this.Value;
        }
    }
}
