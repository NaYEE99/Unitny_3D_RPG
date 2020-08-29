

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

    private Transform cam;      // 攝影機根物件

    // 使此欄位在屬性面板上隱藏
    [HideInInspector]
    /// <summary>
    /// 停止使玩家不能移動
    /// </summary>
    public bool stop = false;

    // 欄位區域結束
    #endregion


    //==========我是分隔線ˊˇˋ==========//


    #region 事件區域

    private void Awake()    // Awake：啟動序列較優先，會在程式開始後立刻執行
    {
        rig = GetComponent<Rigidbody>();        // 使用API：GetComponent<泛型：任何物件類型>，抓取此物件上的同類型物件

        ani = GetComponent<Animator>();         // 抓取 Animator 給此C#使用

        cam = GameObject.Find("攝影機根物件").transform;  // 在場景直接搜尋("")中的物件
    }

    // FixUpdate 為官方建議使用，會延遲 Update 一個影格的時間持續執行
    private void FixedUpdate()
    {
        if (stop == true) return;    // 假設stop啟用，則跳過移動。

        Move();     // 呼叫移動
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "能量罐罐") GetProp(collision.gameObject);
    }


    // 事件區域結束
    #endregion


    //==========我是分隔線ˊˇˋ==========//


    #region 方法區域

    /// <summary>
    /// 移動方法：使用 Input.GetAxis("Vertical"、"Horizontal")來達成鍵位偵測
    /// </summary>
    private void Move()
    {
        float v = Input.GetAxis("Vertical");                            // Vertical為讀取玩家的↑↓ＷＳ的鍵位  
        float h = Input.GetAxis("Horizontal");                          // Horizontal為讀取玩家的→←ＡＤ的鍵位  
        Vector3 pos = cam.forward * v + cam.right * h;                  // 移動座標pos = 攝影機.前方 * v + 攝影機.右方 * h
        rig.MovePosition(transform.position + pos * speed / 10);        // 移動座標(原本座標 + pos 乘上 速度)

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


    private void GetProp(GameObject prop)
    {
        Destroy(prop);
    }


    private void Exp()
    {

    }


    // 方法區域結束
    #endregion




}
