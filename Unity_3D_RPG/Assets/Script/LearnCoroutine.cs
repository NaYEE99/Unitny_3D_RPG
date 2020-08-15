
using UnityEngine;
using System.Collections;   //使用 協同程序 需要此 API


public class LearnCoroutine : MonoBehaviour
{

    public Transform kyle;

    //協程需使用事件來啟動，ex: Start。
    private void Start()
    {

        // 啟動協程 - 基礎
        StartCoroutine(Test());

        // 啟動協程 - 延伸 - 變形 Scale
        StartCoroutine(Big());


    }

    // 定義簡易協程： 
    // 傳回類型必須是 IEnumerator (傳回時間)
    public IEnumerator Test()
    {
        print("安安");

        // 延遲後繼續執行以下程式
        // 時間單位：秒
        yield return new WaitForSeconds(1);

        print("你好");

        yield return new WaitForSeconds(1);

        print("幾歲");

        yield return new WaitForSeconds(1);

        print("住哪");

        yield return new WaitForSeconds(1);

        print("給約嗎");

        yield return new WaitForSeconds(1);

        //可使用 yield 作為時間分隔，可重複使用。
    }


    //基礎範例結束

    //----------我是分隔線 \(OwO)/ ----------//

    //基礎延伸開始

    public IEnumerator Big()
    {
        for (int i = 0; i < 10; i++)
        {
            
            // 設定物件的大小為每次迴圈+1三圍
            // += 指定運算子(每次+1+1+1)，可使用在任何運算上( -=、*=、/= 同理)

            // 物件.指定為比例 累加 針對三維向量.1 *任意浮點數;
            kyle.localScale += Vector3.one * 1.5f;

            yield return new WaitForSeconds(0.2f);

        }

    }









}
