using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class IconGenerator : MonoBehaviour
{
    [SerializeField] private string fileName;
    [SerializeField] private string folderName;

    [SerializeField] private Camera camera;

    [ContextMenu("Take ScreenShot")]
    public void TakeScreenShot()
    {
        camera.clearFlags = CameraClearFlags.Depth;

        RenderTexture rt = new RenderTexture(256, 256, 24);
        camera.targetTexture = rt;
        Texture2D screenShot = new Texture2D(256, 256, TextureFormat.RGBA32, false);
        camera.Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, 256, 256), 0, 0);
        camera.targetTexture = null;
        RenderTexture.active = null;

        if (Application.isEditor)
        {
            DestroyImmediate(rt);
        }
        else
        {
            Destroy(rt);
        }

        byte[] bytes = screenShot.EncodeToPNG();
        System.IO.File.WriteAllBytes($"{Application.dataPath}/{folderName}/{fileName}.png", bytes);
#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif
    }
}
