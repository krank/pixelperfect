using System.Collections.Generic;
using UnityEngine;

namespace PixelPerfect
{
    // TODO: Pixel perfect UI elements
    // TODO: Blinds
    // TODO: GitLab
    // TODO: Comments
    // TODO: Fix bug: desired vert size > screen vert size

    [RequireComponent(typeof(Camera))]
    public class PixelPerfectManager : PixelPerfectAlignmentGrid, IPixelPerfectRegistrar
    {
        // Resolutions
        [SerializeField]
        int desiredVerticalResolution = 240;
        int pGridVerticalResolution;

        public int PixelGridVerticalResolution
        {
            get { return pGridVerticalResolution; }
        }

        // Pixel measurements
        int pixelMultiplier = 0;

        public int PixelMultiplier
        {
            get { return pixelMultiplier; }
        }

        private float ppu = -1;

        public override float GridUnitsPerUnit
        {
            get
            {
                if (ppu < 0)
                {
                    ppu = modelSprite.pixelsPerUnit;
                }
                return ppu;
            }
            protected set { ppu = value; }
        }

        public override float UnitsPerGridUnit
        {
            get { return 1 / ppu; }
        }


        // Model sprite (used to get ppu)
        [SerializeField]
        Sprite modelSprite;

        // Camera reference
        private Camera cam;

        // List of sprites
        private readonly List<PixelPerfectObject> spriteList = new List<PixelPerfectObject>();


        private void Start()
        {
            // Update the pixel grid based on current screen height
            UpdatePixelGrid(Screen.height);

        }

        public void RegisterObject(PixelPerfectObject obj)
        {
            spriteList.Add(obj);
        }

        public void DeregisterObject(PixelPerfectObject obj)
        {
            if (ObjectIsRegistered(obj))
            {
                spriteList.Remove(obj);
            }
        }

        public bool ObjectIsRegistered(PixelPerfectObject obj)
        {
            return spriteList.Contains(obj);
        }


        public void UpdatePixelGrid(int screenHeight)
        {
            // We know there's a camera, so get a reference to it
            cam = GetComponent<Camera>();

            // Calculate the pixel multiplier based on desired vert resolution
            float unevenPixelMultiplier = (float)screenHeight / (float)desiredVerticalResolution;

            // Round the pixel multiplier down
            pixelMultiplier = Mathf.FloorToInt(unevenPixelMultiplier);

            // Generate the actual vertical pixel grid based on the rounded
            // pixel multiplier
            pGridVerticalResolution = screenHeight / pixelMultiplier;

            // Get ppu from model sprite
            GridUnitsPerUnit = modelSprite.pixelsPerUnit;

            // Use vertical pixel grid height to calculate size of camera
            cam.orthographicSize = pGridVerticalResolution / GridUnitsPerUnit / 2;
        }

        private void OnPreRender()
        {
            AlignAll();
        }

        private void OnPostRender()
        {
            UnalignAll();
        }

        public void AlignAll()
        {
            // Align all sprites to pixel grid
            foreach (PixelPerfectObject obj in spriteList)
            {
                obj.Align();
            }

        }

        public void UnalignAll()
        {
            // Unalign all sprites; return them to their original positions
            foreach (PixelPerfectObject obj in spriteList)
            {
                if (obj is PixelPerfectSprite)
                {
                    obj.Unalign();
                }
            }
        }

        public void AlignAllByThis()
        {
            foreach (PixelPerfectSprite sprite in FindObjectsOfType<PixelPerfectSprite>())
            {
                sprite.Align(this.GridUnitsPerUnit);
            }
        }
    }
}