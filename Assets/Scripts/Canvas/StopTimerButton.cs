using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StopTimerButton : MonoBehaviour
{
    public static UnityEvent OnStopStickman = new UnityEvent();
    public static UnityEvent OnGoStickman = new UnityEvent();

    public static bool isStop { get; set; }

    [SerializeField] private Image _backgroundImage;

    private float _time;

    private bool IsUsedInThisGame;

    

    private void Start()
    {
        _time = MainController._mainController.StopTimerTime;
        MainController.OnEndGame.AddListener(ResetBool);

        _backgroundImage.fillAmount = 1;
    }

    public void OnTap()
    {
        CanvasController._canvasController.PlaySound();
        if (PlayerInfo._playerInfo.StopTimerCount > 0 && MainController._mainController.IsGamePlayed && !IsUsedInThisGame)
        {
            PlayerInfo._playerInfo.StopTimerCount--;
            IsUsedInThisGame = true;
            StartCoroutine(StopStickman());
        }
        
    }

    private IEnumerator StopStickman()
    {
        isStop = true;   
        OnStopStickman.Invoke();
        float time = _time;
        while (time > 0)
        {
            yield return new WaitForFixedUpdate();
            time -= 0.02f;
            _backgroundImage.fillAmount -= (0.02f / _time);
        }
        OnGoStickman.Invoke();
        isStop = false;
    }

    private void ResetBool()
    {
        IsUsedInThisGame = false;
    }

}
