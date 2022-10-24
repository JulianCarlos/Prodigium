using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class IconGenerator : MonoBehaviour
{
    [SerializeField] private string fileName;
    [SerializeField] private string folderName;

    [SerializeField] private Camera camera;

    [SerializeField] private bool autoReset = false;

    [SerializeField] private Vector2Int pictureSize;

    [ContextMenu("Take ScreenShot")]
    public void TakeScreenShot()
    {
        if(autoReset)
            camera.clearFlags = CameraClearFlags.Depth;

        RenderTexture rt = new RenderTexture(pictureSize.x, pictureSize.y, 24);
        camera.targetTexture = rt;
        Texture2D screenShot = new Texture2D(pictureSize.x, pictureSize.y, TextureFormat.RGBA32, false);
        camera.Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, pictureSize.x, pictureSize.y), 0, 0);
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
