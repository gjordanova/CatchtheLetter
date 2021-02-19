using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CL : MonoBehaviour
{
    public TMP_Text Letter;
    string[] Alphabet = new string[26] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
    string Lett;
    
    // Start is called before the first frame update
    void Start()
    {
        Lett = Alphabet[Random.Range(0, Alphabet.Length)].ToString();
        Letter.text = Lett.ToString();
        Debug.Log("collectable" + Lett);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
