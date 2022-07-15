using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Stickman : MonoBehaviour
{
    private Renderer _renderer;
    public static UnityEvent OnResetMainLine = new UnityEvent();


    [SerializeField] private GameObject _particle;
    [SerializeField] private float _yParticleOffset;

    [SerializeField] private StickmanColor _stickmanColorType;
    [SerializeField] private Material _red, _green , _yellow;
    [SerializeField] private bool _isMainLine;
    [SerializeField] float _sphereRadius;
    [SerializeField] private List<Transform> _aroundTransforms;
    [SerializeField] private float _bulletDistanceToTransform;

    private Animator _stickmanAnimator;
    private Coroutine _moving;

   
    public bool IsActive { get; set; }
    public bool IsFirstLine { get; set; }
    public bool IsMainLine { get; set; }

    private void Awake()
    {
        _renderer = GetComponentInChildren<Renderer>();
        _stickmanAnimator = GetComponent<Animator>();
        IsActive = true;
    }
    private void Start()
    {
        switch (_stickmanColorType)
        {
            case StickmanColor.Red:
                _renderer.material = _red;
                break;
            case StickmanColor.Green:
                _renderer.material = _green;
                break;
            case StickmanColor.Yellow:
                _renderer.material = _yellow;
                break;
        }

        
        FrontStickmanLine.OnStickmanTouchLine.AddListener(StopStickman);
        StickmanController.OnMoveStickmans.AddListener(MoveStickman);
        StickmanController._stickmanController.IncrementStickmanCount();
        
        StickmanController.OnKillstickman.AddListener(CheckAround);
        OnResetMainLine.AddListener(ResetMainLine);
        
    }
    


    private void OnDestroy()
    {
        OnResetMainLine.Invoke();
        
        StickmanController._stickmanController.DectrementStickmanCount();
        StickmanController.OnMoveStickmans.RemoveListener(MoveStickman);
        if (IsFirstLine)
        {
            StickmanController._stickmanController.FirstLineCount--;
            if(StickmanController._stickmanController.FirstLineCount == 0)
            {
                StickmanController._stickmanController.MoveAllStickmans();
            }
        }
       
    }

    private void OnValidate()
    {
        _renderer = GetComponentInChildren<Renderer>();
        switch (_stickmanColorType)
        {
            case StickmanColor.Red:
                _renderer.material = _red;
                break;
            case StickmanColor.Green:
                _renderer.material = _green;
                break;
            case StickmanColor.Yellow:
                _renderer.material = _yellow;
                break;
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = new Color(1, 0, 0);
    //    Vector3 posOffset = new Vector3(0, _yParticleOffset, 0);
    //    Gizmos.DrawSphere(transform.position + posOffset, _sphereRadius);
    //}

    public void DestroyStickman()
    {
        IsActive = false;
        Vector3 posOffset = new Vector3(0, _yParticleOffset, 0);
        
        Collider [] colliders = Physics.OverlapSphere(transform.position + posOffset, _sphereRadius);
        foreach(Collider c in colliders)
        {
            Stickman s = c.gameObject.GetComponent<Stickman>();
            if(s != null && s.IsActive && s._stickmanColorType == _stickmanColorType)
            {
                s.DestroyStickman();
            } 
        }

        EnableParticle();
        StickmanController._stickmanController.KillStickman();
        Destroy(gameObject);
        
    }

    private void CheckAround()
    {
        IsMainLine = _isMainLine;
        if (!_isMainLine)
        {
            Vector3 posOffset = new Vector3(0, _yParticleOffset, 0);

            Collider[] colliders = Physics.OverlapSphere(transform.position + posOffset, _sphereRadius);
            foreach (Collider c in colliders)
            {
                Stickman s = c.gameObject.GetComponent<Stickman>();
                if (s != null && s.IsMainLine && s.IsActive)
                {
                    IsMainLine = true;
                    GetMainLineAround();
                }
            }

        }
        if (!IsMainLine)
        {
            if (IsActive && gameObject != null)
            {
                StartCoroutine(DethTimer());
            }
        }
    }

    public  void GetMainLineAround()
    {
        Vector3 posOffset = new Vector3(0, _yParticleOffset, 0);

        Collider[] colliders = Physics.OverlapSphere(transform.position + posOffset, _sphereRadius);
        foreach (Collider c in colliders)
        {
            Stickman s = c.gameObject.GetComponent<Stickman>();
            if (s != null && !s.IsMainLine && s.IsActive)
            {
                s.IsMainLine = true;
                s.GetMainLineAround();
            }
        }
    }

   

    public StickmanColor GetColorType()
    {
        return _stickmanColorType;
    }

    public List<Transform> GetTransformsList()
    {
        return _aroundTransforms;
    }

    public void SetMaterial(Material material, StickmanColor colorType)
    {
        _renderer.material = material;
        _stickmanColorType = colorType;
    }

    public void MoveStickman()
    {
        ChangeAnimation(true);
        //StopStickman();
        _moving = StartCoroutine(Moving());
        
    }

    public void StopStickman()
    {
        if (_moving != null)
        {
            ChangeAnimation(false);
            StopCoroutine(_moving);
        }
    }

    private IEnumerator Moving()
    {
        float speed = MainController._mainController.StickmanSpeed;
        while (true)
        {
            
            transform.Translate(transform.forward * speed * Time.deltaTime);

            yield return new WaitForEndOfFrame();
        }
    }

    public void EnableParticle()
    {
        Vector3 posOffset = new Vector3(0, _yParticleOffset, 0);
        Instantiate(_particle, transform.position + posOffset, Quaternion.identity);
    }

    private IEnumerator DethTimer()
    {
        yield return new WaitForFixedUpdate();
        if (!IsMainLine)
        {
            yield return new WaitForSeconds(1f);
            EnableParticle();
            OnResetMainLine.Invoke();
            Destroy(gameObject);
        }
    }

    public void ResetMainLine()
    {
        IsMainLine = _isMainLine;
    }

    public void ChangeAnimation(bool isGo)
    {
        _stickmanAnimator.SetBool("IsWalk", isGo);
    }


    public enum StickmanColor { Red, Green, Yellow }
}
