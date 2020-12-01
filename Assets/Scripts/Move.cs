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
        DontDestroyOnLoad(this.gameObject);
        beforeanim = false;
    }
    void Update()
    {
       transform.position += Vector3.left * speed * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Balloon")
        {
            if (gameObject.tag == "Cloud_A")
            {
                anim.SetTrigger("Cloud_A");
                Destroy(this.gameObject, 0.5f);
                GameControllAirplane.instance.DragonScored();
               
            }
            else if(gameObject.tag == "Letter_Rest")
            {
                anim.SetTrigger("Letter_Rest");
            }  
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