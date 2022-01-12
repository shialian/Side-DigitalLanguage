using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EPOOutline;

public class Item : MonoBehaviour
{
    public Sprite icon;
    private Outlinable outline;

    private void Start()
    {
        InitGetCompoenets();
        InitSettings();
    }

    private void InitGetCompoenets()
    {
        outline = GetComponent<Outlinable>();
    }

    private void InitSettings()
    {
        outline.enabled = false;
    }

    private void OnMouseEnter()
    {
        HighlightItem();
    }

    private void HighlightItem()
    {
        outline.enabled = true;
    }

    private void OnMouseUpAsButton()
    {
        PlayerGetItem();
        Reset();
    }

    private void PlayerGetItem()
    {
        Player.singleton.GetAnItem(this);
    }

    private void Reset()
    {
        gameObject.SetActive(false);
    }

    private void OnMouseExit()
    {
        UnHightLightItem();
    }

    private void UnHightLightItem()
    {
        outline.enabled = false;
    }
}
