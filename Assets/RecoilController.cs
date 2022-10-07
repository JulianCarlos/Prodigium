using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoilController : MonoBehaviour
{
    private Vector3 currentRotation;
    private Vector3 targetRotation;

    [SerializeField] private float snappiness;
    [SerializeField] private float returnSpeed;

    private void Update()
    {
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnSpeed * Time.deltaTime);
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, snappiness * Time.fixedDeltaTime);
        transform.localRotation = Quaternion.Euler(currentRotation);
    }

    public void RecoilFire(Weapon weapon)
    {
        if (weapon.ItemAimController.IsAiming)
        {
            targetRotation += new Vector3(weapon.AimRecoilX, Random.Range(-weapon.AimRecoilY, weapon.AimRecoilY), Random.Range(-weapon.AimRecoilZ, weapon.AimRecoilZ));
        }
        else
        {
            targetRotation += new Vector3(weapon.RecoilX, Random.Range(-weapon.RecoilY, weapon.RecoilY), Random.Range(-weapon.RecoilZ, weapon.RecoilZ));
        }
    }
}
