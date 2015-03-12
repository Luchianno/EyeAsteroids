using UnityEngine;
using System.Collections;
using Tobii.EyeX.Client;
using System.Collections.Generic;
using UnityEngine.Serialization;


// ვაკეთებ დაშვებას რომ მომხმარებლის ყოველი 1 წამიანი მიშტერებული
// მზერა ასტეროიდს აკლებს 100 სიცოცხლეს

public class AsteroidController : EyeXGameObjectInteractorBase
{
    public float MinHealth;
    public float MaxHealth;

    public float MinMass;
    public float MaxMass;

    public GameObject Explosion;
    public Color EndColor;
    public float MinSize;
    public float MaxSize;

    float startingHealth;
    float currentHealth;
    float time;
    Color startingColor;
    SpriteRenderer astRenderer;
    bool isMouseOver;

    void Start()
    {
        astRenderer = this.gameObject.GetComponentInChildren<SpriteRenderer>();
        startingColor = astRenderer.color;

        var size = Random.Range(MinSize, MaxSize);
        this.transform.localScale *= size;

        startingHealth = Mathf.Lerp(MinHealth, MaxHealth, (size - MinSize) / (MaxSize - MinSize));
        currentHealth = startingHealth;
    }



    protected override void Update()
    {
        base.Update();
        if (GameObjectInteractor.HasGaze() || isMouseOver)
        {
            time += Time.deltaTime;
            this.currentHealth -= 100f * Time.deltaTime;
        }

        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
            Instantiate(Explosion, this.transform.position, Quaternion.identity);
        }

        this.astRenderer.color = Color.Lerp(EndColor, startingColor, currentHealth / startingHealth);
        isMouseOver = false;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            this.currentHealth = 0;
        }
        else if (coll.gameObject.tag == "Enemy")
        {
            //??
            //PROFIT
        }
    }

    protected override System.Collections.Generic.IList<IEyeXBehavior> GetEyeXBehaviorsForGameObjectInteractor()
    {
        return new List<IEyeXBehavior> { new EyeXGazeAware() };
    }

    protected override ProjectedRect GetProjectedRect()
    {
        return ProjectedRect.GetProjectedRect(this.collider2D.bounds, Camera.main);
    }

    //protected override UnityEngine.Bounds GetBounds()
    //{
    //    
    //}

    void OnMouseOver()
    {
        isMouseOver = Input.GetMouseButton(0);
    }

    protected override void OnGUI()
    {
        var face = GetProjectedRect();
        if (face.isValid && this.showProjectedBounds)
        {
            GUI.TextField(new UnityEngine.Rect(face.rect.xMin, face.rect.yMin, 30, 20), ((int)currentHealth).ToString());
        }
        base.OnGUI();
    }
}
