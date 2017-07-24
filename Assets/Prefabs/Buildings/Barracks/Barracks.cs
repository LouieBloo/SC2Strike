using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : Building {

    public SpriteRenderer ColoredSprite;

    public override void RpcSetColor(Color inputColor)
    {
        ColoredSprite.color = inputColor;
    }

}
