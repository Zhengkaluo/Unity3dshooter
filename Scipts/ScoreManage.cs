using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManage : MonoBehaviour
{
    public static int score;
    private Text ScoreText;
    // Start is called before the first frame update
    
    void Awake()
    {
        ScoreText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        ScoreText.text = "Score:" + score;
    }
}
