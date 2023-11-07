using UnityEngine;

namespace Magzimof
{
    public static class Util
    {
        public static bool randomBoolean ()
        {
            if (Random.value >= 0.5)
            {
                return true;
            }
            return false;
        }
    }
}