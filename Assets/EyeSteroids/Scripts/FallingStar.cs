using UnityEngine;
using System.Collections;

public class FallingStar : MonoBehaviour
{
    GameObject Prefab;
    public float StartingSpeed;
    public float Force;

    void Start()
    {
    }

    void Update()
    {
        this.transform.position += new Vector3(1, -1) * StartingSpeed * Time.deltaTime;
        StartingSpeed += Force;
        Force = Force * 1.5f;
    }
}
