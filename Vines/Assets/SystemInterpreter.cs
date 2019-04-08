using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemInterpreter : MonoBehaviour {

    public GameObject vinePiece;
    public GameObject redPoint;


    public Lsystem lSystem;

    Vector3 genPoint = new Vector3();
    Vector3 direction = Vector3.up
        ;


    Vector3 savedGenPoint = new Vector3();
    Vector3 savedDirection = new Vector3();

    List<GameObject> components = new List<GameObject>();



    
    void Start () {


        Debug.Log("GOOSE");
        for (int rep = 0; rep < 20; rep++)
        {
            Collider[] colliders = Physics.OverlapSphere(genPoint, 1);

            genPoint = colliders[0].ClosestPoint(genPoint); ;

            Instantiate(redPoint, genPoint, Quaternion.identity);

            genPoint += direction;
            

        }
        



    }
    Vector3 anchorPoint;
    
    




    Vector3 getClosestAnchorPoint(Vector3 pointToLookFrom)
    {
        Collider[] colliders = Physics.OverlapSphere(pointToLookFrom, 1);

        float mindist = float.MaxValue;
        Vector3 postionToAnchorTo = new Vector3(10, 10, 10);
        foreach (Collider collider in colliders)
        {


            float dist = Vector3.Magnitude((pointToLookFrom) - collider.ClosestPoint(pointToLookFrom));

            
            if (dist < mindist)
            {
                mindist = dist;
                postionToAnchorTo = collider.ClosestPoint(pointToLookFrom);

                Debug.Log(mindist + " " + collider.name);
            }
        }

        
        return postionToAnchorTo;
    }
    


    public void GenerateSystem()
    {
        foreach (GameObject component in components)
        {
            Destroy(component);
        }
        genPoint = new Vector3();
        direction = Vector3.up;

        string sentence = lSystem.sentence;

        for (int i = 0; i < sentence.Length; i++)
        {

            if(sentence[i]== 'F')
            {
               GameObject piece = Instantiate(vinePiece, genPoint, Quaternion.identity);
               piece.transform.LookAt(genPoint+ direction);
               genPoint += direction;

                components.Add(piece);
            }
            else if (sentence[i] == '+')
            {
                direction = rotateVectorAroundZaxis(direction, 25);
            }
            else if (sentence[i] == '-')
            {
                direction = rotateVectorAroundZaxis(direction, -25);
            }
            else if (sentence[i] == '[')
            {
                savedDirection = direction;
                savedGenPoint = genPoint;
            }
            else if (sentence[i] == ']')
            {
                direction = savedDirection;
                genPoint = savedGenPoint;
            }

        }
    }

    Vector3 rotateVectorAroundYaxis(Vector3 vec,float degrees)
    {
        float theta = Mathf.Deg2Rad * degrees;
        Vector3 newVector = new Vector3();

        newVector.x = vec.x * Mathf.Cos(theta) + vec.z * Mathf.Sin(theta);
        newVector.y = vec.y;
        newVector.z = -vec.x * Mathf.Sin(theta) + vec.z * Mathf.Cos(theta);

        return newVector;
    }

    Vector3 rotateVectorAroundZaxis(Vector3 vec, float degrees)
    {
        float theta = Mathf.Deg2Rad * degrees;
        Vector3 newVector = new Vector3();

        newVector.x = vec.x * Mathf.Cos(theta) - vec.y * Mathf.Sin(theta);
        newVector.y = vec.x * Mathf.Sin(theta) + vec.y * Mathf.Cos(theta);
        newVector.z = vec.z;

        return newVector;
    }


}
