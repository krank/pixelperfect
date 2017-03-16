using System.Globalization;
using UnityEditor;
using UnityEngine;

namespace PixelPerfect.Editor
{
    [CustomEditor(typeof(PixelPerfectManager))]
    public class PixelPerfectManagerEditor : UnityEditor.Editor
    {

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            PixelPerfectManager pixelPerfectManager = (PixelPerfectManager) target;

            EditorGUILayout.LabelField("Pixels per unit", pixelPerfectManager.GridUnitsPerUnit.ToString(CultureInfo.CurrentCulture));

            EditorGUILayout.LabelField("Units per pixel", pixelPerfectManager.UnitsPerGridUnit.ToString(CultureInfo.CurrentCulture));

            EditorGUILayout.LabelField("Actual vertical resolution", pixelPerfectManager.PixelGridVerticalResolution.ToString(CultureInfo.CurrentCulture));

            EditorGUILayout.LabelField("Pixel multiplier", pixelPerfectManager.PixelMultiplier.ToString(CultureInfo.CurrentCulture));


            if (GUILayout.Button("Update grid & Set Camera"))
            {
                pixelPerfectManager.UpdatePixelGrid((int) Handles.GetMainGameViewSize().y);
            }

            if (GUILayout.Button("Align all sprites by this"))
            {
                pixelPerfectManager.AlignAllByThis();
            }
        }
    }
}