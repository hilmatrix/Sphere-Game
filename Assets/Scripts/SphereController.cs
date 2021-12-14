using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour
{
    private Rigidbody2D rigidBody2D;
    public float xMaxForce = 10;
    public float yMaxForce = 10;
    public float yMoveForce = 10;
    public float xMoveForce = 10;

    float moveX = 0;
    float moveY = 0;

    public float MoveEventInterval = 0.1f;
    private float _moveEventCounter;

    private static SphereController _instance = null;
    public static SphereController Instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<SphereController>();
            }
            return _instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        RestartGame();
    }

    // Update is called once per frame
    void Update()
    {
        float deltaTime = Time.unscaledDeltaTime;
        _moveEventCounter -= deltaTime;

        if (_moveEventCounter < 0f) {
            _moveEventCounter = MoveEventInterval;

            if ((moveX != 0) || (moveY != 0)) {
                rigidBody2D.AddForce(new Vector2(moveX * xMoveForce, moveY * yMoveForce));
                moveX = 0;
                moveY = 0;
            }
        }
    }

    void PushSphere() {
        float xRandomForce = Random.Range(-xMaxForce, xMaxForce);
        float yRandomForce = Random.Range(-yMaxForce, yMaxForce);

        float gayaMaksimum = Mathf.Sqrt(xMaxForce * xMaxForce + yMaxForce * yMaxForce);
        float gayaSekarang = Mathf.Sqrt(xRandomForce * xRandomForce + yRandomForce * yRandomForce);

        if (gayaSekarang == 0) {
            xRandomForce = xMaxForce;
            yRandomForce = yMaxForce;
        }

        float penguatanGaya = gayaMaksimum / gayaSekarang;

        rigidBody2D.AddForce(new Vector2(xRandomForce * penguatanGaya, yRandomForce * penguatanGaya));
    }

    public void Move(float _x, float _y) {
        moveX = _x;
        moveY = _y;
    }

    void ResetBall() {
        transform.position = Vector2.zero;
        rigidBody2D.velocity = Vector2.zero;
    }

    void RestartGame() {
        ResetBall();
        Invoke("PushSphere", 2);
    }

}
