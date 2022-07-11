using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchChecker : MonoBehaviour
{
    public void OnTap()
    {
        if (!MainController._mainController.IsGamePlayed)
        {
            CanvasController._canvasController.StartGame();
        }
        CannonController._cannonController.RotateCannon();
    }

    public void OnPointerUp()
    {
        CannonController._cannonController.StopRotate();
    }
}
