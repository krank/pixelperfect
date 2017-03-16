using UnityEngine;

namespace PixelPerfect
{
    [RequireComponent(typeof(PixelPerfectManager))]
    public class PixelPerfectTilegridManager : PixelPerfectAlignmentGrid
    {

        [SerializeField]
        bool displayGridInEditor = true;

        [SerializeField]
        private int gridTilesToDisplay = 1;

        [SerializeField]
        private int PixelsPerTile = 32;

        private PixelPerfectManager pixelManager;

        public override float UnitsPerGridUnit
        {
            get
            {
                return (float)PixelsPerTile / (float)pixelManager.GridUnitsPerUnit;
            }
        }

        public override float GridUnitsPerUnit
        {
            get
            {
                return 1 / UnitsPerGridUnit;
            }

            protected set { }
        }

        private void OnDrawGizmos()
        {
            pixelManager = GetComponent<PixelPerfectManager>();

            if (displayGridInEditor)
            {

                for (int pos = -gridTilesToDisplay; pos < gridTilesToDisplay + 1; pos++)
                {

                    Gizmos.color = Color.grey;

                    // Draw horizontal lines
                    Gizmos.DrawLine(new Vector3(-gridTilesToDisplay, pos) * UnitsPerGridUnit,
                                    new Vector3(gridTilesToDisplay, pos) * UnitsPerGridUnit
                                    );

                    // Draw vertical lines
                    Gizmos.DrawLine(new Vector3(pos, -gridTilesToDisplay) * UnitsPerGridUnit,
                                    new Vector3(pos, gridTilesToDisplay) * UnitsPerGridUnit
                                    );
                }

            }
        }

    }
}