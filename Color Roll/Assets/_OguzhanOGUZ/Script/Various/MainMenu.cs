using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public static int maxLevelNo = 6;
    public List<Button> levelButtons;
    public Sprite levelEnabled;
    public Sprite levelDisabled;



    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("Level1",1);
        PlayerPrefs.SetInt("Level2",1);
        PlayerPrefs.SetInt("Level3",1);
        PlayerPrefs.SetInt("Level4",1);
        PlayerPrefs.SetInt("Level5",1);
        PlayerPrefs.SetInt("Level6",1);
        for(int i = 0; i < levelButtons.Count; i++)
        {
            bool isLevelUnlocked = (PlayerPrefs.GetInt("Level" + (i+1).ToString()) != 0);
            levelButtons[i].interactable = isLevelUnlocked;

            if(isLevelUnlocked)
            {
                levelButtons[i].GetComponent<Image>().sprite = levelEnabled;
                levelButtons[i].transform.GetChild(0).GetComponent<Text>().text = (i + 1).ToString();
            }
            else
            {
                levelButtons[i].GetComponent<Image>().sprite = levelDisabled;
                levelButtons[i].transform.GetChild(0).GetComponent<Text>().text = "";
            }
        }
    }

    public void SelectLevel(int no)
    {
        if(no <= MainMenu.maxLevelNo)
        {
            SceneManager.LoadScene(no.ToString());
        }
    }
}
