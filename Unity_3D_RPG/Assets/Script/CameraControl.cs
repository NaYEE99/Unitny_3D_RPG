
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [Header("目標")]
    public Transform target;
    [Header("速度"), Range(0, 100)]
    public float speed = 1;
    [Header("旋轉速度"), Range(0, 100)]
    public float turn = 1;
    [Header("上下角度限制")]
    public Vector2 limit = new Vector2(-30, 35);


    /// <summary>
    /// 滑鼠追蹤：
    /// </summary>
    private Quaternion rot;

    /// <summary>
    /// 追蹤人物：跟隨鎖定的人物
    /// </summary>
    private void Track()
    {
        Vector3 posA = transform.position;                          // 設定 A點為攝影機本身
        Vector3 posB = target.position;                             // 設定 B點為目標
        posA = Vector3.Lerp(posA, posB, Time.deltaTime * speed);    // A點則為 = (A點往B點逐漸接近)
        transform.position = posA;                                  // 設定 攝影機座標為 更新後的A點

        // 攝影機旋轉部分
        rot.x += Input.GetAxis("Mouse Y") * turn;                   // 取得滑鼠上下來控制 X角度
        rot.y += -Input.GetAxis("Mouse X") * turn;                  // 取得滑鼠左右來控制 Y角度

        rot.x = Mathf.Clamp(rot.x, limit.x, limit.y);               // 限制 x 在 (-30,35)5 之間

        transform.rotation = Quaternion.Euler(rot.x, rot.y, rot.z); // 攝影機根物件.角度 = 歐拉角(x, y, z)
    }

    

    private void LateUpdate()       //LateUpdate：延遲 Update一個影格執行
    {
        Track();
    }














}
