using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameManager : MonoBehaviour
{
    public static EndGameManager endManager;
    public bool gameOver;
    private PanelController panelController;
    private TextMeshProUGUI scoreTextComponent;

    private int score;

    [HideInInspector] public string lvlUnlock = "LevelUnlock";//保存数据的键

    void Awake()
    {
        if (endManager == null)
        {
            endManager = this;
            DontDestroyOnLoad(endManager);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateScore(int addScore)
    {
        score += addScore;
        scoreTextComponent.text = "Score: " + score;
    }

    public void StartResolveSequence()
    {
        StopCoroutine(nameof(ResolveSequence)); //防止不同脚本多次调用，先停止
        StartCoroutine(ResolveSequence());
    }

    private IEnumerator ResolveSequence()
    {
        yield return new WaitForSeconds(2);
        ResolveGame();
    }

    public void ResolveGame()
    {
        if (gameOver == false)
        {
            WinGame();
        }
        else
        {
            LoseGame();
        }
    }

    public void WinGame()
    {
        //激活面板
        //解锁下一关
        //得分
        ScoreSet();
        panelController.ActivateWin();
        int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextLevel > PlayerPrefs.GetInt(lvlUnlock, 0))
        {
            PlayerPrefs.SetInt(lvlUnlock,nextLevel);
        }
    }

    public void LoseGame()
    {
        ScoreSet();
        panelController.ActivateLose();
    }

    private void ScoreSet()
    {
        PlayerPrefs.SetInt("Score" + SceneManager.GetActiveScene().name, score);
        int highScore = PlayerPrefs.GetInt("HighScore" + SceneManager.GetActiveScene().name, 0);
        if (score > highScore)
            PlayerPrefs.SetInt("HighScore" + SceneManager.GetActiveScene().name, score);
        //重置分数
        score = 0;
    }

    public void RegisterPanelController(PanelController pC)
    {
        panelController = pC;
    }

    public void RegisterScoreText(TextMeshProUGUI scoreTextComp)
    {
        scoreTextComponent = scoreTextComp;
    }
}