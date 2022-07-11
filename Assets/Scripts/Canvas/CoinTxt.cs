using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinTxt : MonoBehaviour
{
    [SerializeField] private Text _txtField;
    [SerializeField] private Animation  _animationTxt;

    private void Start()
    {
        _txtField.text = PlayerInfo._playerInfo.Coins.ToString();
    }

    public void ChangeTxt(int count)
    {
        _txtField.text = count.ToString();
        PlayAnimation();
    }

    public void PlayAnimation()
    {
        if (_animationTxt.isPlaying)
        {
            _animationTxt.Stop();
        }
        _animationTxt.Play();
    }
}
