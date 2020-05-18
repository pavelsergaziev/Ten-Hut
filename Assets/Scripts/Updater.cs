using System.Collections.Generic;
using UnityEngine;

public class Updater : MonoBehaviour
{
    private List<IUpdated> _updatedObjs;

    private void Awake()
    {
        _updatedObjs = new List<IUpdated>();
    }

    private void Update()
    {
        foreach (var item in _updatedObjs)
        {
            item.Tick();
        }
    }

    public void Subscribe(IUpdated obj)
    {
        _updatedObjs.Add(obj);
    }

    public void SubscribeAndPrioritize(IUpdated obj)
    {
        _updatedObjs.Insert(0, obj);
    }

    public void Unsubscribe(IUpdated obj)
    {
        _updatedObjs.Remove(obj);
    }
}
