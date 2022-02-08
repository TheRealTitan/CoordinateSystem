using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionText : MonoBehaviour
{
    private GameObject textBox;
    private Transform prevTrans;

    // Start is called before the first frame update
    void Start()
    {
        prevTrans = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(prevTrans == null || this.transform.position != prevTrans.position)
		{
            
		}
    }
}
