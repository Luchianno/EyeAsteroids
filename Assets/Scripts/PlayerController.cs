using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public int Health { get; private set; }
    public int InitialHealth = 1000;
    public int MinCollisionDamage = 50;
    public int MaxCollisionDamage = 300;

    //public bool DebugMode;


    EyeXHost host;
    //IEyeXDataProvider<EyeXEyePosition> eyePos;
    IEyeXDataProvider<EyeXGazePoint> gazePos;

    void Start()
    {
        host = EyeXHost.GetInstance();
        //eyePos = host.GetEyePositionDataProvider();
        gazePos = host.GetGazePointDataProvider(Tobii.EyeX.Framework.GazePointDataMode.LightlyFiltered);
        gazePos.Start();

        ResetHealth();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }

    public void ResetHealth()
    {
        this.Health = InitialHealth;
    }


    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Wall")
        {
            return;
        }
        var ast = coll.gameObject.GetComponent<AsteroidController>();
        //var temp = coll.rigidbody.mass * (coll.relativeVelocity.sqrMagnitude / maxCollisionSpeed);
        //var damage = (int)Mathf.Lerp(MinCollisionDamage, MaxCollisionDamage, temp / 100);
        var damage = Mathf.Lerp(this.MinCollisionDamage, this.MaxCollisionDamage,
            (ast.MaxSize - ast.transform.localScale.x) / (ast.MaxSize - ast.MinSize));
        this.Health -= Mathf.RoundToInt(damage);


        iTween.PunchScale(this.gameObject, new Vector3(-0.15f, -0.15f), 0.8f);
    }

    void OnGUI()
    {
        if (Debug.isDebugBuild)
        {
            GUI.TextField(new Rect(10, 10, 100, 20), "Health " + Mathf.Clamp(Health, 0, 1000).ToString());
        }
    }
}
