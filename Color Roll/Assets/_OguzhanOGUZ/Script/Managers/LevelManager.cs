using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private bool levelCleared = false;
    [SerializeField] private Color32 wallColor;
    [SerializeField] private Material wallMaterial;
    [SerializeField] private int collectedGemStar = 0;
    [SerializeField] private int collectedGemDiamond = 0;
    // Start is called before the first frame update
    void Start()
    {
        wallMaterial.color = wallColor;
    }

    public void LevelCleared()
    {
        levelCleared = true;
        //string currentSceneName = SceneManager.GetActiveScene().name;
        //if(int.TryParse(currentSceneName, out int currentSceneNo))
        //{
        //    int nextSceneNo = currentSceneNo + 1;
        //    string nextSceneName = nextSceneNo.ToString();
        //    PlayerPrefs.SetInt("Level" + nextSceneName, 4);
        //}
        //else
        //{
        //    Debug.LogError("Error! Be sure that scene name is actually number!");
        //}
    }
    public bool IsLevelCleared()
    {
        return levelCleared;
    }
    public void OnGemCollectedStar()
    {
        collectedGemStar++;
    }
    public void OnGemCollectedDiamond()
    {
        collectedGemDiamond++;
    }
    public int GetGemStar()
    {
        return collectedGemStar;
    }
    public int GetGemDiamond()
    {
        return collectedGemDiamond;
    }
}
