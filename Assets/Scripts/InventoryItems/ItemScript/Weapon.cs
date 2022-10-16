using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum ReloadType
{
    InstantReload,
    SingleBulletReload
}

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public abstract class Weapon : Item
{
    //Magazine Readonly
    public int CurrentMagazineCapacity => currentMagazineCapacity;
    public int MaxMagazineCapacity => maxMagazineCapacity;
    public int BackupAmmo => backupAmmo;

    //Recoil Readonly
    public float RecoilX => recoilX;
    public float RecoilY => recoilY;
    public float RecoilZ => recoilZ;
    public float AimRecoilX => aimRecoilX;
    public float AimRecoilY => aimRecoilY;
    public float AimRecoilZ => aimRecoilZ;
    public float Snappiness => snappiness;
    public float ReturnSpeed => returnSpeed;

    [Header("Gun")]
    [SerializeField] private Transform gunTip;
    [SerializeField] private GameObject muzzleFlashPrefab;

    [Header("Shot Settings")]
    [SerializeField] private float shotCooldown;
    [SerializeField] private float fireRate;
    [SerializeField] private float damage;
    [SerializeField] private bool automatic;

    [SerializeField] private DamageTypes[] damageTypes;

    [Header("Aim Settings")]
    [SerializeField] private float aimSpeed;

    [Header("Magazine Settings")]
    [SerializeField] private int currentMagazineCapacity;
    [SerializeField] private int maxMagazineCapacity;
    [SerializeField] private int backupAmmo;

    [Header("Reload Settings")]
    [SerializeField] private float reloadTime;
    [SerializeField] private ReloadType reloadType;

    [Header("Recoil Settings")]
    [SerializeField] private float recoilX;
    [SerializeField] private float recoilY;
    [SerializeField] private float recoilZ;
    [Space]
    [SerializeField] private float aimRecoilX;
    [SerializeField] private float aimRecoilY;
    [SerializeField] private float aimRecoilZ;

    [SerializeField] private float snappiness;
    [SerializeField] private float returnSpeed;

    [Header("AnimationClips")]
    [SerializeField] private AnimationClip reloadInClip;
    [SerializeField] private AnimationClip reloadIOutClip;
    [Space]
    [SerializeField] private AnimationClip equipClip;

    [Header("AudioClips")]
    [SerializeField] private AudioClip reloadClip;
    [SerializeField] private AudioClip shotClip;
    [SerializeField] private AudioClip emptyShotClip;

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

    public ItemAimController ItemAimController { get; private set; }
    private Animator animator;
    private AudioSource weaponShotAudioSource;
    private RecoilController recoilController;

    private void Awake()
    {
        ItemAimController = GetComponentInParent<ItemAimController>();
        animator = GetComponent<Animator>();
        weaponShotAudioSource = GetComponent<AudioSource>();
        recoilController = FindObjectOfType<RecoilController>();
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

        currentMagazineCapacity = maxMagazineCapacity;
    }

    protected void OnEnable()
    {
        isReloading = false;
        canShoot = true;

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

    protected override void R_Use(InputAction.CallbackContext context)
    {
        if (isReloading || isEquipping || PlayerState.PlayerStateType == PlayerStateType.InMenu)
            return;

        ItemAimController.Aim(context);
    }

    protected virtual void Reload(InputAction.CallbackContext context)
    {
        if (currentMagazineCapacity >= maxMagazineCapacity || backupAmmo <= 0 || isReloading || isEquipping)
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
            if (currentMagazineCapacity <= 0 && backupAmmo > 0)
            {
                weaponShotAudioSource.PlayOneShot(emptyShotClip);
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
        while (currentMagazineCapacity > 0)
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

        var difference = maxMagazineCapacity - currentMagazineCapacity;
        var amount = (BackupAmmo > difference) ? difference : BackupAmmo;

        yield return timeUntilReloadWait;


        if (reloadType == ReloadType.InstantReload)
        {
            weaponShotAudioSource.PlayOneShot(reloadClip);
            currentMagazineCapacity += amount;
            backupAmmo -= amount;
            Actions.OnAmmoChanged(this);
        }
        else if (reloadType == ReloadType.SingleBulletReload)
        {
            while (amount > 0)
            {
                weaponShotAudioSource.PlayOneShot(reloadClip);
                currentMagazineCapacity++;
                backupAmmo--;
                amount--;
                Actions.OnAmmoChanged(this);
                yield return timeBetweenReloadWait;
            }
        }
        ItemAimController.AimOut();
        animator.Play("ReloadOut");
        yield return timeUntilAbleToShoot;
        isReloading = false;
    }

    protected virtual void Shoot()
    {
        weaponShotAudioSource.PlayOneShot(shotClip);

        animator.SetTrigger("Shooting");
        DamageCheck();

        InstantiateMuzzleFlash();
        currentMagazineCapacity--;

        Actions.OnAmmoChanged(this);

        if (currentMagazineCapacity <= 0)
        {
            StopCoroutine(nameof(C_Automatic_Shooting));
        }

        recoilController.RecoilFire(this);
    }

    protected virtual void DamageCheck()
    {
        var hit = RaycastManager.CheckForRaycastType(Camera.main);
        var Monster = hit?.GetComponent<Monster>();

        if (Monster != null)
        {
            Monster.TakeDamage(damageTypes, this.damage);
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
