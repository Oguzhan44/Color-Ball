using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// GameManager is the core script that allows all the other Manager scripts communicate between eachother.
/// The other manager scripts can not reach any other managers directly.
/// 
/// </summary>
public class GameManager : MonoBehaviour
{
    private GameManager() { }
    private static GameManager gameManager;
    public static GameManager Instance
    {
        get
        {
            if(!gameManager)
            {
                gameManager = GameObject.FindObjectOfType<GameManager>();
            }
            return gameManager;
        }
    }

    [SerializeField] private BallManager ballManager;
    [SerializeField] LevelManager levelManager;
    [SerializeField] CameraManager cameraManager;
    [SerializeField] InputManager inputManager;
    [SerializeField] ColorManager colorManager;
    UIManager uIManager;


    private void Start()
    {
        uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }


    public int GetGemStar()
    {
        return levelManager.GetGemStar();
    }
    public int GetGemDiamond()
    {
        return levelManager.GetGemDiamond();
    }
    public void OnGemCollectedStar() 
    {
        levelManager.OnGemCollectedStar();
    }
    public void OnGemCollectedDiamond() 
    {
        levelManager.OnGemCollectedDiamond();
    }

    public void OnBallFell()
    {
        if(!levelManager.IsLevelCleared())
        {
            cameraManager.OnBallFell();
            uIManager.OnBallFell();
        }
    }

    public void OnBallTouchFinalPoint()
    {
        if(!levelManager.IsLevelCleared())
        {
            Debug.Log("Level Cleared");
            levelManager.LevelCleared();
            cameraManager.OnLevelCleared();
            ballManager.OnLevelCleared();
            uIManager.OnLevelCleared();
        }
    }

    public int GetBallCurrentColorNo()
    {
        return ballManager.GetBallCurrentColorNo();
    }
    public Color32 GetBallCurrentColor()
    {
        return ballManager.GetBallCurrentColor();
    }
    public Vector2 GetJoyStickFirstTouchPoint()
    {
        return inputManager.GetJoyStickFirstTouchPoint();
    }
    public float GetJoyStickRadius()
    {
        return inputManager.GetJoyStickRadius();
    }
    public Vector2 GetJoystick()
    {
        return inputManager.GetJoyStick();
    }
    /// <summary>
    /// Gets pre-defined color from colorManager with given colorIndexNo;
    /// </summary>
    /// <param name="colorIndexNo"></param>
    /// <returns></returns>
    public Color32 GetColor(int colorIndexNo)
    {
        return colorManager.GetColor(colorIndexNo);
    }
    /// <summary>
    /// Finds index no of given color if it's defined (existed) in the colorPallette. Returns -1 if there is no any color defined like that.
    /// </summary>
    /// <param name="color"></param>
    /// <returns></returns>
    public int GetColorNo(Color32 color)
    {
        return colorManager.GetColorNo(color);
    }
    /// <summary>
    /// With the given values returns new color of any object which is described in this function. Can be use by calculations like Green + Blue = Green.
    /// </summary>
    /// <returns></returns>
    public int ColorMix(int baseColor, int newColorNo)
    {
        return colorManager.ColorMix(baseColor, newColorNo);
    }
}
