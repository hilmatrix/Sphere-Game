using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    private static GameManager _instance = null;
    public static GameManager Instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<GameManager>();
            }
            return _instance;
        }
    }

    public Camera gameCamera;

    public int minBug = 5;
    public int maxBug = 10;
    public float minBugScale = 0.3f;
    public float maxBugScale = 0.6f;

    public int minChip = 5;
    public int maxChip = 10;
    public float minChipScale = 0.3f;
    public float maxChipScale = 0.6f;

    public int hitBugScore = 2;
    public int hitChipScore = -1;

    public Transform borderLeft;
    public Transform borderRight;
    public Transform borderUp;
    public Transform borderDown;
    public float paddingHorizontal = 0.9f;
    public float paddingVertical = 0.85f;

    public float respawnDelay = 5f;
    private float respawnDelayCounter = 0;

    public GameObject bugPrefab;
    public GameObject chipPrefab;

    private Pool<GameEntity> bugPool;
    private Pool<GameEntity> chipPool;

    private int score = 0;

    // Start is called before the first frame update
    void Start() {
        bugPool = new Pool<GameEntity>(bugPrefab);
        chipPool = new Pool<GameEntity>(chipPrefab);

        int randomBug = Mathf.RoundToInt(Random.Range(minBug, maxBug));
        int randomChip = Mathf.RoundToInt(Random.Range(minChip, maxChip));

        for (int loop = 0; loop < randomBug; loop++) {
            SpawnBug();
        }

        for (int loop = 0; loop < randomChip; loop++) {
            SpawnChip();
        }

        ScoreManager.Instance.text = score.ToString(); 
    }

    // Update is called once per frame
    void Update() {
        float _x = 0;
        float _y = 0;

        if (Input.GetKey(KeyCode.UpArrow)) {
            _y += 1;
        }
        if (Input.GetKey(KeyCode.DownArrow)) {
            _y -= 1;
        }
        if (Input.GetKey(KeyCode.LeftArrow)) {
            _x -= 1;
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            _x += 1;
        }
        if (Input.GetMouseButton(0)) {
            Vector2 moveTo = new Vector2();
            moveTo = (Vector2)(gameCamera.ScreenToWorldPoint(Input.mousePosition) -
                SphereController.Instance.transform.position);
            _x = moveTo.normalized.x;
            _y = moveTo.normalized.y;
        }
        SphereController.Instance.Move(_x, _y);

        float deltaTime = Time.unscaledDeltaTime;
        respawnDelayCounter -= deltaTime;

        if (respawnDelayCounter < 0f) {
            respawnDelayCounter = respawnDelay;

            if (bugPool.TotalActive() < minBug) {
                int randomBug = Mathf.RoundToInt(Random.Range(0, maxBug - bugPool.TotalActive()));
                for (int loop = 0; loop < randomBug; loop++) {
                    SpawnBug();
                }
            }

            if (chipPool.TotalActive() < minChip) {
                int randomChip = Mathf.RoundToInt(Random.Range(0, maxChip - chipPool.TotalActive()));
                for (int loop = 0; loop < randomChip; loop++) {
                    SpawnChip();
                }
            }
        }
    }

    void SpawnBug() {
        float randomSize;
        GameEntity newBug = bugPool.GetOrCreate();
        newBug.transform.position = GetSpawnPosition();
        randomSize = Random.Range(minBugScale, maxBugScale);
        newBug.transform.localScale = new Vector2(randomSize, randomSize);
        newBug.transform.Rotate(0, 0, Random.Range(0, 360));
        newBug.Initialize();
    }

    void SpawnChip() {
        float randomSize;
        GameEntity newChip = chipPool.GetOrCreate();
        newChip.transform.position = GetSpawnPosition();
        randomSize = Random.Range(minChipScale, maxChipScale);
        newChip.transform.localScale = new Vector2(randomSize, randomSize);
        newChip.Initialize();
    }

    public Vector2 GetSpawnPosition() {
        float _x = Random.Range(borderLeft.position.x * paddingHorizontal,
            borderRight.position.x * paddingHorizontal);
        float _y = Random.Range(borderUp.position.y * paddingVertical,
            borderDown.position.y * paddingVertical);
        return new Vector2(_x, _y);
    }

    public void Hit(GameEntity.Type _type) {
        switch (_type) {
            case GameEntity.Type.Bug: score += hitBugScore; break;
            case GameEntity.Type.Chip: score += hitChipScore; break;
        }
        ScoreManager.Instance.text = score.ToString();
    } 
}
