using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    public Text timeLeftText;
    public GameObject gameOverScreen;

    private static TimerManager _instance = null;
    public static TimerManager Instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<TimerManager>();
            }
            return _instance;
        }
    }

    public string text {
        get {
            return timeLeftText.text;
        }
        set {
            timeLeftText.text = "Time Left = " + value;
        }
    }

    public void GameOver() {
        gameOverScreen.SetActive(true);
    }
}
