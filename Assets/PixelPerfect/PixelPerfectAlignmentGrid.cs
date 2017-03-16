using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelPerfect
{
    public abstract class PixelPerfectAlignmentGrid : MonoBehaviour
    {
        public abstract float GridUnitsPerUnit { get; protected set; }

        public abstract float UnitsPerGridUnit { get; }


    }
}