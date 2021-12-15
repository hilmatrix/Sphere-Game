using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEntity : MonoBehaviour
{
    public enum Type {
        Bug, Chip
    };

    public bool initialized = false;
    public bool triggerSomething = false;
    public Type type;
    private float delay = 0.2f;

    public static bool enableRelocate = true;

    private float relocateTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        float deltaTime = Time.unscaledDeltaTime;
        if (initialized && (type == Type.Chip) && (!GameManager.Instance.gameOver)) {
            if (relocateTime > 0)
                relocateTime -= deltaTime;
            else {
                Relocate();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        triggerSomething = true;

        if (initialized && other.tag == "Player" && (!GameManager.Instance.gameOver)) {
            GameManager.Instance.Hit(type);
            Hide();
            gameObject.SetActive(false);
        }
    }

    void OnTriggerStay2D(Collider2D other) {
        triggerSomething = true;
    }

    public void Initialize() {
        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        initialized = false;
        triggerSomething = false;
        gameObject.SetActive(true);
        Invoke("Check", delay);
    }

    public void Check() {
        if (triggerSomething) {
            triggerSomething = false;
            transform.position = GameManager.Instance.GetSpawnPosition();
            Invoke("Check", delay);
        } else {
            initialized = true;
            if (type == Type.Chip)
                relocateTime = GameManager.Instance.ChipRelocateTime();
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        }
    }

    public void Hide() {
        initialized = false;
        triggerSomething = false;
        GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0);
    }

    public void Relocate() {
        Hide();
        transform.position = GameManager.Instance.GetSpawnPosition();
        Invoke("Check", delay);
    }
}
