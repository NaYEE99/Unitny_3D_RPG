using UnityEngine;

public class LearnLoop : MonoBehaviour
{
    // 基礎用
    // 範例用整數
    public int J = 1;
    public int K = 3;

    // 延伸用
    // 立方體物件指定區
    public Transform cube;

    

    private void Start()
    {
        // while功能：連續執行回圈內的程式，需要手動設定停止條件。

        // while 迴圈僅能在Start內執行，如果在Update會直接死掉沒救準備炸電腦 (OwO)b
        while (J <= 3)
        {
            print("成功執行加法" + J );

            print(J + "次");

            J++;
        }
        
        while (K >= 1)
        {
            print("成功執行減法" + K);
            print("次");

            K--;
        }

        // for功能：連續執行回圈內的程式，一定要手動設定停止條件。

        // for 迴圈僅能在Start內執行，如果在Update會直接死掉沒救準備炸電腦 (OwO)b
        for (int i = 0; i <= 3; i++)
        {

            print("成功執行佛法" + i);
            print("次");
        }

        // 基礎範例結束

        //----------我是分隔線(OwO )----------//

        // 基礎延伸開始

        
        // 指定 特定座標 生成 指定方塊
        for (int i = 0; i < 10; i++)
        {
            //給出一個三維座標，來指定方塊生成的位置， pos為簡寫，可自訂義。
            Vector3 pos = new Vector3(i, 0, 0);      
            
            //生成(物件, 座標, 角度)
            Instantiate(cube, pos, Quaternion.identity);

            //Quaternion.identity 為零角度，即生成時不會產生任何角度旋轉。
        }


        // 自主練習 延伸再延伸

        for (int i = 0; i < -10; i--)
        {
            //給出一個三維座標，來指定方塊生成的位置， pos為簡寫，可自訂義。
            Vector3 pos = new Vector3(i, i, i);

            //生成(物件, 座標, 角度)
            Instantiate(cube, pos, Quaternion.identity);

            //Quaternion.identity 為零角度，即生成時不會產生任何角度旋轉。

            //如果要保留生成的物件，可使用 in Run in Editor 這個API

        }


    }




}
