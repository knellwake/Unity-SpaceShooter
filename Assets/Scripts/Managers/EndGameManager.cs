using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameManager : MonoBehaviour
{
    public static EndGameManager endManager;
    public bool gameOver;
    public bool possibileWin;

    private PanelController panelController;
    private TextMeshProUGUI scoreTextComponent;
    private PlayerStats player;
    private RewardedAd rewardedAd;

    public int score;

    [HideInInspector] public string lvlUnlock = "LevelUnlock"; //保存数据的键

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
        if (possibileWin == true && gameOver == false)
        {
            WinGame();
        }
        else if (possibileWin == false && gameOver == true)
        {
            //We lost, but we can continue because we are not at the end of a level.
            //我们输了，但我们可以继续，因为我们还没有到一个关卡的终点。
            AdLoseGame();
        }
        else if (possibileWin == true && gameOver == true)
        {
            //我们在关卡结束时输了。玩家和boss都被摧毁了
            //或者计时器过期了，但玩家被最后一颗流星/子弹摧毁了。
            LoseGame();
        }
    }

    public void WinGame()
    {
        player.canTakeDmg = false;
        //激活面板
        //解锁下一关
        //得分
        ScoreSet();
        panelController.ActivateWin();
        int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextLevel > PlayerPrefs.GetInt(lvlUnlock, 0))
        {
            PlayerPrefs.SetInt(lvlUnlock, nextLevel);
        }
    }

    public void LoseGame()
    {
        ScoreSet();
        panelController.ActivateLose();
    }

    public void AdLoseGame()
    {
        ScoreSet();
        if (rewardedAd.adNumber > 0)
        {
            rewardedAd.adNumber -= 1;
            panelController.ActivateAdLose();
        }
        else
        {
            panelController.ActivateLose();
        }
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

    public void RegisterPlayerStats(PlayerStats statsPlayer)
    {
        player = statsPlayer;
    }

    public void RegisterRewardAd(RewardedAd ad)
    {
        rewardedAd = ad;
    }
}