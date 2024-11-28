using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingLayer : MonoBehaviour
{

    public Renderer renderer;

    private void Awake()
    {
        renderer.sortingLayerName = "Floor";
        renderer.sortingOrder = 100;
    }

    private void Start()
    {
        renderer.sortingLayerName = "Floor";
        renderer.sortingOrder = 100;
    }

}
