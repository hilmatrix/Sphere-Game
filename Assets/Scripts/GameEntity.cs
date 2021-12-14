using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEntity : MonoBehaviour
{
    public bool initialized = false;
    public bool triggerSomething = false;
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
    }

    void OnTriggerStay2D(Collider2D other) {
        triggerSomething = true;
    }

    public void Initialize() {
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0f);
        initialized = false;
        triggerSomething = false;
        Invoke("Check", delay);
    }

    public void Check() {
        if (triggerSomething) {
            //Debug.Log(gameObject.name + " Hitting something");
            transform.position = GameManager.Instance.GetSpawnPosition();
            Invoke("Initialize", delay);
        } else {
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            initialized = true;
            //Debug.Log(gameObject.name + "Finish");
        }
    }
}
