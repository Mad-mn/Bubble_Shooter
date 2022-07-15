using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject _particle;
    [SerializeField] float _sphereRadius;

    private float _bulletSpeed;
    private Coroutine _moving;
    private Vector3 _direction;
    private Renderer _renderer;
    private AudioSource _audio;

    public Stickman.StickmanColor Color { get; set; }

    private void Awake()
    {
        //_bulletSpeed = MainController._mainController.BulletSpeed;
        _renderer = GetComponent<Renderer>();
    }

    private void Start()
    {
        MainController.OnEndGame.AddListener(DestroyBullet);
        _audio = GetComponent<AudioSource>();
    }

    public void MoveTo(Vector3 direction)
    {
        _moving = StartCoroutine(Moving());
        _direction = direction;
    }

    private IEnumerator Moving()
    {
        _bulletSpeed = PlayerInfo._playerInfo.BulletSpeed;
        while (true)
        {
            transform.Translate(_direction.normalized * _bulletSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }

    public void SetMaterial(Material material)
    {
        GetComponent<Renderer>().material = material;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            return;
        }
        if (other.CompareTag("Player"))
        {
            StopCoroutine(_moving);

            bool isEqual = false;

            Collider[] colliders = Physics.OverlapSphere(transform.position, _sphereRadius);
            foreach(Collider c in colliders)
            {
                Stickman s = c.GetComponent<Stickman>();
                if(s != null)
                {
                    isEqual = s.GetColorType() == Color ? true : false ;
                    if (isEqual)
                    {
                        s.DestroyStickman();
                        break;
                    }
                }

                
                
            }
            if (!isEqual)
            {
                StickmanController._stickmanController.CreateNewStickman(transform.position, Color, other.GetComponent<Stickman>().GetTransformsList());
            }
           

            Instantiate(_particle, transform.localPosition, Quaternion.identity);
            Destroy(gameObject);
        }
        else
        {
            
            StopCoroutine(_moving);
            int colliderSide = (other.transform.position.x - transform.position.x) > 0 ? -1 : 1;

            _direction = (other.transform.right * colliderSide) + (other.transform.right * colliderSide + _direction);

            _moving = StartCoroutine(Moving());
            if (!AudioController._audioController.IsOff)
            {
                _audio.Play();
            }
        }
        
    }

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
