using UnityEngine;
using System.Collections;

public class Touch : MonoBehaviour
{


    public Marine testMarine;

    public float singleClickTime;
    public float holdingTime;

    float clickTime = 0;

    bool holdingClick = false;



    Collider2D[] touchColliders;

    // Use this for initialization
    void Start()
    {

    }

    void Update()
    {

        clickTime += Time.deltaTime;


        //LEFT CLICK MOUSE DOWN
        if (Input.GetMouseButtonDown(0))
        {
            holdingClick = true;

            clickTime = 0;
            firstClickHandler();
        }//LEFT CLICK MOUSE UP
        else if (Input.GetMouseButtonUp(0))
        {
            //GlobalVariables.variables.UIController.createToast(clickTime + "", 1);
            holdingClick = false;
            mouseReleased();
        }//LEFT CLICK HOLDING
        else if (holdingClick)
        {

            if (clickTime > holdingTime)
            {
                foreach (Collider2D c in touchColliders)
                {
                    //long hold
                }
            }
        }


    }

    void mouseReleased()
    {

        //MOUSE IS RELEASED, CHECK TO SEE IF IT WAS LONG ENOUGH TO CALL A SINGLE CLICK, INSTEAD OF A HOLD
        if (clickTime <= singleClickTime)
        {
            //Debug.Log("Single Click");
            foreach (Collider2D c in touchColliders)
            {
              


            }

            Vector2 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Debug.Log(clickPos);
            //Debug.Log(Pathing.positionToPoint(clickPos).x);

            testMarine.moveOrder(Pathing.positionToPoint(clickPos));
        }
        else
        {
            //long click
            foreach (Collider2D c in touchColliders)
            {
                
            }
        }

    }

    //INTIAL CLICK HANDLER, THIS HAPPENS RIGHT WHEN THE CLICK IS MADE, REGARDLESS OF ANYTHING ELSE
    //CASTS A RAY AND HANDLES WHAT SHOULD BE DONE WITH IT
    public void firstClickHandler()
    {
        //touchColliders = null;

        //Vector3 mousePos = Input.mousePosition;
        //mousePos.z = 10;

        //Vector3 screenPos = Camera.main.ScreenToWorldPoint(mousePos);

        //RaycastHit2D hit = Physics2D.Raycast(screenPos, Vector2.zero);

        //Debug.Log(UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject());

        //if (hit)
        //{
        //    Debug.Log(hit.collider.name);
        //}
        int id = -1;
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            id = Input.GetTouch(0).fingerId;
        }


        if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(id))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 5f;

            Vector2 v = Camera.main.ScreenToWorldPoint(mousePosition);

            touchColliders = Physics2D.OverlapPointAll(v);



            if (touchColliders.Length > 0)
            {
                foreach (Collider2D c in touchColliders)
                {
                    //Debug.Log("col");
                    //Debug.Log("Collided with: " + c.GetComponent<Collider2D>().gameObject.name);

                    

                    //targetPos = c.collider2D.gameObject.transform.position;
                }
            }
        }
        else
        {
            touchColliders = new Collider2D[0];
        }
    }


    public bool wasSingleClick()
    {
        if (clickTime <= singleClickTime)
        {
            return true;
        }

        return false;
    }
}