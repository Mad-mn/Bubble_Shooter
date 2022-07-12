using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuffParticle : MonoBehaviour
{
    private void Start()
    {
        if (!AudioController._audioController.IsOff)
        {
            GetComponent<AudioSource>().Play();
        }
        Destroy(gameObject, 2f);
    }
}
