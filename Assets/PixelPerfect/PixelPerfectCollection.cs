using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelPerfect
{
    public class PixelPerfectCollection : PixelPerfectSprite
    {
        Vector3 alignmentOffset;

        public override void Align()
        {
            Align(alignmentManager.GridUnitsPerUnit, Vector3.zero);
        }

        public override void Align(float gridUnitsPerUnit, Vector3 pivotOffset)
        {

            bool first = true;

            foreach(Transform child in transform)
            {
                if (first)
                {
                    Vector3 alignedVector = GenerateAlignedVector(child.gameObject, alignmentManager.GridUnitsPerUnit, Vector3.zero);

                    alignmentOffset = child.transform.position - alignedVector;
                    first = false;
                }

                child.transform.position += alignmentOffset;
                
            }
        }

        public override void Unalign()
        {
            foreach(Transform child in transform)
            {
                child.transform.position -= alignmentOffset;
            }
        }

    }
}