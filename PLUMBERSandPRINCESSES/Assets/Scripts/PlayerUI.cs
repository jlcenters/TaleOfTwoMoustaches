using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    public TextMeshProUGUI lvlTxt;
    public TextMeshProUGUI inventoryTxt;
    public TextMeshProUGUI interactTxt;
    public Image hpFill;
    public Image xpFill;

    private Player player;





    private void Awake()
    {
        player = FindObjectOfType<Player>();   
    }

    public void UpdateLvlTxt()
    {
        lvlTxt.text = "LVL\n" + player.lvl;
    }

    public void UpdateHp()
    {
        hpFill.fillAmount = (float)player.hp / (float)player.hpPool;
    }

    public void UpdateXp()
    {
        xpFill.fillAmount = (float)player.xp / (float)player.xpToNextLvl;
    }

    public void SetInteractTxt(Vector3 pos, string txt)
    {
        interactTxt.gameObject.SetActive(true);
        interactTxt.text = txt;

        interactTxt.transform.position = Camera.main.WorldToScreenPoint(pos + Vector3.up);
    }

    public void DisableInteractTxt()
    {
        if(interactTxt.gameObject.activeInHierarchy)
        {
            interactTxt.gameObject.SetActive(false);
        }
    }

    public void UpdateInventoryTxt()
    {
        inventoryTxt.text = "";

        foreach(string item in player.inventory)
        {
            inventoryTxt.text += item + "\n";
        }
    }
}
