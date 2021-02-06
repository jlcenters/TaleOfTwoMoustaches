using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform doorEntryPos;
    public void OpenDoor()
    {
        FindObjectOfType<Player>().transform.position = doorEntryPos.position;
    }
}
