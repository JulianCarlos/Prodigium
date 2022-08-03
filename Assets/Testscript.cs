using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testscript : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Actions.OnPlayerJump += Jump;
        Actions.OnMonsterSpawned += MonsterSpawned;
        Actions.OnMoneyAdded += AddMoney;
        Actions.OnMoneyRemoved += RemoveMoney;
        Actions.OnFadeInBegin += FadeIn;
        Actions.OnFadeOutBegin += FadeOut;
        Actions.OnPlayerTakeFallDamage += PlayerTakeFallDamage;
    }

    public void Jump(PlayerInputs inputs)
    {
        Debug.Log("Player jumped");
    }

    public void MonsterSpawned(SpawnController controller)
    {
        Debug.Log("Monster spawned"); 
    }

    public void AddMoney(float amount)
    {
        Debug.Log("Added Money");
    }

    public void RemoveMoney(float amount)
    {
        Debug.Log("Removed Money");
    }

    public void FadeIn(TransitionManager manager, float fadeTime)
    {
        Debug.Log("Fade in");
    }

    public void FadeOut(TransitionManager manager, float fadeTime)
    {
        Debug.Log("Fade out");
    }

    public void PlayerTakeFallDamage(Player player, float amount)
    {
        Debug.Log("Player took falldamage");
    }
}
