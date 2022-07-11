using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stickman : MonoBehaviour
{
    private Renderer _renderer;

    [SerializeField] private float _maxRayDistance;
    [SerializeField] private GameObject _particle;
    [SerializeField] private float _yParticleOffset;

    private Animator _stickmanAnimator;

    public StickmanColor Color { get; set; }
    public bool IsActive { get; set; }

    private void Awake()
    {
        _renderer = GetComponentInChildren<Renderer>();
        _stickmanAnimator = GetComponent<Animator>();
        IsActive = true;
    }
    private void Start()
    {
        MainController.OnStartGame.AddListener(ChangeAnimation);
        MainController.OnStartGame.AddListener(MoveStickman);
        StickmanController._stickmanController.AllStickmanInLevel++;
        
    }
    public void DestroyStickman()
    {
        IsActive = false;
        Vector3 posOffset = new Vector3(0, _yParticleOffset, 0);
        Ray rayRight = new Ray(transform.localPosition + posOffset, transform.right);
        Ray rayLeft = new Ray(transform.localPosition + posOffset, -transform.right);
        Ray rayFront = new Ray(transform.localPosition + posOffset, transform.forward);
        Ray rayBack = new Ray(transform.localPosition + posOffset, -transform.forward);


        for (int i = 0; i < 4; i++)
        {
            Ray _currentRay = new Ray();
            switch (i)
            {
                case 0:
                    _currentRay = rayRight;
                    break;
                case 1:
                    _currentRay = rayLeft;
                    break;
                case 2:
                    _currentRay = rayFront;
                    break;
                case 3:
                    _currentRay = rayBack;
                    break;
            }
            if (Physics.Raycast(_currentRay, out RaycastHit hit, _maxRayDistance))
            {
                
                if (hit.transform.GetComponentInParent<Stickman>())
                {
                    if (hit.transform.GetComponentInParent<Stickman>().Color == Color && hit.transform.GetComponentInParent<Stickman>().IsActive)
                    {
                        hit.transform.GetComponentInParent<Stickman>().DestroyStickman();
                    }

                }
            }
        }
        EnableParticle();
        OnDestroyStickman();
        Destroy(gameObject);
        
    }

    public void OnDestroyStickman()
    {
        StickmanController._stickmanController.IncrementStickmanCount();
    }

    public void MoveStickman()
    {
        StartCoroutine(Moving());
    }

    private IEnumerator Moving()
    {
        float speed = MainController._mainController.StickmanSpeed;
        while (MainController._mainController.IsGamePlayed)
        {
            transform.Translate(transform.forward * speed * Time.deltaTime);

            yield return new WaitForEndOfFrame();
        }
    }

    public void EnableParticle()
    {
        Vector3 posOffset = new Vector3(0, _yParticleOffset, 0);
        Instantiate(_particle, transform.localPosition + posOffset, Quaternion.identity);
    }

    public void SetColor(Material color)
    {
       _renderer.material = color;
    } 

    public void ChangeAnimation()
    {
        _stickmanAnimator.SetBool("IsWalk", !_stickmanAnimator.GetBool("IsWalk"));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            EnableParticle();
            StickmanController._stickmanController.DectrementStickmanCount();
            Destroy(gameObject);
        }
    }


    public enum StickmanColor { Red, Green, Yellow }
}
