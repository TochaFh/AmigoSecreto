using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmigoSecreto.Models
{
    public class SecretFriendResult
    {
        public SecretFriendResult(Sfr result)
        {
            Result = result;
        }

        public BasicPerson Person { get; set; }
        public string SecretFriend { get; init; }
        public Sfr Result { get; }
    }

    /// <summary>
    /// SecretFriendResult results
    /// </summary>
    public enum Sfr
    {
        Success,
        AlreadyRevelead,
        NotFound
    }
}
