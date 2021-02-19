using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Collectable_Letter : MonoBehaviour
{
    static public Collectable_Letter instance { get { return i_Instance; } }
    static protected Collectable_Letter i_Instance;
    GameControllAirplane GCA;
    public TMP_Text random_let;
    
    // Start is called before the first frame update
    private void Awake()
    {
        i_Instance = this;
    }
    void Start()
    {
        //if (oneLet == true)
        //{
        //    Lett = Alphabet[Random.Range(0, Alphabet.Length)].ToString();
        //    Letter.text = Lett.ToString();
        //    Debug.Log("collectable" + Lett);
        //    oneLet = false;
        //}
    
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
