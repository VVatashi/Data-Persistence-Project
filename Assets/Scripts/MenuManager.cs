using TMPro;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(1000)]
public class MenuManager : MonoBehaviour
{
    public TMP_InputField nameInput;
    public Button startButton;
    public Button highScoresButton;
    public Button quitButton;

    private void Start()
    {
        nameInput.text = GameManager.instance.playerName;
        nameInput.onValueChanged.AddListener(OnNameChanged);
        startButton.onClick.AddListener(OnStartClick);
        highScoresButton.onClick.AddListener(OnHighScoresClick);
        quitButton.onClick.AddListener(OnQuitClick);
    }

    private void OnNameChanged(string value)
    {
        GameManager.instance.playerName = string.IsNullOrWhiteSpace(value) ? "Name" : value;
    }

    private void OnStartClick()
    {
        GameManager.instance.StartGame();
    }

    private void OnHighScoresClick()
    {
        GameManager.instance.ShowHighScores();
    }

    private void OnQuitClick()
    {
        GameManager.instance.Quit();
    }
}
