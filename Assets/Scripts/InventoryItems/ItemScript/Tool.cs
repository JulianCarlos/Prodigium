using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SphereCollider))]
public abstract class Tool : Item
{
    [SerializeField] private DamageTypes[] damageType;
    [SerializeField] private float damage;

    [SerializeField] private AnimationClip useAnimation;
    [SerializeField] private AnimationClip equipAnimation;

    private WaitForSeconds useAnimationWait;
    private WaitForSeconds equipAnimationWait;

    private Animator animator;
    private SphereCollider attackCollider;

    private bool isInUse;
    private bool isEquipping;

    private bool isAttacking;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        attackCollider = GetComponent<SphereCollider>();
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

    //Animation Event
    protected void StartAttack()
    {
        isAttacking = true;
        attackCollider.enabled = true;
    }

    protected void EndAttack()
    {
        isAttacking = false;
        attackCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.root.TryGetComponent(out Monster monster))
        {
            monster.TakeDamage(damageType, damage);
            isAttacking = false;
            attackCollider.enabled = false;
        }
    }
}
