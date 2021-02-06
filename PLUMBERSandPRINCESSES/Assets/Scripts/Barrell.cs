using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrell : MonoBehaviour
{
    public int xp;
    public void OpenBarrell()
    {
        FindObjectOfType<Player>().AddXP(xp);
        Destroy(gameObject);

    }
}
