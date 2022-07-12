using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo _playerInfo;

    [SerializeField] private float _stopTimerLvlStep, _bulletSpeedLvlStep, _reloadSpeedLvlvStep;

    public int Coins { get; set; }
    public float ReloadSpeed { get; set; }
    public float BulletSpeed { get; set; }
    

    public int ReloadSpeedLvl { get; set; }
    public int BulletSpeedLvl { get; set; }
    public int StopTimerCount { get; set; }

    private void Awake()
    {
        if(_playerInfo == null)
        {
            _playerInfo = this;
        }
    }

    public void UpdateValueLvl(Value type)
    {
        switch (type)
        {
            case Value.StopTimer:
                StopTimerCount++;
                break;
            case Value.BulletSpeed:
                BulletSpeedLvl++;
                break;
            case Value.ReloadSpeed:
                ReloadSpeedLvl++;
                break;
        }
        UpdateValue(type);
    }

    public void UpdateValue(Value type)
    {
        switch (type)
        {
            case Value.BulletSpeed:
                BulletSpeed += _bulletSpeedLvlStep;
                break;
            case Value.ReloadSpeed:
                ReloadSpeed += _reloadSpeedLvlvStep;
                break;
        }
    }

    public enum Value { BulletSpeed, ReloadSpeed, StopTimer}
}
