using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OrdersAndExecution
{

    [CreateAssetMenu(fileName = "Order", menuName = "GameScriptableObjectsAsset/Order")]
    public class OrderSO : ScriptableObject
    {

        [SerializeField] private AudioClipsArraySO _orderAudioClips;
        public AudioClipsArraySO OrderAudioClips { get { return _orderAudioClips; } }

        [SerializeField] private OrdersAndActions _order;
        public OrdersAndActions Order { get { return _order; } }

        [Tooltip("Сколько раз может повторяться подряд")]
        [Range(1,10)]
        [SerializeField] private int _maxIssuedConsecutively;
        public int MaxIssuedConsecutively { get { return _maxIssuedConsecutively; } }
    }
}