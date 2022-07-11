using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainController : MonoBehaviour
{
    public static MainController _mainController;

    [SerializeField] private float _startBulletSpeed;
    [SerializeField] private float _startStickmanSpeed;

    public float BulletSpeed { get; set; }
    public bool IsGamePlayed { get; set; }
    public float StickmanSpeed { get; set; }

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
    }

    public void StartGame()
    {
        
        IsGamePlayed = true;
        OnStartGame.Invoke();
    }

    public void EndGame()
    {
        Debug.Log(0);
        IsGamePlayed = false;
        OnEndGame.Invoke();
    }
}
