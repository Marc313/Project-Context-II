/*using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(Transform))]
public class TransformCopyPastePos : Editor
{
    private Vector3 latestLocalPos;

    private const string LatestLocalPosXKey = "TransformCopyPastePos_LatestLocalPosX";
    private const string LatestLocalPosYKey = "TransformCopyPastePos_LatestLocalPosY";
    private const string LatestLocalPosZKey = "TransformCopyPastePos_LatestLocalPosZ";

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Copy LocalPos"))
        {
            Transform transform = target as Transform;
            latestLocalPos = transform.localPosition;

            EditorPrefs.SetFloat(LatestLocalPosXKey, latestLocalPos.x);
            EditorPrefs.SetFloat(LatestLocalPosYKey, latestLocalPos.y);
            EditorPrefs.SetFloat(LatestLocalPosZKey, latestLocalPos.z);
        }

        if (GUILayout.Button("Paste LocalPos"))
        {
            Transform transform = target as Transform;
            latestLocalPos = new Vector3(
                                EditorPrefs.GetFloat(LatestLocalPosXKey),
                                EditorPrefs.GetFloat(LatestLocalPosYKey),
                                EditorPrefs.GetFloat(LatestLocalPosZKey)
                            );

            transform.localPosition = latestLocalPos;
        }
    }
}
*/