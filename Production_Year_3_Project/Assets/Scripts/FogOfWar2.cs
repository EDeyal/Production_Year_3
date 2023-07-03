using UnityEngine;
using UnityEngine.UI;

public class FogOfWar2 : MonoBehaviour
{
    public Texture2D fogTexture;
    public float radius;
    public Transform playerUi;

    [ContextMenu("clear fog")]
    public void Test()
    {
        UpdateFogOfWar(playerUi.position, radius);
    }

    public void UpdateFogOfWar(Vector2 position, float radius)
    {
        Vector3 screenPosition = GameManager.Instance.Cam.MainCam.WorldToScreenPoint(position);
        Vector2 pixelUV = new Vector2(screenPosition.x / Screen.width, screenPosition.y / Screen.height);

        //pixels positions on the texture.
        pixelUV.x = fogTexture.width;
        pixelUV.y = fogTexture.height;

        // loop over all pixels within radius and change alpha
        int radiusInt = Mathf.RoundToInt(radius);
        for (int x = -radiusInt; x <= radiusInt; x++)
        {
            for (int y = -radiusInt; y <= radiusInt; y++)
            {
                if (x * x + y * y <= radiusInt * radiusInt)
                {
                    int pixelX = Mathf.RoundToInt(pixelUV.x + x);
                    int pixelY = Mathf.RoundToInt(pixelUV.y + y);

                    // Check if the pixel coordinates are within the texture boundaries
                    if (pixelX >= 0 && pixelX < fogTexture.width && pixelY >= 0 && pixelY < fogTexture.height)
                    {
                        Color pixelColor = fogTexture.GetPixel(pixelX, pixelY);
                        pixelColor.a = 0;
                        fogTexture.SetPixel(pixelX, pixelY, pixelColor);

                        Debug.Log("pxiel changed at " + pixelX + " " + pixelY);
                    }
                }
            }
        }
        fogTexture.Apply();
    }
}
/*Vector2 position = new Vector2(10f, 10f); // World space position
float radius = 5f;
FogOfWar.UpdateFogOfWar(position, radius);*/


