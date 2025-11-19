using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro; 

public class MenuWinscreen : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI winnerText;

    [SerializeField]
    private Button playAgainButton;

    [SerializeField]
    private Winner winner;

    void Start()
    {
        string winner = this.winner.winner;

        if (winner == "red")
        {
            winnerText.text = "RED PLAYER WINS";
            winnerText.color = Color.red;
        }
        else if (winner == "blue")
        {
            winnerText.text = "BLUE PLAYER WINS";
            winnerText.color = Color.blue;
        }

     
        if (playAgainButton != null)
        {
            playAgainButton.onClick.AddListener(LoadGamingScene);
        }
    }

    private void LoadGamingScene()
    {
        SceneManager.LoadScene("ArnauEscena");
    }
}
