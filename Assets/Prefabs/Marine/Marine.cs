using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathFind;

public class Marine : MonoBehaviour {

    public float moveSpeed;
    public float turnSpeed;

    IEnumerator walkRoutine;
    IEnumerator movingToPointRoutine;
    IEnumerator rotationRoutine;

    Point currentPosition = new Point(0,0);

	// Use this for initialization
	void Start () {
	}
	

    public void moveOrder(Point target)
    {
        clearAllRoutines();

        walkRoutine = walk(target);
        StartCoroutine(walkRoutine);
    }

    IEnumerator walk(Point target)
    {
        List<Point> path = Pathing.getPath(myPosition(), target);

        Debug.Log("Walk towards: " + target.x + " " + target.y);

        foreach(Point p in path)
        {
            //rotate towards point
            rotationRoutine = rotateTowardsPoint(p);
            StartCoroutine(rotationRoutine);
            while(rotationRoutine != null)
            {
                yield return null;
            }
                
            //moves toward point
            movingToPointRoutine = moveToPoint(p);
            StartCoroutine(movingToPointRoutine);
            while(movingToPointRoutine != null)
            {
                yield return null;
            }
        }
    }

    IEnumerator moveToPoint(Point target)
    {
        Vector2 targetPos = Pathing.pointToPosition(target);

        //move
        while (!haveReachedPoint(target))
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        movingToPointRoutine = null;
    }

    IEnumerator rotateTowardsPoint(Point target)
    {
        Vector3 targetPos = Pathing.pointToPosition(target);


        while(true)
        {
            Vector3 vectorToTarget = targetPos - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q, turnSpeed * Time.deltaTime );

            if(isLookingAtObject(q))
            {
                break;
            }

            yield return null;
        }
        

        //Debug.Log("done rotationg");
        rotationRoutine = null;
    }

    bool isLookingAtObject(Quaternion targetQuaternion)
    {
        return Mathf.Abs(transform.rotation.eulerAngles.z - targetQuaternion.eulerAngles.z) < 1 ? true : false;
    }


    bool haveReachedPoint(Point p)
    {
        Point myPos = myPosition();

        return (myPos.x == p.x && myPos.y == p.y) ? true : false;
    }

    

    Point myPosition()
    {
        currentPosition.x = Mathf.FloorToInt(transform.position.x);
        currentPosition.y = Mathf.FloorToInt(transform.position.y);

        return currentPosition;
    }
    

    void clearAllRoutines()
    {
        clearWalkRoutine();
        clearMovingToPointRoutine();
        clearRotationRoutine();
    }

    void clearWalkRoutine()
    {
        if (walkRoutine != null)
        {
            StopCoroutine(walkRoutine);
            walkRoutine = null;
        }
    }

    void clearRotationRoutine()
    {
        if(rotationRoutine != null)
        {
            StopCoroutine(rotationRoutine);
            rotationRoutine = null;
        }
    }

    void clearMovingToPointRoutine()
    {
        if (movingToPointRoutine != null)
        {
            StopCoroutine(movingToPointRoutine);
            movingToPointRoutine = null;
        }
    }
}
