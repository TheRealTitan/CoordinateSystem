using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartialExpressionScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

	private void Awake()
	{
        
    }

	// Update is called once per frame
	void Update()
    {
		
        InputField input = gameObject.GetComponent<InputField>();

        if (GameScript.currentPartialExpression.Length > 0)
        {
            input.text = GameScript.currentPartialExpression;
        }
		else
		{
            input.text = " ";
        }
    }
}
