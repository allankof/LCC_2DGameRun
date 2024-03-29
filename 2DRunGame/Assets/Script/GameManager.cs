﻿
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public Text textLoading;
    public Image imageLoading;
    public GameObject tip;

    /// <summary>
    /// 重新開始
    /// </summary>
    /// <param name="scene"></param>
    public void ReplayGame(string scene)
    {
        // 場景管理.載入場景
        SceneManager.LoadScene(scene);
    }

    /// <summary>
    /// 離開遊戲
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// 開始載入場景
    /// </summary>
    public void StartLoading()
    {
        StartCoroutine(Loading());
    }

    public IEnumerator Loading()
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync("關卡1");         // 取得載入場景的資料
        ao.allowSceneActivation = false;                                  // 取消載入

        

        while (ao.isDone == false)                                        // 當 尚未載入完成
        {
            textLoading.text = ao.progress / 0.9f * 100 + "/ 100";        // 更新文字介面 progress=載入進度 0~0.9
            imageLoading.fillAmount = ao.progress / 0.9f;                 // 更新進度條

            yield return null;                                            // 等待 (null為一個fram)

            if (ao.progress == 0.9f)
            {
                tip.SetActive(true);

                if(Input.anyKey) ao.allowSceneActivation = true;          // 按任意鍵繼續
            }
        }

    }
    
}
