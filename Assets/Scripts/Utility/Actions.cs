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
<<<<<<< Updated upstream
    public static Action<TransitionManager, float> OnFadeInBegin;
    public static Action<TransitionManager, float> OnFadeOutBegin;
    
    //MoneySystem Actions
=======
    public static Action<TransitionManager> OnFadeInBegin;
    public static Action<TransitionManager> OnFadeOutBegin;

    //Money Actions
>>>>>>> Stashed changes
    public static Action<float> OnMoneyAdded;
    public static Action<float> OnMoneyRemoved;
}
