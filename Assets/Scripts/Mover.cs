using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using org.mariuszgromada.math.mxparser;
using System;

public class Mover : MonoBehaviour
{
    public string stringFunction = string.Empty;
    public float x = 0.0f;
    public float xInc = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Argument xArg = new Argument("x = " + x);
        Expression exp = new Expression(stringFunction, xArg);
        double yDouble = 0;

        try
        {
            yDouble = exp.calculate();
        }
        catch(Exception e)
		{
            Debug.Log(e);
            throw e;
		}

        float y = float.Parse(yDouble.ToString());

        this.transform.position = new Vector3(x, y);
        TrailRenderer renderer = gameObject.GetComponent<TrailRenderer>();

		if (renderer.enabled == false)
		{
            renderer.enabled = true;
        }

        x += xInc;
    }
}
