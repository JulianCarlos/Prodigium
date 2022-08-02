using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Actions
{
    //Player Actions
    public static Action<Player> OnTakeDamage;
    public static Action<Player> OnTakeFallDamge;
    public static Action<Player> OnPlayerDeath;

    //Player Input Action
    public static Action<PlayerInputs> OnPlayerJump;

    //Monster Actions
    public static Action<Monster> OnMonsterDeath;

    //Spawner Actions
    public static Action<SpawnController> OnMonsterSpawned;

    //Fade Actions
    public static Action<TransitionManager> OnFadeInBegin;
    public static Action<TransitionManager> OnFadeOutBegin;
}
