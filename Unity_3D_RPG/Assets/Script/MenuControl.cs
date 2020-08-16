using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  //切換畫面用
using System.Collections;


public class MenuControl : MonoBehaviour
{

    [Header("載入畫面")]
    public GameObject panelLoding;
    [Header("載入文字")]
    public Text textLoading;
    [Header("載入進度條")]
    public Image imgLoading;
    [Header("載入場景的名稱")]
    public string nameScene = "遊戲場景";


    /// <summary>
    /// 離開遊戲：關閉遊戲
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }

    /// <summary>
    /// 開始遊戲：進入載入畫面
    /// </summary>
    public void StartGame()
    {
        StartCoroutine(Loading());
    }


    private IEnumerator Loading()
    {
        panelLoding.SetActive(true);                // 顯示載入畫面
                                                                        // 使用API：AsyncOperation 
        AsyncOperation ao = SceneManager.LoadSceneAsync(nameScene);     // 異步載入場景(場景名稱)
        ao.allowSceneActivation = false;                                // 取消自動載入

        // 當場景尚未載入完成時 執行
        while (!ao.isDone)
        {
            textLoading.text = ao.progress * 100 + "%";     // 更新文字
            imgLoading.fillAmount = ao.progress;            // 更新吧條

            yield return null;                              // null為一個影格的時間
        }
    }


}
