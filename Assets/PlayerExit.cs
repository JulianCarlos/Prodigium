using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.TryGetComponent(out Player player))
        {
            PlayerData.Instance.TransferItemsToPlayerData();
            TransitionManager.Instance.TransitionToScene(1, TransitionMethod.BlackScreen);
        }
    }
}
