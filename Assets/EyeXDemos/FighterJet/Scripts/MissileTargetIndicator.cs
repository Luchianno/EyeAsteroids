//-----------------------------------------------------------------------
// Copyright 2014 Tobii Technology AB. All rights reserved.
//-----------------------------------------------------------------------

using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class MissileTargetIndicator : MonoBehaviour
{
    private SpriteRenderer _renderer;

    public Sprite TargetSprite;
    public Sprite TargetLockSprite;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    public void Hide()
    {
        renderer.enabled = false;
    }

    public void SetState(bool locked)
    {
        _renderer.enabled = true;
        _renderer.sprite = locked ? TargetLockSprite : TargetSprite;
    }
}
