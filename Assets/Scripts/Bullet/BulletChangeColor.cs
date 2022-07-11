using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletChangeColor : MonoBehaviour
{
    [SerializeField] private Material _red, _green, _yellow;

    private Material _nextBulletMaterial;
    private Stickman.StickmanColor _nextBulletColorType;
    private Renderer _renderer;

    public Stickman.StickmanColor ColorType { get; set; }
    public Material Material { get; set; }

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void Start()
    {
        SetNextBulletColor();
    }
    public void SetNextBulletColor()
    {
        int rnd = Random.Range(0, 100);
        if (rnd < 34)
        {
            _nextBulletMaterial = _red;
            _nextBulletColorType = Stickman.StickmanColor.Red;
        }
        else if (rnd > 33 && rnd < 66)
        {
            _nextBulletMaterial = _green;
            _nextBulletColorType = Stickman.StickmanColor.Green;
        }
        else
        {
            _nextBulletMaterial = _yellow;
            _nextBulletColorType = Stickman.StickmanColor.Yellow;
        }
        _renderer.material = _nextBulletMaterial;
    }

    
    public Stickman.StickmanColor GetNextBulletColorType()
    {
        return _nextBulletColorType;
    }

    public Material GetNextBulletMaterial()
    {
        return _nextBulletMaterial;
    }

    private void OnMouseDown()
    {
        SwitchColors();
    }

    private void SwitchColors()
    {
        Material temp = _nextBulletMaterial;
        Stickman.StickmanColor tempType = _nextBulletColorType;

        _nextBulletMaterial = CannonController._cannonController._nextBulletMaterial;
        _nextBulletColorType = CannonController._cannonController._nextBulletColorType;

        CannonController._cannonController._nextBulletMaterial = temp;
        CannonController._cannonController._nextBulletColorType = tempType;

        _renderer.material = _nextBulletMaterial;
        CannonController._cannonController.ChangeBodyColor();
    }
}
