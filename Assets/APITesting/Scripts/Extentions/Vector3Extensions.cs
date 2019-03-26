using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Extensions
{
    public static class Vector3Extensions
    {
        /// <summary>
        /// Allows to set transform like other transform but with different one axis...
        /// </summary>
        /// <param name="original"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public static Vector3 With (this Vector3 original, float? x = null, float? y = null, float? z = null)
        {
            float newX = x ?? original.x;
            float newY = y ?? original.y;
            float newZ = z ?? original.z;

            return new Vector3(newX, newY, newZ);
        }
    }
}
