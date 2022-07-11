using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StickmanCountTxt : MonoBehaviour
{
    private Text _txtField;
    private Animation _animation;

    private void Awake()
    {
        _txtField = GetComponent<Text>();
        _animation = GetComponent<Animation>();
    }

    public void ChangeTxt(int count)
    {
        _txtField.text = "Stickmens x" + count;
        if (MainController._mainController.IsGamePlayed)
        {
            PlayAnimation();
        }
        
    }

    public void PlayAnimation()
    {
        _animation.Play();
    }
}
