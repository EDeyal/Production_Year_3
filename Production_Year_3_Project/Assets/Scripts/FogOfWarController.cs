using UnityEngine;
using UnityEngine.UI;

public class FogOfWarController : MonoBehaviour
{
    public Texture2D fogOfWarTexture; // Reference to the fog-of-war texture
    public float revealRadius = 5f; // Radius within which the fog-of-war is revealed around the player
    public RectTransform PlayerMapIcon;
    public RectTransform fogOfWarDisplay;
    public Image display;

    private Color[] fogColors; // Array to store the fog-of-war colors

    [ContextMenu("reveal")]
    private void CalcRangeFromPlayerToPixelOnMap()
    {
        Color[] fogColors = fogOfWarTexture.GetPixels();
        int height = fogOfWarTexture.height;
        int width = fogOfWarTexture.width;

        Vector2 playerScreenPos = RectTransformUtility.WorldToScreenPoint(GameManager.Instance.Cam.MainCam, PlayerMapIcon.position);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int pixelIndex = y * width + x;
                Color pixelColor = fogColors[pixelIndex];
                pixelColor.a = 0f;
                fogColors[pixelIndex] = pixelColor;
            }
        }

        fogOfWarTexture.SetPixels(fogColors);
        fogOfWarTexture.Apply();
    }

    private Vector2 GetPixelScreenPos(int x, int y)
    {
        // Calculate the screen position of the pixel based on its x and y coordinates

        // Calculate the size of the UI element
        float uiElementWidth = fogOfWarDisplay.rect.width;
        float uiElementHeight = fogOfWarDisplay.rect.height;

        // Calculate the position of the UI element in screen space
        Vector2 uiElementScreenPos = fogOfWarDisplay.position;

        // Calculate the position of the pixel relative to the UI element
        float pixelPosX = (x / (float)fogOfWarTexture.width) * uiElementWidth;
        float pixelPosY = (y / (float)fogOfWarTexture.height) * uiElementHeight;

        // Calculate the screen position of the pixel by adding the relative position to the UI element position
        Vector2 pixelScreenPos = uiElementScreenPos + new Vector2(pixelPosX, pixelPosY);

        return pixelScreenPos;
    }


    private void Update()
    {
        // CalcRangeFromPlayerToPixelOnMap();
    }

    /* private void Update()
     {
         // Get the position of the player's GameObject
         Vector3 playerPos = PlayerMapIcon.transform.position;


         int startX = Mathf.Max(0, Mathf.FloorToInt(playerPos.x - revealRadius));
         int startY = Mathf.Max(0, Mathf.FloorToInt(playerPos.y - revealRadius));
         int endX = Mathf.Min(fogOfWarTexture.width, Mathf.CeilToInt(playerPos.x + revealRadius));
         int endY = Mathf.Min(fogOfWarTexture.height, Mathf.CeilToInt(playerPos.y + revealRadius));

         for (int y = startY; y < endY; y++)
         {
             for (int x = startX; x < endX; x++)
             {
                 // Check if the position is within the reveal radius of the player
                 Vector2 texturePos = new Vector2(x, y);
                 float distance = Vector2.Distance(playerPos, texturePos);
                 if (distance <= revealRadius)
                 {
                     int index = y * fogOfWarTexture.width + x;
                     fogColors[index].a = 0f;
                 }
             }
         }

         fogOfWarTexture.SetPixels(fogColors);
         fogOfWarTexture.Apply();
     }
 */
}
