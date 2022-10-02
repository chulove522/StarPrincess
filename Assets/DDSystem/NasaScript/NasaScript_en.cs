using System;
using System.Collections;
using System.Collections.Generic;
using Doublsb.Dialog;
using UnityEngine;

public class NasaScript_en : MonoBehaviour {

    public DialogManager DialogManager;

    public GameObject[] showSomething;
    public GameObject showTrophies;



    private void Start() {
        // TODO
        ShowDialog((int)MainGameController.getDialog());
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
        //dialog01.Add(new DialogData("/size:up/故事發生在未來的某一年...，", "C", null, false, false));
        //dialog01.Add(new DialogData("卡靈頓事件即將爆發", "C", () => ShowThings(2)));
        dialog01.Add(new DialogData("/size:up/The story takes place in a future year...", "C", null, false, false));
        dialog01.Add(new DialogData("when the Carrington incident is about to breakout.", "C", () => ShowThings(2)));
        //dialog01.Add(new DialogData("來自太陽的高能量帶電粒子，不僅會在地球高緯度地區形成美麗的極光，也可能帶來具有強大破壞力的地磁風暴", "C"));
        dialog01.Add(new DialogData("High-energy charged particles from the sun will not only form beautiful auroras in the high latitudes of the earth", "C"));
        dialog01.Add(new DialogData("but also may bring powerful and destructive geomagnetic.", "C"));

        //dialog01.Add(new DialogData("恆星自轉速率越快，黑子的覆蓋率也會越高，最終導致「超級閃焰」的生成。", "C"));
        dialog01.Add(new DialogData("The faster the rotation rate of the stellar, the higher the coverage rate of the sunspot will be, ", "C"));
        dialog01.Add(new DialogData("eventually leading to the generation of 'superflare.'", "C"));
        //dialog01.Add(new DialogData("恆星爆發超級閃焰（solar flares），它們會忽然變得極其明亮，亮度比平常提高 20 倍、光度增加 1000 倍，最少持續幾個小時～一個星期。", "C"));
        dialog01.Add(new DialogData("Stellar emits superflare, which suddenly becomes extra bright, 20 times lighter," , "C"));
        dialog01.Add(new DialogData("and 1,000 times more luminous than usual, for at least a few hours to a week.", "C"));
        //dialog01.Add(new DialogData("下個世紀的人們就可能不幸遇上，屆時，所有電子設備都可能被高能輻射摧毀", "C"));
        dialog01.Add(new DialogData("People in the next century may unfortunately encounter, then, all electronic equipment might be destroyed by high-energy radiation.", "C"));
        //dialog01.Add(new DialogData("你將背負人類平安的未來，搭乘飛船到宇宙", "C"));
        dialog01.Add(new DialogData("You will carry the future of human peace on your back and take a spaceship to the universe.", "C"));
        //dialog01.Add(new DialogData("為了獲取神奇力量的星星，你必須來到恆星去冒險。", "C"));
        dialog01.Add(new DialogData("To obtain the magical power of the stars, you must come to the stars to adventure.", "C"));
        //dialog01.Add(new DialogData("去恆星上，尋找足以抗衡超級閃焰的超級力量", "C"));
        dialog01.Add(new DialogData("Go to the stars and find the superpower to withstand the superflare", "C"));
        dialog01.Add(new DialogData("收集12神奇力量的星星以保護地球, 拯救全人類...", "C"));
        dialog01.Add(new DialogData("Collect 12 magical powers of the stars to protect the Earth, and save all humanity...", "C"));

        //dialog01.Add(new DialogData("那?，你想去哪個恆星呢", "B"));
        dialog01.Add(new DialogData("So, which star do you want to go to?", "B"));
        //dialog01.Add(new DialogData("我根本不知道有哪些恆星，只知道太陽而已…", "A"));
        dialog01.Add(new DialogData("I don't even know what stars there are, and what I only know is the sun.", "A"));
        //dialog01.Add(new DialogData("那麼你可以自己設計一個恆星呢", "B"));
        dialog01.Add(new DialogData("In that way, you can design a star yourself!", "B"));
        //dialog01.Add(new DialogData("設計…？是什麼意思", "A",()=>ShowThings(2,false)));
        dialog01.Add(new DialogData("Design...? What do you mean?", "A",()=>ShowThings(2,false)));
        //dialog01.Add(new DialogData("你看看這個面板。顏色代表者恆星的溫度，不同溫度恆星有著不同顏色。", "B",() => ShowThings(0)));
        dialog01.Add(new DialogData("Look at this panel. The color represents the temperature of the star,", "B"));
        dialog01.Add(new DialogData("and different temperature stars have different colors.", "B",() => ShowThings(0)));
        //dialog01.Add(new DialogData("接著請你自己調整看看吧", "B"));
        dialog01.Add(new DialogData("Next, please adjust it yourself.", "B"));
        //dialog01.Add(new DialogData("我會幫你從12星座中，選擇最適配去的星球", "B"));
        dialog01.Add(new DialogData("I will help you choose the most suitable planet for you from the 12 signs of the constellation.", "B"));
        //dialog01.Add(new DialogData("好吧我試試看", "A", () => ShowThings(1),false));
        dialog01.Add(new DialogData("Ok! Let me try.", "A", () => ShowThings(1),false));

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
        // dialog02.Add(new DialogData("我做好了!", "A"));
        dialog02.Add(new DialogData("I've done!", "A"));
        // dialog02.Add(new DialogData("哦~我看看，根據你設定的這個溫度跟尺寸呢...", "B"));
        dialog02.Add(new DialogData("Oh~, let me see, according to the temperature and size you set...", "B"));
        // dialog02.Add(new DialogData("不是很像這個星座的這個恆星嘛?...", "B", ()=> ShowThings(3,true)));
        dialog02.Add(new DialogData("Doesn't it look like this star in this constellation?", "B", ()=> ShowThings(3,true)));
        // dialog02.Add(new DialogData("诶~ /speed:0.1/ 有...嗎....", "A"));
        dialog02.Add(new DialogData("Eh~ /speed:0.1/ Are you ... sure? ...", "A"));
        // dialog02.Add(new DialogData("那麼就送你上去這裡吧，沒問題吧？", "B"));
        dialog02.Add(new DialogData("Then let's send you up here, okay?", "B"));
        // dialog02.Add(new DialogData("欸就這麼隨便決定的嗎？", "A"));
        dialog02.Add(new DialogData("Hey, is it just a random decision?", "A"));
        // dialog02.Add(new DialogData("地球的太空總署NASA會協助你發射到恆星去。", "B")); //(火箭發射動畫!!~) 🚀
        dialog02.Add(new DialogData("NASA, Earth's space agency, will help you launch to a star", "B"));
        // dialog02.Add(new DialogData("在那邊，你將面對殘酷的高溫與各種未知挑戰。", "B")); //(讓我們出發吧!!~) 🧑🏻‍🚀
        dialog02.Add(new DialogData("On the other side, you will face brutal high temperatures and various unknown challenges.", "B"));
        // dialog02.Add(new DialogData("那就~ /color:red/ 事不宜遲,準備出發吧!", "A", () => ShowThings(4) ,false)); 
        dialog02.Add(new DialogData("Then, /color:red/ it's not too late. Let's get ready to go!", "A", () => ShowThings(4) ,false)); 

        DialogManager.Show(dialog02);
        
    }

    // 3. 抵達恆星 {A: 玩家, B: 📱}
    private void Bosstalk02() {
        var dialog03 = new List<DialogData>();
        // dialog03.Add(new DialogData("莫名奇妙就被送上來了，長官也太隨便了吧", "A", null, false, false));
        dialog03.Add(new DialogData("I don't know why I transferred here, but the chief is too casual!", "A", null, false, false));
        // dialog03.Add(new DialogData("喂喂，聽得到嗎？", "B"));
        dialog03.Add(new DialogData("Radio check, do you read me?", "B"));
        // dialog03.Add(new DialogData("欸？什麼時候我帶了這個通訊器上來的OAO", "A"));
        dialog03.Add(new DialogData("Hey? When did I bring this communicator up here? OAO", "A"));
        // dialog03.Add(new DialogData("你看一下任務清單，這邊有恆星的地圖，你試著探索一下，尋找看看星星會在哪裡。", "B"));
        dialog03.Add(new DialogData("You look at the list of missions, there is a map of the stars, and you try to explore it, looking to see where the stars will be.", "B"));
        // dialog03.Add(new DialogData("等等，我自己一個人探索嗎？", "A"));
        dialog03.Add(new DialogData("Wait, you want me to explore alone?", "A"));
        // dialog03.Add(new DialogData("你看看附近還有別人嗎？", "B"));
        dialog03.Add(new DialogData("Do you see anyone else around?", "B"));
        // dialog03.Add(new DialogData("...", "A",null, false));
        dialog03.Add(new DialogData("......", "A", null, false));

        DialogManager.Show(dialog03);
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


    /*
        玩家：（左右上下操作）
        ⬇️
        觸發關卡: (2) 恆星本身閃焰或爆炸: 防衛太陽風小遊戲，解鎖🌟🌟🌟
        ⬇️
        */
    private void Gametalk01() {
        var dialog03 = new List<DialogData>();
        // dialog03.Add(new DialogData("如何？探索有一些收穫嗎？", "B", null, false, false));
        dialog03.Add(new DialogData("How was it? Did you have some success in your quest?", "B", null, false, false));
        // dialog03.Add(new DialogData("我剛剛好像進去了異世界玩了什麽防衛太陽風的遊戲….", "A"));
        dialog03.Add(new DialogData("I just seemed to have gone into Isekai to play the defense of the solar wind game...", "A"));
        // dialog03.Add(new DialogData("你是在暗喻什麼嗎？你就在恆星閃焰上啊，不過你的防護衣可以抵禦，沒問題的。", "B"));
        dialog03.Add(new DialogData("Are you alluding to something? You're on a solar flare, but your suit can withstand it. Don't worry about it.", "B"));
        // dialog03.Add(new DialogData("我還偶然間找到了🌟🌟🌟，難道我找到的就是.../wait:0.5/神奇力量星星OAO？", "A"));
        dialog03.Add(new DialogData("I also stumbled upon 🌟🌟🌟🌟🌟. Is it possible that what I found .../wait:0.5/ are the magic power stars?", "A"));
        // dialog03.Add(new DialogData("欸是的就是這個！你運氣也太好了吧？過去一下子就找到了3顆。", "B"));
        dialog03.Add(new DialogData("Hey, yes, These are them! You're so lucky! You found 3 in one visit.", "B"));
        // dialog03.Add(new DialogData("我們本來沒有預期你會找到的。", "B"));
        dialog03.Add(new DialogData("We even didn't expect you to find it.", "B"));
        // dialog03.Add(new DialogData("所以... /speed:down/你們本來預期我是來送死的嗎🙂", "A"));
        dialog03.Add(new DialogData("So..., /speed:down/You were expecting me to die?🙂", "A"));
        // dialog03.Add(new DialogData("咳， /wait:0.5/沒有這個意思🙂", "B"));
        dialog03.Add(new DialogData("Ahem, /wait:0.5/we don't mean that 🙂", "B"));
        // dialog03.Add(new DialogData("那請你繼續探索吧！地球就交給你了啊😇", "B"));
        dialog03.Add(new DialogData("Then please keep exploring! The earth is yours to protect😇", "B"));
        // dialog03.Add(new DialogData("….🙂", "A"));
        dialog03.Add(new DialogData("......🙂", "A"));

        DialogManager.Show(dialog03);
    }

    /*
    ⬇️
    觸發關卡：(3)被其他恆星等遮擋: 移開恆星，發現最亮的那一顆
    ⬇️				
    */

    private void Gametalk02() {
        var dialog03 = new List<DialogData>();

        // dialog03.Add(new DialogData("報告，我又找到了三顆喲。", "A", null, false, false));
        dialog03.Add(new DialogData("Hey, I found three more.", "A", null, false, false));
        // dialog03.Add(new DialogData("太厲害了吧！看來神奇力量星星不是什麼難找的東西啊，早知道我也上去看看了。", "B"));
        dialog03.Add(new DialogData("It's incredible! It seems that the magic power star is not something difficult to find. If I had known it, I would go also.", "B"));
        // dialog03.Add(new DialogData("你不是說預期我會回不來嗎？", "A"));
        dialog03.Add(new DialogData("Didn't you say you expected me not to come back?", "A"));
        // dialog03.Add(new DialogData("等等，/wait:0.5/通訊不太好...", "B"));
        dialog03.Add(new DialogData("Wait, /wait:0.5/communication is not good enough.", "B"));
        // dialog03.Add(new DialogData("明明就很好嘛。", "A"));
        dialog03.Add(new DialogData("Loud and clear.", "A"));
        // dialog03.Add(new DialogData("那個，還剩下6顆🌟要找呢，就麻煩你繼續探索了。", "B", null, false));
        dialog03.Add(new DialogData("There are still 6 🌟 left to find it, so please continue to explore.", "B", null, false));

        DialogManager.Show(dialog03);
    }


    /*
    ⬇️
    走一走
    觸發關卡: (4)恆星的一生
    ⬇️		
    */
    private void Gametalk03() {
        var dialog03 = new List<DialogData>();
        // dialog03.Add(new DialogData("呼，為什麼有那麼多種恆星啦。我到底是來玩Puzzle Bobble還是來拯救世界的啊。", "A", null, false, false));
        dialog03.Add(new DialogData("Oh, why are there so many kinds of stars? I am here to play Puzzle Bobble or to save the world.", "A", null, false, false));
        // dialog03.Add(new DialogData("什麼Puzzle Bobble？你累了嗎？", "B"));
        dialog03.Add(new DialogData("What Puzzle Bobble, you are tired?", "B"));
        // dialog03.Add(new DialogData("嚇死我了，不要偷聽好嗎", "A"));
        dialog03.Add(new DialogData("You scared me, don't eavesdrop, okay?", "A"));
        // dialog03.Add(new DialogData("你說的一切我都聽的一清二楚呢！", "B"));
        dialog03.Add(new DialogData("Everything you said is 'loud and clear.'", "B"));
        // dialog03.Add(new DialogData("不要像變態一樣好嗎QAQ", "A"));
        dialog03.Add(new DialogData("Don't be like a pervert, okay? QAQ", "A"));
        // dialog03.Add(new DialogData("這邊跟你說明，其實恆星有很多種面向，也依照生存年份的長短，質量的增加與損失，呈現不同的Bobble...", "B"));
        dialog03.Add(new DialogData("Here to explain to you is that there are many kinds of stars facing, but also according to the length of survival, ", "B"));
        dialog03.Add(new DialogData("the increase and loss of mass, shows different Bobble...", "B"));
        // dialog03.Add(new DialogData("我是說，樣貌。", "B"));
        dialog03.Add(new DialogData("I mean, the appearance", "B"));
        // dialog03.Add(new DialogData("我看你才累了🙂", "A"));
        dialog03.Add(new DialogData("I think you are the tired one.🙂", "A"));
        // dialog03.Add(new DialogData("就剩最後三個，我等你趕快回來唷❤️", "B"));
        dialog03.Add(new DialogData("Only the last three are left, and I'm waiting for you to come back soon!❤️", "B"));
        // dialog03.Add(new DialogData("太噁心了吧，我要把通訊器關機。", "A", null, false));
        dialog03.Add(new DialogData("It's disgusting. I want to turn off the communicator.", "A", null, false));

        DialogManager.Show(dialog03);
    }


    /*
    ⬇️
    回到玩家控制
    繼續往下走
    觸發關卡：(1) 地球大氣層擾動星光。 (最後一個關卡)		
    ⬇️
    */
    private void Gametalk04() {

        var dialog03 = new List<DialogData>();
        // dialog03.Add(new DialogData("喂喂喂，為什麼不回應啊，你死了嗎？", "B", null, false, false));
        dialog03.Add(new DialogData("Hey, hey, hey, why don't you respond? Are you dead?", "B", null, false, false));
        // dialog03.Add(new DialogData("我只是覺得你很吵，剛剛關機而已。不要隨便詛咒我好嗎…", "A"));
        dialog03.Add(new DialogData("I just thought you were noisy and turned off my communicator. Don't just curse me, okay?", "A"));
        // dialog03.Add(new DialogData("看來是任務完成了啊，如何？", "B"));
        dialog03.Add(new DialogData("It looks like the mission is complete. How is it?", "B"));
        // dialog03.Add(new DialogData("看著光子被折射十分有趣吧，這就是為什麼在地球上看，恆星閃閃發亮呢。", "B"));
        dialog03.Add(new DialogData("Watching photons refracted is very interesting, isn't it? That's why the stars brightly shine when viewed from Earth.", "B"));
        // dialog03.Add(new DialogData("我現在看著恆星也十分的亮…", "A"));
        dialog03.Add(new DialogData("The star I am looking at is also very bright...", "A"));
        // dialog03.Add(new DialogData("我好像忘記跟你說，防護衣能夠持續的時間並不長", "B"));
        dialog03.Add(new DialogData("I forgot to tell you that protective clothing can not last long.", "B"));
        // dialog03.Add(new DialogData("你要是再不回來地球，可能就要葬生火海.../wait:0.5/葬生核聚變之中。", "B"));
        dialog03.Add(new DialogData("If you do not come to Earth, you may bury in the sea of fire .../wait:0.5/ and nuclear fusion.", "B"));
        // dialog03.Add(new DialogData("/speed:down/？？？？？", "A"));
        dialog03.Add(new DialogData("/speed:down/????", "A"));
        // dialog03.Add(new DialogData("怎麼樣，我很幽默吧😉", "B"));
        dialog03.Add(new DialogData("How about that? I'm funny, right? 😉", "B"));
        // dialog03.Add(new DialogData("/speed:down/？？？？？", "A"));
        dialog03.Add(new DialogData("/speed:down/?????", "A"));

        DialogManager.Show(dialog03);
    }
    /*
    ⬇️
    搭乘火箭離開
    */

    // 4. 結尾 {A: 玩家, B: 長官}

    /*
    闖關完畢拯救了地球與人類
    ⬇️
    回到地球
    */

    private void Ending() {

        var dialog04 = new List<DialogData>();


        //dialog04.Add(new DialogData("把哺把哺～～～🎉歡迎你回來，我好開心", "B", null, false, false));
        dialog04.Add(new DialogData("(horn sound)🎉Welcome back, I'm so happy!", "B", null, false, false));
        // dialog04.Add(new DialogData("我感覺被玩弄了，一點都不開心", "A"));
        dialog04.Add(new DialogData("I feel played, not happy at all.", "A"));
        // dialog04.Add(new DialogData("不要這樣嘛。起碼你成為了全人類中，第一個踏上恆星的人，而且還帶回了超級星星拯救人類。", "B"));
        dialog04.Add(new DialogData("Don't be like that. At least you not only became the first person to step on a star but also brought back the superstar to save humid!", "B"));
        // dialog04.Add(new DialogData("這次旅途中，你不但知道了恆星的亮度跟溫度有關，也認識到了xxx星座的xxx星。", "B"));
        dialog04.Add(new DialogData("During this journey, you not only learned that the brightness of a star is related to its temperature but also learned about the star xxx in the constellation xxx.", "B"));
        // dialog04.Add(new DialogData("大氣、閃焰、恆星遮擋、一生變化，其實都分分秒秒的發生在宇宙中的某個角落。", "B"));
        dialog04.Add(new DialogData("The atmosphere, flashing flames, stellar occlusion, and lifelong changes occur in the universe every minute.", "B"));
        // dialog04.Add(new DialogData("主畫面可以看到你獲取的星星列表", "B"));
        dialog04.Add(new DialogData("You can see the list of stars that you have acquired on the main screen.", "B"));
        // dialog04.Add(new DialogData("你也可以選擇再重來一次任務，或許會去到不同的星座旅行喔！", "B"));
        dialog04.Add(new DialogData("You can also start with the mission all over again or travel to a different constellation!", "B"));

        // dialog04.Add(new DialogData("本來被暗物質籠罩的天空", "C", null, false, false));
        dialog04.Add(new DialogData("The sky originally shrouded by dark matter, ", "C", null, false, false));
        // dialog04.Add(new DialogData("在超級星星的力量之下", "C"));
        dialog04.Add(new DialogData("but under the power of superstars, ", "C"));
        // dialog04.Add(new DialogData("地球終於恢復了平靜", "C"));
        dialog04.Add(new DialogData("the Earth finally restored peace.", "C"));
        // dialog04.Add(new DialogData("恭喜你體驗完本次的恆星旅行🚀", "C"));
        dialog04.Add(new DialogData("Congratulations on your experience of the stellar journey!🚀", "C"));
        // dialog04.Add(new DialogData("我們祝福地球和平愉快❤️再見", "C"));
        dialog04.Add(new DialogData("We wish the earth peace and happiness ❤️. See you soon.", "C"));
        // dialog04.Add(new DialogData("人們仰望天空又可以看到美麗的星空了🌃🌟🌠", "C"));
        dialog04.Add(new DialogData("People can look at the sky and see the beautiful starry sky again.", "C"));
        DialogManager.Show(dialog04);

    }

        private void ShowThings(int index, bool isShow = true) {
        showSomething[index].SetActive(isShow);
    }
    public void ShowTrophies() {


    }
}
