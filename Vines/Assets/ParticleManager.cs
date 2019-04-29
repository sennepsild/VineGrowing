using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    ParticleSystem particleSys;
    List<ParticleCollisionEvent> collisionEvents;

    List<Vector3> potentialPositions;
    List<Vector3> theNormals;


    Vector3 currentPos;
    Vector3 AnchorPoint;
    Vector3 AnchorNormal;

    bool newAnchorFound = false;

    bool add = true;

    public GameObject prefab;
    public GameObject VinePiece;

    // Start is called before the first frame update
    void Start()
    {
        particleSys = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
        potentialPositions = new List<Vector3>();
        theNormals = new List<Vector3>();

        currentPos = transform.position;
       

        


    }

    private void OnParticleCollision(GameObject other)
    {
        ParticlePhysicsExtensions.GetCollisionEvents(particleSys, other, collisionEvents);
        for (int i = 0; i < collisionEvents.Count; i++)
        {
            potentialPositions.Add(collisionEvents[i].intersection);
            theNormals.Add(collisionEvents[i].normal);

           // Instantiate(prefab, collisionEvents[i].intersection,Quaternion.identity);
        }

        
    }

    private void Update()
    {
        if (add)
        {
            StartCoroutine( AddBranch());
            add = false;
        }

        if (newAnchorFound){
            GameObject piece = Instantiate(VinePiece, currentPos, Quaternion.identity);
            piece.transform.LookAt( AnchorPoint);

            piece.transform.localScale = new Vector3(0.3f,0.3f, Vector3.Distance(AnchorPoint, currentPos));
            currentPos = AnchorPoint;
            transform.position = currentPos+AnchorNormal;
            newAnchorFound = false;
            add = true;
        }
        
    }


   IEnumerator AddBranch()
    {
        particleSys.Emit(30);
        yield return new WaitForSeconds(2);

        float[] scores= new float[potentialPositions.Count];
        for (int i = 0; i < potentialPositions.Count; i++)
        {
            float score = 0;
            score += potentialPositions[i].y - currentPos.y;
            score*= Vector3.Distance(potentialPositions[i], currentPos);
            scores[i] = score;
        }

        float[] highScores = { float.MinValue,float.MinValue,float.MinValue };
        int[] scoreNumber = { 0, 0, 0 };
        
        for (int i = 0; i < scores.Length; i++)
        {
            if (scores[i] > highScores[0])
            {
                highScores[0] = scores[i];
                scoreNumber[0] = i;
            }
            else if(scores[i] > highScores[1])
            {
                highScores[1] = scores[i];
                scoreNumber[1] = i;
            }
            else if (scores[i] > highScores[2])
            {
                highScores[2] = scores[i];
                scoreNumber[2] = i;
            }
        }


        int chosenInt = Mathf.RoundToInt(Random.Range(0, 2));
        AnchorPoint = potentialPositions[scoreNumber[chosenInt]];
        AnchorNormal = theNormals[scoreNumber[chosenInt]];
        
        potentialPositions = new List<Vector3>();
        theNormals = new List<Vector3>();
        newAnchorFound = true;

        yield return false;
    }


    
}
