using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{

    // Use this for initialization
    public static GameManager instance = null;
    public static int PlayerScore1 = 0;
    public static int PlayerScore2 = 0;
    public KeyCode start = KeyCode.Return;

    public Text p1_score_text;
    public Text p2_score_text;

    public Button one_player_button;
    public Button two_player_button;

    public Text general;

    public Text subtitle;

    private int numberPlayers = 0;

    public int NumberPlayers
    {
        get { return numberPlayers; }
    }
    private GameObject player;
    private int lastScored = 0;
    public int LastScored
    {
        get { return lastScored; }
    }




    private string gameStatus = "start";

    public string GameStatus
    {

        get { return gameStatus; }

    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

    }
    void Start()
    {
        p1_score_text.text = "0";
        p2_score_text.text = "0";
        general.text = "Press Enter to Start";
        subtitle.text = "";
        gameStatus = "serve";
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {



        if (gameStatus == "gameover" && Input.GetKey(start))
        {
            BeginMatch();
        }



    }

    private void HidePlayerButtons()
    {
        one_player_button.gameObject.SetActive(false);
        two_player_button.gameObject.SetActive(false);

    }


    private void ShowPlayerButtons()
    {
        one_player_button.gameObject.SetActive(true);
        two_player_button.gameObject.SetActive(true);

    }

    public void SetGameStatusToServe()
    {
        gameStatus = "serve";
        EnableUI();
    }

    public void SetGameStatusToActive()
    {
        gameStatus = "active";
        DisableUI();
    }
    private void StartGame()
    {
        general.text = "How many hooman players?";
        ShowPlayerButtons();
        UpdateUI();
    }

    public void SetUpPlayers(int players)
    {
        numberPlayers = players;
        if (numberPlayers == 1)
        {
            player = GameObject.Find("paddle_left");
            player.GetComponent<PlayerControls>().human_player = true;
        }
        HidePlayerButtons();
        EnterGame();
    }

    private void EnterGame()
    {
        BeginMatch();
    }

    private void UpdateUI()
    {
        DisableUI();
        EnableUI();
    }
    private void EnableUI()
    {
        p1_score_text.enabled = true;
        p2_score_text.enabled = true;
        general.enabled = true;
        subtitle.enabled = true;
    }

    private void DisableUI()
    {
        p1_score_text.enabled = false;
        p2_score_text.enabled = false;
        general.enabled = false;
        subtitle.enabled = false;

    }
    public void Score(int player)
    {
        if (player == 1)
        {
            PlayerScore1++;
            lastScored = 1;
            p1_score_text.text = PlayerScore1.ToString();
            general.text = "Player 1's Serve";
        }
        else
        {
            PlayerScore2++;
            lastScored = 2;
            p2_score_text.text = PlayerScore2.ToString();
            general.text = "Player 2's Serve";

        }
        Ball.instance.ResetBall();
        if (PlayerScore1 == 10 || PlayerScore2 == 10)
        {
            GameOver();
        }
        else
        {
            SetGameStatusToServe();

        }

    }

    private void GameOver()
    {
        if (PlayerScore1 == 10)
        {
            general.text = "Player 1 Wins!";
        }
        else
        {
            general.text = "Player 2 Wins!";
        }
        subtitle.text = "Press Enter to Play Again.";
        gameStatus = "gameover";

        EnableUI();


    }

    private void BeginMatch()
    {
        PlayerScore1 = 0;
        PlayerScore2 = 0;
        p1_score_text.text = PlayerScore1.ToString();
        p2_score_text.text = PlayerScore2.ToString();

        subtitle.text = "";
        general.text = "Press Enter to Start";
        lastScored = 0;
        DisableUI();
        EnableUI();

        Invoke("SetGameStatusToServe", .3f);
    }
}
