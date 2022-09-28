using System.Collections;
using System.Collections.Generic;
using Doublsb.Dialog;
using UnityEngine;

public class ButtonControl : MonoBehaviour
{

    public DialogManager DialogManager;

    public GameObject[] Example;

    private void Start() {
        ShowDialog();
    }
    /*
     
     引數: 內容 /人名 / callback/能否略過/能否倒退(只有第一句是false)其他句子都不要動第五個參數
     */
    public void ShowDialog()
    {
        var dialog01 = new List<DialogData>();

        dialog01.Add(new DialogData("/size:up/Hi, /size:init/my name is Li.", "Li",null,false,false));

        dialog01.Add(new DialogData("I am Sa. Popped out to let you know Asset can show other characters.", "Sa", null, true));
        
        dialog01.Add(new DialogData("This Asset, The D'Dialog System has many features.", "Li",null,true));

        dialog01.Add(new DialogData("You can easily change text /color:red/color, /color:white/and /size:up//size:up/size/size:init/ like this.", "Li", () => ShowPic(0)));

        dialog01.Add(new DialogData("Just put the command in the string!", "Li", () => ShowPic(1)));

        dialog01.Add(new DialogData("You can also change the character's sprite /emote:Sad/like this, /click//emote:Happy/Smile.", "Li",  () => ShowPic(2)));

        dialog01.Add(new DialogData("If you need an emphasis effect, /wait:0.5/wait... /click/or click command.", "Li", () => ShowPic(3)));

        dialog01.Add(new DialogData("Text can be /speed:down/slow... /speed:init//speed:up/or fast.", "Li", () => ShowPic(4)));

        dialog01.Add(new DialogData("You don't even need to click on the window like this.../speed:0.1/ tada!/close/", "Li", () => ShowPic(5)));

        dialog01.Add(new DialogData("/speed:0.1/AND YOU CAN'T SKIP THIS SENTENCE.", "Li", () => ShowPic(6), false));

        dialog01.Add(new DialogData("And here we go, the haha sound! /click//sound:haha/haha.", "Li", null, false));

        dialog01.Add(new DialogData("That's it! Please check the documents. Good luck to you.", "Sa"));

        DialogManager.Show(dialog01);
    }

    private void ShowPic(int index)
    {
        Example[index].SetActive(true);
    }
}
