using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class Fly_Plane : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed = 10f;
    private float dirY;
    public Animator anim;
    public AudioClip flying_up;
    public AudioClip take_collectable;
    public AudioClip hit_enemy;
   
    public bool isDead = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim.GetComponent<Animator>();
      
    }

    // Update is called once per frame
    void Update()
    {
        dirY = CrossPlatformInputManager.GetAxis("Vertical") * speed;
        rb.velocity = new Vector2(0f, dirY);
        if (dirY > 0|| dirY < 0)
        {
            anim.SetTrigger("Fly_Down1");
        }
       
    }
    IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Cloud_A")
        {
            AudioSource.PlayClipAtPoint(take_collectable, Vector3.zero);
        }
        else if (collision.gameObject.tag == "Letter_Rest")
        {
            
            //anim.SetTrigger("death");
            died("wrongLetter");
            //StartCoroutine(AnimWait(1.0f));
            //died("Letter_Rest");
            //collision.gameObject.SetActive(false);
            //Debug.Log("1");
            collision.GetComponent<Collider2D>().enabled = false;
            //Debug.Log("2");
            yield return new WaitForSeconds(2f);
            //Debug.Log("3");
            collision.GetComponent<Collider2D>().enabled = true;
            //Debug.Log("4");
        }
        else if (collision.gameObject.tag == "Floor")
        {
            died("screenFall");
        }
    }
  
    private void died(string reasonOfDeath)
    {
        Debug.Log("died by:" + reasonOfDeath);
        GameControllAirplane.instance.AirplaneDied(reasonOfDeath);
        rb.velocity = Vector2.zero;
        rb.freezeRotation = false;
        //isDead = true;
        AudioSource.PlayClipAtPoint(hit_enemy, Vector3.zero);
    }
    //private IEnumerator AnimWait(float waitTime)
    //{
    //    yield return new WaitForSeconds(waitTime);
    //    died("screenFall");
        
    //}
}
