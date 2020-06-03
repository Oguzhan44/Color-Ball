using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    private ColorManager() { }
    private readonly List<Color32> colorPallette = new List<Color32>()
    {
        new Color32(255,255,255,255), //White
        new Color32(255,255,0,255), //Yellow
        new Color32(65,150,255,255), //Blue
        new Color32(0,255,0,255), //Green
        new Color32(222,45,61,255), //Red
        new Color32(255,145,0,255), //Orange
        new Color32(143,70,183,255), //Purple
    };


    /// <summary>
    /// With the given values returns new color of any object which is described in this function. Can be use by calculations like Green + Blue = Green.
    /// </summary>
    /// <param name="baseColorNo"></param>
    /// <param name="newColorNo"></param>
    /// <returns></returns>
    public int ColorMix(int baseColorNo, int newColorNo)
    {
        if(baseColorNo == 1 && newColorNo == 2) //Yellow & Blue
        {
            return 3;
        }
        if(baseColorNo == 2 && newColorNo == 1) //Blue & Yellow
        {
            return 3;
        }
        if(baseColorNo == 1 && newColorNo == 4) //Yellow & Red
        {
            return 5;
        }
        if(baseColorNo == 4 && newColorNo == 1) //Red & Yellow
        {
            return 5;
        }
        if(baseColorNo == 2 && newColorNo == 4) //Blue & Red
        {
            return 6;
        }
        if(baseColorNo == 4 && newColorNo == 2) //Red & Blue
        {
            return 6;
        }
        return newColorNo;
    }

    /// <summary>
    /// Gets Color32 with colorIndexNo that is described in ColorManager.
    /// </summary>
    /// <param name="colorIndexNo"></param>
    /// <returns></returns>
    public Color32 GetColor(int colorIndexNo)
    {
        if(colorIndexNo >= 0)
        {
            if(colorIndexNo < colorPallette.Count)
            {
                return colorPallette[colorIndexNo];
            }
        }
        else
        {
            Debug.LogError("Desired color is not defined. " + colorIndexNo);
        }
        Debug.LogError("Desired color couldn't be found. " + colorIndexNo);
        return Color.white;
    }
    /// <summary>
    /// Gets color no defined in ColorManager.cs. Warning: Returns -1 if there is no any pre defined color as given value.
    /// </summary>
    /// <param name="color"></param>
    /// <returns></returns>
    public int GetColorNo(Color32 color)
    {
        for(int i = 0; i < colorPallette.Count; i++)
        {
            if(CheckIfSameColors(color, colorPallette[i]))
            {
                return i;
            }
        }
        Debug.Log("Warning: Desired color is not defined");
        return -1;
    }
    /// <summary>
    /// Checks if given colors are actually same colors.
    /// </summary>
    /// <param name="colorA"></param>
    /// <param name="colorB"></param>
    /// <returns></returns>
    private bool CheckIfSameColors(Color32 colorA, Color32 colorB)
    {
        return (colorA.r == colorB.r && colorA.g == colorB.g && colorA.b == colorB.b && colorA.a == colorB.a);
    }
}