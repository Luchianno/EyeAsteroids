using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Rotator2D : MonoBehaviour
{
    public float MinAngular;
    public float MaxAngular;

    // Use this for initialization
    void Start()
    {
        this.rigidbody2D.angularVelocity = Random.Range(MinAngular, MaxAngular);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
