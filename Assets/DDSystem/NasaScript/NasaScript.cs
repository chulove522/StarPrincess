using System;
using System.Collections;
using System.Collections.Generic;
using Doublsb.Dialog;
using UnityEngine;

public class NasaScript : MonoBehaviour {

    public DialogManager DialogManager;

    public GameObject[] showSomething;
    public GameObject showTrophies;



    private void Start() {
        ShowDialog(MainGameController.getDialog);
    }
    /*
     
     引數: 內容 /人名 / callback/能否略過/能否倒退(只有第一句是false)其他句子都不要動第五個參數
    A:玩家 B:老闆 C:旁白
     */
    public void ShowDialog(int DialogueNum) {
        switch (DialogueNum) {
            case 1: //旁白->星球製作
                opening();
                break;

            case 2: //做好星球,比對的畫面,並且出發(切換至travel場景)
                Bosstalk01();
                break;
            case 3: //出發後，切換到travel之後展開第一次對話
                Bosstalk02();
                break;
            case 4: //觸發game01後回來
                Gametalk01();
                break;
            case 5: //觸發game02後回來
                Gametalk02();
                break;
            case 6: //觸發game03後回來
                Gametalk03();
                break;
            case 7: //觸發game04後回來
                Gametalk04();
                break;
            case 8: //返回地球
                Ending();
                break;

            default:
                Debug.LogError("dialogue 數字設定有錯");
                opening();
                break;

        }
    }

    
    private void opening() {
        var dialog01 = new List<DialogData>();

        dialog01.Add(new DialogData("/size:up/故事發生在未來的某一年...，", "C", null, false, false));
        dialog01.Add(new DialogData("卡靈頓事件即將爆發", "C", () => ShowThings(2)));
        dialog01.Add(new DialogData("來自太陽的高能量帶電粒子，不僅會在地球高緯度地區形成美麗的極光，也可能帶來具有強大破壞力的地磁風暴", "C"));

        dialog01.Add(new DialogData("恆星自轉速率越快，黑子的覆蓋率也會越高，最終導致「超級閃焰」的生成。", "C"));
        dialog01.Add(new DialogData("恆星爆發超級閃焰（solar flares），它們會忽然變得極其明亮，亮度比平常提高 20 倍、光度增加 1000 倍，最少持續幾個小時～一個星期。", "C"));
        dialog01.Add(new DialogData("下個世紀的人們就可能不幸遇上，屆時，所有電子設備都可能被高能輻射摧毀", "C"));
        dialog01.Add(new DialogData("你將背負人類平安的未來，搭乘飛船到宇宙", "C"));
        dialog01.Add(new DialogData("為了獲取神奇力量的星星，你必須來到恆星去冒險。", "C"));
        dialog01.Add(new DialogData("去恆星上，尋找足以抗衡超級閃焰的超級力量", "C"));
        dialog01.Add(new DialogData("收集12神奇力量的星星以保護地球", "C"));
        dialog01.Add(new DialogData("拯救全人類...", "C"));

        dialog01.Add(new DialogData("那?，你想去哪個恆星呢", "B"));
        dialog01.Add(new DialogData("我根本不知道有哪些恆星，只知道太陽而已…", "A"));
        dialog01.Add(new DialogData("那麼你可以自己設計一個恆星呢", "B"));
        dialog01.Add(new DialogData("設計…？是什麼意思", "A",()=>ShowThings(2,false)));
        dialog01.Add(new DialogData("你看看這個面板。顏色代表者恆星的溫度，不同溫度恆星有著不同顏色。", "B",() => ShowThings(0)));
        dialog01.Add(new DialogData("接著請你自己調整看看吧", "B"));
        dialog01.Add(new DialogData("我會幫你從12星座中，選擇最適配去的星球", "B"));
        dialog01.Add(new DialogData("好吧我試試看", "A", () => ShowThings(1),false));

        /*
        dialog01.Add(new DialogData("", "A"));
        dialog01.Add(new DialogData("", "B"));
        */
        /*
         * 特效示範
        dialog01.Add(new DialogData("You can easily change text /color:red/color, /color:white/and /size:up//size:up/size/size:init/ like this.", "C", () => ShowPic(0)));
        dialog01.Add(new DialogData("You can also change the character's sprite /emote:Sad/like this, /click//emote:Happy/Smile.", "Li", () => ShowPic(2)));

        dialog01.Add(new DialogData("", "C"));
        dialog01.Add(new DialogData("If you need an emphasis effect, /wait:0.5/wait... /click/or click command.", "Li", () => ShowPic(3)));

        dialog01.Add(new DialogData("Text can be /speed:down/slow... /speed:init//speed:up/or fast.", "Li", () => ShowPic(4)));

        dialog01.Add(new DialogData("You don't even need to click on the window like this.../speed:0.1/ tada!/close/", "Li", () => ShowPic(5)));

        dialog01.Add(new DialogData("/speed:0.1/AND YOU CAN'T SKIP THIS SENTENCE.", "Li", () => ShowPic(6), false));

        dialog01.Add(new DialogData("And here we go, the haha sound! /click//sound:haha/haha.", "Li", null, false));

        dialog01.Add(new DialogData("That's it! Please check the documents. Good luck to you.", "Sa"));
        */
        DialogManager.Show(dialog01);
    }

    private void Bosstalk01() {
        var dialog02 = new List<DialogData>();
        dialog02.Add(new DialogData("我做好了!", "A"));
        dialog02.Add(new DialogData("哦~我看看，根據你設定的這個溫度跟尺寸呢...", "B"));
        dialog02.Add(new DialogData("不是很像這個星座的這個恆星嘛?...", "B", ()=> ShowThings(3,true)));
        dialog02.Add(new DialogData("诶~ /speed:0.1/ 有...嗎....", "A"));
        dialog02.Add(new DialogData("那就~ /color:red/ 事不宜遲,準備出發吧!", "A", () => ShowThings(4) ,false)); 

        DialogManager.Show(dialog02);
        
    }

    private void Bosstalk02() {
        var dialog02 = new List<DialogData>();
        dialog02.Add(new DialogData("這是到星球上的劇情", "A"));
        dialog02.Add(new DialogData("這是到星球上的劇情", "B", ()=>ShowThings(5)));
    }

    /*
     這邊 ShowThings 
        第0個 星球製作操作說明圖
        第1個 跳轉scene的按鈕
        第2個 太陽閃焰的rawimage(為了關閉他)
        第3個 12星座恆星比對結果
        第4個 那就出發吧!按鈕
        5
     */
    private void ShowThings(int index, bool isShow = true) {
        showSomething[index].SetActive(isShow);
    }
    private void Gametalk04() {
        var dialog02 = new List<DialogData>();
        dialog02.Add(new DialogData("這是到遊玩後的的劇情", "A"));
    }

    private void Gametalk03() {
        var dialog02 = new List<DialogData>();
        dialog02.Add(new DialogData("這是到遊玩後的的劇情", "A"));
    }

    private void Gametalk02() {
        var dialog02 = new List<DialogData>();
        dialog02.Add(new DialogData("這是到遊玩後的的劇情", "A"));
    }

    private void Gametalk01() {
        var dialog02 = new List<DialogData>();
        dialog02.Add(new DialogData("這是到遊玩後的的劇情", "A"));
    }

    private void Ending() {
        var dialog02 = new List<DialogData>();
        dialog02.Add(new DialogData("這是到結束的劇情", "A"));
    }

    public void ShowTrophies() {


    }
}
