using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static LevelController _levelController;

    [SerializeField] private int _levelLineCountStep;
    [SerializeField] private int _startLineCount;
    [SerializeField] private int _maxLineCount;

    [SerializeField] private float _startMaterialStep;
    [SerializeField] private int _materialLevelStep;

    private int _levelLineCount;
    private float _stickmanMaterialStep;

    public int LevelId { get; set; }

    private void Awake()
    {
        if(_levelController == null)
        {
            _levelController = this;
        }

        LevelId = 1;
    }
    private void Start()
    {
        MainController.OnEndGame.AddListener(IncrementLevelId);
    }

    public int GetStickmanLineCount()
    {
        CheckLevelLineCount();
        return _levelLineCount;
    }

    private void IncrementLevelId()
    {
        LevelId++;
        CanvasController._canvasController.ChangeLevelId();
    }

    private void CheckLevelLineCount()
    {
        _levelLineCount = _startLineCount + (LevelId / _levelLineCountStep);
        
        _levelLineCount = _levelLineCount > _maxLineCount ? _maxLineCount : _levelLineCount;
    }

    public float GetMaterialStep()
    {
        CheckMaterialStep();
        return _stickmanMaterialStep;
    }

    public void CheckMaterialStep()
    {
        _stickmanMaterialStep = _startMaterialStep + (LevelId / _materialLevelStep);
    }
}
