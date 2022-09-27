using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Actions
{
    //Player Actions
    public static Action<Player> OnTakeDamage;
    public static Action<Player, float> OnPlayerTakeFallDamage;
    public static Action<Player> OnPlayerDeath;

    //Player Input Action
    public static Action<PlayerInputs> OnPlayerJump;

    //Monster Actions
    public static Action<Monster> OnMonsterDeath;
    public static Action<Monster> OnMonsterTakeDamage;

    //Spawner Actions
    public static Action<SpawnController> OnMonsterSpawned;

    //Fade Actions
    public static Action<TransitionManager, float> OnFadeInBegin;
    public static Action<TransitionManager, float> OnFadeOutBegin;
    
    //MoneySystem Actions
    public static Action<float> OnMoneyChanged;

    //ShopSystem Actions
    public static Action<ItemData> OnSelectedItemChanged;
    public static Action<ItemData> OnItemBought;

    //Location Arrived Actions
    public static Action<Vector3> OnLocationArrived;

    //Collect Actions
    public static Action<ItemData> OnItemCollected;

    //Achievement Actions
    public static Action<Achievement> OnAchievementCompleted;

    //ITem Actions
    public static Action<ItemData> OnItemChanged;
    public static Action<Weapon> OnAmmoChanged;

    //UI Change Actions
    public static Action<Vector2> OnSensitivityChanged;
}
