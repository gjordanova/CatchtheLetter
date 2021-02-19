using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class RandomLetter : MonoBehaviour
{
    public TMP_Text Letter;
    string Lett;
    string[] Alphabet = new string[26] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
    void Start()
    {
        Lett = Alphabet[Random.Range(0, Alphabet.Length)].ToString();
        Letter.text = Lett.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
