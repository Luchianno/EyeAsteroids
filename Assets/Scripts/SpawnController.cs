using UnityEngine;
using System.Collections;

public class SpawnController : MonoBehaviour
{
    public GameObject[] asteroids;

    public float MinRespawn;
    public float MaxRespawn;
    public float MinSpeed;
    public float MaxSpeed;

    public GameObject parent;

    void Start()
    {
        Invoke("Spawn", Random.Range(0, 2f));
        if (parent == null)
        {
            parent = GameObject.Find("Asteroids");
        }

        //LeanTween.move(this.gameObject, transform.position + transform.up* , 1f).setLoopType(LeanTweenType.pingPong);
    }



    public void Spawn()
    {
        var temp = (GameObject)Instantiate(asteroids[Random.Range(0, asteroids.Length)], this.transform.position, Quaternion.identity);
        temp.transform.parent = parent.transform;

        temp.rigidbody2D.velocity = Random.Range(MinSpeed, MaxSpeed) * Random.insideUnitCircle;

        Invoke("Spawn", Random.Range(MinRespawn, MaxRespawn));
    }
}
