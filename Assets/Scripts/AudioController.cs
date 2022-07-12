using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public static AudioController _audioController;

    [SerializeField] private Sprite _onSound, _offSound;
    [SerializeField] private Image _soundButton;

    public bool IsOff { get; set; }

    private void Awake()
    {
        if(_audioController == null)
        {
            _audioController = this;
        }
        if (!PlayerPrefs.HasKey("audio") || PlayerPrefs.GetString("audio") == "on")
        {
            IsOff = false;
        }
        if(PlayerPrefs.GetString("audio") == "off")
        {
            IsOff = true;
        }
    }

    private void Start()
    {
        CheckImage();
    }

    public void ChangeSound()
    {
        IsOff = !IsOff;
        CheckImage();

    }

    private void CheckImage()
    {
        if (IsOff)
        {
            PlayerPrefs.SetString("audio", "off");
            _soundButton.sprite = _offSound;
        }
        else
        {
            PlayerPrefs.SetString("audio", "on");
            _soundButton.sprite = _onSound;
        }
    }
}
