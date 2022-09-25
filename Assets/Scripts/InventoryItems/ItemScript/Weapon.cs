using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum ReloadType
{
    InstantReload,
    SingleBulletReload
}

public abstract class Weapon : Item
{
    [Header("Gun")]
    [SerializeField] private Transform gunTip;
    [SerializeField] private GameObject muzzleFlashPrefab;

    [Header("Shot Settings")]
    [SerializeField] private float shotCooldown;
    [SerializeField] private float fireRate;
    [SerializeField] private float damage;
    [SerializeField] private bool automatic;

    [Header("Aim Settings")]
    [SerializeField] private float aimSpeed;

    [Header("Magazine Settings")]
    public int CurrentMagazineCapacity;
    public int MaxMagazineCapacity; 
    public int BackupAmmo;

    [Header("Reload Settings")]
    [SerializeField] private float reloadTime;
    [SerializeField] private ReloadType reloadType;

    [Header("Clips")]
    [SerializeField] private AnimationClip reloadInClip;
    [SerializeField] private AnimationClip reloadIOutClip;
    [Space]
    [SerializeField] private AnimationClip equipClip;

    private Vector3 localPos;

    private WaitForSeconds timeUntilUseWait;
    private WaitForSeconds fireRateWait;
    private WaitForSeconds singleShotCooldownWait;
    private WaitForSeconds timeUntilReloadWait;
    private WaitForSeconds timeUntilAbleToShoot;
    private WaitForSeconds timeBetweenReloadWait;

    private bool canShoot = true;
    private bool isReloading = false;
    private bool isEquipping = false;

    private RaycastHit hitInfo;

    private ItemAimController itemAimController;
    private Animator animator;

    private void Awake()
    {
        itemAimController = GetComponentInParent<ItemAimController>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        localPos = transform.localPosition;

        fireRateWait = new WaitForSeconds(1f / fireRate);
        singleShotCooldownWait = new WaitForSeconds(shotCooldown);

        timeUntilUseWait = new WaitForSeconds(equipClip.length);
        timeUntilReloadWait = new WaitForSeconds(reloadInClip.length);
        timeUntilAbleToShoot = new WaitForSeconds(reloadIOutClip.length);
        timeBetweenReloadWait = new WaitForSeconds(reloadTime);

        CurrentMagazineCapacity = MaxMagazineCapacity;
    }

    protected void OnEnable()
    {
        PlayerInputs.InputAction.Player.LeftClick.started += L_Use;
        PlayerInputs.InputAction.Player.LeftClick.canceled += L_Use;
        PlayerInputs.InputAction.Player.RightClick.started += R_Use;
        PlayerInputs.InputAction.Player.RightClick.canceled += R_Use;
        PlayerInputs.InputAction.Player.Reload.started += Reload;

        StartCoroutine(nameof(C_Equip));
    }

    protected void OnDisable()
    {
        PlayerInputs.InputAction.Player.LeftClick.started -= L_Use;
        PlayerInputs.InputAction.Player.LeftClick.canceled -= L_Use;
        PlayerInputs.InputAction.Player.RightClick.started -= R_Use;
        PlayerInputs.InputAction.Player.RightClick.canceled -= R_Use;
        PlayerInputs.InputAction.Player.Reload.started -= Reload;

        StopAllCoroutines();
    }

    protected override void L_Use(InputAction.CallbackContext context)
    {
        if (isReloading || isEquipping || PlayerState.PlayerStateType == PlayerStateType.InMenu)
            return;

        Shoot(context);
    }

    protected virtual void Reload(InputAction.CallbackContext context)
    {
        if (CurrentMagazineCapacity >= MaxMagazineCapacity || BackupAmmo <= 0 || isReloading || isEquipping)
            return;

        if (context.started)
        {
            StartCoroutine(nameof(C_Reload));
        }
    }

    protected virtual void Shoot(InputAction.CallbackContext context)
    {
        if (isReloading || isEquipping || BackupAmmo <= 0)
            return;

        if (context.started)
        {
            if (CurrentMagazineCapacity <= 0 && BackupAmmo > 0)
            {
                StartCoroutine(nameof(C_Reload));
                return;
            }

            if (automatic)
            {
                StartCoroutine(nameof(C_Automatic_Shooting));
            }
            else
            {
                if (!canShoot)
                    return;

                StartCoroutine(nameof(C_SingleShot_Shooting));
            }
        }
        else if (context.canceled)
        {
            StopCoroutine(nameof(C_Automatic_Shooting));
        }
    }

    protected virtual IEnumerator C_Equip()
    {
        isEquipping = true;
        animator.Play("Equip");
        yield return timeUntilUseWait;
        isEquipping = false;
    }

    protected virtual IEnumerator C_Automatic_Shooting()
    {
        while (CurrentMagazineCapacity > 0)
        {
            Shoot();
            yield return fireRateWait;
        }
    }

    protected virtual IEnumerator C_SingleShot_Shooting()
    {
        Shoot();
        canShoot = false;
        yield return singleShotCooldownWait;
        canShoot = true;
    }

    protected virtual IEnumerator C_Reload()
    {
        isReloading = true;
        animator.Play("ReloadIn");

        var difference = MaxMagazineCapacity - CurrentMagazineCapacity;
        var amount = (BackupAmmo > difference) ? difference : BackupAmmo;

        yield return timeUntilReloadWait;

        if (reloadType == ReloadType.InstantReload)
        {
            CurrentMagazineCapacity += amount;
            BackupAmmo -= amount;
            Actions.OnAmmoChanged(this);
        }
        else if (reloadType == ReloadType.SingleBulletReload)
        {
            while (amount > 0)
            {
                CurrentMagazineCapacity ++;
                BackupAmmo--;
                amount--;
                Actions.OnAmmoChanged(this);

                yield return timeBetweenReloadWait;
            }
        }

        animator.Play("ReloadOut");
        yield return timeUntilAbleToShoot;
        isReloading = false;
    }

    protected virtual void Shoot()
    {
        animator.SetTrigger("Shooting");
        DamageCheck();

        InstantiateMuzzleFlash();
        CurrentMagazineCapacity--;

        Actions.OnAmmoChanged(this);

        if (CurrentMagazineCapacity <= 0)
        {
            StopCoroutine(nameof(C_Automatic_Shooting));
        }
    }

    protected virtual void DamageCheck()
    {
        var hit = RaycastManager.CheckForRaycastType(Camera.main);
        var Monster = hit?.GetComponent<Monster>();

        var damageType = new DamageTypes[2] { DamageTypes.Fire, DamageTypes.Salt };

        if (Monster != null)
        {
            Monster.TakeDamage(damageType, this.damage);
        }
    }

    protected virtual void InstantiateMuzzleFlash()
    {
        var bullet = Instantiate(muzzleFlashPrefab, gunTip);
        bullet.transform.localPosition = Vector3.zero;
        bullet.transform.localRotation = gunTip.localRotation;
        Destroy(bullet, 0.5f);
    }
}
