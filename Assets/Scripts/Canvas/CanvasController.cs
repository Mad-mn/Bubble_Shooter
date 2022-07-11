using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public static CanvasController _canvasController;

    [SerializeField] private GameObject _mainPanel, _gamePanel, _mainMenuPanel;
    [SerializeField] private StickmanCountTxt _stickmanCountTxt;
    [SerializeField] private GameObject _reloadCanvas;
    [SerializeField] private GameObject _endTxt;
    [SerializeField] private float _takeCoinSpeed;
    [SerializeField] private CoinTxt _coinTxt;
    [SerializeField] private Text _levelTxt;

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

        ChangeLevelId();
    }

    public void StartGame()
    {
        _mainMenuPanel.SetActive(false);
        _gamePanel.SetActive(true);
        MainController._mainController.StartGame();
        _reloadCanvas.SetActive(true);
    }

    public void ChangeStickmanCountTxt(int count)
    {
        if (MainController._mainController.IsGamePlayed)
        {
            _stickmanCountTxt.ChangeTxt(count);
        }
    }

    public void ChangeLevelId()
    {
        _levelTxt.text = "Level " + LevelController._levelController.LevelId;
    }

    public void EndGame()
    {
        _endTxt.SetActive(true);
        StartCoroutine(TakeCoin());
    }

    public IEnumerator TakeCoin()
    {
        yield return new WaitForSeconds(1);
        int coins = StickmanController._stickmanController.StickmanDestroyCount;
        if (coins > 0)
        {
            for (int i = coins; i >= 0; i--)
            {
                StickmanController._stickmanController.DectrementStickmanCount();
                PlayerInfo._playerInfo.Coins++;
                _coinTxt.ChangeTxt(PlayerInfo._playerInfo.Coins);

                _stickmanCountTxt.ChangeTxt(StickmanController._stickmanController.StickmanDestroyCount--);

                yield return new WaitForSeconds(_takeCoinSpeed);
            }
        }

        _gamePanel.SetActive(false);
        _mainMenuPanel.SetActive(true);
    }
}
