using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{

    public int life = 3;
    public float timer;
    public bool timeStarted = false;
    public bool stuck = false;
    // Start is called before the first frame update
    void Start()
    {
        timeStarted = true;
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeStarted == true)
        {
            timer += Time.deltaTime;
        }
        if (stuck)
        {
            Time.timeScale = 0.5f;
            Time.fixedDeltaTime = 0.01f;
        }
        else {
            Time.timeScale = 1f;
            Time.fixedDeltaTime = 0.02f;
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Projectile")
        {
            ParticleSystem ps = gameObject.GetComponent<ParticleSystem>();
            ps.Play();
            GameObject.Destroy(col.gameObject);
            life -= 1;
            if (life <= 0)
            {
                timeStarted = false;
                gameObject.GetComponent<Move>().enabled = false;
                gameObject.GetComponent<Renderer>().material.color = Color.red; //SetColor("_Color", Color.red);
                Time.timeScale = 0.5f;
                Time.fixedDeltaTime = 0.01f;

            }
        }
        else if(col.gameObject.tag == "StickyProjectile"){
            stuck = true;
        }
        
    }

    void OnGUI()
    {
        int minutes = Mathf.FloorToInt(timer / 60F);
        int seconds = Mathf.FloorToInt(timer - minutes * 60);
        float miliseconds = timer - minutes - seconds;
        string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);

        GUI.skin.label.fontSize = 50;

        GUI.skin.button.fontSize = 40;

        GUI.Label(new Rect(10, 10, 1000, 250), niceTime);

        float buttonw = Screen.width / 3;
        float buttonh = buttonw / 3;

        if (GUI.Button(new Rect((Screen.width / 2) - (buttonw/2), 10, buttonw, buttonh), "Restart")){
            SceneManager.LoadScene("SampleScene");
        }

    }
}
