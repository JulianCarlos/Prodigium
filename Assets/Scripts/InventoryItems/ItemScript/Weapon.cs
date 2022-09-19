using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    [Header("Magazine Settings")]
    public int currentMagazineCapacity;
    public int maxMagazineCapacity; 
    public int backupAmmo;

    [Header("Reload Settings")]
    [SerializeField] private float reloadTime;

    private WaitForSeconds fireRateWait;
    private WaitForSeconds singleShotCooldownWait;
    private WaitForSeconds reloadTimeWait;

    private bool canShoot = true;
    private bool isReloading = false;

    private RaycastHit hitInfo;

    private void Awake()
    {
        fireRateWait = new WaitForSeconds(1f / fireRate);
        singleShotCooldownWait = new WaitForSeconds(shotCooldown);
        reloadTimeWait = new WaitForSeconds(reloadTime);
    }

    protected void OnEnable()
    {
        PlayerInputs.InputAction.Player.LeftClick.started += Use;
        PlayerInputs.InputAction.Player.LeftClick.canceled += Use;

        PlayerInputs.InputAction.Player.Reload.started += Reload;
    }

    protected void OnDisable()
    {
        PlayerInputs.InputAction.Player.LeftClick.started -= Use;
        PlayerInputs.InputAction.Player.LeftClick.canceled -= Use;
    }

    protected override void Use(InputAction.CallbackContext context)
    {
        if(PlayerState.PlayerStateType == PlayerStateType.InGame)
        {
            Shoot(context);
        }
    }

    protected virtual void Reload(InputAction.CallbackContext context)
    {
        if (currentMagazineCapacity >= maxMagazineCapacity || backupAmmo <= 0)
            return;

        if (context.started)
        {
            StartCoroutine(nameof(C_Reload));
        }
    }

    protected virtual void Shoot(InputAction.CallbackContext context)
    {
        if (isReloading)
            return;

        if (context.started)
        {
            if (currentMagazineCapacity <= 0)
                return;

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

    protected virtual IEnumerator C_Automatic_Shooting()
    {
        while (true)
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
        yield return reloadTimeWait;
        isReloading = false;

        var difference = maxMagazineCapacity - currentMagazineCapacity;
        var amount = (backupAmmo > difference) ? difference : backupAmmo;
        currentMagazineCapacity += amount;
        backupAmmo -= amount;
        Actions.OnAmmoChanged(this);
    }

    protected virtual void Shoot()
    {
        DamageCheck();

        InstantiateMuzzleFlash();
        currentMagazineCapacity--;

        Actions.OnAmmoChanged(this);

        if (currentMagazineCapacity <= 0)
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
