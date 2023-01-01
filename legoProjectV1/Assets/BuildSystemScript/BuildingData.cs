using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CreateAssetMenu(menuName = "Building System/Build Data")]
public class BuildingData : ScriptableObject
{
    public string DisplayName;
    public Sprite Icon;
    public float GridSize;
    public GameObject PreFab;
    public Vector3 BuildingSize;
    public PartType PartType;
}
public enum PartType
{
    engine = 0,
    body = 1,
    wheel = 2

}

#if UNITY_EDITOR
[CustomEditor(typeof(BuildingData))]
public class BuildingDataEdit : Editor
{
    public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height)
    {
        var data = (BuildingData)target;

        if (data == null || data.Icon == null) return null;

        var texture = new Texture2D(width, height);
        EditorUtility.CopySerialized(data.Icon.texture, texture);
        return texture;

    }
}
#endif