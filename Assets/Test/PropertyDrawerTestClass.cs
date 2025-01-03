﻿using System;
using System.Collections;
using UnityEngine;

public enum IngredientUnit { Spoon, Cup, Bowl, Piece }

// Custom serializable class
[Serializable]
public class Ingredient
{
    public string name;
    public int amount = 1;
    public IngredientUnit unit;
}

public class PropertyDrawerTestClass : MonoBehaviour
{
    public Ingredient potionResult;
    public Ingredient[] potionIngredients;
}