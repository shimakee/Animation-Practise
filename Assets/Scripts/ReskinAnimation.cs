﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReskinAnimation : MonoBehaviour
{
    public string spriteSheetName;
    private void LateUpdate()
    {
        var subSprites = Resources.LoadAll<Sprite>($"Sprite sheets/{spriteSheetName}");

        foreach (var renderer in GetComponentsInChildren<SpriteRenderer>())
        {
            string spriteName = renderer.sprite.name;

            var newSprite = Array.Find(subSprites, item => item.name == spriteName);

            if (newSprite)
                renderer.sprite = newSprite;
        }
    }
}
