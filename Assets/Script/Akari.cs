using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Akari : PixelObjectBase
{
    [SerializeField]
    private Texture banzaiTexture, jumpTexture, ojigiTexture;
    [SerializeField]
    private AnimationClip banzaiAnim, jumpLAnim, jumpRAnim, ojigiAnim, idleAnim;
    [SerializeField]
    private JumpDirection jumpDirection;
    [SerializeField]
    private GameObject heartEmitter;

    [SerializeField]
    private int delayCount = 0;

    private int startCount;

    public enum JumpDirection
    {
        LEFT,
        RIGHT
    }

    private SpriteRenderer spriteRenderer;
    private static int idMainTex = Shader.PropertyToID("_MainTex");
    private MaterialPropertyBlock block;

    private Animator animator;

    private int animCount;
    private AkariAnimationState animState;
    private enum AkariAnimationState
    {
        Idle,
        Ojigi,
        Idle2,
        Banzai,
        LJump,
        RJump,
        Idle3,
    }


    void Start()
    {
        animator = GetComponent<Animator>();

        block = new MaterialPropertyBlock();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.GetPropertyBlock(block);

        animCount = 0;
        animState = AkariAnimationState.Idle;

        startCount = 0;
    }

    void Update()
    {
        startCount ++;

        if (startCount <= delayCount)
        {
            animator.speed = 0;
        }
        else
        {
            animator.speed = 1;
        }

        changeState();

        replaceTextureIfMatchName(banzaiAnim.name, banzaiTexture);
        replaceTextureIfMatchName(ojigiAnim.name, ojigiTexture);
        replaceTextureIfMatchName(jumpLAnim.name, jumpTexture);
        replaceTextureIfMatchName(jumpRAnim.name, jumpTexture);
        replaceTextureIfMatchName(idleAnim.name, jumpTexture);
        spriteRenderer.SetPropertyBlock(block);
    }

    private void replaceTextureIfMatchName(string name, Texture texture)
    {
        AnimatorStateInfo animationState = animator.GetCurrentAnimatorStateInfo(0);
        if (animationState.IsName("Base Layer." + name))
        {
            block.SetTexture(idMainTex, texture);
        }
    }

    public void AnimationClipEndEvent()
    {
        animCount++;
    }

    public void EmitHeart()
    {
        Instantiate(heartEmitter, gameObject.transform);
    }

    private void changeState()
    {
        switch(animState)
        {
            case AkariAnimationState.Idle:
                if (animCount >= 1)
                {
                    animCount = 0;
                    animState = AkariAnimationState.Ojigi;
                    animator.Play(ojigiAnim.name);
                }
                break;
            case AkariAnimationState.Ojigi:
                if (animCount >= 1)
                {
                    animCount = 0;
                    animState = AkariAnimationState.Idle2;
                    animator.Play(idleAnim.name);
                }
                break;
            case AkariAnimationState.Idle2:
                if (animCount >= 1)
                {
                    animCount = 0;
                    animState = AkariAnimationState.Banzai;
                    animator.Play(banzaiAnim.name);
                }
                break;
            case AkariAnimationState.Banzai:
                if (animCount >= 3)
                {
                    animCount = 0;
                    if( jumpDirection == JumpDirection.LEFT)
                    {
                        animState = AkariAnimationState.LJump;
                        animator.Play(jumpLAnim.name);
                    }
                    else
                    {
                        animState = AkariAnimationState.RJump;
                        animator.Play(jumpRAnim.name);
                    }
                }
                break;
            case AkariAnimationState.LJump:
                if (animCount >= 2)
                {
                    animCount = 0;
                    if (jumpDirection == JumpDirection.LEFT)
                    {
                        animState = AkariAnimationState.RJump;
                        animator.Play(jumpRAnim.name);
                    }
                    else
                    {
                        animState = AkariAnimationState.Idle;
                        animator.Play(idleAnim.name);
                    }
                }
                break;
            case AkariAnimationState.RJump:
                if (animCount >= 2)
                {
                    animCount = 0;
                    if (jumpDirection == JumpDirection.LEFT)
                    {
                        animState = AkariAnimationState.Idle;
                        animator.Play(idleAnim.name);
                    }
                    else
                    {
                        animState = AkariAnimationState.LJump;
                        animator.Play(jumpLAnim.name);
                    }
                }
                break;
        }
    }

}