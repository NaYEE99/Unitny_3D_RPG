using UnityEngine;
using UnityEngine.AI;       // NavMesh引用

public class Enemy_Purple : MonoBehaviour
{ 
    [Header("移動速度"), Range(0.1f, 3)]
    public float speed = 2.5f;
    [Header("攻擊力"), Range(35f, 50f)]
    public float attack = 40f;
    [Header("血量"), Range(200, 300)]
    public float hp = 200f;
    [Header("怪物經驗值"), Range(30, 100)]
    public float exp = 30;
    [Header("攻擊停止距離"), Range(0.1f, 5f)]
    public float distanceAttack = 1.5f;

    private Transform player;       // 玩家座標
    private Animator ani;           // 動畫控制器

    private NavMeshAgent nav;       // 代理器欄位



    private void Awake()
    {
        player = GameObject.Find("Robot Kyle").transform;   // 尋找玩家.座標

        ani = GetComponent<Animator>();                     // 取得動畫控制器
        nav = GetComponent<NavMeshAgent>();                 // 取得該物件的導覽代理器
        nav.speed = speed;
    }

    private void Update()
    {
        Move();
    }

    // = = = = = 分隔仔是我 \(OwO)/

    private void Move()
    {
        nav.SetDestination(player.position);            // 追蹤玩家座標
        ani.SetFloat("移動", nav.velocity.magnitude);   // 設定移動動畫 導覽器.加速度.數值
    }




}
