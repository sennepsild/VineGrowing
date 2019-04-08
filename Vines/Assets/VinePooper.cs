using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VinePooper : MonoBehaviour {

    public GameObject vineToPoop;
    Vector3 pos, lastPos;


	// Use this for initialization
	void Start () {

        lastPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        poopVine();
        transform.Translate(Vector3.up * Time.deltaTime);
		
	}


    void poopVine()
    {
        pos = transform.position;

        if (Vector3.Distance(pos, lastPos) > 0.9f)
        {
            GameObject piece = Instantiate(vineToPoop, lastPos, Quaternion.identity);
            piece.transform.LookAt(pos);
            lastPos = transform.position;
        }
    }
}
