using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public bool isGameOver = true;
    public float GrowTime = 2f;
    public float StartingPlayerSize = 2.5f;


    private float eyesDetectedDuration; // ramdeni xania dafiqsirebuli tvalebi
    private float eyesDetectedLast; // bolo dafiqsirebis dro

    public PlayerController Player;
    public AudioSource DeathSound;
    public Canvas UI;

    EyeXHost host;
    IEyeXDataProvider<EyeXGazePoint> gazePos;

    public void Start()
    {
        host = EyeXHost.GetInstance();
        gazePos = host.GetGazePointDataProvider(Tobii.EyeX.Framework.GazePointDataMode.LightlyFiltered);
        Player.transform.localScale = new Vector3(StartingPlayerSize, StartingPlayerSize, StartingPlayerSize);
    }

    public void StartGame()
    {
        isGameOver = false;
        Player.enabled = true;
        Player.ResetHealth();
        LeanTween.scale(Player.gameObject, new Vector3(1, 1, 1), GrowTime).setLoopType(LeanTweenType.easeOutCubic).setLoopOnce();

        StartSpawners();
        UI.GetComponent<Animator>().SetBool("Active", false);
    }

    void EndGame(bool win)
    {
        DeathSound.Play();
        isGameOver = true;
        StopSpawners();

        foreach (var item in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            item.GetComponent<hObjectPoolItem>().DespawnSafely();
        }

        LeanTween.cancel(Player.gameObject);
        LeanTween.scale(Player.gameObject, new Vector3(3, 3, 3), GrowTime);
        LeanTween.move(this.gameObject, Vector3.zero, GrowTime);
        UI.GetComponent<Animator>().SetBool("Active", true);
    }

    void Update()
    {
        if (Input.GetButtonUp("Jump"))
        {
            StartGame();
        }

        if (isGameOver)
        {
            //თუ თვალებს ვხედავთ
            if (gazePos.Last.IsValid && gazePos.Last.IsWithinScreenBounds)
            {
                eyesDetectedDuration += Time.deltaTime;
                eyesDetectedLast = Time.time;
                if (eyesDetectedDuration > 4.5f)
                {
                    eyesDetectedDuration = 0;
                    StartGame();
                }
            }
            //else if (Time.time - eyesDetectedLast <= 0.3f) // 300 miliwami?
            //{
            //    eyesDetectedDuration += Time.deltaTime;
            //}
            else
            {
                eyesDetectedDuration = 0;
            }
        }
        else
        {
            if (Player.Health <= 0)
            {
                EndGame(false);
            }
        }
    }

    public void StopSpawners()
    {
        foreach (var item in GameObject.FindGameObjectsWithTag("Spawner"))
        {
            item.SendMessage("CancelInvoke", "Spawn");
        }
    }

    public void StartSpawners()
    {
        foreach (var item in GameObject.FindGameObjectsWithTag("Spawner"))
        {
            item.SendMessage("Spawn");
        }
    }

    void OnGUI()
    {
        if (Debug.isDebugBuild)
        {
            GUI.TextField(new Rect(10, 30, 100, 20), "Time " + this.eyesDetectedDuration.ToString());
        }
    }

}
