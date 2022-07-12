using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    public static CannonController _cannonController;

    [SerializeField] private Transform _bulletSpawnPosition;
    [SerializeField] private GameObject _bulletPrefab;
    
    [SerializeField] private Renderer _bodyRenderer;
    [SerializeField] private BulletChangeColor _bulletChangeColor;

    [SerializeField] private ReloadCanvas _reloadCanvas;

    private Transform _cannonTransform;
    private Vector3 _tapPosition;
    private Coroutine _rotatingCannon;
    private LineRenderer _lineRenderer;
    private Ray _ray;
    private Animation _animation;
    private AudioSource _audio;

    public Material _nextBulletMaterial { get; set; }
    public Stickman.StickmanColor _nextBulletColorType { get; set; }
    

    private void Awake()
    {
        if(_cannonController == null)
        {
            _cannonController = this;
        }
    }

    private void Start()
    {
        _cannonTransform = GetComponent<Transform>();
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.SetPosition(0, _cannonTransform.position);
        _animation = GetComponent<Animation>();
        MainController.OnStartGame.AddListener(EnableOrDisableAniamtion);
        //MainController.OnEndGame.AddListener(EnableOrDisableAniamtion);

        _audio = GetComponent<AudioSource>();
        Invoke("SetNextBulletColor", 0.01f);

       
    }

    private void Update()
    {
        if (!MainController._mainController.IsGamePlayed)
        {
            Debug.Log(0);
            CastRayForSight();
        }
    }

    public void RotateCannon()
    {
        _rotatingCannon = StartCoroutine(Rotation());
        _lineRenderer.enabled = true;
    }
    public void StopRotate()
    {
        StopCoroutine(_rotatingCannon);
        Shot();
        _lineRenderer.positionCount = 1;
        _lineRenderer.enabled = false;
    }

    private IEnumerator Rotation()
    {
        while (true)
        {
#if UNITY_EDITOR
            _tapPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + Camera.main.transform.position);
            
#elif UNITY_ANDROID
             _tapPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 0) + Camera.main.transform.position);
#endif
            _tapPosition = new Vector3(_tapPosition.x, _cannonTransform.position.y, _tapPosition.z);
            _cannonTransform.LookAt(_tapPosition, Vector3.up);

            CastRayForSight();

            yield return new WaitForEndOfFrame();
        }
    }

    private void Shot()
    {
        if (_reloadCanvas.IsReady)
        {

            GameObject bullet = Instantiate(_bulletPrefab, _bulletSpawnPosition.position, Quaternion.identity);

            bullet.GetComponent<Bullet>().MoveTo(_cannonTransform.forward);

            bullet.GetComponent<Bullet>().SetMaterial(_nextBulletMaterial);
            bullet.GetComponent<Bullet>().Color = _nextBulletColorType;
            SetNextBulletColor();

            _reloadCanvas.Reload();
            if (!AudioController._audioController.IsOff)
            {
                _audio.Play();
            }
        }
    }

    public void SetNextBulletColor()
    {
        _nextBulletMaterial = _bulletChangeColor.GetNextBulletMaterial();
        _nextBulletColorType = _bulletChangeColor.GetNextBulletColorType();
        ChangeBodyColor();
        _bulletChangeColor.SetNextBulletColor();
    }

    public void ChangeBodyColor()
    {
        _bodyRenderer.material = _nextBulletMaterial;
    }

    private void CastRayForSight() /// Пускаємо промені для визначення напрямку прицілу
    {
        _ray = new Ray(_bulletSpawnPosition.position, _cannonTransform.forward);

        if (Physics.Raycast(_ray, out RaycastHit hit))
        {
            int hitPointSide = (hit.point.x - transform.position.x) > 0 ? -1 : 1;

            ChangeLineRenderer(hit.point + _bulletPrefab.transform.localScale / 2 * hitPointSide, 1);

            if (!hit.transform.CompareTag("Player"))
            {
                Vector3 newRayDirection = (hit.transform.right * hitPointSide) + (hit.transform.right * hitPointSide + _cannonTransform.forward);
                _ray = new Ray(hit.point, newRayDirection.normalized);
                if (Physics.Raycast(_ray, out RaycastHit hit2))
                {
                    ChangeLineRenderer(hit2.point - _bulletPrefab.transform.localScale / 2, 2);
                }
            }
           
        }
    }

    private void ChangeLineRenderer(Vector3 postition, int positionCount)
    {
       
        _lineRenderer.positionCount = positionCount+1;
        
        _lineRenderer.SetPosition(positionCount, postition);
    }

    public void EnableOrDisableAniamtion()
    {
        if (_animation.isPlaying)
        {
            _animation.Stop();
        }
        else
        {
            _animation.Play();
        }
    }


}
