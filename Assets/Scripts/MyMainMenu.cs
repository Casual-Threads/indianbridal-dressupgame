using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class MainMenuElements
{
    [Header("GameObject")]
    public GameObject LoadingPanel;
    public Image fillbar;
}
public class MyMainMenu : MonoBehaviour
{
    public MainMenuElements uIElements;
    //public Sprite[] botSprites;
    //public Image botImageInScene;
    //public Image botImageInPanel;
    //public Text InputField;
    //public Text NameInScene;
    //public Text PlaceHolderName;
    //public GameObject InfoPanel;
    //void Start()
    //{
    //    //if (GAManager.Instance != null)
    //    //{
    //    //    GAManager.Instance.LogDesignEvent("MainMenu:Show");
    //    //}
    //    //if (GameManager.Instance.Initialized == false)
    //    //{
    //    //    GameManager.Instance.Initialized = true;
    //    //    GSF_SaveLoad.LoadProgress();
    //    //}
    //    //botImageInScene.sprite = botSprites[SaveData.Instance.SelectedAvatar];
    //    //NameInScene.text = SaveData.Instance.ProfileName;
    //    //PlaceHolderName.text = SaveData.Instance.ProfileName;
    //}

    public void Play(string str)
    {
        //ShowInterstitial();
        //if (GAManager.Instance != null)
        //{
        //    GAManager.Instance.LogDesignEvent("MainMenu:PlayClick");
        //}
        uIElements.LoadingPanel.SetActive(true);
        StartCoroutine(LoadingScene(str));
    }
    IEnumerator LoadingScene(string str)
    {

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(str);
        asyncLoad.allowSceneActivation = false;

        while (uIElements.fillbar.fillAmount < 1)
        {
            uIElements.fillbar.fillAmount += Time.deltaTime / 3;
            yield return null;
        }
        asyncLoad.allowSceneActivation = true;
    }

    //public void SelectedModel(int index)
    //{
    //    GameManager.Instance.selectedmodel = index;
    //    CharacterImageInSelection.sprite = CharacterSprites[index];
    //}
    //public void SelectAvatar(int index)
    //{
    //    SaveData.Instance.SelectedAvatar = index;
    //    botImageInPanel.sprite = botImageInScene.sprite = botSprites[index];
    //}
    //public void GetName()
    //{
    //    //SaveData.Instance.PlayerName = InputField.GetComponent<Text>().text;
    //    if (InputField.GetComponent<Text>().text == "")
    //    {
    //        NameInScene.GetComponent<Text>().text = SaveData.Instance.ProfileName;
    //    }
    //    else
    //    {
    //        SaveData.Instance.ProfileName = NameInScene.text = InputField.text;
    //    }
    //    //print(ShowNameInPanel.GetComponent<Text>().text);
    //    InfoPanel.SetActive(false);
    //    //SaveData.Instance.PlayerName = NameInScene.text = ShowNameInPanel.GetComponent<Text>().text;
    //    GSF_SaveLoad.SaveProgress();
    //}
}