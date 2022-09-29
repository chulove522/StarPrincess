using UnityEngine;
using UnityEngine.UI;

public class StarSign : MonoBehaviour {
    public GameObject stellar;
    public Text color;
    public Text size;
    public Text introductionText;
    public string Name;
    private bool _clear=false;
    private bool _isfront = false; //防止星座前進2次導致顯示錯誤

    public bool isFront => _isfront;
    public bool getClear => _clear;
    public void setClear(bool c) { _clear = c; }
    public void setFront(bool f) { _isfront = f; }

    
    public void hideAll() {
        stellar.SetActive(false);
    }

}
