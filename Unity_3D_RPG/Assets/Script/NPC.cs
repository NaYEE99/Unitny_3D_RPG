
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

    private AudioSource aud;

    /// <summary>
    /// NPC 對話系統
    /// </summary>
    public void Dialog()
    {
        panelDialog.SetActive(true);        // SetActive 使[]設定為顯示
        textName.text = name;               // 名稱內的文字 = 當前物件的名稱
        textContent.text = data.dialogs[0]; // 內容內的文字 = NPC 資料內的陣列[0]之內容

    }

    private IEnumerator Print()
    {
        string dialog = data.dialogs[0];    // 對話內容 = NPC 資料.對話第一段
        textContent.text = "";              // 清空字串

        for (int i = 0; i < data.dialogs.Length; i++)   // i < dialog.Length 
        {                                               // 到此字串.最後一個字元

            textContent.text += dialog[i];              // 對話內容.文字 += 對話[]的字元
            aud.PlayOneShot(soundPrint, 0.5f);
            yield return new WaitForSeconds(printSpeed); 

        }


    }

    private void Awake()
    {
        aud = GetComponent<AudioSource>();
    }



    private void OnTriggerEnter(Collider other)     // ote：剛體碰撞器(其他剛體)
    {
        if (other.name == "Robot Kyle") Dialog();   // 如果碰撞到""內的指定物件，執行...

    }


}
