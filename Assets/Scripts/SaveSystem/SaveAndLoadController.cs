using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAndLoadController : MonoBehaviour
{
    private ISaveSystem _saveSystem;

    private SaveData _myData;
    private PlayerInfo _playerInfo;

    private void Start()
    {
        _saveSystem = new BinarySaveSystem();

        _playerInfo = PlayerInfo._playerInfo;

        if (PlayerPrefs.HasKey("notFirst"))
        {
            LoadGame();
        }
        else
        {
            SetDefaultGameValues();

            PlayerPrefs.SetString("notFirst", "0");
        }
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    public void SetDefaultGameValues()
    {
        _playerInfo.Coins = 0;
        _playerInfo.BulletSpeed = 4;
        _playerInfo.ReloadSpeed = 0.01f;
        _playerInfo.StopTimer = 1f;

        _playerInfo.StopTimerLvl = 1;
        _playerInfo.ReloadSpeedLvl = 1;
        _playerInfo.BulletSpeedLvl = 1;
        LevelController._levelController.LevelId = 1;
    }

    public void SaveGame()
    {
        _myData = new SaveData();

        _myData.Coins = _playerInfo.Coins;
        _myData.BulletLvl = _playerInfo.BulletSpeedLvl;
        _myData.BulletSpeed = _playerInfo.BulletSpeed;
        _myData.ReloadLvl = _playerInfo.ReloadSpeedLvl;
        _myData.ReloadSpeed = _playerInfo.ReloadSpeed;
        _myData.StopTimer = _playerInfo.StopTimer;
        _myData.TimerLvl = _playerInfo.StopTimerLvl;

        _myData.LevelId = LevelController._levelController.LevelId;


        _saveSystem.Save(_myData);
    }

    public void LoadGame()
    {
        _myData = _saveSystem.Load();

        LevelController._levelController.LevelId = _myData.LevelId;

        _playerInfo.Coins = _myData.Coins;

        _playerInfo.BulletSpeedLvl = _myData.BulletLvl;
        _playerInfo.BulletSpeed = _myData.BulletSpeed;
        _playerInfo.ReloadSpeedLvl = _myData.ReloadLvl;
        _playerInfo.ReloadSpeed = _myData.ReloadSpeed;
        _playerInfo.StopTimerLvl = _myData.TimerLvl;
        _playerInfo.StopTimer = _myData.StopTimer;
    }
}
