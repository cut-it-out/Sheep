using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;

public class StarManager : MonoBehaviour
{
    [SerializeField] Sprite starFull;
    [SerializeField] Sprite starEmpty;

    List<StarController> starControllers;

    void Start()
    {
        starControllers = GetComponentsInChildren<StarController>().ToList();
    }

    public void Setup(int _stars)
    {
        starControllers.ForEach(star => {
            if (star.starNumber <= _stars)
            {
                star.gameObject.GetComponent<Image>().sprite = starFull;
            }
            else
            {
                star.gameObject.GetComponent<Image>().sprite = starEmpty;
            }
            });
    }
}
