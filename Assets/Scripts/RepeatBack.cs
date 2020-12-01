using UnityEngine;
using System.Collections;

public class RepeatBack : MonoBehaviour 
{
	
	public GameControllAirplane gameControll;
	//public GameControl gameControll;
	private BoxCollider2D groundCollider;		
	private float groundHorizontalLength;		

	private void Awake ()
	{
		groundCollider = GetComponent<BoxCollider2D> ();
		groundHorizontalLength = groundCollider.size.x;
	}

	private void Update()
	{
		if (gameControll.start == true && transform.position.x < -groundHorizontalLength)
		{
			RepositionBackground ();
		}
	}

	private void RepositionBackground()
	{
		
		Vector2 groundOffSet = new Vector2(groundHorizontalLength * 3f, 0);
		transform.position = (Vector2) transform.position + groundOffSet;
	}
}