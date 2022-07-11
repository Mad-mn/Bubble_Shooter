using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickmanController : MonoBehaviour
{
    public static StickmanController _stickmanController;

    [SerializeField] private GameObject _linePrefab;
    [SerializeField] private Vector3 _firstLineSpawnPosition;
    [SerializeField] private float _createLineTimeStep;
    [SerializeField] private float _linePositionOffset;
    [SerializeField] private float _stickmanSpawnOffset;
    [SerializeField] private GameObject _stickmanPrefab;

    
    private Vector3 _firstLinePosition;

    public int StickmanDestroyCount { get; set; }
    
    public int AllStickmanInLevel { get; set; }
    public float StickmanSpawnOffset { get; set; }

    private void Awake()
    {
        if(_stickmanController == null)
        {
            _stickmanController = this;
        }
    }

    private void Start()
    {
       
        StickmanSpawnOffset = _stickmanSpawnOffset;
        MainController.OnStartGame.AddListener(GetStickmanCount);
        MainController.OnEndGame.AddListener(CreateNewLines);
        StartCoroutine(CreateLines());
        StickmanDestroyCount = 0;
        _firstLinePosition = _firstLineSpawnPosition;
    }

    private void CreateNewLines()
    {
        
        StartCoroutine(CreateLines());
    }
        

    public void IncrementStickmanCount()
    {
        AllStickmanInLevel--;
        StickmanDestroyCount++;
        CanvasController._canvasController.ChangeStickmanCountTxt(StickmanDestroyCount);
        if (AllStickmanInLevel == 0)
        {
            MainController._mainController.EndGame();
        }

    }

    public void DectrementStickmanCount()
    {
        AllStickmanInLevel--;
        if (AllStickmanInLevel == 0 && MainController._mainController.IsGamePlayed)
        {
            
            MainController._mainController.EndGame();
        }
    }

    private void GetStickmanCount()
    {
        CanvasController._canvasController.ChangeStickmanCountTxt(StickmanDestroyCount);
    }

    private IEnumerator CreateLines()
    {
        yield return new WaitForEndOfFrame();

        int lineCount = LevelController._levelController.GetStickmanLineCount();

        for (int i = 0; i < lineCount; i++)
        {
            GameObject line = Instantiate(_linePrefab, _firstLinePosition, Quaternion.identity);

           
            _firstLinePosition -= new Vector3(0f, 0f, _linePositionOffset);

            yield return new WaitForSeconds(_createLineTimeStep);
        }
        _firstLinePosition = _firstLineSpawnPosition;
    }

    public void CreateNewStickman(Vector3 position, Vector3 direction, Material color, Stickman.StickmanColor colorType)
    {
        Ray ray = new Ray(position, direction);

        if(Physics.Raycast(ray, out RaycastHit hit, 2)) /// Змінюємо спавн позицію якщо перед стікменом стоїть ішший стікмен
        {
            if (hit.transform.CompareTag("Player"))
            {
                
                position = hit.transform.localPosition;
                CreateNewStickman(position, direction, color, colorType);
                return;
            }
        }
        
        Vector3 newStickmanPosition = new Vector3(position.x, position.y, position.z + _linePositionOffset);

        GameObject stickman = Instantiate(_stickmanPrefab, newStickmanPosition, Quaternion.identity);

        Stickman s = stickman.GetComponent<Stickman>();
        s.SetColor(color);
        s.Color = colorType;
        s.EnableParticle();
        s.ChangeAnimation();
        s.MoveStickman();
    }
}
