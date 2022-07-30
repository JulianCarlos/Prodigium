using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayVersion : MonoBehaviour
{
    private void Awake()
    {
        if (TryGetComponent(out TextMeshProUGUI output))
        {
            output.text = Application.version;
        }
    }
}
