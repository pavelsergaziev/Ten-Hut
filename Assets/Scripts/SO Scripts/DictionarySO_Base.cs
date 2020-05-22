using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class DictionarySO_Base<TK,TV> : DictionarySO_NonGenericBase, ISerializationCallbackReceiver
{
    [SerializeField] private TK[] _keys = default;
    [SerializeField] private TV[] _values = default;

    public Dictionary<TK, TV> _dictionary = new Dictionary<TK, TV>();

    public void OnBeforeSerialize()
    {
    }

    public void OnAfterDeserialize()
    {
        Flag_SameKeyUsed = false;
        _dictionary.Clear();

        for (int i = 0; i < _keys.Length; i++)
        {
            if (_dictionary.ContainsKey(_keys[i]))
            {
                Flag_SameKeyUsed = true;
                _dictionary.Clear();
                break;
            }

            _dictionary.Add(_keys[i], _values[i]);
        }
    }

    public TV GetValueByKey(TK key)
    {
        return _dictionary[key];
    }

    public bool ContainsKey(TK key)
    {
        return _dictionary.ContainsKey(key);
    }

    public bool ContainsValue(TV value)
    {
        return _dictionary.ContainsValue(value);
    }
}