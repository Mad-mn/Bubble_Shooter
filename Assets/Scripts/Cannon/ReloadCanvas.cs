using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadCanvas : MonoBehaviour
{
    [SerializeField] private Image _fillImage;
    [SerializeField] private Gradient _gradient;

    public bool IsReady { get; set; }
    public float ReloadImageStep { get; set; }

    private void OnEnable()
    {
        _fillImage.fillAmount = 0;
        IsReady = true;
        ReloadImageStep = PlayerInfo._playerInfo.ReloadSpeed;
    }

    public void Reload()
    {
        _fillImage.fillAmount = 0;
        IsReady = false;
        StartCoroutine(FillingImage());
    }

    private IEnumerator FillingImage()
    {
        while(_fillImage.fillAmount < 1)
        {
            _fillImage.fillAmount += ReloadImageStep;
            _fillImage.color = _gradient.Evaluate(_fillImage.fillAmount);
            yield return new WaitForFixedUpdate();
        }

        IsReady = true;
    }

   
}
