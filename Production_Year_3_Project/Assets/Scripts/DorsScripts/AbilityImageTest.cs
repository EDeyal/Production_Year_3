using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UI;

public class AbilityImageTest : MonoBehaviour
{
    [SerializeField] Texture abilityOne;
    [SerializeField] Texture abilityTwo;
    [SerializeField] Texture abilityThree;
    [SerializeField] RawImage currentImage;
    //List<Image> abilityImages;

    private void Awake()
    {
        //abilityImages.Add(abilityOne);
        //abilityImages.Add(abilityTwo);
        //abilityImages.Add(abilityThree);
         currentImage = GetComponent<RawImage>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            currentImage.texture = abilityOne;
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            currentImage.texture = abilityTwo;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            currentImage.texture = abilityThree;
        }
    }


}
