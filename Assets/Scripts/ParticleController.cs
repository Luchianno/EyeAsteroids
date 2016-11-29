using UnityEngine;
using System.Collections;

public class ParticleController : MonoBehaviour
{

    void Start()
    {
        GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = "Stars";
    }

    void Update()
    {

    }
}
