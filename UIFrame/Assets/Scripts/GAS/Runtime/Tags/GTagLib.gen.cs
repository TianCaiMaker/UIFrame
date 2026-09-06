using System.Collections.Generic;

namespace GAS.Runtime
{
    public static class GTagLib
    {
        public static readonly Dictionary<string, GameplayTag> TagMap =
            new Dictionary<string, GameplayTag>
            {
                { "Ability", new GameplayTag("Ability") },
                { "Ability.Attack", new GameplayTag("Ability.Attack") },
            };
    }
}
