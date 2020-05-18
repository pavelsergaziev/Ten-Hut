using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SergeantAnimation", menuName = "GameScriptableObjectsAsset/SergeantAnimation")]
public class SergeantAnimationSettngsSO : ScriptableObject
{
    [SerializeField] private Sprite _idleSprite;
    [SerializeField] private Sprite[] _animationSpritesSequenced;
    [SerializeField] private float _animationFrameChangeDelay;

    public Sprite IdleSprite { get { return _idleSprite; } }
    public Sprite[] AnimationSpritesSequenced { get { return _animationSpritesSequenced; } }
    public float AnimationFrameChangeDelay { get { return _animationFrameChangeDelay; } }
}
