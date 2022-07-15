using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchChecker : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{


    //public void OnTap()
    //{
    //    if (!MainController._mainController.IsGamePlayed)
    //    {
    //        CanvasController._canvasController.StartGame();
    //    }
    //    CannonController._cannonController.RotateCannon();
    //}

    //public void OnPointerUp()
    //{
    //    CannonController._cannonController.StopRotate();
    //}

    public void OnPointerDown(PointerEventData eventData)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            
            if (hit.transform.gameObject.CompareTag("TouchChecker"))
            {
                CannonController._cannonController.RotateCannon();
            }
        }
       
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {

            if (hit.transform.gameObject.CompareTag("TouchChecker"))
            {
                CannonController._cannonController.StopRotate();
            }
        }
       
    }
}
