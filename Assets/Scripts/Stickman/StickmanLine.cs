using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickmanLine : MonoBehaviour
{
    [SerializeField] private GameObject _stickmanPrefab;
    [SerializeField] private float _stickmanCreateTimeStep;
    [SerializeField] private Transform _fisrstStickmanSpawn;
    [SerializeField] private Material _red, _green, _yellow;

    private const int STICKMANCOUNT = 8;
    private Vector3 _stickmanSpawnOffsetVector;

    private float _stickmanMaterialStep;
    private List<Stickman> _stickmens;
    private Material _stickmanMaterial;
    private Material _laststickmanMaterial;
    private Material _anotherMaterialOne, _anotherMaterilTwo;

    

    private void Start()
    {
        _stickmens = new List<Stickman>();
        
        _stickmanSpawnOffsetVector = new Vector3(StickmanController._stickmanController.StickmanSpawnOffset, 0, 0);

        MainController.OnEndGame.AddListener(DestroyLine);

        StartCoroutine(CreateStickmens());
    }

    private IEnumerator CreateStickmens()
    {
        _stickmanMaterialStep = LevelController._levelController.GetMaterialStep();
        for(int i = 0; i<STICKMANCOUNT; i++)
        {
            GameObject stickman = Instantiate(_stickmanPrefab, _fisrstStickmanSpawn.position, Quaternion.identity);

            if(_stickmens.Count == 0)   /// Задаємо колір першому стікману в лінії
            {
                int rnd = Random.Range(0, 100);

                if(rnd < 34)
                {
                    _stickmanMaterial = _red;
                } else
                if(rnd > 33 && rnd < 67)
                {
                    _stickmanMaterial = _green;
                }
                else
                {
                    _stickmanMaterial = _yellow;
                }
            }
            else
            {
                int rnd = Random.Range(0, 100);
                if(rnd < (33 - _stickmanMaterialStep)) // Задаємо той самий колір що і у попереднього стікмана
                {
                    _stickmanMaterial = _laststickmanMaterial;
                }
                else // Задаємо інакший колір ніж у поперенього стікмана
                {
                    if(_laststickmanMaterial == _red)
                    {
                        _anotherMaterialOne = _green;
                        _anotherMaterilTwo = _yellow;
                    } else if(_laststickmanMaterial == _green)
                    {
                        _anotherMaterialOne = _red;
                        _anotherMaterilTwo = _yellow;
                    }
                    else
                    {
                        _anotherMaterialOne = _red;
                        _anotherMaterilTwo = _green;
                    }
                    rnd = Random.Range(0, 100);
                    if(rnd < 50)
                    {
                        _stickmanMaterial = _anotherMaterialOne;
                    }
                    else
                    {
                        _stickmanMaterial = _anotherMaterilTwo;
                    }
                }
            }

            _laststickmanMaterial = _stickmanMaterial;

            stickman.GetComponent<Stickman>().SetColor(_stickmanMaterial);
            if(_stickmanMaterial == _red) { stickman.GetComponent<Stickman>().Color = Stickman.StickmanColor.Red; }
            if (_stickmanMaterial == _green) { stickman.GetComponent<Stickman>().Color = Stickman.StickmanColor.Green; }
            if (_stickmanMaterial == _yellow) { stickman.GetComponent<Stickman>().Color = Stickman.StickmanColor.Yellow; }

            _stickmens.Add(stickman.GetComponent<Stickman>());

            _fisrstStickmanSpawn.position -= _stickmanSpawnOffsetVector;

            yield return new WaitForSeconds(_stickmanCreateTimeStep);
        }
    }

    public void DestroyLine()
    {

        Destroy(gameObject);
    }
    
}
