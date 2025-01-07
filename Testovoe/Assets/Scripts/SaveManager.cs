using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public Transform playerTransform;
    public ActionBarContoller actionBarContoller;
    private void Awake()
    {
        LoadData();
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }

    public void SaveData()
    {
        
        PlayerPrefs.SetFloat("playerPosX", playerTransform.position.x);
        PlayerPrefs.SetFloat("playerPosY", playerTransform.position.y);
        PlayerPrefs.SetFloat("playerPosZ", playerTransform.position.z);
        PlayerPrefs.SetInt("abilityNumber", actionBarContoller.selectedAbilityNumber);
        string leftCDString = string.Join("*", actionBarContoller.abilitiesCDLeft);
        PlayerPrefs.SetString("leftCDString", leftCDString);
        PlayerPrefs.Save(); 
        
        Debug.Log("Data saved!");
    }

    public void LoadData()
    {
        if (PlayerPrefs.HasKey("playerPosX") && PlayerPrefs.HasKey("playerPosY") && PlayerPrefs.HasKey("playerPosZ"))
        {
            float x = PlayerPrefs.GetFloat("playerPosX");
            float y = PlayerPrefs.GetFloat("playerPosY");
            float z = PlayerPrefs.GetFloat("playerPosZ");
            playerTransform.position = new Vector3(x, y, z);
            Debug.Log("Player position loaded");
        }
        else
        {
            Debug.Log("No saved player position");
        }

        if (PlayerPrefs.HasKey("abilityNumber"))
        {
            actionBarContoller.selectedAbilityNumber = PlayerPrefs.GetInt("abilityNumber");
            Debug.Log("Ability number loaded");
        }
        else
        {
            Debug.Log("No saved ability number");
        }
        
        if (PlayerPrefs.HasKey("leftCDString"))
        {
            string leftCDString = PlayerPrefs.GetString("leftCDString");
            if (!string.IsNullOrEmpty(leftCDString))
            {
                string[] floatStrings = leftCDString.Split('*');
                List<float> loadedFloatList = new List<float>();
                foreach (string str in floatStrings)
                {
                    if (!string.IsNullOrEmpty(str))
                    {
                        loadedFloatList.Add(float.Parse(str));
                    }
                }

                actionBarContoller.abilitiesCDLeft = loadedFloatList;
            }
            
            Debug.Log("abilities colldowns loaded");
        }
        else
        {
            Debug.Log("No saved abilities colldowns");
        }
    }
}
