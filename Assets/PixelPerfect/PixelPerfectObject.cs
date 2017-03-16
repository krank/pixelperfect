using UnityEngine;

namespace PixelPerfect
{
    public class PixelPerfectObject : MonoBehaviour
    {

        [SerializeField, Tooltip("Grid manager to use for alignment")]
        protected PixelPerfectAlignmentGrid alignmentManager;

        public virtual void Align<TManager>()
            where TManager : PixelPerfectAlignmentGrid
        {
            PixelPerfectAlignmentGrid manager = alignmentManager ?? (TManager)FindObjectOfType(typeof(TManager));

            if (manager == null)
            {
                Debug.LogError("No suitable manager found!");
            }
            else
            {
                Align(manager.GridUnitsPerUnit);
            }

        }

        public virtual void Align()
        {
            Align(alignmentManager.GridUnitsPerUnit);
        }

        public virtual void Align(float gridUnitsPerUnit)
        {
            Align(gridUnitsPerUnit, Vector2.zero);
        }

        public virtual void Align(float gridUnitsPerUnit, Vector3 pivotOffset)
        {
            // Create a new grid-aligned vector.

            Vector3 alignedVector = GenerateAlignedVector(gameObject, gridUnitsPerUnit, pivotOffset);

            // Move object to aligned position
            gameObject.transform.position = alignedVector;
        }

        public Vector3 GenerateAlignedVector(GameObject obj, float gridUnitsPerUnit, Vector3 pivotOffset)
        {
            return pivotOffset + new Vector3(

                // x component
                Mathf.Round(
                    (obj.transform.position.x - pivotOffset.x)
                    * gridUnitsPerUnit
                ) / gridUnitsPerUnit,

                // y component
                Mathf.Round(
                    (obj.transform.position.y - pivotOffset.y)
                    * gridUnitsPerUnit
                ) / gridUnitsPerUnit,
                obj.transform.position.z

            );
        }

        public virtual void Unalign() { }

    }
}