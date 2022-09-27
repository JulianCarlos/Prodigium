using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RaycastManager
{
    public static RaycastHit HitInfo;

    public static Transform CheckForRaycastType(Camera targetCamera)
    {
        var hit = Physics.Raycast(targetCamera.transform.position, targetCamera.transform.forward, out HitInfo, 100);

        if(HitInfo.collider != null)
        {
            return HitInfo.collider.transform.root;
        }
        else
        {
            return null;
        }
    }
}
