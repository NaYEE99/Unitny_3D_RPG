
using UnityEngine;

public class DayControl : MonoBehaviour
{
    // 教學來源：2019.3 - https://www.youtube.com/watch?v=H3JpkcGi8DI
    // 設定一天的時間值域，因為一天是24小時ˊˇˋ
    [Header("當前時間軸"),Range(0, 24)]
    public float timeOfDay;
   

    // 設定Light系統中要丟的物件(光源)(太陽)(月亮)
    public Light sun;
    public Light moom;      //共需要兩個光源
    
    [Header("移動軌跡的速度")]
    public float orbitSpeed;

    // 確認是否進入夜晚
    private bool isNight;



    /// <summary>
    /// 持續更新資訊：
    /// </summary>
    void Update()
    {

        // 一天時間 累加 由軌跡速度控制
        timeOfDay += Time.deltaTime * orbitSpeed;

        // 當時間 > 24時，reset時間為0
        if (timeOfDay > 24)
            timeOfDay = 0;

        //持續更新 UpdateTime內的程式
        UpdateTime();

    }

    
    private void OnValidate()
    {
        UpdateTime();
    }


    
    private void UpdateTime()
    {

        #region 太陽設定區
        // 設定一個浮點數 α = 一天時間 除以24 (用於計算)
        // 設定一個浮點數 太陽的角度 = Mathf.Lerp -90~270範圍內，時間進度(α/24)的比例調整太陽位置
        float alpha = timeOfDay / 24.0f;
        float sunRotation = Mathf.Lerp(-90, 270, alpha);

        // 使光源.座標訊息.角度 = Quaternion.Euler 內(太陽角度, Y角度, Z角度)(X角度是控制光源亮暗的)
        sun.transform.rotation = Quaternion.Euler(sunRotation, -150.0f, 0);
        #endregion


        #region 月亮設定區
        // 此為設定月亮，且設定參考值為太陽的對角 (360°-180°的概念)
        float moomRotation = sunRotation - 180;
        // 同上為一樣的設置，只是由 sun 改為 moom
        moom.transform.rotation = Quaternion.Euler(moomRotation, -150.0f, 0);
        #endregion

        // 檢查兩光源陰影是否同時出現，故在此方法中同時進行更新
        CheckNightDayTransistion();
    }


    #region 光影效果檢查區

    // 檢查兩光源的陰影效果(Shadow)，使兩個不會同時開啟
    private void CheckNightDayTransistion()
    {
        
        if (isNight=true)   // 如果已經進入夜晚               
        {
           
            if (moom.transform.rotation.eulerAngles.x >180)     // 且月亮的 X座標 已大於180時
            {                
                StartDay();         // 切換至白天開始
            }
        }

        else                // 其餘=進入白天
        {
            
            if (sun.transform.rotation.eulerAngles.x > 180)     // 且太陽的 X座標 已大於180時
            {                
                StartNight();       // 切換至夜晚開始
            }
        }
    }

    // 白天開始的方法
    private void StartDay()
    {       
        isNight = false;                        // 當沒進入夜晚時       
        sun.shadows = LightShadows.Soft;        // 開啟太陽的陰影  
        moom.shadows = LightShadows.None;       // 關閉月亮的陰影
    }

    // 夜晚開始的方法
    private void StartNight()
    {       
        isNight = true;                         // 當進入夜晚時       
        sun.shadows = LightShadows.None;         // 關閉太陽的陰影      
        moom.shadows = LightShadows.Soft;        // 開啟月亮的陰影
    }
    #endregion

}
