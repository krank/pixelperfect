using UnityEngine;

namespace PixelPerfect
{
    public class PixelPerfectSprite : PixelPerfectObject
    {

        // Vector position memories
        private Vector3 unalignedVector;
        private Vector3 alignedVector;
        private Vector3 positionLastFrame;

        public bool AlignInRealtime
        {

            get
            {
                if (!(alignmentManager is IPixelPerfectRegistrar))
                {
                    Debug.LogError("Alignment manager incapable of registering objects.");
                    return false;
                }

                IPixelPerfectRegistrar registrar = alignmentManager as IPixelPerfectRegistrar;

                return registrar.ObjectIsRegistered(this);

            }
            set
            {
                if (alignmentManager == null)
                {
                    Debug.LogError("No alignment manager set or available.");
                }

                if (!(alignmentManager is IPixelPerfectRegistrar))
                {
                    Debug.LogError("Alignment manager incapable of registering objects!");
                }
                else
                {
                    IPixelPerfectRegistrar registrar = alignmentManager as IPixelPerfectRegistrar;

                    if (value)
                    {
                        registrar.RegisterObject(this);
                    }
                    else
                    {
                        registrar.DeregisterObject(this);
                    }
                }

            }
        }

        void Start()
        {
            /* if the alignment manager hasn't been set, get the first one we
             * can find
             */

            if (alignmentManager == null)
            {
                // Get the first pixelManager we can find (should only be one per scene)
                alignmentManager = (PixelPerfectManager)Object.FindObjectOfType(typeof(PixelPerfectManager));
            }

            // Align sprite to pixel grid
            Align();

            // Activate constant realtime alignment
            AlignInRealtime = true;
        }


        public override void Align(float gridUnitsPerUnit)
        {

            // If our (unaligned, presubably) position has changed since the last frame, calculate new aligned position.
            if (positionLastFrame != transform.position)
            {
                // Back up current position
                unalignedVector = transform.position;

                // Determine new alignment position, align sprite
                base.Align(gridUnitsPerUnit);

                // Remember the aligned vector
                alignedVector = transform.position;
            }
            else
            {
                // Align sprite
                this.transform.position = alignedVector;
            }

        }

        public override void Unalign()
        {
            // Reset the sprite's position to its unaligned original position
            this.transform.position = unalignedVector;

            // Save current position for comparisons in the next update
            positionLastFrame = this.transform.position;
        }
    }
}