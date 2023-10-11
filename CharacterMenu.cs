using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{

    private int selectedOption = 0;

    public void Start()
    {
        if(!PlayerPrefs.HasKey("selectedOption"))
        {
            selectedOption = 0;
        }
        else
        {
            Load();
        }
    }

    public void VirtualManChosen()
    {
        selectedOption = 0;
        Save();
    }
    public void PinkGuyChosen()
    {
        selectedOption = 1;
        Save();
    }
    public void MaskManChosen()
    {
        selectedOption = 2;
        Save();
    }
    public void NinjaFrogChosen()
    {
        selectedOption = 3;
        Save();
    }

    private void Load()
    {
        PlayerPrefs.GetInt("selectedOption");
    }

    private void Save()
    {
        PlayerPrefs.SetInt("selectedOption", selectedOption);
    }
    public void backToStartMenu()
    {
        SceneManager.LoadScene(1);
    }
}
