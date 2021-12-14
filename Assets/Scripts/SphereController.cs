using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour
{
    private Rigidbody2D rigidBody2D;
    public float xMaxForce = 10;
    public float yMaxForce = 10;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        RestartGame();
    }

    // Update is called once per frame
    void Update()
    {
        
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

    void ResetBall() {
        transform.position = Vector2.zero;
        rigidBody2D.velocity = Vector2.zero;
    }

    void RestartGame() {
        ResetBall();
        Invoke("PushSphere", 2);
    }
}
