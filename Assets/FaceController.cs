using UnityEngine;
using System.Collections;

public class FaceController : MonoBehaviour
{
    PlayerController player;
    public SpriteRenderer spriteRenderer;
    public GameObject outer;
    public GameObject inner;

    public Sprite[] sprites;

    EyeXHost host;
    IEyeXDataProvider<EyeXGazePoint> gaze;

    void Start()
    {
        host = EyeXHost.GetInstance();
        gaze = host.GetGazePointDataProvider(Tobii.EyeX.Framework.GazePointDataMode.LightlyFiltered);
        player = gameObject.GetComponentInParent<PlayerController>();
        gaze.Start();
    }

    void Update()
    {
        // TODO test this
        spriteRenderer.sprite = sprites[(int)Mathf.Lerp(sprites.Length - 1, 0, (float)player.Health / (float)player.InitialHealth)];

        if (gaze.Last.IsValid && gaze.Last.IsWithinScreenBounds)
        {
            var dir = Camera.main.ScreenToWorldPoint(gaze.Last.Screen) - transform.position;

            outer.transform.localPosition = Mathf.SmoothStep(0, 0.8f, dir.magnitude / 3f) * dir.normalized;
            inner.transform.localPosition = Mathf.SmoothStep(0, 0.5f, dir.magnitude / 3f) * dir.normalized;
        }
        else
        {
            var dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

            outer.transform.localPosition = Mathf.SmoothStep(0, 0.8f, dir.magnitude / 3f) * dir.normalized;
            inner.transform.localPosition = Mathf.SmoothStep(0, 0.5f, dir.magnitude / 3f) * dir.normalized;
        }
    }

}
