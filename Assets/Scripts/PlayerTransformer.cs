using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using org.mariuszgromada.math.mxparser;
using System;

public class PlayerTransformer : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		
		
	}

	public void SetPosition()
	{
		string yString = "";

		try
		{
			Argument x = new Argument("x = 0");
			Expression expression = new Expression(GameScript.GetExpression(), x);
			yString = expression.calculate().ToString();
			Debug.Log(yString);

		}
		catch (Exception e)
		{
			Debug.Log(e);
		}

		if (yString != "NaN")
		{
			this.transform.position = new Vector2(0, float.Parse(yString));
		}
		else
		{
			this.transform.position = new Vector2(0, 0);
		}
	}
}
