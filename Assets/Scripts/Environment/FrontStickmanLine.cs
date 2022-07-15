using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FrontStickmanLine : MonoBehaviour
{
    public static UnityEvent OnStickmanTouchLine = new UnityEvent();
    

   

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!other.GetComponent<Stickman>().IsFirstLine)
            {
                StickmanController._stickmanController.FirstLineCount++;
                other.GetComponent<Stickman>().IsFirstLine = true;
                EnterOnTrigger();
            }
        }
    }

    

    private void EnterOnTrigger()
    {
        OnStickmanTouchLine.Invoke();
    }
}
