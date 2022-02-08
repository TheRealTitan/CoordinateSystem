using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGrid : MonoBehaviour
{
    public int SizeOfGrid = 0;
    public GameObject lineObject;

    // Start is called before the first frame update
    void Start()
    {
		if (SizeOfGrid <= 0)
		{
            return;
		}

        for (int i = 0; i < SizeOfGrid; i++)
		{
            GameObject hL = Instantiate(lineObject, new Vector3(0, i + 1, 0), new Quaternion(0, 0, 0, 0));
            hL.transform.parent = this.transform;

            GameObject vL = Instantiate(lineObject, new Vector3(i + 1, 0, 0), Quaternion.Euler(0, 0, 90));
            vL.transform.parent = this.transform;

            //Negative lines
            int negativeI = -Mathf.Abs(i);
            GameObject hLN = Instantiate(lineObject, new Vector3(0, negativeI - 1, 0), new Quaternion(0, 0, 0, 0));
            hLN.transform.parent = this.transform;

            GameObject vLN = Instantiate(lineObject, new Vector3(negativeI - 1, 0, 0), Quaternion.Euler(0, 0, 90));
            vLN.transform.parent = this.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
