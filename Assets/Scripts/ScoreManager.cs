using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;

    private static ScoreManager _instance = null;
    public static ScoreManager Instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<ScoreManager>();
            }
            return _instance;
        }
    }

    public string text {
        get {
            return scoreText.text;
        }
        set {
            scoreText.text = "Score = " + value;
        }
    }
}
