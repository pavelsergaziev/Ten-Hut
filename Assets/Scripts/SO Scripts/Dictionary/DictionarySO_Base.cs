using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class DictionarySO_Base<TK,TV> : DictionarySO_NonGenericBase, ISerializationCallbackReceiver
{
    [SerializeField] private TK[] _keys = default;
    [SerializeField] private TV[] _values = default;

    protected Dictionary<TK, TV> Dictionary = new Dictionary<TK, TV>();

    public void OnBeforeSerialize()
    {
    }

    public void OnAfterDeserialize()
    {
        Flag_SameKeyUsed = false;
        Dictionary.Clear();

        for (int i = 0; i < _keys.Length; i++)
        {
            if (Dictionary.ContainsKey(_keys[i]))
            {
                Flag_SameKeyUsed = true;
                Dictionary.Clear();
                break;
            }

            Dictionary.Add(_keys[i], _values[i]);
        }
    }

    public TV GetValueByKey(TK key)
    {
        return Dictionary[key];
    }

    public bool ContainsKey(TK key)
    {
        return Dictionary.ContainsKey(key);
    }

    public bool ContainsValue(TV value)
    {
        return Dictionary.ContainsValue(value);
    }
}