using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(1000)]
public class HighScoresManager : MonoBehaviour
{
    public TextMeshProUGUI scoresText;
    public Button menuButton;

    private void Start()
    {
        StringBuilder sb = new StringBuilder();
        foreach (var highScore in GameManager.instance.highScores.highScores)
        {
            string score = highScore.score.ToString();

            sb.Append(highScore.playerName);
            sb.Append(new string('.', Mathf.Max(3, 24 - highScore.playerName.Length - score.Length)));
            sb.Append(score);
            sb.Append('\n');
        }

        scoresText.text = sb.ToString();
        menuButton.onClick.AddListener(OnMenuClick);
    }

    private void OnMenuClick()
    {
        GameManager.instance.ShowMenu();
    }
}
