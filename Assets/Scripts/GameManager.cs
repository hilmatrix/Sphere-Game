using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        int _x = 0;
        int _y = 0;

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
        SphereController.Instance.Move(_x, _y);
    }
}
