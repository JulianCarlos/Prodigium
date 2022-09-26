using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public abstract class Tool : Item
{
    [SerializeField] private AnimationClip useAnimation;
    [SerializeField] private AnimationClip equipAnimation;

    private WaitForSeconds useAnimationWait;
    private WaitForSeconds equipAnimationWait;

    private Animator animator;

    private bool isInUse;
    private bool isEquipping;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if(useAnimation)
            useAnimationWait = new WaitForSeconds(useAnimation.length);

        if(equipAnimation)
            equipAnimationWait = new WaitForSeconds(equipAnimation.length);
    }

    private void OnEnable()
    {
        PlayerInputs.InputAction.Player.LeftClick.started += L_Use;
        PlayerInputs.InputAction.Player.LeftClick.canceled += L_Use;
        
        if (equipAnimation)
            StartCoroutine(nameof(C_Equip));
    }

    private void OnDisable()
    {
        PlayerInputs.InputAction.Player.LeftClick.started -= L_Use;
        PlayerInputs.InputAction.Player.LeftClick.canceled -= L_Use;
    }

    protected override void L_Use(InputAction.CallbackContext context)
    {
        if (isInUse || isEquipping || !useAnimation)
            return;

        if (context.started)
        {
            StartCoroutine(nameof(C_Use));
        }
    }

    protected override void R_Use(InputAction.CallbackContext context)
    {

    }

    protected virtual IEnumerator C_Use()
    {
        isInUse = true;
        animator.SetTrigger("Use");
        yield return useAnimationWait;
        isInUse = false;
    }

    protected virtual IEnumerator C_Equip()
    {
        isEquipping = true;
        animator.Play("Equip");
        yield return equipAnimationWait;
        isEquipping = false;
    }
}
