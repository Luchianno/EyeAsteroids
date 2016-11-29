using UnityEngine;
using System.Collections;

public class SpawnController : MonoBehaviour
{
    public GameObject[] asteroids;

    public float MinRespawn;
    public float MaxRespawn;
    public float MinSpeed;
    public float MaxSpeed;

    void Start()
    {
        //Invoke("Spawn", Random.Range(0, 2f));

        //LeanTween.move(this.gameObject, transform.position + transform.up* , 1f).setLoopType(LeanTweenType.pingPong);
    }


    public void Spawn()
    {
        var temp = hObjectPool.Instance.Spawn(asteroids[Random.Range(0, asteroids.Length)], this.transform.position, Quaternion.identity);

        // (GameObject)Instantiate(asteroids[Random.Range(0, asteroids.Length)], this.transform.position, Quaternion.identity);

        temp.GetComponent<Rigidbody2D>().velocity = Random.Range(MinSpeed, MaxSpeed) * Random.insideUnitCircle;

        Invoke("Spawn", Random.Range(MinRespawn, MaxRespawn));
    }
}
