//-----------------------------------------------------------------------
// Copyright 2014 Tobii Technology AB. All rights reserved.
//-----------------------------------------------------------------------

using UnityEngine;

public class Explosion : MonoBehaviour
{
    void Start()
    {
        // Destroy the explosion when particles are finished.
        var delay = gameObject.particleSystem.duration + gameObject.particleSystem.startLifetime;
        Destroy(gameObject, delay);
    }
}