using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollObj : MonoBehaviour 
{
	private Rigidbody2D rb2d;
	public GameControllAirplane gameControll;
	void Start () 
	{
		rb2d = GetComponent<Rigidbody2D>();
		rb2d.velocity = Vector2.zero;
	}

	void Update()
	{
        if (gameControll.scroling == true)
        {
			rb2d.velocity = new Vector2(gameControll.scrollSpeed, 0);
		}
		if (gameControll.gameOver == true)
		{
			rb2d.velocity = Vector2.zero;
		}

	}
}
