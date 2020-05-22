using UnityEngine;
using UnityEditor;

public static class EditorExtensions
{
    public static void SetPropertyValueToNullIfPossible(this SerializedProperty property)
    {
        switch (property.propertyType)
        {
            case SerializedPropertyType.Generic:
                break;
            case SerializedPropertyType.Integer:
                break;
            case SerializedPropertyType.Boolean:
                break;
            case SerializedPropertyType.Float:
                break;
            case SerializedPropertyType.String:
                break;
            case SerializedPropertyType.Color:
                break;
            case SerializedPropertyType.ObjectReference:
                property.objectReferenceValue = null;
                break;
            case SerializedPropertyType.LayerMask:
                break;
            case SerializedPropertyType.Enum:
                break;
            case SerializedPropertyType.Vector2:
                break;
            case SerializedPropertyType.Vector3:
                break;
            case SerializedPropertyType.Vector4:
                break;
            case SerializedPropertyType.Rect:
                break;
            case SerializedPropertyType.ArraySize:
                break;
            case SerializedPropertyType.Character:
                break;
            case SerializedPropertyType.AnimationCurve:
                break;
            case SerializedPropertyType.Bounds:
                break;
            case SerializedPropertyType.Gradient:
                break;
            case SerializedPropertyType.Quaternion:
                break;
            case SerializedPropertyType.ExposedReference:
                break;
            case SerializedPropertyType.FixedBufferSize:
                break;
            case SerializedPropertyType.Vector2Int:
                break;
            case SerializedPropertyType.Vector3Int:
                break;
            case SerializedPropertyType.RectInt:
                break;
            case SerializedPropertyType.BoundsInt:
                break;
            default:
                break;
        }
    }
}
