using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPSDIsplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fpsText;

    private void Update()
    {
        fpsText.text = (1f / Time.deltaTime).ToString();
    }
}
