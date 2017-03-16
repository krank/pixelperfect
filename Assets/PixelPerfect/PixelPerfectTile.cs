using UnityEngine;

namespace PixelPerfect
{
    [RequireComponent(typeof(Renderer))]
    public class PixelPerfectTile : PixelPerfectObject
    {
        public override void Align(float gridUnitsPerUnit)
        {
            Renderer r = GetComponent<Renderer>();

            Align(gridUnitsPerUnit, transform.position - r.bounds.min);

        }
    }
}