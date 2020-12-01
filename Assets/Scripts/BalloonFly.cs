using System.Collections;
using System.Collections.Generic;
using AD.Services;
using AD.Services.Tests;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;


public class BalloonFly : MonoBehaviour
{
    public static BalloonFly Instance { get { return i_Instance; } }
    public static BalloonFly i_Instance;
    public float velosity = 30f;
    [HideInInspector]
    public Rigidbody2D rb;
    private Animator anim;
    public AudioClip flying_up;
    public AudioClip take_collectable;
    public AudioClip hit_enemy;
    public bool isDead = false;
    private bool isTouched = false;
    Vector2 dist;
    private void Awake()
    {
         
    i_Instance = this;

    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {

        if (isTouched == false && AdService.Instance.AdShowing == true)
        {
            transform.GetComponent<Rigidbody2D>().gravityScale = 0;
        }

        if (Input.GetMouseButtonDown(0) /*&& GameControl.instance.start == true*/)
        {
            isTouched = true;
            isDead = false;
            transform.GetComponent<Rigidbody2D>().gravityScale = 5.0f;
            AudioSource.PlayClipAtPoint(flying_up, Vector3.zero);
            rb.velocity = Vector2.up * velosity;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Dragon")
        {
            died("dragonHit");

        }
        else if (collision.gameObject.tag == "Floor")
        {
            died("screenFall");
        }
        else
        {
            GameControl.instance.Player_Scored();
            AudioSource.PlayClipAtPoint(take_collectable, Vector3.zero);
            collision.gameObject.SetActive(false);
        }

    }
    private void died(string reasonOfDeath)
    {
        Debug.Log("died by:" + reasonOfDeath);
        GameControl.instance.BalloonDied(reasonOfDeath);
        rb.velocity = Vector2.zero;
        rb.freezeRotation = false;
        isDead = true;
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(hit_enemy, Vector3.zero);
        //gameObject.GetComponent<Animator>().SetTrigger("Death");
    }

}
