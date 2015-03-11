//-----------------------------------------------------------------------
// Copyright 2014 Tobii Technology AB. All rights reserved.
//-----------------------------------------------------------------------

using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour
{
    private Enemy _enemy;
    private const float _scale = 0.35f;

    void Update()
    {
        // Make sure we got an enemy.
        if (_enemy == null)
        {
            return;
        }

        // Move the missile towards the enemy ship.
        var speed = Time.deltaTime * 10f;
        transform.position = Vector3.MoveTowards(transform.position, _enemy.transform.position, speed);

        // Scale the missile (and flip it if necessary).
        var isLeft = transform.position.x < 0;
        transform.localScale = new Vector3(isLeft ? _scale : -_scale, _scale, _scale);

        // Reached center?
        var dist = Vector3.Distance(renderer.bounds.center, _enemy.renderer.bounds.center);
        if (Mathf.Abs(dist) < 0.5f)
        {
            // Destroy the enemy and the missile.
            Destroy(_enemy.gameObject);
            Destroy(this.gameObject);
        }
    }

    public void SetEnemyTarget(Enemy enemy)
    {
        _enemy = enemy;
    }
}
