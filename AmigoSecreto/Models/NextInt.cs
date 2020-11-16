using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmigoSecreto.Models
{
    public static class NextInt
    {

        public static int Next(int current, int max, int toAdd = 1)
        {
            current += toAdd;
            
            if (current >= max)
            {
                current -= max;
            }

            return current;
        }
    }
}
