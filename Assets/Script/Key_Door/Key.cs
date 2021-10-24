using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : PickUpItem
{

    [Header("Visual")]
    [SerializeField] private SpriteRenderer stone;

    [SerializeField] private Color stoneColor;

    private void OnValidate()
    {
        if (stone && item)
        {
            item.color = stoneColor;
            stone.color = item.color;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        stone.color = item.color;
    }
}
