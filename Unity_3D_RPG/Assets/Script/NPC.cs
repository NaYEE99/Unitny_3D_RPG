
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NPC : MonoBehaviour
{

    [Header("NPC 資料")]
    public NPCData data;
    [Header("對話區塊")]
    public GameObject panelDialog;
    [Header("名稱")]
    public Text textName;
    [Header("內容")]
    public Text textContent;
    [Header("打字速度"), Range(0.01f, 10)]
    public float printSpeed;
    [Header("打字音效")]
    public AudioClip soundPrint;
    [Header("任務區塊")]
    public RectTransform panelMission;

    private AudioSource aud;
    private Animator ani;
    private Player player;

    public int count;       // 任務計數器

    /// <summary>
    /// NPC 對話系統
    /// </summary>
    public void Dialog()
    {
        panelDialog.SetActive(true);        // SetActive 使[]設定為顯示
        textName.text = name;               // 名稱內的文字 = 當前物件的名稱
        StartCoroutine(Print()); 

    }

    /// <summary>
    /// 離開時關閉對話框
    /// </summary>
    private void CancelDialog()
    {
        panelDialog.SetActive(false);
    }


    private IEnumerator Print()
    {
        AnimationControl();

        Missioning();

        player.stop = true;                                     // 使玩家在進入對話瞬間停止動作

        string dialog = data.dialogs[(int)data._NPCState];      // 對話內容 = NPC 資料.對話[(轉為整數)data內的資料序號]
        textContent.text = "";                                  // 清空字串

        for (int i = 0; i < dialog.Length; i++)         // i < dialog.Length 
        {                                               // 到此字串.最後一個字元

            textContent.text += dialog[i];              // 對話內容.文字 += 對話[]的字元
            aud.PlayOneShot(soundPrint, 0.5f);
            yield return new WaitForSeconds(printSpeed); 

        }

        player.stop = false;                                    // 使玩家在對話結束後可以繼續動作

        NoMission();
    }

    // 動畫控制器(未完成、進行中 = 說話  /  完成 = 感謝)
    private void AnimationControl()
    {
        if (data._NPCState == NPCState.NoMission || data._NPCState == NPCState.Missioning)
            ani.SetBool("說話開關", true);
        else
            ani.SetTrigger("感謝開關");
    }




    private void Awake()
    {
        data._NPCState = NPCState.NoMission;        // 遊戲開始時重置任務狀態

        aud = GetComponent<AudioSource>();          // 尋找音效
        ani = GetComponent<Animator>();             // 尋找動畫控制器

        player = FindObjectOfType<Player>();        // 尋找同名物件(僅限單獨物件使用)(不然會死掉w)
    }



    private void OnTriggerEnter(Collider other)     // ote：剛體碰撞器 進入 (其他剛體)
    {
        if (other.name == "Robot Kyle") Dialog();   // 如果碰撞到""內的指定物件，執行...

    }

    private void OnTriggerExit(Collider other)     // ote：剛體碰撞器 離開 (其他剛體)
    {
        if (other.name == "Robot Kyle") CancelDialog();   // 如果碰撞到""內的指定物件，執行...

    }


    /// <summary>
    /// 未接受任務狀態切換為任務中：對話後執行
    /// </summary>
    private void NoMission()
    {
        if (data._NPCState == NPCState.NoMission)
        {
            data._NPCState = NPCState.Missioning;
            StartCoroutine(ShowMission());
        }

    }

    private IEnumerator ShowMission()
    {
        while (panelMission.anchoredPosition.x > -600)
        {
            panelMission.anchoredPosition = Vector3.Lerp(panelMission.anchoredPosition, new Vector3(-600, -340, 0), 10 * Time.deltaTime);
            yield return null;
        }
    }

    /// <summary>
    /// 任務中狀態切換為任務完成：對話前執行
    /// </summary>
    private void Missioning()
    {
        if (count >= data.count) data._NPCState = NPCState.Finish;
    }

}
