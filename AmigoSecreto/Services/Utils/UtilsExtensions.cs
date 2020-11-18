using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace AmigoSecreto.Services
{
    public static class UtilsExtensions
    {
        public static bool IsNullOrWhite(this string str) => string.IsNullOrWhiteSpace(str);

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> enumerable)
        {
            var rnd = new Random();
            return enumerable.OrderBy(_ => rnd.Next());
        }

        public static string Value(this IConfiguration configuration, string key)
        {
            string value = configuration[key];
            if (value.IsNullOrWhite()) throw new Exception($"Valor não encontrado da configuração: '{key}'");
            return value;
        }
    }
}
