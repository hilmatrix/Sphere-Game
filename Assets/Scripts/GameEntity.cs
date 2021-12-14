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
    private float delay = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other) {
        triggerSomething = true;

        if (initialized && other.tag == "Player") {
            GameManager.Instance.Hit(type);
            Deactivate();
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
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        }
    }

    public void Deactivate() {
        initialized = false;
        triggerSomething = false;
        GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0);
        gameObject.SetActive(false);
    }
}
