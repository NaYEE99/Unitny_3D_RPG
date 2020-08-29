using UnityEngine;

// 列舉：下拉式選單，可隨意放置
public enum NPCState
{
    NoMission, Missioning, Finish   // 設定3種資料模式(no ,ing ,ed)，可自訂
}

// ScriptableObject 腳本化物件：可儲存於專案的資料
[CreateAssetMenu(fileName = "NPC 資料" ,menuName = "NaYEE/NPC 資料")]
public class NPCData : ScriptableObject
{
    [Header("NPC 狀態")]     // NPCState.指定為no模式(若不加上 = 指定，則預設值為指定第一項)
    public NPCState _NPCState = NPCState.NoMission;

    [Header("任務需求數量")]
    public int count;

    [Header("任務未接受、任務進行中、任務完成")]    // string[] 為建立一個字串陣列 
    public string[] dialogs = new string[3];


}
