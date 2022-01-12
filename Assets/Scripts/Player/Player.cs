using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player singleton;

    public Image[] itemSlots;
    private int itemSlotIndex = 0;

    private void Start()
    {
        singleton = this;
    }

    public void GetAnItem(Item item)
    {
        if (itemSlotIndex < itemSlots.Length)
        {
            itemSlots[itemSlotIndex].gameObject.SetActive(true);
            itemSlots[itemSlotIndex].sprite = item.icon;
            itemSlotIndex++;
        }
    }
}