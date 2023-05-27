using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWarController : MonoBehaviour
{
    public Material fogOfWarMaterial;
    public Texture2D fogOfWarTexture; // Reference to the fog-of-war texture
    public float revealRadius = 5f; // Radius within which the fog-of-war is revealed around the player
    public Transform PlayerMapIcon;

    private Color32[] fogColors; // Array to store the fog-of-war colors
    private int textureWidth; // Width of the fog-of-war texture
    private int textureHeight; // Height of the fog-of-war texture
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        fogOfWarMaterial.SetVector("_PlayerPosition", GameManager.Instance.PlayerManager.transform.position);
        Graphics.Blit(source, destination, fogOfWarMaterial);
    }
    private void Start()
    {
        // Initialize variables
        textureWidth = fogOfWarTexture.width;
        textureHeight = fogOfWarTexture.height;
        fogColors = fogOfWarTexture.GetPixels32();
    }

    private void Update()
    {
        // Get the position of the player's GameObject
        Vector3 playerPos = PlayerMapIcon.transform.position;

        // Convert player position to texture coordinates (if necessary)
        // Vector2 playerTexturePos = WorldToTextureCoord(playerPos);

        // Iterate through the fog-of-war texture within the reveal radius of the player
        int startX = Mathf.Max(0, Mathf.FloorToInt(playerPos.x - revealRadius));
        int startY = Mathf.Max(0, Mathf.FloorToInt(playerPos.z - revealRadius));
        int endX = Mathf.Min(fogOfWarTexture.width, Mathf.CeilToInt(playerPos.x + revealRadius));
        int endY = Mathf.Min(fogOfWarTexture.height, Mathf.CeilToInt(playerPos.z + revealRadius));

        for (int y = startY; y < endY; y++)
        {
            for (int x = startX; x < endX; x++)
            {
                // Check if the position is within the reveal radius of the player
                Vector2 texturePos = new Vector2(x, y);
                float distance = Vector2.Distance(playerPos, texturePos);
                if (distance <= revealRadius)
                {
                    // Calculate the transparency based on the distance
                    float alpha = distance / revealRadius;

                    // Update the fog-of-war color with the transparency
                    int index = y * fogOfWarTexture.width + x;
                    fogColors[index].a = (byte)(255f * (1f - alpha));
                }
            }
        }

        // Update the fog-of-war texture with the modified colors
        fogOfWarTexture.SetPixels32(fogColors);
        fogOfWarTexture.Apply();
    }

}
