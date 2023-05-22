using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using DG.Tweening;


[System.Serializable]
public class PopMusicElements
{
    [Header("Panels")]
    public GameObject vsPanel;
    public GameObject vsAnimPanel, gamePanel, submitPanel, screenShotPanel, judgementalPanel, stageDecuration, levelCompletePanel, adPanel, loadingPanel;
    [Header("PopUp")]
    public GameObject videoUnlockPopUp;
    public GameObject coinUnlockPopUp, enoughCoinPopUp, videoAdNotAvailablePopUp, warningPopUp;
    [Header("Scrollers")]
    public GameObject allScroller;
    public GameObject dressUpCategoryScroller, makeUpCategoryScroller, topScroller, bangleScroller, dressScroller, earingScroller, bottomScroller, eyebrowScroller, blushScroller,
                      eyeshadeScroller, necklaceScroller, hairScroller, lipsScroller, shoesScroller;
    [Header("UI")]
    public GameObject dressUpBtn;
    public GameObject makeUpBtn, connected, startBtn, coinSlot, previewBtn, scoreSlot, submitPanelbar, lastPanel;
    [Header("PopMusic Image")]
    public Image bgImage;
    public Image cardImage, screenShotImage, fillbar;

    [Header("Player Score")]
    public GameObject scoreMoveableParticle;
    public GameObject dotAnim;
}

[System.Serializable]
public class PopMusicPlayerElenemts
{
    [Header("Player Character")]
    public GameObject character;
    [Header("Player Images")]
    public Image topImage;
    public Image bangleImage, dressImage, earingImage, bottomImage, eyeshadeImage, necklaceImage, hairImage, lipsImage, shoesImage, eyebrowImage, blushImage;
    [Header("Player Score Text")]
    public Text txtPlayerScore;
    public Text voteScore, totalScore;
    [Header("Player Winner")]
    public GameObject winner;
}

[System.Serializable]
public class PopMusicOpponentElenemts
{
    [Header("Opponent Character")]
    public GameObject character;
    [Header("Opponent Images")]
    public Image topImage;
    public Image bangleImage, dressImage, earingImage, bottomImage, eyeshadeImage, necklaceImage, hairImage, lipsImage, shoesImage, eyebrowImage, blushImage;
    [Header("Opponent Bot Images")]
    public Image botInVsPanel;
    public Image botInVsAnimPanel, botInGamePlay, botInJudgementalPanel;
    [Header("Opponent Text")]
    public Text nameVsPanel;
    public Text txtOppoScore, voteScore, totalScore;
    [Header("Opponent Winner")]
    public GameObject winner;
}

[System.Serializable]
public enum PopMusicSelectedItem
{
    dress, top, bottom, bangle, earing, necklace, shoes, lips, hair, eyeshade, eyebrow, blush
}

public class PopMusic : MonoBehaviour
{
    public PopMusicSelectedItem selectedItem;
    [FoldoutGroup("UI Elements")]
    [HideLabel]
    public PopMusicElements uIElements;
    [FoldoutGroup("Player Elements")]
    [HideLabel]
    public PopMusicPlayerElenemts playerElements;
    [FoldoutGroup("Opponent Elements")]
    [HideLabel]
    public PopMusicOpponentElenemts oppoElements;
    [Header("Mover Item")]
    public MRS_Manager playerCharacterMover;
    public MRS_Manager oppoCharacterMover;
    public CoinsAdder coinsAdder;
    [Header("Sprites Array")]
    public Sprite[] cardSprites;
    public Sprite[] botSprites;
    public Sprite[] topSprites;
    public Sprite[] bangleSprites;
    public Sprite[] dressSprites;
    public Sprite[] earingSprites;
    public Sprite[] bottomSprites;
    public Sprite[] eyeshadeSprites;
    public Sprite[] necklaceSprites;
    public Sprite[] hairSprites;
    public Sprite[] lipsSprites;
    public Sprite[] shoesSprites;
    public Sprite[] eyebrowSprites;
    public Sprite[] blushSprites;
    public Sprite[] defaultIconSprites;
    public Sprite[] selectedIconSprites;
    [Header("Scroller Btn Image Array")]
    public Image[] categoryBtn;
    [Header("Item List")]
    private List<ItemInfo> topList = new List<ItemInfo>();
    private List<ItemInfo> bangleList = new List<ItemInfo>();
    private List<ItemInfo> dressList = new List<ItemInfo>();
    private List<ItemInfo> earingList = new List<ItemInfo>();
    private List<ItemInfo> bottomList = new List<ItemInfo>();
    private List<ItemInfo> eyeshadeList = new List<ItemInfo>();
    private List<ItemInfo> necklaceList = new List<ItemInfo>();
    private List<ItemInfo> hairList = new List<ItemInfo>();
    private List<ItemInfo> eyebrowList = new List<ItemInfo>();
    private List<ItemInfo> blushList = new List<ItemInfo>();
    private List<ItemInfo> lipsList = new List<ItemInfo>();
    private List<ItemInfo> shoesList = new List<ItemInfo>();
    private int[] bangleScroe = { 1965, 3186, 6593, 5914, 1500, 2963, 1479, 7598, 1665, 6985, 2008, 6521, 1240, 1654, 4452, 3464};
    private int[] earingScroe = { 1964, 3175, 2595, 3815, 5805, 3735, 2475, 5487, 3578, 4886, 2455, 3425, 3282, 1546, 2354};
    private int[] dressScroe = { 1564, 3245, 2845, 1515, 4705, 1635, 2585, 4357, 3065, 4546, 1457, 2415, 5162, 3245, 5456};
    private int[] topScroe = { 1763, 1296, 4584, 3715, 2401, 1744, 2568, 4589, 2054, 5726, 3548, 5128, 2381, 2585, 1472, 6453};
    private int[] necklaceScroe = { 2874, 2165, 1485, 2415, 4705, 2734, 2185, 2467, 3578, 3526, 1456, 2585, 1272, 2945, 2464 };
    private int[] bottomScroe = { 2864, 2185, 4593, 6915, 3505, 3965, 3476, 6489, 3676, 5876, 3425, 4523, 1242, 1656 };
    private int[] blushScroe = {5481, 2894, 9545, 4784, 8415, 4892, 1654, 8756, 5645, 6156, 3121, 5726, 1161, 1644};
    private int[] eyebrowScroe = {2154, 8421, 2184, 3214, 8489, 7212, 1848, 4231, 2484, 2156, 4844, 2391, 9681, 0824};
    private int[] eyeshadeScroe = {2112, 1421, 4821, 5212, 1613, 8264, 1262, 3162, 1142, 3146, 3121, 2984};
    private int[] hairScroe = {2156, 4121, 5451, 4923, 1842, 1564, 8921, 4215, 4121, 5648, 2165, 2118};
    private int[] lipsScroe = {1164, 5421, 2174, 3214, 5489, 7252, 1643, 2181, 2974, 2125, 4754, 2381, 1651, 3726};
    private int[] shoesScroe = {2132, 4842, 1482, 2164, 4512, 1641, 2316, 6213, 2156, 4821, 1568, 4895, 1231, 6123};
    [Header("Default Character Sprites")]
    public Sprite defaultdress;
    public Sprite defaultHair, defaultLips, defaultEyebrow;
    [Header("Sprites")]
    public Sprite bgSprite;
    public Sprite connectedSprite, defaultBtnSprite, selectedBtnSprite, submitPanelBgSprite, judgementalPanelBgSprite, selectionSelectedSprite, selectionDefaultSprite, greenSprite,
                  graySprite, tickSprite;
    [Header("Text")]
    public Text totalCoins;
    public Text itemUnlockCoin, categoriesText;
    [Header("ItemInfo Variable")]
    private ItemInfo tempItem;
    [Header("Different Index")]
    private int selectedIndex;
    [Header("Bool Variable")]
    private bool canShowInterstitial;
    [Header("Animator")]
    public Animator categorySlot;
    public Animator voteCard;
    [Header("Particles")]
    public GameObject submitPartical;
    public GameObject finalPartical;
    [Header("AudioSources")]
    public AudioSource categorySFX;
    public AudioSource purchaseSFX, itemSelectSFX, vsAnimSFX, winSFX, loseSFX, voteCatSFX, voteGivenSFX;
    public AudioSource[] voiceSounds;

    private int playerScore = 0;
    private int oppoScore = 0;
    int playerTotalScore, oppoTotalScore = 0;

    private bool IsDressing, IsAccessioress, IsMakeup;
    private int playerdressScore, playershoesScore, playerbangleScore, playerearingScore, playernecklaceScore, playertopScore, playerbottomScore, playerlipsScore, playerhairScore,
                playereyeshadeScore, playereyebrowScore, playerblushScore = 0;
    private int oppodressScore, opposhoesScore, oppobangleScore, oppoearingScore, opponecklaceScore, oppotopScore, oppobottomScore, oppolipsScore, oppohairScore, oppoeyeshadeScore,
                oppoeyebrowScore, oppoblushScore = 0;
    private enum RewardType
    {
        none, coins, multipulOfTwo, selectionItem
    }
    private RewardType rewardType;
    private enum SelectionName
    {
        Sita, Aashvi, Mirza, Saia, Surya, Zoya, Alani, Divya, Jaya, Khushi, Nirvi, Sana, Uma, Ziya, Zunaira, Sonia, Shivani, Prisha, Kiyana, Jasmine, Misha,
        Lakshmi, Kareena
    }
    private SelectionName Name;

    #region Start
    // Start is called before the first frame update
    void Start()
    {
        Usman_SaveLoad.LoadProgress();
        selectedItem = PopMusicSelectedItem.dress;
        uIElements.dressScroller.SetActive(true);
        SetInitialValues();
        GetItemsInfo();
        StartCoroutine(AdDelay(45));
        totalCoins.text = SaveData.Instance.Coins.ToString();
        StartCoroutine(findOpponent());
    }
    public void ShowInterstitial()
    {
        if (MyAdsManager.instance)
        {
            MyAdsManager.instance.ShowInterstitialAds();
        }
    }

    void OnEnable()
    {
        if (MyAdsManager.Instance != null)
        {
            MyAdsManager.Instance.onRewardedVideoAdCompletedEvent += OnRewardedVideoComplete;
        }
    }

    void OnDisable()
    {
        if (MyAdsManager.Instance != null)
        {
            MyAdsManager.Instance.onRewardedVideoAdCompletedEvent -= OnRewardedVideoComplete;
        }
    }
    #endregion

    #region SetInitialValues
    private void SetInitialValues()
    {

        #region Initialing top
        if (uIElements.topScroller)
        {
            var topInfo = uIElements.topScroller.GetComponentsInChildren<ItemInfo>();
            for (int i = 0; i < topInfo.Length; i++)
            {
                topList.Add(topInfo[i]);
            }
        }
        SetupItemData(SaveData.Instance.PopMusicModeElements.top, topList);
        SetItemIcon(topList, topSprites);
        #endregion
        
        #region Initialing bangle
        if (uIElements.bangleScroller)
        {
            var bangleInfo = uIElements.bangleScroller.GetComponentsInChildren<ItemInfo>();
            for (int i = 0; i < bangleInfo.Length; i++)
            {
                bangleList.Add(bangleInfo[i]);
            }
        }
        SetupItemData(SaveData.Instance.PopMusicModeElements.bangle, bangleList);
        SetItemIcon(bangleList, bangleSprites);
        #endregion

        #region Initialing dress
        if (uIElements.dressScroller)
        {
            var dressInfo = uIElements.dressScroller.GetComponentsInChildren<ItemInfo>();
            for (int i = 0; i < dressInfo.Length; i++)
            {
                dressList.Add(dressInfo[i]);
            }
        }
        SetupItemData(SaveData.Instance.PopMusicModeElements.dress, dressList);
        SetItemIcon(dressList, dressSprites);
        #endregion

        #region Initialing earing
        if (uIElements.earingScroller)
        {
            var earingInfo = uIElements.earingScroller.GetComponentsInChildren<ItemInfo>();
            for (int i = 0; i < earingInfo.Length; i++)
            {
                earingList.Add(earingInfo[i]);
            }
        }
        SetupItemData(SaveData.Instance.PopMusicModeElements.earing, earingList);
        SetItemIcon(earingList, earingSprites);
        #endregion

        #region Initialing bottom
        if (uIElements.bottomScroller)
        {
            var bottomInfo = uIElements.bottomScroller.GetComponentsInChildren<ItemInfo>();
            for (int i = 0; i < bottomInfo.Length; i++)
            {
                bottomList.Add(bottomInfo[i]);
            }
        }
        SetupItemData(SaveData.Instance.PopMusicModeElements.bottom, bottomList);
        SetItemIcon(bottomList, bottomSprites);
        #endregion

        #region Initialing eyeshade
        if (uIElements.eyeshadeScroller)
        {
            var eyeshadeInfo = uIElements.eyeshadeScroller.GetComponentsInChildren<ItemInfo>();
            for (int i = 0; i < eyeshadeInfo.Length; i++)
            {
                eyeshadeList.Add(eyeshadeInfo[i]);
            }
        }
        SetupItemData(SaveData.Instance.PopMusicModeElements.eyeshade, eyeshadeList);
        SetItemIcon(eyeshadeList, eyeshadeSprites);
        #endregion

        #region Initialing necklace
        if (uIElements.necklaceScroller)
        {
            var necklaceInfo = uIElements.necklaceScroller.GetComponentsInChildren<ItemInfo>();
            for (int i = 0; i < necklaceInfo.Length; i++)
            {
                necklaceList.Add(necklaceInfo[i]);
            }
        }
        SetupItemData(SaveData.Instance.PopMusicModeElements.necklace, necklaceList);
        SetItemIcon(necklaceList, necklaceSprites);
        #endregion

        #region Initialing hair
        if (uIElements.hairScroller)
        {
            var hairInfo = uIElements.hairScroller.GetComponentsInChildren<ItemInfo>();
            for (int i = 0; i < hairInfo.Length; i++)
            {
                hairList.Add(hairInfo[i]);
            }
        }
        SetupItemData(SaveData.Instance.PopMusicModeElements.hair, hairList);
        SetItemIcon(hairList, hairSprites);
        #endregion

        #region Initialing blush
        if (uIElements.blushScroller)
        {
            var blushInfo = uIElements.blushScroller.GetComponentsInChildren<ItemInfo>();
            for (int i = 0; i < blushInfo.Length; i++)
            {
                blushList.Add(blushInfo[i]);
            }
        }
        SetupItemData(SaveData.Instance.PopMusicModeElements.blush, blushList);
        SetItemIcon(blushList, blushSprites);
        #endregion

        #region Initialing eyebrow
        if (uIElements.eyebrowScroller)
        {
            var eyebrowInfo = uIElements.eyebrowScroller.GetComponentsInChildren<ItemInfo>();
            for (int i = 0; i < eyebrowInfo.Length; i++)
            {
                eyebrowList.Add(eyebrowInfo[i]);
            }
        }
        SetupItemData(SaveData.Instance.PopMusicModeElements.eyebrow, eyebrowList);
        SetItemIcon(eyebrowList, eyebrowSprites);
        #endregion

        #region Initialing lips
        if (uIElements.lipsScroller)
        {
            var lipsInfo = uIElements.lipsScroller.GetComponentsInChildren<ItemInfo>();
            for (int i = 0; i < lipsInfo.Length; i++)
            {
                lipsList.Add(lipsInfo[i]);
            }
        }
        SetupItemData(SaveData.Instance.PopMusicModeElements.lips, lipsList);
        SetItemIcon(lipsList, lipsSprites);
        #endregion

        #region Initialing shoes
        if (uIElements.shoesScroller)
        {
            var shoesInfo = uIElements.shoesScroller.GetComponentsInChildren<ItemInfo>();
            for (int i = 0; i < shoesInfo.Length; i++)
            {
                shoesList.Add(shoesInfo[i]);
            }
        }
        SetupItemData(SaveData.Instance.PopMusicModeElements.shoes, shoesList);
        SetItemIcon(shoesList, shoesSprites);
        #endregion

        Usman_SaveLoad.SaveProgress();
    }
    #endregion

    #region SetupItemData
    private void SetupItemData(List<bool> unlockItems, List<ItemInfo> _ItemsInfo)
    {
        if (_ItemsInfo.Count > 0)
        {
            if (unlockItems.Count < _ItemsInfo.Count)
            {
                for (int i = 0; i < _ItemsInfo.Count; i++)
                {
                    if (unlockItems.Count <= i)
                    {
                        // Add new data to SaveData file in case the file is empty or new data is available
                        unlockItems.Add(_ItemsInfo[i].isLocked);
                    }
                }
            }
            // Setting up Hairs Properties to actual Properties from SaveData file  
            for (int i = 0; i < _ItemsInfo.Count; i++)
            {
                _ItemsInfo[i].isLocked = unlockItems[i];
            }
            //Adding Click listeners to btns 
            for (int i = 0; i < _ItemsInfo.Count; i++)
            {
                int Index = i;
                if (_ItemsInfo[i].itemBtn)
                {
                    _ItemsInfo[i].itemBtn.onClick.AddListener(() =>
                    {
                        selectedIndex = Index;
                        SelectItem(Index);
                    });
                }
            }
        }
    }
    #endregion

    #region SetItemIcon
    private void SetItemIcon(List<ItemInfo> refList, Sprite[] btnIcons)
    {
        if (refList != null)
        {
            for (int i = 0; i < refList.Count; i++)
            {
                if (btnIcons.Length > i)
                {
                    if (btnIcons[i] && refList[i].itemIcon)
                    {
                        refList[i].itemIcon.sprite = btnIcons[i];
                    }
                }
            }
        }
    }
    #endregion

    #region SelectItem
    public void SelectItem(int index)
    {
        if (selectedItem == PopMusicSelectedItem.top)
        {
            CheckSelectedItem(topList, topSprites, playerElements.topImage);
        }
        else if (selectedItem == PopMusicSelectedItem.bangle)
        {
            CheckSelectedItem(bangleList, bangleSprites, playerElements.bangleImage);
        }
        else if (selectedItem == PopMusicSelectedItem.dress)
        {
            CheckSelectedItem(dressList, dressSprites, playerElements.dressImage);
        }
        else if (selectedItem == PopMusicSelectedItem.earing)
        {
            CheckSelectedItem(earingList, earingSprites, playerElements.earingImage);
        } 
        else if (selectedItem == PopMusicSelectedItem.bottom)
        {
            CheckSelectedItem(bottomList, bottomSprites, playerElements.bottomImage);
        }
        else if (selectedItem == PopMusicSelectedItem.eyeshade)
        {
            CheckSelectedItem(eyeshadeList, eyeshadeSprites, playerElements.eyeshadeImage);
        }
        else if (selectedItem == PopMusicSelectedItem.necklace)
        {
            CheckSelectedItem(necklaceList, necklaceSprites, playerElements.necklaceImage);
        }
        else if (selectedItem == PopMusicSelectedItem.hair)
        {
            CheckSelectedItem(hairList, hairSprites, playerElements.hairImage);
        }
        else if (selectedItem == PopMusicSelectedItem.lips)
        {
            CheckSelectedItem(lipsList, lipsSprites, playerElements.lipsImage);
        }
        else if (selectedItem == PopMusicSelectedItem.shoes)
        {
            CheckSelectedItem(shoesList, shoesSprites, playerElements.shoesImage);
        }
        else if (selectedItem == PopMusicSelectedItem.blush)
        {
            CheckSelectedItem(blushList, blushSprites, playerElements.blushImage);
        }
        else if (selectedItem == PopMusicSelectedItem.eyebrow)
        {
            CheckSelectedItem(eyebrowList, eyebrowSprites, playerElements.eyebrowImage);
        }
        GetItemsInfo();
        totalCoins.text = SaveData.Instance.Coins.ToString();
    }
    #endregion

    #region CheckSelectedItem
    private void CheckSelectedItem(List<ItemInfo> itemInfoList, Sprite[] itemSprites, Image itemImage)
    {
        
        rewardType = RewardType.selectionItem;
        if (itemInfoList.Count > selectedIndex)
        {
            tempItem = itemInfoList[selectedIndex];
            if (itemInfoList[selectedIndex].isLocked)
            {
                if (itemInfoList[selectedIndex].videoUnlock)
                {
                    //CheckVideoStatus();
                    uIElements.videoUnlockPopUp.SetActive(true);
                    uIElements.previewBtn.SetActive(false);
                }
                else if (itemInfoList[selectedIndex].coinsUnlock)
                {
                    uIElements.coinUnlockPopUp.SetActive(true);
                    uIElements.previewBtn.SetActive(false);
                    itemUnlockCoin.text = itemInfoList[selectedIndex].requiredCoins.ToString();
                }
            }
            else
            {
                if (itemSprites.Length > selectedIndex)
                {
                    if (itemSprites[selectedIndex])
                    {
                        if (itemImage)
                        {

                            if (selectedItem == PopMusicSelectedItem.dress)
                            {
                                IsDressing = true;
                                playerElements.dressImage.gameObject.SetActive(true);
                                playerElements.topImage.gameObject.SetActive(false);
                                playerElements.bottomImage.gameObject.SetActive(false);
                                if (playerdressScore == int.Parse(itemInfoList[selectedIndex].ItemScore.text))
                                {
                                    return;
                                }

                                if (playerdressScore == 0)
                                {
                                    playerScore = playerScore + int.Parse(itemInfoList[selectedIndex].ItemScore.text);
                                    playerdressScore = int.Parse(itemInfoList[selectedIndex].ItemScore.text);
                                }
                                else
                                {
                                    playerScore = playerScore - playerdressScore;
                                    playerScore = playerScore + int.Parse(itemInfoList[selectedIndex].ItemScore.text);
                                    playerdressScore = int.Parse(itemInfoList[selectedIndex].ItemScore.text);
                                }

                                if (playertopScore > 0)
                                {
                                    playerScore = playerScore - playertopScore;
                                    playertopScore = 0;
                                }
                                if (playerbottomScore > 0)
                                {
                                    playerScore = playerScore - playerbottomScore;
                                    playerbottomScore = 0;
                                }

                            }

                            else if (selectedItem == PopMusicSelectedItem.top)
                            {
                                IsDressing = true;
                                playerElements.dressImage.gameObject.SetActive(false);
                                playerElements.topImage.gameObject.SetActive(true);
                                playerElements.bottomImage.gameObject.SetActive(true);
                                if (playertopScore == int.Parse(itemInfoList[selectedIndex].ItemScore.text))
                                {
                                    return;
                                }

                                if (playertopScore == 0)
                                {
                                    playerScore = playerScore + int.Parse(itemInfoList[selectedIndex].ItemScore.text);
                                    playertopScore = int.Parse(itemInfoList[selectedIndex].ItemScore.text);
                                }
                                else
                                {
                                    playerScore = playerScore - playertopScore;
                                    playerScore = playerScore + int.Parse(itemInfoList[selectedIndex].ItemScore.text);
                                    playertopScore = int.Parse(itemInfoList[selectedIndex].ItemScore.text);
                                }

                                if (playerdressScore > 0)
                                {
                                    playerScore = playerScore - playerdressScore;
                                    playerdressScore = 0;
                                }

                            }

                            else if (selectedItem == PopMusicSelectedItem.bottom)
                            {
                                IsDressing = true;
                                playerElements.dressImage.gameObject.SetActive(false);
                                playerElements.topImage.gameObject.SetActive(true);
                                playerElements.bottomImage.gameObject.SetActive(true);
                                if (playerbottomScore == int.Parse(itemInfoList[selectedIndex].ItemScore.text))
                                {
                                    return;
                                }

                                if (playerbottomScore == 0)
                                {
                                    playerScore = playerScore + int.Parse(itemInfoList[selectedIndex].ItemScore.text);
                                    playerbottomScore = int.Parse(itemInfoList[selectedIndex].ItemScore.text);
                                }
                                else
                                {
                                    playerScore = playerScore - playerbottomScore;
                                    playerScore = playerScore + int.Parse(itemInfoList[selectedIndex].ItemScore.text);
                                    playerbottomScore = int.Parse(itemInfoList[selectedIndex].ItemScore.text);
                                }

                                if (playerdressScore > 0)
                                {
                                    playerScore = playerScore - playerdressScore;
                                    playerdressScore = 0;
                                }
                            }

                            else if (selectedItem == PopMusicSelectedItem.shoes)
                            {
                                IsDressing = true;
                                if (playershoesScore == int.Parse(itemInfoList[selectedIndex].ItemScore.text))
                                {
                                    return;
                                }

                                if (playershoesScore == 0)
                                {
                                    playerScore = playerScore + int.Parse(itemInfoList[selectedIndex].ItemScore.text);
                                    playershoesScore = int.Parse(itemInfoList[selectedIndex].ItemScore.text);
                                }
                                else
                                {
                                    playerScore = playerScore - playershoesScore;
                                    playerScore = playerScore + int.Parse(itemInfoList[selectedIndex].ItemScore.text);
                                    playershoesScore = int.Parse(itemInfoList[selectedIndex].ItemScore.text);
                                }
                            }

                            else if (selectedItem == PopMusicSelectedItem.necklace)
                            {
                                IsAccessioress = true;
                                if (playernecklaceScore == int.Parse(itemInfoList[selectedIndex].ItemScore.text))
                                {
                                    return;
                                }

                                if (playernecklaceScore == 0)
                                {
                                    playerScore = playerScore + int.Parse(itemInfoList[selectedIndex].ItemScore.text);
                                    playernecklaceScore = int.Parse(itemInfoList[selectedIndex].ItemScore.text);
                                }
                                else
                                {
                                    playerScore = playerScore - playernecklaceScore;
                                    playerScore = playerScore + int.Parse(itemInfoList[selectedIndex].ItemScore.text);
                                    playernecklaceScore = int.Parse(itemInfoList[selectedIndex].ItemScore.text);
                                }
                            }

                            else if (selectedItem == PopMusicSelectedItem.bangle)
                            {
                                IsAccessioress = true;
                                if (playerbangleScore == int.Parse(itemInfoList[selectedIndex].ItemScore.text))
                                {
                                    return;
                                }

                                if (playerbangleScore == 0)
                                {
                                    playerScore = playerScore + int.Parse(itemInfoList[selectedIndex].ItemScore.text);
                                    playerbangleScore = int.Parse(itemInfoList[selectedIndex].ItemScore.text);
                                }
                                else
                                {
                                    playerScore = playerScore - playerbangleScore;
                                    playerScore = playerScore + int.Parse(itemInfoList[selectedIndex].ItemScore.text);
                                    playerbangleScore = int.Parse(itemInfoList[selectedIndex].ItemScore.text);
                                }
                            }

                            else if (selectedItem == PopMusicSelectedItem.earing)
                            {
                                IsAccessioress = true;
                                if (playerearingScore == int.Parse(itemInfoList[selectedIndex].ItemScore.text))
                                {
                                    return;
                                }

                                if (playerearingScore == 0)
                                {
                                    playerScore = playerScore + int.Parse(itemInfoList[selectedIndex].ItemScore.text);
                                    playerearingScore = int.Parse(itemInfoList[selectedIndex].ItemScore.text);
                                }
                                else
                                {
                                    playerScore = playerScore - playerearingScore;
                                    playerScore = playerScore + int.Parse(itemInfoList[selectedIndex].ItemScore.text);
                                    playerearingScore = int.Parse(itemInfoList[selectedIndex].ItemScore.text);
                                }
                            } 

                            else if (selectedItem == PopMusicSelectedItem.eyeshade)
                            {
                                IsMakeup = true;
                                if (playereyeshadeScore == int.Parse(itemInfoList[selectedIndex].ItemScore.text))
                                {
                                    return;
                                }

                                if (playereyeshadeScore == 0)
                                {
                                    playerScore = playerScore + int.Parse(itemInfoList[selectedIndex].ItemScore.text);
                                    playereyeshadeScore = int.Parse(itemInfoList[selectedIndex].ItemScore.text);
                                }
                                else
                                {
                                    playerScore = playerScore - playereyeshadeScore;
                                    playerScore = playerScore + int.Parse(itemInfoList[selectedIndex].ItemScore.text);
                                    playereyeshadeScore = int.Parse(itemInfoList[selectedIndex].ItemScore.text);
                                }
                            }

                            else if (selectedItem == PopMusicSelectedItem.blush)
                            {
                                IsMakeup = true;
                                if (playerblushScore == int.Parse(itemInfoList[selectedIndex].ItemScore.text))
                                {
                                    return;
                                }

                                if (playerblushScore == 0)
                                {
                                    playerScore = playerScore + int.Parse(itemInfoList[selectedIndex].ItemScore.text);
                                    playerblushScore = int.Parse(itemInfoList[selectedIndex].ItemScore.text);
                                }
                                else
                                {
                                    playerScore = playerScore - playerblushScore;
                                    playerScore = playerScore + int.Parse(itemInfoList[selectedIndex].ItemScore.text);
                                    playerblushScore = int.Parse(itemInfoList[selectedIndex].ItemScore.text);
                                }
                            }

                            else if (selectedItem == PopMusicSelectedItem.eyebrow)
                            {
                                IsMakeup = true;
                                if (playereyebrowScore == int.Parse(itemInfoList[selectedIndex].ItemScore.text))
                                {
                                    return;
                                }

                                if (playereyebrowScore == 0)
                                {
                                    playerScore = playerScore + int.Parse(itemInfoList[selectedIndex].ItemScore.text);
                                    playereyebrowScore = int.Parse(itemInfoList[selectedIndex].ItemScore.text);
                                }
                                else
                                {
                                    playerScore = playerScore - playereyebrowScore;
                                    playerScore = playerScore + int.Parse(itemInfoList[selectedIndex].ItemScore.text);
                                    playereyebrowScore = int.Parse(itemInfoList[selectedIndex].ItemScore.text);
                                }
                            }

                            else if (selectedItem == PopMusicSelectedItem.hair)
                            {
                                IsMakeup = true;
                                if (playerhairScore == int.Parse(itemInfoList[selectedIndex].ItemScore.text))
                                {
                                    return;
                                }

                                if (playerhairScore == 0)
                                {
                                    playerScore = playerScore + int.Parse(itemInfoList[selectedIndex].ItemScore.text);
                                    playerhairScore = int.Parse(itemInfoList[selectedIndex].ItemScore.text);
                                }
                                else
                                {
                                    playerScore = playerScore - playerhairScore;
                                    playerScore = playerScore + int.Parse(itemInfoList[selectedIndex].ItemScore.text);
                                    playerhairScore = int.Parse(itemInfoList[selectedIndex].ItemScore.text);
                                }
                            }

                            else if (selectedItem == PopMusicSelectedItem.lips)
                            {
                                IsMakeup = true;
                                if (playerlipsScore == int.Parse(itemInfoList[selectedIndex].ItemScore.text))
                                {
                                    return;
                                }

                                if (playerlipsScore == 0)
                                {
                                    playerScore = playerScore + int.Parse(itemInfoList[selectedIndex].ItemScore.text);
                                    playerlipsScore = int.Parse(itemInfoList[selectedIndex].ItemScore.text);
                                }
                                else
                                {
                                    playerScore = playerScore - playerlipsScore;
                                    playerScore = playerScore + int.Parse(itemInfoList[selectedIndex].ItemScore.text);
                                    playerlipsScore = int.Parse(itemInfoList[selectedIndex].ItemScore.text);
                                }
                            }

                            voiceSounds[Random.Range(0, voiceSounds.Length)].Play();

                            InstantiateScoreParticle(itemInfoList);
                            
                            itemImage.gameObject.SetActive(false);
                            itemImage.gameObject.SetActive(true);
                            itemImage.sprite = itemSprites[selectedIndex];
                            ItemStatus((int)selectedItem);
                        }
                    }
                }
                CheckInterstitialAD();
            }
        }
    }
    #endregion

    private void InstantiateScoreParticle(List<ItemInfo> itemInfoList)
    {
        var scoreParticle = Instantiate(uIElements.scoreMoveableParticle, itemInfoList[selectedIndex].gameObject.transform);
        scoreParticle.transform.localPosition = Vector3.zero;
        scoreParticle.transform.parent = null;
        scoreParticle.SetActive(true);
    }

    public void AddPlayerScore()
    {
        StartCoroutine(scaleScroe());
        playerElements.txtPlayerScore.text = playerScore.ToString();
    }

    IEnumerator scaleScroe()
    {
        iTween.ScaleFrom(playerElements.txtPlayerScore.gameObject, iTween.Hash("x", 1.5f, "y", 1.5f, "time", 0.3f, "easetype", iTween.EaseType.easeOutBack));
        yield return new WaitForSeconds(0.3f);
    }

    #region ItemStatus
    public void ItemStatus(int itemIndex)
    {
        if(itemIndex == 1)
        {
            categoryBtn[itemIndex - 1].transform.GetChild(1).GetComponent<Image>().sprite = graySprite;
            categoryBtn[itemIndex - 1].transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
            categoryBtn[itemIndex + 1].transform.GetChild(1).GetComponent<Image>().sprite = greenSprite;
            categoryBtn[itemIndex + 1].transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
            categoryBtn[itemIndex + 1].transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = tickSprite;
        }
        else if(itemIndex == 2)
        {
            categoryBtn[itemIndex - 2].transform.GetChild(1).GetComponent<Image>().sprite = graySprite;
            categoryBtn[itemIndex - 2].transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
            categoryBtn[itemIndex - 1].transform.GetChild(1).GetComponent<Image>().sprite = greenSprite;
            categoryBtn[itemIndex - 1].transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
            categoryBtn[itemIndex - 1].transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = tickSprite;
        }
        else if(itemIndex == 0)
        {
            categoryBtn[itemIndex + 1].transform.GetChild(1).GetComponent<Image>().sprite = graySprite;
            categoryBtn[itemIndex + 1].transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
            categoryBtn[itemIndex + 2].transform.GetChild(1).GetComponent<Image>().sprite = graySprite;
            categoryBtn[itemIndex + 2].transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
        }
        categoryBtn[itemIndex].transform.GetChild(1).GetComponent<Image>().sprite = greenSprite;
        categoryBtn[itemIndex].transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
        categoryBtn[itemIndex].transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = tickSprite;
    }
    #endregion

    #region UnlockCoinItems
    public void UnlockCoinItems()
    {
        if (selectedItem == PopMusicSelectedItem.top)
        {
            coinUnlockItem(topList);
        }
        else if (selectedItem == PopMusicSelectedItem.bangle)
        {
            coinUnlockItem(bangleList);
        }
        else if (selectedItem == PopMusicSelectedItem.dress)
        {
            coinUnlockItem(dressList);
        }
        else if (selectedItem == PopMusicSelectedItem.earing)
        {
            coinUnlockItem(earingList);
        }     
        else if (selectedItem == PopMusicSelectedItem.bottom)
        {
            coinUnlockItem(bottomList);
        }
        else if (selectedItem == PopMusicSelectedItem.eyeshade)
        {
            coinUnlockItem(eyeshadeList);
        }
        else if (selectedItem == PopMusicSelectedItem.necklace)
        {
            coinUnlockItem(necklaceList);
        }
        else if (selectedItem == PopMusicSelectedItem.hair)
        {
            coinUnlockItem(hairList);
        }
        else if (selectedItem == PopMusicSelectedItem.lips)
        {
            coinUnlockItem(lipsList);
        }
        else if (selectedItem == PopMusicSelectedItem.shoes)
        {
            coinUnlockItem(shoesList);
        }
        else if (selectedItem == PopMusicSelectedItem.eyebrow)
        {
            coinUnlockItem(eyebrowList);
        }
        GetItemsInfo();
        totalCoins.text = SaveData.Instance.Coins.ToString();
    }
    #endregion

    #region CoinUnlockFunction
    public void coinUnlockItem(List<ItemInfo> itemInfoList)
    {
        if (SaveData.Instance.Coins >= itemInfoList[selectedIndex].requiredCoins)
        {
            uIElements.coinUnlockPopUp.SetActive(false);
            itemInfoList[selectedIndex].isLocked = false;
            SaveData.Instance.Coins -= itemInfoList[selectedIndex].requiredCoins;
            UnlockSingleItem();
            if (purchaseSFX) purchaseSFX.Play();
            SelectItem(selectedIndex);
            uIElements.previewBtn.SetActive(true);
        }
        else
        {
            if (uIElements.enoughCoinPopUp)
                uIElements.enoughCoinPopUp.SetActive(true);
        }
    }
    #endregion

    #region GetItemsInfo
    private void GetItemsInfo()
    {
        if (selectedItem == PopMusicSelectedItem.top)
        {
            SetItemsInfo(topList, topScroe);
        }
        else if (selectedItem == PopMusicSelectedItem.bangle)
        {
            SetItemsInfo(bangleList, bangleScroe);
        }
        if (selectedItem == PopMusicSelectedItem.dress)
        {
            SetItemsInfo(dressList, dressScroe);
        }
        else if (selectedItem == PopMusicSelectedItem.earing)
        {
            SetItemsInfo(earingList, earingScroe);
        }
        else if (selectedItem == PopMusicSelectedItem.bottom)
        {
            SetItemsInfo(bottomList, bottomScroe);
        }
        else if (selectedItem == PopMusicSelectedItem.eyeshade)
        {
            SetItemsInfo(eyeshadeList, eyeshadeScroe);
        }
        else if (selectedItem == PopMusicSelectedItem.necklace)
        {
            SetItemsInfo(necklaceList, necklaceScroe);
        }
        else if (selectedItem == PopMusicSelectedItem.hair)
        {
            SetItemsInfo(hairList, hairScroe);
        }
        else if (selectedItem == PopMusicSelectedItem.lips)
        {
            SetItemsInfo(lipsList, lipsScroe);
        }
        else if (selectedItem == PopMusicSelectedItem.shoes)
        {
            SetItemsInfo(shoesList, shoesScroe);
        }
        else if (selectedItem == PopMusicSelectedItem.blush)
        {
            SetItemsInfo(blushList, blushScroe);
        }
        else if (selectedItem == PopMusicSelectedItem.eyebrow)
        {
            SetItemsInfo(eyebrowList, eyebrowScroe);
        }
    }
    #endregion

    #region SetItemsInfo
    private void SetItemsInfo(List<ItemInfo> _ItemInfo,int[] itemScore)
    {
        if (_ItemInfo == null) return;
        for (int i = 0; i < _ItemInfo.Count; i++)
        {
            _ItemInfo[i].ItemScore.text = itemScore[i].ToString();
           
            if (_ItemInfo[i].btnBG)
            {
                if (i == selectedIndex)
                {
                    _ItemInfo[i].btnBG.sprite = selectionSelectedSprite;
                }
                else
                {
                    _ItemInfo[i].btnBG.sprite = selectionDefaultSprite;
                }
            }
            if (_ItemInfo[i].isLocked)
            {
                if (_ItemInfo[i].videoUnlock)
                {
                    if (_ItemInfo[i].videoLock)
                    {
                        _ItemInfo[i].videoLock.SetActive(true);
                    }
                    if (_ItemInfo[i].coinLock)
                    {
                        _ItemInfo[i].coinLock.SetActive(false);
                    }
                }
                else if (_ItemInfo[i].coinsUnlock)
                {
                    if (_ItemInfo[i].videoLock)
                    {
                        _ItemInfo[i].videoLock.SetActive(false);
                    }
                    if (_ItemInfo[i].coinLock)
                    {
                        _ItemInfo[i].coinLock.SetActive(true);
                        if (_ItemInfo[i].unlockCoins)
                        {
                            _ItemInfo[i].unlockCoins.text = _ItemInfo[i].requiredCoins.ToString();
                        }
                    }
                }
            }
            else
            {
                if (_ItemInfo[i].videoLock) _ItemInfo[i].videoLock.SetActive(false);
                if (_ItemInfo[i].coinLock) _ItemInfo[i].coinLock.SetActive(false);
            }
            if(_ItemInfo[i].hotRib) _ItemInfo[i].hotRibben.SetActive(true);
            if(_ItemInfo[i].premiumRib) _ItemInfo[i].premiumRibben.SetActive(true);

        }
    }
    #endregion

    #region SelectedCatagory
    private void DisableScrollers()
    {
        for (int i = 0; i < categoryBtn.Length; i++)
        {
            categoryBtn[i].transform.GetChild(0).GetComponent<Image>().sprite = defaultIconSprites[i];
            categoryBtn[i].GetComponent<Image>().sprite = defaultBtnSprite;
        }
        
        uIElements.topScroller.SetActive(false);
        uIElements.bangleScroller.SetActive(false);
        uIElements.dressScroller.SetActive(false);
        uIElements.earingScroller.SetActive(false);
        uIElements.bottomScroller.SetActive(false);
        uIElements.eyeshadeScroller.SetActive(false);
        uIElements.necklaceScroller.SetActive(false);
        uIElements.hairScroller.SetActive(false);
        uIElements.lipsScroller.SetActive(false);
        uIElements.shoesScroller.SetActive(false);
        uIElements.blushScroller.SetActive(false);
        uIElements.eyebrowScroller.SetActive(false);
    }
    public void SelectedCatagory(int index)
    {
        uIElements.videoUnlockPopUp.SetActive(false);
        uIElements.coinUnlockPopUp.SetActive(false);
        DisableScrollers();
        if (categorySFX) categorySFX.Play();
        categoryBtn[index].transform.GetChild(0).GetComponent<Image>().sprite = selectedIconSprites[index];
        categoryBtn[index].GetComponent<Image>().sprite = selectedBtnSprite;

        if (index == (int)PopMusicSelectedItem.top)
        {
            playerCharacterMover.Move(new Vector3(90, -22, 0), 0.5f, true, false);
            selectedItem = PopMusicSelectedItem.top;
            uIElements.topScroller.SetActive(true);
        }
        else if (index == (int)PopMusicSelectedItem.bangle)
        {
            playerCharacterMover.Move(new Vector3(90, -22, 0), 0.5f, true, false);
            selectedItem = PopMusicSelectedItem.bangle;
            uIElements.bangleScroller.SetActive(true);
        }
        else if (index == (int)PopMusicSelectedItem.dress)
        {
            playerCharacterMover.Move(new Vector3(90, -22, 0), 0.5f, true, false);
            selectedItem = PopMusicSelectedItem.dress;
            uIElements.dressScroller.SetActive(true);
        }
        else if (index == (int)PopMusicSelectedItem.earing)
        {
            playerCharacterMover.Move(new Vector3(20, -550, -1000), 0.5f, true, false);
            selectedItem = PopMusicSelectedItem.earing;
            uIElements.earingScroller.SetActive(true);
        }
        else if (index == (int)PopMusicSelectedItem.bottom)
        {
            playerCharacterMover.Move(new Vector3(90, -22, 0), 0.5f, true, false);
            selectedItem = PopMusicSelectedItem.bottom;
            uIElements.bottomScroller.SetActive(true);
        }
        else if (index == (int)PopMusicSelectedItem.eyeshade)
        {
            playerCharacterMover.Move(new Vector3(20, -550, -1000), 0.5f, true, false);
            selectedItem = PopMusicSelectedItem.eyeshade;
            uIElements.eyeshadeScroller.SetActive(true);
        }
        else if (index == (int)PopMusicSelectedItem.necklace)
        {
            playerCharacterMover.Move(new Vector3(20, -550, -1000), 0.5f, true, false);
            selectedItem = PopMusicSelectedItem.necklace;
            uIElements.necklaceScroller.SetActive(true);
        }
        else if (index == (int)PopMusicSelectedItem.hair)
        {
            playerCharacterMover.Move(new Vector3(20, -550, -1000), 0.5f, true, false);
            selectedItem = PopMusicSelectedItem.hair;
            uIElements.hairScroller.SetActive(true);
        }
        else if (index == (int)PopMusicSelectedItem.lips)
        {
            playerCharacterMover.Move(new Vector3(20, -550, -1000), 0.5f, true, false);
            selectedItem = PopMusicSelectedItem.lips;
            uIElements.lipsScroller.SetActive(true);
        }   
        else if (index == (int)PopMusicSelectedItem.blush)
        {
            playerCharacterMover.Move(new Vector3(20, -550, -1000), 0.5f, true, false);
            selectedItem = PopMusicSelectedItem.blush;
            uIElements.blushScroller.SetActive(true);
        }
        else if (index == (int)PopMusicSelectedItem.eyebrow)
        {
            playerCharacterMover.Move(new Vector3(20, -550, -1000), 0.5f, true, false);
            selectedItem = PopMusicSelectedItem.eyebrow;
            uIElements.eyebrowScroller.SetActive(true);
        }
        else if (index == (int)PopMusicSelectedItem.shoes)
        {
            playerCharacterMover.Move(new Vector3(90, -22, 0), 0.5f, true, false);
            selectedItem = PopMusicSelectedItem.shoes;
            uIElements.shoesScroller.SetActive(true);
        }
        GetItemsInfo();
    }
    #endregion

    #region UnlockSingleItem
    public void UnlockSingleItem()
    {
        if (selectedItem == PopMusicSelectedItem.top)
        {
            SaveData.Instance.PopMusicModeElements.top[selectedIndex] = false;
        }
        else if (selectedItem == PopMusicSelectedItem.bangle)
        {
            SaveData.Instance.PopMusicModeElements.bangle[selectedIndex] = false;
        }
        else if (selectedItem == PopMusicSelectedItem.dress)
        {
            SaveData.Instance.PopMusicModeElements.dress[selectedIndex] = false;
        }
        else if (selectedItem == PopMusicSelectedItem.earing)
        {
            SaveData.Instance.PopMusicModeElements.earing[selectedIndex] = false;
        }
        else if (selectedItem == PopMusicSelectedItem.bottom)
        {
            SaveData.Instance.PopMusicModeElements.bottom[selectedIndex] = false;
        }
        else if (selectedItem == PopMusicSelectedItem.eyeshade)
        {
            SaveData.Instance.PopMusicModeElements.eyeshade[selectedIndex] = false;
        }
        else if (selectedItem == PopMusicSelectedItem.necklace)
        {
            SaveData.Instance.PopMusicModeElements.necklace[selectedIndex] = false;
        }
        else if (selectedItem == PopMusicSelectedItem.hair)
        {
            SaveData.Instance.PopMusicModeElements.hair[selectedIndex] = false;
        }
        else if (selectedItem == PopMusicSelectedItem.lips)
        {
            SaveData.Instance.PopMusicModeElements.lips[selectedIndex] = false;
        }
        else if (selectedItem == PopMusicSelectedItem.shoes)
        {
            SaveData.Instance.PopMusicModeElements.shoes[selectedIndex] = false;
        }
        else if (selectedItem == PopMusicSelectedItem.blush)
        {
            SaveData.Instance.PopMusicModeElements.blush[selectedIndex] = false;
        }
        else if (selectedItem == PopMusicSelectedItem.eyebrow)
        {
            SaveData.Instance.PopMusicModeElements.eyebrow[selectedIndex] = false;
        }
        totalCoins.text = SaveData.Instance.Coins.ToString();
        Usman_SaveLoad.SaveProgress();
    }
    #endregion
    
    #region UnEquipFunction
    public void UnEquipet()
    {
        if (selectedItem == PopMusicSelectedItem.top)
        {
            playerElements.dressImage.sprite = defaultdress;
        }
        else if (selectedItem == PopMusicSelectedItem.bangle)
        {
            playerElements.bangleImage.gameObject.SetActive(false);
        }
        else if (selectedItem == PopMusicSelectedItem.dress)
        {
            playerElements.dressImage.sprite = defaultdress;
        }
        else if (selectedItem == PopMusicSelectedItem.earing)
        {
            playerElements.earingImage.gameObject.SetActive(false);
        }
        else if (selectedItem == PopMusicSelectedItem.bottom)
        {
            playerElements.bottomImage.gameObject.SetActive(false);
        }
        else if (selectedItem == PopMusicSelectedItem.eyeshade)
        {
            playerElements.eyeshadeImage.gameObject.SetActive(false);
        }
        else if (selectedItem == PopMusicSelectedItem.necklace)
        {
            playerElements.necklaceImage.gameObject.SetActive(false);
        }
        else if (selectedItem == PopMusicSelectedItem.hair)
        {
            playerElements.hairImage.sprite = defaultHair;
        }
        else if (selectedItem == PopMusicSelectedItem.lips)
        {
            playerElements.lipsImage.sprite = defaultLips;
        }
        else if (selectedItem == PopMusicSelectedItem.shoes)
        {
            playerElements.shoesImage.gameObject.SetActive(false);
        }
        else if (selectedItem == PopMusicSelectedItem.blush)
        {
            playerElements.blushImage.gameObject.SetActive(false);
        }
        else if (selectedItem == PopMusicSelectedItem.eyebrow)
        {
            playerElements.eyebrowImage.sprite = defaultEyebrow;
        }
        ItemStatusOff((int)selectedItem);
    }
    #endregion

    #region ItemStatusOff
    public void ItemStatusOff(int itemIndex)
    {
        categoryBtn[itemIndex].transform.GetChild(1).GetComponent<Image>().sprite = graySprite;
        categoryBtn[itemIndex].transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
    }
    #endregion

    #region ChangeScroller
    public void ChangeScroller(int a)
    {
        if (a == 0)
        {
            DisableScrollers();
            uIElements.dressScroller.SetActive(true);
            uIElements.dressUpCategoryScroller.SetActive(true);
            uIElements.makeUpCategoryScroller.SetActive(false);
            selectedItem = PopMusicSelectedItem.dress;
            playerCharacterMover.Move(new Vector3(90, -22, 0), 0.5f, true, false);
            uIElements.dressUpBtn.SetActive(false);
            uIElements.makeUpBtn.SetActive(true);
        }
        else if (a == 1)
        {
            DisableScrollers();
            uIElements.lipsScroller.SetActive(true);
            uIElements.makeUpCategoryScroller.SetActive(true);
            uIElements.dressUpCategoryScroller.SetActive(false);
            selectedItem = PopMusicSelectedItem.lips;
            playerCharacterMover.Move(new Vector3(20, -550, -1000), 0.5f, true, false);
            uIElements.makeUpBtn.SetActive(false);
            uIElements.dressUpBtn.SetActive(true);
        }
        GetItemsInfo();
    }
    #endregion

    #region BtnsTask
    public void Preview()
    {
        if (IsDressing == true && IsMakeup == true && IsAccessioress == true)
        {
            StartCoroutine(preview());
        }
        else
        {
            uIElements.warningPopUp.SetActive(true);
        }
    }

    public void SubmitLook()
    {
        StartCoroutine(submitlook());
    }

    public void BackToGamePlay()
    {
        uIElements.bgImage.sprite = bgSprite;
        uIElements.submitPanel.SetActive(false);
        uIElements.allScroller.SetActive(true);
        uIElements.coinSlot.SetActive(true);
        DisableScrollers();
        uIElements.dressScroller.SetActive(true);
        uIElements.dressUpCategoryScroller.SetActive(true);
        uIElements.makeUpCategoryScroller.SetActive(false);
        selectedItem = PopMusicSelectedItem.dress;
        playerCharacterMover.Move(new Vector3(86, -67.49f, 0), 0.5f, true, false);
        uIElements.dressUpBtn.SetActive(false);
        uIElements.makeUpBtn.SetActive(true);
    }

    public void Play(string str)
    {
        finalPartical.gameObject.SetActive(false);
        uIElements.judgementalPanel.SetActive(false);
        uIElements.loadingPanel.SetActive(true);
        StartCoroutine(LoadingScene(str));
    }

    public void StartGame()
    {
        uIElements.vsPanel.SetActive(false);
        uIElements.vsAnimPanel.SetActive(true);
        if (vsAnimSFX) vsAnimSFX.Play();
        StartCoroutine(EnableOrDisable(3f, uIElements.vsAnimPanel, false));
        StartCoroutine(EnableOrDisable(3.2f, uIElements.gamePanel, true));
        StartCoroutine(EnableOrDisable(3.5f, uIElements.coinSlot, true));
        uIElements.bgImage.sprite = bgSprite;
    }

    #endregion

    #region IEnumerator
    IEnumerator findOpponent()
    {

        yield return new WaitForSeconds(2f);
        for (int i = 0; i < Random.Range(10, 25); i++)
        {
            uIElements.cardImage.gameObject.SetActive(false);
            uIElements.cardImage.gameObject.SetActive(true);
            uIElements.cardImage.GetComponent<AudioSource>().Play();
            uIElements.cardImage.sprite = cardSprites[Random.Range(0, cardSprites.Length)];
            yield return new WaitForSeconds(0.1f);
        }
        uIElements.cardImage.sprite = cardSprites[3];

        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < Random.Range(10, 25); i++)
        {
            oppoElements.botInVsPanel.gameObject.SetActive(false);
            oppoElements.botInVsPanel.gameObject.SetActive(true);
            oppoElements.botInVsPanel.GetComponent<AudioSource>().Play();
            oppoElements.botInVsPanel.sprite = botSprites[Random.Range(0, botSprites.Length)];
            oppoElements.botInVsAnimPanel.sprite = oppoElements.botInVsPanel.sprite;
            oppoElements.botInGamePlay.sprite = oppoElements.botInVsPanel.sprite;
            oppoElements.botInJudgementalPanel.sprite = oppoElements.botInVsPanel.sprite;
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < Random.Range(10, 25); i++)
        {
            Name = (SelectionName)Random.Range(0, 23);
            oppoElements.nameVsPanel.gameObject.SetActive(false);
            oppoElements.nameVsPanel.gameObject.SetActive(true);
            oppoElements.nameVsPanel.GetComponent<AudioSource>().Play();
            oppoElements.nameVsPanel.text = Name.ToString();
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.5f);
        uIElements.connected.GetComponent<Image>().sprite = connectedSprite;
        uIElements.connected.GetComponent<AudioSource>().Play();
        StartCoroutine(EnableOrDisable(0.1f, uIElements.startBtn, true));

    }
    IEnumerator preview()
    {
        uIElements.bgImage.sprite = submitPanelBgSprite;
        uIElements.submitPanel.SetActive(true);
        uIElements.allScroller.SetActive(false);
        uIElements.scoreSlot.SetActive(false);
        uIElements.coinSlot.SetActive(false);
        playerCharacterMover.Move(new Vector3(0, -300, -800), 0.7f, true, false);
        yield return new WaitForSeconds(0.68f);
        playerCharacterMover.Move(new Vector3(0, 200, -800), 0.7f, true, false);
        yield return new WaitForSeconds(0.68f);
        playerCharacterMover.Move(new Vector3(0, 96, 0), 0.7f, true, false);
        yield return new WaitForSeconds(0.5f);
        if (winSFX) winSFX.Play();
        submitPartical.SetActive(true);
        playerElements.txtPlayerScore.text = "0000";

    }

    IEnumerator submitlook()
    {
        submitPartical.SetActive(false);
        uIElements.bgImage.sprite = judgementalPanelBgSprite;
        uIElements.judgementalPanel.SetActive(true);
        uIElements.submitPanel.SetActive(false);
        yield return new WaitForSeconds(1f);
        oppoElements.character.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        playerCharacterMover.Move(new Vector3(-290, -50f, 0), 0.7f, true, false);
        yield return new WaitForSeconds(0.3f);
        oppoCharacterMover.Move(new Vector3(290, -50f, 0), 0.7f, true, false);
        SaveData.Instance.vsMode = false;
        yield return new WaitForSeconds(0.2f);
        uIElements.scoreSlot.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        OpponentDressing();
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(startComparing());

    }

    IEnumerator VideoPanelOff()
    {
        yield return new WaitForSeconds(2.2f);
        uIElements.videoAdNotAvailablePopUp.SetActive(false);
    }

    IEnumerator AddCoins(float delay, int coins)
    {
        yield return new WaitForSeconds(delay);
        if (coinsAdder)
        {
            coinsAdder.addCoins = coins;
            coinsAdder.addNow = true;
        }
    }

    IEnumerator AddCoinAnim(int coins)
    {
        yield return null;
        if (coinsAdder)
        {
            coinsAdder.addCoins = coins;
            coinsAdder.addNow = true;
        }
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
    #endregion

    #region GetRewardedCoins
    public void GetRewardedCoins()
    {
        rewardType = RewardType.coins;
        CheckVideoStatus();
    }
    #endregion

    #region CheckVideoStatus
    public void CheckVideoStatus()
    {
        if (MyAdsManager.Instance != null)
        {
            if (MyAdsManager.Instance.IsRewardedAvailable())
            {
                uIElements.enoughCoinPopUp.SetActive(false);
                MyAdsManager.Instance.ShowRewardedVideos();
            }
            else
            {
                uIElements.videoAdNotAvailablePopUp.SetActive(true);
                StartCoroutine(VideoPanelOff());
            }
        }
        else
        {
            uIElements.videoAdNotAvailablePopUp.SetActive(true);
            StartCoroutine(VideoPanelOff());
        }
        //uIElements.videoAdNotAvailablePopUp.SetActive(true);
        //StartCoroutine(VideoPanelOff());
    }

    public void showRewardedVideo()
    {
        if (MyAdsManager.Instance != null)
        {
            if (MyAdsManager.Instance.IsRewardedAvailable())
            {
                uIElements.videoUnlockPopUp.SetActive(false);
                uIElements.coinUnlockPopUp.SetActive(false);
                uIElements.previewBtn.SetActive(true);
                MyAdsManager.Instance.ShowRewardedVideos();
            }
            else
            {
                uIElements.videoAdNotAvailablePopUp.SetActive(true);
                StartCoroutine(VideoPanelOff());
            }
        }
        else
        {
            uIElements.videoAdNotAvailablePopUp.SetActive(true);
            StartCoroutine(VideoPanelOff());
        }
        //uIElements.videoAdNotAvailablePopUp.SetActive(true);
        //StartCoroutine(VideoPanelOff());
    }

    #endregion

    #region RewardedVideoCompleted
    public void OnRewardedVideoComplete()
    {
        if (rewardType == RewardType.selectionItem)
        {
            if (tempItem != null) tempItem.isLocked = false;
            UnlockSingleItem();
            canShowInterstitial = false;
            StartCoroutine(AdDelay(45));
            SelectItem(selectedIndex);
        }
        else if (rewardType == RewardType.coins)
        {
            StartCoroutine(AddCoinAnim(200));
            totalCoins.text = SaveData.Instance.Coins.ToString();
            Usman_SaveLoad.SaveProgress();
        }
        //else if (rewardType == RewardType.multipulOfTwo)
        //{
        //    StartCoroutine(AddCoinAnim(2 * totalReward));
        //    //SaveData.Instance.Coins += 2 * totalReward;
        //    totalCoins.text = SaveData.Instance.Coins.ToString();
        //    Usman_SaveLoad.SaveProgress();
        //}
        GetItemsInfo();
        rewardType = RewardType.none;
        //if (purchaseSFX) purchaseSFX.Play();
    }
    public void changeRewardedCategory()
    {
        rewardType = RewardType.multipulOfTwo;
        CheckVideoStatus();
    }
    #endregion

    #region ShowInterstitialAD
    private void CheckInterstitialAD()
    {
        if (MyAdsManager.Instance != null)
        {

            if (MyAdsManager.Instance.IsInterstitialAvailable() && canShowInterstitial)
            {
                canShowInterstitial = !canShowInterstitial;
                StartCoroutine(AdDelay(45));
                StartCoroutine(ShowInterstitialAD());
            }
        }
    }

    IEnumerator ShowInterstitialAD()
    {
        if (uIElements.adPanel)
        {
            uIElements.adPanel.SetActive(true);
            yield return new WaitForSeconds(1f);
            uIElements.adPanel.SetActive(false);
        }
        ShowInterstitial();
    }
    IEnumerator AdDelay(float _Delay)
    {
        yield return new WaitForSeconds(_Delay);
        canShowInterstitial = !canShowInterstitial;
    }
    #endregion

    #region EnableOrDisable
    IEnumerator EnableOrDisable(float _Delay, GameObject activateObject, bool isTrue)
    {
        yield return new WaitForSecondsRealtime(_Delay);
        activateObject.SetActive(isTrue);
    }
    IEnumerator EnableAnim(float _Delay, Animator activateObject)
    {
        yield return new WaitForSecondsRealtime(_Delay);
        activateObject.enabled = true;
    }
    #endregion

    #region OpponentDressing
    private void OpponentDressing()
    {
        int randomIndex = 0;

        #region dress
        randomIndex = Random.Range(0, dressList.Count);
        if (dressList[randomIndex] && oppoElements.dressImage)
        {
            oppoElements.dressImage.gameObject.SetActive(true);
            oppoElements.topImage.gameObject.SetActive(false);
            oppoElements.bottomImage.gameObject.SetActive(false);
            oppoElements.dressImage.sprite = dressSprites[randomIndex];
        }
         
        oppoScore = oppoScore + int.Parse(dressList[randomIndex].ItemScore.text);
        oppodressScore = int.Parse(dressList[randomIndex].ItemScore.text);
        #endregion

        #region top
        randomIndex = Random.Range(0, topList.Count);
        if (topList[randomIndex] && oppoElements.topImage)
        {
            oppoElements.dressImage.gameObject.SetActive(false);
            oppoElements.topImage.gameObject.SetActive(true);
            oppoElements.bottomImage.gameObject.SetActive(true);
            oppoElements.topImage.sprite = topSprites[randomIndex];
        }
        oppoScore = oppoScore + int.Parse(topList[randomIndex].ItemScore.text);
        oppotopScore = int.Parse(topList[randomIndex].ItemScore.text);
        #endregion

        #region bottom
        randomIndex = Random.Range(0, bottomList.Count);
        if (bottomList[randomIndex] && oppoElements.bottomImage)
        {
            oppoElements.dressImage.gameObject.SetActive(false);
            oppoElements.topImage.gameObject.SetActive(true);
            oppoElements.bottomImage.gameObject.SetActive(true);
            oppoElements.bottomImage.sprite = bottomSprites[randomIndex];
        }
        oppoScore = oppoScore + int.Parse(bottomList[randomIndex].ItemScore.text);
        oppobottomScore = int.Parse(bottomList[randomIndex].ItemScore.text);
        #endregion

        #region shoes
        randomIndex = Random.Range(0, shoesList.Count);
        if (shoesList[randomIndex] && oppoElements.shoesImage)
        {
            oppoElements.shoesImage.gameObject.SetActive(true);
            oppoElements.shoesImage.sprite = shoesSprites[randomIndex];
        }
        oppoScore = oppoScore + int.Parse(shoesList[randomIndex].ItemScore.text);
        opposhoesScore = int.Parse(shoesList[randomIndex].ItemScore.text);
        #endregion

        #region eyeshade
        randomIndex = Random.Range(0, eyeshadeList.Count);
        if (eyeshadeList[randomIndex] && oppoElements.eyeshadeImage)
        {
            oppoElements.eyeshadeImage.gameObject.SetActive(true);
            oppoElements.eyeshadeImage.sprite = eyeshadeSprites[randomIndex];
        }
        oppoScore = oppoScore + int.Parse(eyeshadeList[randomIndex].ItemScore.text);
        oppoeyeshadeScore = int.Parse(eyeshadeList[randomIndex].ItemScore.text);
        #endregion

        #region hair
        randomIndex = Random.Range(0, hairList.Count);
        if (hairList[randomIndex] && oppoElements.hairImage)
        {
            oppoElements.hairImage.gameObject.SetActive(true);
            oppoElements.hairImage.sprite = hairSprites[randomIndex];
        }
        oppoScore = oppoScore + int.Parse(hairList[randomIndex].ItemScore.text);
        oppohairScore = int.Parse(hairList[randomIndex].ItemScore.text);
        #endregion

        #region lips
        randomIndex = Random.Range(0, lipsList.Count);
        if (lipsList[randomIndex] && oppoElements.lipsImage)
        {
            oppoElements.lipsImage.gameObject.SetActive(true);
            oppoElements.lipsImage.sprite = lipsSprites[randomIndex];
        }
        oppoScore = oppoScore + int.Parse(lipsList[randomIndex].ItemScore.text);
        oppolipsScore = int.Parse(lipsList[randomIndex].ItemScore.text);
        #endregion

        #region blush
        randomIndex = Random.Range(0, blushList.Count);
        if (blushList[randomIndex] && oppoElements.blushImage)
        {
            oppoElements.blushImage.gameObject.SetActive(true);
            oppoElements.blushImage.sprite = blushSprites[randomIndex];
        }
        oppoScore = oppoScore + int.Parse(blushList[randomIndex].ItemScore.text);
        oppoblushScore = int.Parse(blushList[randomIndex].ItemScore.text);
        #endregion

        #region eybrow
        randomIndex = Random.Range(0, eyebrowList.Count);
        if (eyebrowList[randomIndex] && oppoElements.eyebrowImage)
        {
            oppoElements.eyebrowImage.gameObject.SetActive(true);
            oppoElements.eyebrowImage.sprite = eyebrowSprites[randomIndex];
        }
        oppoScore = oppoScore + int.Parse(eyebrowList[randomIndex].ItemScore.text);
        oppoeyebrowScore = int.Parse(eyebrowList[randomIndex].ItemScore.text);
        #endregion

        #region earing
        randomIndex = Random.Range(0, earingList.Count);
        if (earingList[randomIndex] && oppoElements.earingImage)
        {
            oppoElements.earingImage.gameObject.SetActive(true);
            oppoElements.earingImage.sprite = earingSprites[randomIndex];
        }
        oppoScore = oppoScore + int.Parse(earingList[randomIndex].ItemScore.text);
        oppoearingScore = int.Parse(earingList[randomIndex].ItemScore.text);
        #endregion

        #region bangle
        randomIndex = Random.Range(0, bangleList.Count);
        if (bangleList[randomIndex] && oppoElements.bangleImage)
        {
            oppoElements.bangleImage.gameObject.SetActive(true);
            oppoElements.bangleImage.sprite = bangleSprites[randomIndex];
        }
        oppoScore = oppoScore + int.Parse(bangleList[randomIndex].ItemScore.text);
        oppobangleScore = int.Parse(bangleList[randomIndex].ItemScore.text);
        #endregion

        #region necklace
        randomIndex = Random.Range(0, necklaceList.Count);
        if (necklaceList[randomIndex] && oppoElements.necklaceImage)
        {
            oppoElements.necklaceImage.gameObject.SetActive(true);
            oppoElements.necklaceImage.sprite = necklaceSprites[randomIndex];
        }
        oppoScore = oppoScore + int.Parse(necklaceList[randomIndex].ItemScore.text);
        opponecklaceScore = int.Parse(necklaceList[randomIndex].ItemScore.text);
        #endregion
    }
    #endregion

    #region Comparing
    IEnumerator startComparing()
    {
        int playerdressupTotal, playermakeupTotal, playeraccessioresTotal;
        int oppodressupTotal, oppomakeupTotal, oppoaccessioresTotal;
        int playerTotal = 0, oppoTotal = 0;
        yield return new WaitForSeconds(2);
        uIElements.dotAnim.SetActive(false);
        oppoElements.txtOppoScore.gameObject.SetActive(true);
        categorySlot.gameObject.SetActive(true);
        categoriesText.text = "DressUp";
        if (voteCatSFX) voteCatSFX.Play();
        categorySlot.Play(0);
        //player
        yield return new WaitForSeconds(2f);
        playerdressupTotal = playerdressScore + playershoesScore + playerbottomScore + playertopScore;
        playerTotal += playerdressupTotal;
        playerElements.voteScore.text = playerdressupTotal.ToString();
        //oppo
        oppodressupTotal = oppodressScore + oppobottomScore + oppotopScore + opposhoesScore;
        oppoTotal += oppodressupTotal;
        oppoElements.voteScore.text = oppodressupTotal.ToString();

        voteCard.gameObject.SetActive(true);
        if (voteGivenSFX) voteGivenSFX.Play();
        voteCard.Play(0);
        TotalScoring();

        yield return new WaitForSeconds(2f);
        categoriesText.text = "MakeUp";
        if (voteCatSFX) voteCatSFX.Play();
        categorySlot.Play(0);
        voteCard.Play(0);
        //player
        yield return new WaitForSeconds(1f);
        playermakeupTotal = playereyeshadeScore + playerlipsScore + playerhairScore + playerblushScore + playereyebrowScore;
        playerTotal += playermakeupTotal;
        playerElements.voteScore.text = playermakeupTotal.ToString();
        //oppo
        oppomakeupTotal = oppoeyeshadeScore + oppolipsScore + oppohairScore + oppoblushScore + oppoeyebrowScore;
        oppoTotal += oppomakeupTotal;
        oppoElements.voteScore.text = oppomakeupTotal.ToString();

        if (voteGivenSFX) voteGivenSFX.Play();
        TotalScoring();


        yield return new WaitForSeconds(2f);
        categoriesText.text = "Accessiores";
        if (voteCatSFX) voteCatSFX.Play();
        categorySlot.Play(0);
        voteCard.Play(0);
        //player
        yield return new WaitForSeconds(1f);
        playeraccessioresTotal = playerearingScore + playernecklaceScore + playerbangleScore;
        playerTotal += playeraccessioresTotal;
        playerElements.voteScore.text = playeraccessioresTotal.ToString();
        //oppo
        oppoaccessioresTotal = oppoearingScore + opponecklaceScore + oppobangleScore;
        oppoTotal += oppoaccessioresTotal;
        oppoElements.voteScore.text = oppoaccessioresTotal.ToString();

        if (voteGivenSFX) voteGivenSFX.Play();
        TotalScoring();

        yield return new WaitForSeconds(2);
        categorySlot.gameObject.SetActive(false);
        voteCard.gameObject.SetActive(false);

        if (playerTotal >= oppoTotal)
        {
            //player win
            if (winSFX) winSFX.Play();
            yield return new WaitForSeconds(1);
            playerElements.winner.SetActive(true);
            yield return new WaitForSeconds(3f);
            playerElements.winner.SetActive(false);
            playerElements.character.transform.SetSiblingIndex(-1);
            uIElements.judgementalPanel.SetActive(false);
            yield return new WaitForSeconds(1f);
            oppoCharacterMover.Move(new Vector3(1500, -50, 0), 0.5f, true, false);
            yield return new WaitForSeconds(1f);
            uIElements.levelCompletePanel.SetActive(true);
            uIElements.stageDecuration.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            playerCharacterMover.Move(new Vector3(55, -110, 0), 0.5f, true, false);
            yield return new WaitForSeconds(0.5f);
            finalPartical.SetActive(true);
            yield return new WaitForSeconds(1f);
            uIElements.coinSlot.SetActive(true);
            StartCoroutine(AddCoins(0.1f, 2000));
            StartCoroutine(EnableOrDisable(5f, uIElements.lastPanel, true));
        }
        else
        {
            if (loseSFX) loseSFX.Play();
            yield return new WaitForSeconds(1);
            oppoElements.winner.SetActive(true);
            yield return new WaitForSeconds(3f);
            oppoElements.winner.SetActive(false);
            oppoElements.character.transform.SetSiblingIndex(-1);
            uIElements.judgementalPanel.SetActive(false);
            yield return new WaitForSeconds(1f);
            playerCharacterMover.Move(new Vector3(-1500, -50, 0), 0.5f, true, false);
            yield return new WaitForSeconds(1f);
            uIElements.levelCompletePanel.SetActive(true);
            uIElements.stageDecuration.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            oppoCharacterMover.Move(new Vector3(55, -110, 0), 0.5f, true, false);
            yield return new WaitForSeconds(0.5f);
            finalPartical.SetActive(true);
            yield return new WaitForSeconds(1f);
            uIElements.coinSlot.SetActive(true);
            StartCoroutine(AddCoins(0.1f, 500));
            StartCoroutine(EnableOrDisable(5f, uIElements.lastPanel, true));
        }
    }
    private void TotalScoring()
    {
        playerTotalScore += int.Parse(playerElements.voteScore.text);
        playerElements.totalScore.text = playerTotalScore.ToString();
        oppoTotalScore += int.Parse(oppoElements.voteScore.text);
        oppoElements.totalScore.text = oppoTotalScore.ToString();
    }
    #endregion

    #region ShareScreenShot
    public void ShareScreenShot()
    {
        uIElements.submitPanelbar.SetActive(false);
        StartCoroutine(shareScreenShot());
    }

    IEnumerator shareScreenShot()
    {
        yield return new WaitForEndOfFrame();
        Texture2D tx = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        tx.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        tx.Apply();
        string path = Path.Combine(Application.temporaryCachePath, "sharedImage.png");//image name
        File.WriteAllBytes(path, tx.EncodeToPNG());

        Destroy(tx); //to avoid memory leaks

        new NativeShare()
            .AddFile(path)
            //.SetSubject("This is my score")
            //.SetText("share your score with your friends")
            .Share();
        //uIElements.SubmitPanel.SetActive(false); //hide the panel
        uIElements.submitPanelbar.SetActive(true);
    }
    #endregion

    #region TakeScreenShot
    Texture2D _Taxture;
    public void TakeScreenShot()
    {
        uIElements.screenShotImage.transform.parent.localScale = Vector3.one;
        //uIElements.previewPanel.SetActive(false);
        StartCoroutine(takeScreenShot());
    }
    IEnumerator takeScreenShot()
    {
        yield return new WaitForEndOfFrame();
        Texture2D _Texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGBA32, false);
        _Texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        _Texture.Apply();
        _Texture.LoadImage(_Texture.EncodeToPNG());
        Sprite sprite = Sprite.Create(_Texture, new Rect(0, 0, _Texture.width, _Texture.height), new Vector2(_Texture.width / 2, _Texture.height / 2));
        if (uIElements.screenShotImage)
        {
            uIElements.screenShotImage.sprite = sprite;
            uIElements.screenShotPanel.SetActive(true);
            //DownloadImage();
        }
        _Taxture = _Texture;
        Invoke("DownloadImage", 0.8f);
    }
    public void DownloadImage()
    {
        string picturName = "ScreenShot_" + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png";
        NativeGallery.SaveImageToGallery(_Taxture, "My Pictures", picturName);
        Invoke("PictureSaved", 0.8f);

    }
    private void PictureSaved()
    {
        uIElements.screenShotPanel.SetActive(false);
        uIElements.submitPanel.SetActive(true);
        Destroy(_Taxture);
    }
    #endregion
}
