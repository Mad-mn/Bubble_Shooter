using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerValueButton : MonoBehaviour
{
    [SerializeField] private PlayerInfo.Value _value;
    [SerializeField] private Text _lvlTxt;

    private PlayerInfo _playerInfo;
    [SerializeField] private int _price;

    private void Start()
    {
        _playerInfo = PlayerInfo._playerInfo;
        Invoke("SetValue", 0.01f);
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
                _lvlTxt.text = _playerInfo.StopTimerLvl.ToString();
                break;
        }
    }

    public void OnTap()
    {
        if(_playerInfo.Coins >= _price)
        {
            _playerInfo.UpdateValueLvl(_value);
            SetValue();
        }
    }
}
