using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DictionarySO_NonGenericBase), true)]
public class DictionarySOInspector : Editor
{
    private DictionarySO_NonGenericBase _target;
    private SerializedProperty _someInt;
    private SerializedProperty _keys;
    private SerializedProperty _values;

    private void OnEnable()
    {
        _target = (DictionarySO_NonGenericBase)target;
        _someInt = serializedObject.FindProperty("TestInt");
        _keys = serializedObject.FindProperty("_keys");
        _values = serializedObject.FindProperty("_values");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        for (int i = 0; i < _keys.arraySize; i++)
        {
            EditorGUILayout.BeginHorizontal("box");

            EditorGUILayout.PropertyField(_keys.GetArrayElementAtIndex(i), GUIContent.none);
            EditorGUILayout.PropertyField(_values.GetArrayElementAtIndex(i), GUIContent.none);


            if (GUILayout.Button("X", GUILayout.Width(20), GUILayout.Height(20)))
            {
                DeleteArrayElement(_keys, i);
                DeleteArrayElement(_values, i);
            }

            EditorGUILayout.EndHorizontal();
        }

        if (_target.Flag_SameKeyUsed == true)
        {
            EditorGUILayout.HelpBox("Same key is used several times, so the dictionary is invalid and will be empty!", MessageType.Error);
        }


        if (GUILayout.Button("Add item", GUILayout.Width(80), GUILayout.Height(30)))
        {
            _values.arraySize = ++_keys.arraySize;
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void DeleteArrayElement(SerializedProperty arrayProperty, int indexInArray)
    {

        //if (arrayProperty.GetArrayElementAtIndex(indexInArray)
        // .propertyType == SerializedPropertyType.Enum
        // // || и т.д., всё, что нужно предзачистить
        // )
        //{
        // arrayProperty.DeleteArrayElementAtIndex(indexInArray);
        //}

        //или второй вариант
        arrayProperty.GetArrayElementAtIndex(indexInArray).SetPropertyValueToNullIfPossible();


        arrayProperty.DeleteArrayElementAtIndex(indexInArray);
    }
}
