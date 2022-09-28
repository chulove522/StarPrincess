using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game4
{
    public enum BubbleType
    {
        Empty,
        LowMassStar,
        MediumStar,
        RedGiant,
        PlanetaryNebula,
        WhiteDwarf,
        BlackDwarf,
        HighMassStar,
        MassiveStar,
        RedSuperGiant,
        SuperNova,
        NeutronStar,
        BlackHole,
        Random,
    }

    public static class BubbTypeUtil
    {
        public static BubbleType GetRandomStageType()
        {
            return (BubbleType)UnityEngine.Random.Range((int)BubbleType.LowMassStar, (int)BubbleType.Random);
        }

        public static BubbleType GetRandomRandType()
        {
            return (BubbleType)UnityEngine.Random.Range((int)BubbleType.LowMassStar, (int)BubbleType.Random + 1);
        }

        // 按权重选取
        public static T SelectByWeight<T>(this IDictionary<T, int> dict)
        {
            var allWeight = 0;
            foreach (var itemWeight in dict.Values)
                allWeight += itemWeight;

            if (allWeight == 0) return default;

            var value = UnityEngine.Random.Range(0, allWeight);
            foreach (var item in dict)
            {
                if (value < item.Value)
                    return item.Key;

                value -= item.Value;
            }

            return default;
        }
    }
}
