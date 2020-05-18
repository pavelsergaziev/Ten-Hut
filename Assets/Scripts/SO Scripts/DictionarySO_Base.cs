using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class DictionarySO_Base<TK,TV> : ScriptableObject, ISerializationCallbackReceiver
{
    private Dictionary<TK, TV> _dictionary;
    [SerializeField] private TK[] _keys;
    [SerializeField] private TV[] _values;

    public void OnBeforeSerialize()
    {
        _dictionary.Clear();
    }

    public void OnAfterDeserialize()
    {
        _dictionary = new Dictionary<TK, TV>();
        for (int i = 0; i != Math.Min(_keys.Length, _values.Length); i++)
        {
            _dictionary.Add(_keys[i], _values[i]);
        }
    }

    public TV GetValueByKey(TK key)
    {
        return _dictionary[key];
    }
}
