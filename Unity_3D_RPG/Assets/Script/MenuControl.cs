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
    [Header("提示")]
    public GameObject tip;

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

        // 當場景尚未載入完成時 執行    (!為反向輸入，可當反向閘使用)
        while (!ao.isDone)
        {   
            // progress 值域範圍為 0-0.9f，故需要 除以 0.9f 以修正值域至0-1。
            // Tostring("F+任意整數") 即可控制顯示至小數點後幾位，ex:("f0")時，顯示至100.；("f2")時，顯示至100.00。
            textLoading.text = (ao.progress / 0.9F * 100).ToString("F2") + "%";     // 更新文字
            imgLoading.fillAmount = ao.progress / 0.9F;                             // 更新吧條
            yield return null;                                                      // null為一個影格的時間


            // 當場景載入完成時 執行(ao.progress = 0.9f 即載入進度完成)
            if (ao.progress == 0.9f)
            {
                tip.SetActive(true);                                            // 顯示(呼叫)提示文字

                if (Input.anyKeyDown == true) ao.allowSceneActivation = true;       // 如果 按下任意鍵 則允許自動載入
            }

        }

    }


}
