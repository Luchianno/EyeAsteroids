using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public bool isGameOver = true;

    public float PlayTime = 120;

    private float eyesDetectedDuration; // ramdeni xania dafiqsirebuli tvalebi
    private float eyesDetectedLast; // bolo dafiqsirebis dro

    public void StartGame()
    {
        StartCoroutine(runTimer());
        isGameOver = false;
        //isGameOver = fal
    }

    void EndGame(bool win)
    {
        StopCoroutine("runTimer");
        isGameOver = true;
        this.collider2D.enabled = false;
        float time = 2f;
        LeanTween.scale(this.gameObject, new Vector3(3, 3, 3), time);
        LeanTween.move(this.gameObject, Camera.main.transform.position, time);

        isGameOver = false;
    }

    void Start()
    {

    }

    void Update()
    {

        if (isGameOver)
        {
            //თუ თვალებს ვხედავთ
            //if (gazePos.Last.IsValid && gazePos.Last.IsWithinScreenBounds)
            {
                eyesDetectedDuration += Time.deltaTime;
                eyesDetectedLast = Time.time;
            }
            //else if (Time.time - eyesDetectedLast <= 0.3f) // 300 miliwami?
            {
                eyesDetectedDuration += Time.deltaTime;
            }
            // else
            {
                eyesDetectedDuration = 0;
            }
        }
        else
        {
            //if (Health <= 0)
            {
                //EndGame(false);
            }
        }
    }

    IEnumerator runTimer()
    {
        yield return new WaitForSeconds(PlayTime);
        EndGame(true);
    }


    void OnGUI()
    {
        if (Debug.isDebugBuild)
        {
            GUI.TextField(new Rect(10, 30, 50, 20), this.eyesDetectedDuration.ToString());
        }
    }

}
