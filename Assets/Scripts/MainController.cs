using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainController : MonoBehaviour
{
    public static MainController _mainController;

    [SerializeField] private float _startBulletSpeed;
    [SerializeField] private float _startStickmanSpeed;
    [SerializeField] private float _stopTimerTime;

    public float BulletSpeed { get; set; }
    public bool IsGamePlayed { get; set; }
    public float StickmanSpeed { get; set; }
    public float StopTimerTime { get; private set; }

    public static UnityEvent OnStartGame = new UnityEvent();
    public static UnityEvent OnEndGame = new UnityEvent();

    private void Awake()
    {
        if (_mainController == null)
        {
            _mainController = this;
        }
    }

    private void Start()
    {
        BulletSpeed = _startBulletSpeed;
       
        StickmanSpeed = _startStickmanSpeed;
        StopTimerTime = _stopTimerTime;
    }

    public void StartGame()
    {
        
        IsGamePlayed = true;
        OnStartGame.Invoke();
    }

    public void EndGame()
    {
       
        IsGamePlayed = false;
        OnEndGame.Invoke();
    }
}
