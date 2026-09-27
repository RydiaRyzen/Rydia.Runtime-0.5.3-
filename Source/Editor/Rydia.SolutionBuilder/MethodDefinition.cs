using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.SolutionBuilder
{
    public sealed class MethodDefinition
    {
        public string Name { get; }

        public string ReturnType { get; }

        public MethodDefinition(
            string name,
            string returnType = "void")
        {
            Name = name;
            ReturnType = returnType;
        }
    }
}
