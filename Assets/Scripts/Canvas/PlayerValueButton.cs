using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerValueButton : MonoBehaviour
{
    [SerializeField] private PlayerInfo.Value _value;
    [SerializeField] private Text _lvlTxt, _priceTxt;
    [SerializeField] private int _price;

    private PlayerInfo _playerInfo;
    

    private void Start()
    {
        _playerInfo = PlayerInfo._playerInfo;
        Invoke("SetValue", 0.01f);

        _priceTxt.text = _price.ToString();
    }
    public void  SetValue()
    {
        switch (_value)
        {
            case PlayerInfo.Value.BulletSpeed:
                _lvlTxt.text = _playerInfo.BulletSpeedLvl.ToString();
                break;
            case PlayerInfo.Value.ReloadSpeed:
                _lvlTxt.text = _playerInfo.ReloadSpeedLvl.ToString();
                break;
            case PlayerInfo.Value.StopTimer:
                _lvlTxt.text = _playerInfo.StopTimerCount.ToString();
                break;
        }
    }

    public void OnTap()
    {
        if(_playerInfo.Coins >= _price)
        {
            _playerInfo.Coins -= _price;
            _playerInfo.UpdateValueLvl(_value);
            SetValue();
            CanvasController._canvasController.ChangeCointTxt();
        }

        CanvasController._canvasController.PlaySound();
    }
}
