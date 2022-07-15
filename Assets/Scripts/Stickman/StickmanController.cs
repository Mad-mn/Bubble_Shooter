using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StickmanController : MonoBehaviour
{
    public static StickmanController _stickmanController;

    public static UnityEvent OnMoveStickmans = new UnityEvent();
    public static UnityEvent OnDestroyStickman = new UnityEvent();
    public static UnityEvent OnKillstickman = new UnityEvent();

    [SerializeField] private GameObject _stickmanPrefab;
    [SerializeField] private Material _red, _green, _yellow;


    public int FirstLineCount { get; set; }
   
    public int MainLineCount { get; set; }

    private int _allStickmanInLevel { get; set; }


    private void Awake()
    {
        if(_stickmanController == null)
        {
            _stickmanController = this;
        }
    }

    public void MoveAllStickmans()
    {
        if (_allStickmanInLevel > 0)
        {
            Invoke("MoveAll", 0.2f);
        }
    }

    private void MoveAll()
    {
        OnMoveStickmans.Invoke();
    }
        
    public void ResetStickmanLineInfo()
    {
        OnDestroyStickman.Invoke();
    }

    public void KillStickman()
    {
        Invoke("Kill", 0.02f);
    }

    private void Kill()
    {
        OnKillstickman.Invoke();
    }
    public void IncrementStickmanCount()
    {
        _allStickmanInLevel++;
    }

    public void DectrementStickmanCount()
    {
        _allStickmanInLevel--;
        if(_allStickmanInLevel == 0)
        {
            MainController._mainController.EndGame();
        }
    }

    public void CreateNewStickman(Vector3 bulletPosition, Stickman.StickmanColor colorType, List<Transform> _aroundTransforms)
    {
        float one = Vector3.Distance(bulletPosition, _aroundTransforms[1].position);
        float two = Vector3.Distance(bulletPosition, _aroundTransforms[2].position);
        float three = Vector3.Distance(bulletPosition, _aroundTransforms[3].position);
        float four = Vector3.Distance(bulletPosition, _aroundTransforms[4].position);
        float five = Vector3.Distance(bulletPosition, _aroundTransforms[0].position);
        float six = Vector3.Distance(bulletPosition, _aroundTransforms[5].position);

        float min = Mathf.Min(one, two, three, four, five, six);

        GameObject stickman = new GameObject();
        foreach (Transform t in _aroundTransforms)
        {

            if (Vector3.Distance(bulletPosition, t.position) == min)
            {
                stickman = Instantiate(_stickmanPrefab, t.position, Quaternion.identity);
                break;
            }
        }

        Stickman s = stickman.GetComponent<Stickman>();
        
        //s.Color = colorType;
        switch (colorType)
        {
            case Stickman.StickmanColor.Red:
                s.SetMaterial(_red, colorType);
                break;
            case Stickman.StickmanColor.Green:
                s.SetMaterial(_green, colorType);
                break;
            case Stickman.StickmanColor.Yellow:
                s.SetMaterial(_yellow, colorType);
                break;
        }
        s.EnableParticle();
        
    }

    private void CheckConnectionStickmans()
    {

    }
}
