using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    public int score = 0;
    [SerializeField] int scoreIncreaseAmount = 5;

    TextMeshProUGUI scoreText;
    int targetScore = 0;

    void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();    
    }

    public void IncreaseScore(int scoreToIncrease)
    {
        targetScore = score + scoreToIncrease;

        
    }

    private void Update()
    {
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (score < targetScore)
        {
            score += scoreIncreaseAmount;
        }

        scoreText.text = "Score: " + score.ToString();
    }
}
