using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sparkle : MovingEffect
{
    public void AnimationClipEndEvent()
    {
        Destroy(gameObject);
    }
}
