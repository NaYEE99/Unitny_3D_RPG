

using UnityEngine;

public class Player : MonoBehaviour
{

    #region 欄位區域

    [Header("速度"), Range(0, 100)]
    public float speed = 1;
    [Header("旋轉速度"), Range(0, 100)]
    public float turn = 1;

    private float attack = 10;
    private float hp = 100;
    private float mp = 100;
    private float exp;
    private int lv = 1;

    private Rigidbody rig;
    private Animator ani;
    private AudioSource aud;
    private Transform cam;      // 攝影機根物件

    private NPC npc;
    [Header("傳送門：0 = N-1,1 = N-2")]
    public Transform[] doors;


    // 使此欄位在屬性面板上隱藏
    [HideInInspector]
    /// <summary>
    /// 停止使玩家不能移動
    /// </summary>
    public bool stop = false;   // 給 NPC 進行控制

    // 欄位區域結束
    #endregion


    //==========我是分隔線ˊˇˋ==========//


    #region 事件區域

    private void Awake()    // Awake：啟動序列較優先，會在程式開始後立刻執行
    {
        rig = GetComponent<Rigidbody>();        // 使用API：GetComponent<泛型：任何物件類型>，抓取此物件上的同類型物件

        ani = GetComponent<Animator>();         // 抓取 Animator 給此C#使用

        cam = GameObject.Find("攝影機根物件").transform;  // 在場景直接搜尋("")中的物件

        aud = GetComponent<AudioSource>();      // 抓取聲音來源

        npc = FindObjectOfType<NPC>();
    }

    
    private void Update()
    {   // 無物理運算，可放置在Update，不會造成效能負擔
        Attack();
    }

    
    private void FixedUpdate()
    {   // FixUpdate 為官方建議使用，會延遲 Update 一個影格的時間持續執行
        if (stop == true) return;    // 假設stop啟用，則跳過移動。

        Move();     // 呼叫移動
    }

    /// <summary>
    /// 碰觸到任務道具時，消除道具，計數器+1
    /// </summary>
    /// <param name="collision">標籤為：能量罐罐</param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "能量罐罐") GetProp(collision.gameObject);
    }

    /// <summary>
    /// 傳送系統：中央對其他
    /// </summary>
    /// <param name="other">任何物件</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Particle System - 傳送門 N-1")
        {
            transform.position = doors[1].position;                     // 傳送至 N-2傳送門
            doors[1].GetComponent<CapsuleCollider>().enabled = false;   // 先關閉 N-2碰撞器
            Stop();                     // 使玩家暫時不能移動
            Invoke("ReStop", 3);        // 操控權恢復
            Invoke("OpenDoorN2", 5);                                    // 延遲 5秒後重啟
        }
        if (other.name == "Particle System - 傳送門 N-2")
        {
            transform.position = doors[0].position;                     // 同上功能
            doors[0].GetComponent<CapsuleCollider>().enabled = false;
            Stop();
            Invoke("ReStop", 3);
            Invoke("OpenDoorN1", 5);
        }
    }


    // 事件區域結束
    #endregion


    //==========我是分隔線ˊˇˋ==========//


    #region 方法區域

    /// <summary>
    /// 玩家移動限制器
    /// </summary>
    private void Stop()
    {
        stop = true;
    }
    private void ReStop()
    {
        stop = false;
    }
       
    /// <summary>
    /// 開啟指定傳送門的碰撞器
    /// </summary>
    private void OpenDoorN1()
    {
        doors[0].GetComponent<CapsuleCollider>().enabled = true;
    }
    private void OpenDoorN2()
    {
        doors[1].GetComponent<CapsuleCollider>().enabled = true;
    }

    /// <summary>
    /// 移動方法：使用 Input.GetAxis("Vertical"、"Horizontal")來達成鍵位偵測
    /// </summary>
    private void Move()
    {
        float v = Input.GetAxis("Vertical");                            // Vertical為讀取玩家的↑↓ＷＳ的鍵位  
        float h = Input.GetAxis("Horizontal");                          // Horizontal為讀取玩家的→←ＡＤ的鍵位  
        Vector3 pos = cam.forward * v + cam.right * h;                  // 移動座標pos = 攝影機.前方 * v + 攝影機.右方 * h
        rig.MovePosition(transform.position + pos * speed / 8);        // 移動座標(原本座標 + pos 乘上 速度)

        ani.SetFloat("移動", Mathf.Abs(v) + Mathf.Abs(h));              // 設定ani的浮點數為("移動", 絕對值v, 絕對值h)


        if (v != 0 || h !=0)
        {
            pos.y = 0;      // 設定座標 y 固定不動，以防人物模型傾斜
            Quaternion angle = Quaternion.LookRotation(pos);                            // B 角度angle = 面向角度
            transform.rotation = Quaternion.Slerp(transform.rotation, angle, turn);     // A 角度 = 角度.插值(A角度, B角度, 旋轉速度)
        }



    }


    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ani.SetTrigger("攻擊觸發");
        }
    }


    private void Skill()
    {

    }


    private void Hit()
    {

    }


    private void Dead()
    {

    }

    /// <summary>
    /// 獲取物品：消除物品、更新 NPC計數器
    /// </summary>
    /// <param name="prop">能量罐罐</param>
    private void GetProp(GameObject prop)
    {
        Destroy(prop);
        //aud.PlayOneShot(soundProp);
        npc.UpdateTextMission();
    }


    private void Exp()
    {

    }


    // 方法區域結束
    #endregion



}
