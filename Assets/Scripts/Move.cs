using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Move : MonoBehaviour
{
    static public Move instance { get { return i_Instance; } }
    static protected Move i_Instance;
    public float speed;
    public Animator anim;
    public bool beforeanim = false;
    private void Start()
    {
        anim.GetComponent<Animator>();
       
        beforeanim = false;
    }
    void Update()
    {
       transform.position += Vector3.left * speed * Time.deltaTime;
    }
    IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Plane")
        {
            if (gameObject.tag == "Cloud_A")
            {
                anim.SetTrigger("Cloud_A");
                Destroy(this.gameObject, 0.5f);
                GameControllAirplane.instance.AirplaneScored();
            }
            else if(gameObject.tag == "Letter_Rest")
            {
                anim.SetTrigger("Letter_Rest");
               
            }
         
            if (GameControllAirplane.instance.continue_game == true)
            {
                gameObject.GetComponent<Collider2D>().isTrigger = false;
                yield return new WaitForSeconds(2);
                gameObject.GetComponent<Collider2D>().isTrigger = true;
            }
        }
       else if (collision.gameObject.tag == "Zid")
        {
            Debug.Log("zid");
            Destroy(this.gameObject);
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Zid")
        {
            if (gameObject.tag == "Cloud_A")
            {
                Destroy(this.gameObject, 0.5f);
                Debug.Log("zid");
            }
            Debug.Log("zid");
    
        }
    }
    private IEnumerator AnimWait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        beforeanim = true;
    }
    private IEnumerator AnimWait1(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
    }
}