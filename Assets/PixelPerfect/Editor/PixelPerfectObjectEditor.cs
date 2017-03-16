using UnityEngine;
using UnityEditor;
using Object = UnityEngine.Object;

namespace PixelPerfect.Editor
{
    [CustomEditor(typeof(PixelPerfectObject), true)]
    [CanEditMultipleObjects]
    public class PixelPerfectObjectEditor : UnityEditor.Editor
    {

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Align"))
            {
                foreach (Object tgt in targets)
                {
                    PixelPerfectObject obj = (PixelPerfectObject)tgt;

                    if (obj is PixelPerfectSprite)
                    {
                        obj.Align<PixelPerfectManager>();
                    }
                    else if (obj is PixelPerfectTile)
                    {
                        obj.Align<PixelPerfectTilegridManager>();
                    }
                }
            }
        }
    }
}
