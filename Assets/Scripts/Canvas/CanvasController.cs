using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public static CanvasController _canvasController;

    [SerializeField] private GameObject _endGamePanel;

    private AudioSource _audio;

    private void Awake()
    {
        if(_canvasController == null)
        {
            _canvasController = this;
        }
        
    }

    private void Start()
    {
        MainController.OnEndGame.AddListener(EndGame);

        _audio = GetComponent<AudioSource>();

       
    }

    public void PlaySound()
    {
        if (!AudioController._audioController.IsOff)
        {
            _audio.Play();
        }
    }

    public void EndGame()
    {
        _endGamePanel.SetActive(true);
    }

   

    
}
