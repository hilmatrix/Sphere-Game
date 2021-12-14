using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Camera gameCamera;

    // Start is called before the first frame update
    void Start()
    {
        
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
    }
}
