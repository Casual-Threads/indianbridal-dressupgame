using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using DG.Tweening;


[System.Serializable]
public class WeddingElements
{
    [Header("Panels")]
    public GameObject vsPanel;
    public GameObject vsAnimPanel, gamePanel, submitPanel, screenShotPanel, judgementalPanel, stageDecuration, levelCompletePanel, adPanel, loadingPanel;
    [Header("PopUp")]
    public GameObject videoUnlockPopUp;
    public GameObject coinUnlockPopUp, enoughCoinPopUp, videoAdNotAvailablePopUp, warningPopUp;
    [Header("Scrollers")]
    public GameObject allScroller;
    public GameObject dressUpCategoryScroller, makeUpCategoryScroller, mathapattiScroller, bindiScroller, bangleScroller, lehangaScroller, earingScroller, noseringScroller, eyebrowScroller,
                      blushScroller, eyeshadeScroller, necklaceScroller, hairScroller, lipsScroller, shoesScroller;
    [Header("UI")]
    public GameObject dressUpBtn;
    public GameObject makeUpBtn, connected, startBtn, coinSlot, previewBtn, scoreSlot, submitPanelbar, lastPanel;
    [Header("Wedding Image")]
    public Image bgImage;
    public Image cardImage, screenShotImage, fillbar;

    [Header("Player Score")]
    public GameObject scoreMoveableParticle;
    public GameObject dotAnim;
}

[System.Serializable]
public class WeddingPlayerElenemts
{
    [Header("Player Character")]
    public GameObject character;
    [Header("Player Images")]
    public Image mathapattiImage;
    public Image bindiImage, leftBangleImage, rightBangleImage, lehangaImage, earingImage, noseringImage, eyeshadeImage, necklaceImage, hairImage, lipsImage, shoesImage, eyebrowImage,
                 blushImage;
    [Header("Player Score Text")]
    public Text txtPlayerScore;
    public Text voteScore, totalScore;
    [Header("Player Winner")]
    public GameObject winner;
}

[System.Serializable]
public class WeddingOpponentElenemts
{
    [Header("Opponent Character")]
    public GameObject character;
    [Header("Opponent Images")]
    public Image mathapattiImage;
    public Image bindiImage, leftBangleImage, rightBangleImage, lehangaImage, earingImage, noseringImage, eyeshadeImage, necklaceImage, hairImage, lipsImage, shoesImage, eyebrowImage,
                 blushImage;
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
public enum WeddingSelectedItem
{
    lehanga, shoes, bangle, earing, necklace, mathapatti, nosering, bindi, lips, hair, eyeshade, eyebrow, blush
}

public class Wedding : MonoBehaviour
{
    public WeddingSelectedItem selectedItem;
    [FoldoutGroup("UI Elements")]
    [HideLabel]
    public WeddingElements uIElements;
    [FoldoutGroup("Player Elements")]
    [HideLabel]
    public WeddingPlayerElenemts playerElements;
    [FoldoutGroup("Opponent Elements")]
    [HideLabel]
    public WeddingOpponentElenemts oppoElements;
    [Header("Mover Item")]
    public MRS_Manager playerCharacterMover;
    public MRS_Manager oppoCharacterMover;
    public CoinsAdder coinsAdder;
    [Header("Sprites Array")]
    public Sprite[] cardSprites;
    public Sprite[] botSprites;
    public Sprite[] mathapattiSprites;
    public Sprite[] bindiSprites;
    public Sprite[] bangleSprites;
    public Sprite[] lehangaSprites;
    public Sprite[] earingSprites;
    public Sprite[] noseringSprites;
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
    private List<ItemInfo> mathapattiList = new List<ItemInfo>();
    private List<ItemInfo> bindiList = new List<ItemInfo>();
    private List<ItemInfo> bangleList = new List<ItemInfo>();
    private List<ItemInfo> lehangaList = new List<ItemInfo>();
    private List<ItemInfo> earingList = new List<ItemInfo>();
    private List<ItemInfo> noseringList = new List<ItemInfo>();
    private List<ItemInfo> eyeshadeList = new List<ItemInfo>();
    private List<ItemInfo> necklaceList = new List<ItemInfo>();
    private List<ItemInfo> hairList = new List<ItemInfo>();
    private List<ItemInfo> eyebrowList = new List<ItemInfo>();
    private List<ItemInfo> blushList = new List<ItemInfo>();
    private List<ItemInfo> lipsList = new List<ItemInfo>();
    private List<ItemInfo> shoesList = new List<ItemInfo>();

    private int[] bangleScroe = { 1365, 3186, 6593, 5914, 1500, 2963, 1479, 7598, 4665, 6985, 2008, 6521, 8240, 1654, 4452 };

    private int[] bindiScroe = { 1265, 1186, 5594, 4915, 2501, 1964, 2478, 6599, 3966, 1586, 3028, 5522, 1241, 2655, 3453, 7564 };

    private int[] earingScroe = { 1164, 3175, 2595, 1815, 5805, 3735, 2475, 1487, 3578, 4886, 1655, 3425, 3282, 1546, 2354 };

    private int[] lehangaScroe = { 1564, 3245, 1545, 4515, 8705, 1635, 2585, 5357, 1065, 4546, 3457, 1415, 5162 };

    private int[] mathapatiScroe = { 1463, 1296, 4584, 3715, 2401, 1744, 1568, 4589, 2054, 5726, 1548, 5128, 2381, 2585, 1472, 6453 };

    private int[] necklaceScroe = { 1274, 2165, 1485, 2415, 4705, 2734, 1185, 2467, 3578, 1526, 3456, 2585, 1272, 2945, 2464 };

    private int[] noseringScroe = { 1564, 1085, 4593, 8915, 3505, 1965, 3476, 1489, 3676, 6876, 3425, 1523, 5242, 1656 };

    private int[] blushScroe = { 1381, 2894, 9545, 1784, 1415, 4892, 5654, 1756, 5645, 1156, 3121, 5726, 1161, 1644 };

    private int[] eyebrowScroe = {1154, 1421, 1184, 3214, 8489, 7212, 1848, 4231, 2484, 1156, 4844, 2391, 9681, 0824 };

    private int[] eyeshadeScroe = {1412, 5421, 4821, 5212, 1613, 8264, 1262, 3162, 1142, 3146, 3121, 2984 };

    private int[] hairScroe = { 1056, 4121, 5451, 1923, 4842, 1564, 8921, 4215, 1121, 5648, 2165, 2118 };

    private int[] lipsScroe = { 1164, 5421, 2174, 1214, 5489, 7252, 1643, 2181, 2974, 2125, 1754, 1381, 4651, 3726 };

    private int[] shoesScroe = { 1532, 1842, 5482, 2164, 3512, 1641, 2316, 6213, 2156, 4821, 1568, 4895};

    [Header("Default Character Sprites")]
    public Sprite defaultlehanga;
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
    private int playerlehangaScore, playershoesScore, playerbangleScore, playerearingScore, playernecklaceScore, playermathapattiScore, playernoseringScore, playerbindiScore,
                playerlipsScore, playerhairScore, playereyeshadeScore, playereyebrowScore, playerblushScore = 0;
    private int oppolehangaScore, opposhoesScore, oppobangleScore, oppoearingScore, opponecklaceScore, oppomathapattiScore, opponoseringScore, oppobindiScore,
                oppolipsScore, oppohairScore, oppoeyeshadeScore, oppoeyebrowScore, oppoblushScore = 0;
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
        selectedItem = WeddingSelectedItem.lehanga;
        uIElements.lehangaScroller.SetActive(true);
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

        #region Initialing mathapatti
        if (uIElements.mathapattiScroller)
        {
            var mathapattiInfo = uIElements.mathapattiScroller.GetComponentsInChildren<ItemInfo>();
            for (int i = 0; i < mathapattiInfo.Length; i++)
            {
                mathapattiList.Add(mathapattiInfo[i]);
            }
        }
        SetupItemData(SaveData.Instance.WeddingModeElements.mathapatti, mathapattiList);
        SetItemIcon(mathapattiList, mathapattiSprites);
        #endregion
        
        #region Initialing bindi
        if (uIElements.bindiScroller)
        {
            var bindiInfo = uIElements.bindiScroller.GetComponentsInChildren<ItemInfo>();
            for (int i = 0; i < bindiInfo.Length; i++)
            {
                bindiList.Add(bindiInfo[i]);
            }
        }
        SetupItemData(SaveData.Instance.WeddingModeElements.bindi, bindiList);
        SetItemIcon(bindiList, bindiSprites);
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
        SetupItemData(SaveData.Instance.WeddingModeElements.bangle, bangleList);
        SetItemIcon(bangleList, bangleSprites);
        #endregion

        #region Initialing lehanga
        if (uIElements.lehangaScroller)
        {
            var lehangaInfo = uIElements.lehangaScroller.GetComponentsInChildren<ItemInfo>();
            for (int i = 0; i < lehangaInfo.Length; i++)
            {
                lehangaList.Add(lehangaInfo[i]);
            }
        }
        SetupItemData(SaveData.Instance.WeddingModeElements.lehanga, lehangaList);
        SetItemIcon(lehangaList, lehangaSprites);
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
        SetupItemData(SaveData.Instance.WeddingModeElements.earing, earingList);
        SetItemIcon(earingList, earingSprites);
        #endregion

        #region Initialing nosering
        if (uIElements.noseringScroller)
        {
            var noseringInfo = uIElements.noseringScroller.GetComponentsInChildren<ItemInfo>();
            for (int i = 0; i < noseringInfo.Length; i++)
            {
                noseringList.Add(noseringInfo[i]);
            }
        }
        SetupItemData(SaveData.Instance.WeddingModeElements.nosering, noseringList);
        SetItemIcon(noseringList, noseringSprites);
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
        SetupItemData(SaveData.Instance.WeddingModeElements.eyeshade, eyeshadeList);
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
        SetupItemData(SaveData.Instance.WeddingModeElements.necklace, necklaceList);
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
        SetupItemData(SaveData.Instance.WeddingModeElements.hair, hairList);
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
        SetupItemData(SaveData.Instance.WeddingModeElements.blush, blushList);
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
        SetupItemData(SaveData.Instance.WeddingModeElements.eyebrow, eyebrowList);
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
        SetupItemData(SaveData.Instance.WeddingModeElements.lips, lipsList);
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
        SetupItemData(SaveData.Instance.WeddingModeElements.shoes, shoesList);
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
        if (selectedItem == WeddingSelectedItem.mathapatti)
        {
            CheckSelectedItem(mathapattiList, mathapattiSprites, playerElements.mathapattiImage);
        }
        else if (selectedItem == WeddingSelectedItem.bindi)
        {
            CheckSelectedItem(bindiList, bindiSprites, playerElements.bindiImage);
        }
        else if (selectedItem == WeddingSelectedItem.bangle)
        {
            CheckSelectedItem(bangleList, bangleSprites, playerElements.leftBangleImage);
        }
        else if (selectedItem == WeddingSelectedItem.lehanga)
        {
            CheckSelectedItem(lehangaList, lehangaSprites, playerElements.lehangaImage);
        }
        else if (selectedItem == WeddingSelectedItem.earing)
        {
            CheckSelectedItem(earingList, earingSprites, playerElements.earingImage);
        } 
        else if (selectedItem == WeddingSelectedItem.nosering)
        {
            CheckSelectedItem(noseringList, noseringSprites, playerElements.noseringImage);
        }
        else if (selectedItem == WeddingSelectedItem.eyeshade)
        {
            CheckSelectedItem(eyeshadeList, eyeshadeSprites, playerElements.eyeshadeImage);
        }
        else if (selectedItem == WeddingSelectedItem.necklace)
        {
            CheckSelectedItem(necklaceList, necklaceSprites, playerElements.necklaceImage);
        }
        else if (selectedItem == WeddingSelectedItem.hair)
        {
            CheckSelectedItem(hairList, hairSprites, playerElements.hairImage);
        }
        else if (selectedItem == WeddingSelectedItem.lips)
        {
            CheckSelectedItem(lipsList, lipsSprites, playerElements.lipsImage);
        }
        else if (selectedItem == WeddingSelectedItem.shoes)
        {
            CheckSelectedItem(shoesList, shoesSprites, playerElements.shoesImage);
        }
        else if (selectedItem == WeddingSelectedItem.blush)
        {
            CheckSelectedItem(blushList, blushSprites, playerElements.blushImage);
        }
        else if (selectedItem == WeddingSelectedItem.eyebrow)
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

                            if (selectedItem == WeddingSelectedItem.lehanga)
                            {
                                IsDressing = true;
                                if (playerlehangaScore == int.Parse(itemInfoList[selectedIndex].ItemScore.text))
                                {
                                    return;
                                }

                                if (playerlehangaScore == 0)
                                {
                                    playerScore = playerScore + int.Parse(itemInfoList[selectedIndex].ItemScore.text);
                                    playerlehangaScore = int.Parse(itemInfoList[selectedIndex].ItemScore.text);
                                }
                                else
                                {
                                    playerScore = playerScore - playerlehangaScore;
                                    playerScore = playerScore + int.Parse(itemInfoList[selectedIndex].ItemScore.text);
                                    playerlehangaScore = int.Parse(itemInfoList[selectedIndex].ItemScore.text);
                                }
                                
                            }

                            else if (selectedItem == WeddingSelectedItem.shoes)
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

                            else if (selectedItem == WeddingSelectedItem.necklace)
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

                            else if (selectedItem == WeddingSelectedItem.mathapatti)
                            {
                                IsAccessioress = true;
                                if (playermathapattiScore == int.Parse(itemInfoList[selectedIndex].ItemScore.text))
                                {
                                    return;
                                }

                                if (playermathapattiScore == 0)
                                {
                                    playerScore = playerScore + int.Parse(itemInfoList[selectedIndex].ItemScore.text);
                                    playermathapattiScore = int.Parse(itemInfoList[selectedIndex].ItemScore.text);
                                }
                                else
                                {
                                    playerScore = playerScore - playermathapattiScore;
                                    playerScore = playerScore + int.Parse(itemInfoList[selectedIndex].ItemScore.text);
                                    playermathapattiScore = int.Parse(itemInfoList[selectedIndex].ItemScore.text);
                                }
                            }

                            else if (selectedItem == WeddingSelectedItem.bindi)
                            {
                                IsAccessioress = true;
                                if (playerbindiScore == int.Parse(itemInfoList[selectedIndex].ItemScore.text))
                                {
                                    return;
                                }

                                if (playerbindiScore == 0)
                                {
                                    playerScore = playerScore + int.Parse(itemInfoList[selectedIndex].ItemScore.text);
                                    playerbindiScore = int.Parse(itemInfoList[selectedIndex].ItemScore.text);
                                }
                                else
                                {
                                    playerScore = playerScore - playerbindiScore;
                                    playerScore = playerScore + int.Parse(itemInfoList[selectedIndex].ItemScore.text);
                                    playerbindiScore = int.Parse(itemInfoList[selectedIndex].ItemScore.text);
                                }
                            }

                            else if (selectedItem == WeddingSelectedItem.bangle)
                            {
                                IsAccessioress = true;
                                playerElements.rightBangleImage.gameObject.SetActive(true);
                                playerElements.rightBangleImage.sprite = bangleSprites[selectedIndex];
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

                            else if (selectedItem == WeddingSelectedItem.earing)
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

                            else if (selectedItem == WeddingSelectedItem.nosering)
                            {
                                IsAccessioress = true;
                                if (playernoseringScore == int.Parse(itemInfoList[selectedIndex].ItemScore.text))
                                {
                                    return;
                                }

                                if (playernoseringScore == 0)
                                {
                                    playerScore = playerScore + int.Parse(itemInfoList[selectedIndex].ItemScore.text);
                                    playernoseringScore = int.Parse(itemInfoList[selectedIndex].ItemScore.text);
                                }
                                else
                                {
                                    playerScore = playerScore - playernoseringScore;
                                    playerScore = playerScore + int.Parse(itemInfoList[selectedIndex].ItemScore.text);
                                    playernoseringScore = int.Parse(itemInfoList[selectedIndex].ItemScore.text);
                                }
                            }

                            else if (selectedItem == WeddingSelectedItem.eyeshade)
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

                            else if (selectedItem == WeddingSelectedItem.blush)
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

                            else if (selectedItem == WeddingSelectedItem.eyebrow)
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

                            else if (selectedItem == WeddingSelectedItem.hair)
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

                            else if (selectedItem == WeddingSelectedItem.lips)
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

                            //if (IsDressing == true && IsJewellery == true && IsMakeup == true)
                            //{
                            //    uIElements.previewBtn.GetComponent<Button>().interactable = true;
                            //}
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
        categoryBtn[itemIndex].transform.GetChild(1).GetComponent<Image>().sprite = greenSprite;
        categoryBtn[itemIndex].transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
        categoryBtn[itemIndex].transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = tickSprite;
    }
    #endregion

    #region UnlockCoinItems
    public void UnlockCoinItems()
    {
        if (selectedItem == WeddingSelectedItem.mathapatti)
        {
            coinUnlockItem(mathapattiList);
        }
        else if (selectedItem == WeddingSelectedItem.bindi)
        {
            coinUnlockItem(bindiList);
        }
        else if (selectedItem == WeddingSelectedItem.bangle)
        {
            coinUnlockItem(bangleList);
        }
        else if (selectedItem == WeddingSelectedItem.lehanga)
        {
            coinUnlockItem(lehangaList);
        }
        else if (selectedItem == WeddingSelectedItem.earing)
        {
            coinUnlockItem(earingList);
        }     
        else if (selectedItem == WeddingSelectedItem.nosering)
        {
            coinUnlockItem(noseringList);
        }
        else if (selectedItem == WeddingSelectedItem.eyeshade)
        {
            coinUnlockItem(eyeshadeList);
        }
        else if (selectedItem == WeddingSelectedItem.necklace)
        {
            coinUnlockItem(necklaceList);
        }
        else if (selectedItem == WeddingSelectedItem.hair)
        {
            coinUnlockItem(hairList);
        }
        else if (selectedItem == WeddingSelectedItem.lips)
        {
            coinUnlockItem(lipsList);
        }
        else if (selectedItem == WeddingSelectedItem.shoes)
        {
            coinUnlockItem(shoesList);
        }
        else if (selectedItem == WeddingSelectedItem.eyebrow)
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
        if (selectedItem == WeddingSelectedItem.mathapatti)
        {
            SetItemsInfo(mathapattiList, mathapatiScroe);
        }
        else if (selectedItem == WeddingSelectedItem.bindi)
        {
            SetItemsInfo(bindiList, bindiScroe);
        }
        else if (selectedItem == WeddingSelectedItem.bangle)
        {
            SetItemsInfo(bangleList, bangleScroe);
        }
        if (selectedItem == WeddingSelectedItem.lehanga)
        {
            SetItemsInfo(lehangaList, lehangaScroe);
        }
        else if (selectedItem == WeddingSelectedItem.earing)
        {
            SetItemsInfo(earingList, earingScroe);
        }
        else if (selectedItem == WeddingSelectedItem.nosering)
        {
            SetItemsInfo(noseringList, noseringScroe);
        }
        else if (selectedItem == WeddingSelectedItem.eyeshade)
        {
            SetItemsInfo(eyeshadeList, eyeshadeScroe);
        }
        else if (selectedItem == WeddingSelectedItem.necklace)
        {
            SetItemsInfo(necklaceList, necklaceScroe);
        }
        else if (selectedItem == WeddingSelectedItem.hair)
        {
            SetItemsInfo(hairList, hairScroe);
        }
        else if (selectedItem == WeddingSelectedItem.lips)
        {
            SetItemsInfo(lipsList, lipsScroe);
        }
        else if (selectedItem == WeddingSelectedItem.shoes)
        {
            SetItemsInfo(shoesList, shoesScroe);
        }
        else if (selectedItem == WeddingSelectedItem.blush)
        {
            SetItemsInfo(blushList, blushScroe);
        }
        else if (selectedItem == WeddingSelectedItem.eyebrow)
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
                if (_ItemInfo[i].LockIcon) _ItemInfo[i].LockIcon.SetActive(false);
                if (_ItemInfo[i].videoLock) _ItemInfo[i].videoLock.SetActive(false);
                if (_ItemInfo[i].coinLock) _ItemInfo[i].coinLock.SetActive(false);
            }
            if(_ItemInfo[i].hotRib) _ItemInfo[i].hotRibben.SetActive(true);
            if(_ItemInfo[i].premiumRib) _ItemInfo[i].premiumRibben.SetActive(true);

        }
    }
    #endregion

    #region RankingFormula
    private int GetRank(int selectedCard, int totalItems)
    {
        int rankDivider = 0;
        rankDivider = totalItems / 10;
        if (rankDivider == 0)
        {
            rankDivider += 1;
        }
        if (selectedCard / rankDivider < 10)
        {
            return (selectedCard / rankDivider) + 1;
        }
        else
        {
            return 10;
        }
    }
    private int GetRankValue(int _Rank)
    {
        if (_Rank > -1)
            return _Rank;
        else
            return 0;
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
        
        uIElements.mathapattiScroller.SetActive(false);
        uIElements.bindiScroller.SetActive(false);
        uIElements.bangleScroller.SetActive(false);
        uIElements.lehangaScroller.SetActive(false);
        uIElements.earingScroller.SetActive(false);
        uIElements.noseringScroller.SetActive(false);
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

        if (index == (int)WeddingSelectedItem.mathapatti)
        {
            playerCharacterMover.Move(new Vector3(57, -600, -1000), 0.5f, true, false);
            selectedItem = WeddingSelectedItem.mathapatti;
            uIElements.mathapattiScroller.SetActive(true);
        }
        else if (index == (int)WeddingSelectedItem.bindi)
        {
            playerCharacterMover.Move(new Vector3(57, -600, -1000), 0.5f, true, false);
            selectedItem = WeddingSelectedItem.bindi;
            uIElements.bindiScroller.SetActive(true);
        }
        else if (index == (int)WeddingSelectedItem.bangle)
        {
            playerCharacterMover.Move(new Vector3(86, -110, 0), 0.5f, true, false);
            selectedItem = WeddingSelectedItem.bangle;
            uIElements.bangleScroller.SetActive(true);
        }
        else if (index == (int)WeddingSelectedItem.lehanga)
        {
            playerCharacterMover.Move(new Vector3(86, -110, 0), 0.5f, true, false);
            selectedItem = WeddingSelectedItem.lehanga;
            uIElements.lehangaScroller.SetActive(true);
        }
        else if (index == (int)WeddingSelectedItem.earing)
        {
            playerCharacterMover.Move(new Vector3(57, -600, -1000), 0.5f, true, false);
            selectedItem = WeddingSelectedItem.earing;
            uIElements.earingScroller.SetActive(true);
        }
        else if (index == (int)WeddingSelectedItem.nosering)
        {
            playerCharacterMover.Move(new Vector3(57, -600, -1000), 0.5f, true, false);
            selectedItem = WeddingSelectedItem.nosering;
            uIElements.noseringScroller.SetActive(true);
        }
        else if (index == (int)WeddingSelectedItem.eyeshade)
        {
            playerCharacterMover.Move(new Vector3(57, -600, -1000), 0.5f, true, false);
            selectedItem = WeddingSelectedItem.eyeshade;
            uIElements.eyeshadeScroller.SetActive(true);
        }
        else if (index == (int)WeddingSelectedItem.necklace)
        {
            playerCharacterMover.Move(new Vector3(57, -600, -1000), 0.5f, true, false);
            selectedItem = WeddingSelectedItem.necklace;
            uIElements.necklaceScroller.SetActive(true);
        }
        else if (index == (int)WeddingSelectedItem.hair)
        {
            playerCharacterMover.Move(new Vector3(57, -600, -1000), 0.5f, true, false);
            selectedItem = WeddingSelectedItem.hair;
            uIElements.hairScroller.SetActive(true);
        }
        else if (index == (int)WeddingSelectedItem.lips)
        {
            playerCharacterMover.Move(new Vector3(57, -600, -1000), 0.5f, true, false);
            selectedItem = WeddingSelectedItem.lips;
            uIElements.lipsScroller.SetActive(true);
        }   
        else if (index == (int)WeddingSelectedItem.blush)
        {
            playerCharacterMover.Move(new Vector3(57, -600, -1000), 0.5f, true, false);
            selectedItem = WeddingSelectedItem.blush;
            uIElements.blushScroller.SetActive(true);
        }
        else if (index == (int)WeddingSelectedItem.eyebrow)
        {
            playerCharacterMover.Move(new Vector3(57, -600, -1000), 0.5f, true, false);
            selectedItem = WeddingSelectedItem.eyebrow;
            uIElements.eyebrowScroller.SetActive(true);
        }
        else if (index == (int)WeddingSelectedItem.shoes)
        {
            playerCharacterMover.Move(new Vector3(86, -110, 0), 0.5f, true, false);
            selectedItem = WeddingSelectedItem.shoes;
            uIElements.shoesScroller.SetActive(true);
        }
        GetItemsInfo();
    }
    #endregion

    #region UnlockSingleItem
    public void UnlockSingleItem()
    {
        if (selectedItem == WeddingSelectedItem.mathapatti)
        {
            SaveData.Instance.WeddingModeElements.mathapatti[selectedIndex] = false;
        }
        else if (selectedItem == WeddingSelectedItem.bindi)
        {
            SaveData.Instance.WeddingModeElements.bindi[selectedIndex] = false;
        }
        else if (selectedItem == WeddingSelectedItem.bangle)
        {
            SaveData.Instance.WeddingModeElements.bangle[selectedIndex] = false;
        }
        else if (selectedItem == WeddingSelectedItem.lehanga)
        {
            SaveData.Instance.WeddingModeElements.lehanga[selectedIndex] = false;
        }
        else if (selectedItem == WeddingSelectedItem.earing)
        {
            SaveData.Instance.WeddingModeElements.earing[selectedIndex] = false;
        }
        else if (selectedItem == WeddingSelectedItem.nosering)
        {
            SaveData.Instance.WeddingModeElements.nosering[selectedIndex] = false;
        }
        else if (selectedItem == WeddingSelectedItem.eyeshade)
        {
            SaveData.Instance.WeddingModeElements.eyeshade[selectedIndex] = false;
        }
        else if (selectedItem == WeddingSelectedItem.necklace)
        {
            SaveData.Instance.WeddingModeElements.necklace[selectedIndex] = false;
        }
        else if (selectedItem == WeddingSelectedItem.hair)
        {
            SaveData.Instance.WeddingModeElements.hair[selectedIndex] = false;
        }
        else if (selectedItem == WeddingSelectedItem.lips)
        {
            SaveData.Instance.WeddingModeElements.lips[selectedIndex] = false;
        }
        else if (selectedItem == WeddingSelectedItem.shoes)
        {
            SaveData.Instance.WeddingModeElements.shoes[selectedIndex] = false;
        }
        else if (selectedItem == WeddingSelectedItem.blush)
        {
            SaveData.Instance.WeddingModeElements.blush[selectedIndex] = false;
        }
        else if (selectedItem == WeddingSelectedItem.eyebrow)
        {
            SaveData.Instance.WeddingModeElements.eyebrow[selectedIndex] = false;
        }
        totalCoins.text = SaveData.Instance.Coins.ToString();
        Usman_SaveLoad.SaveProgress();
    }
    #endregion
    
    #region UnEquipFunction
    public void UnEquipet()
    {
        if (selectedItem == WeddingSelectedItem.mathapatti)
        {
            playerElements.mathapattiImage.gameObject.SetActive(false);
        }
        else if (selectedItem == WeddingSelectedItem.bindi)
        {
            playerElements.bindiImage.gameObject.SetActive(false);
        }
        else if (selectedItem == WeddingSelectedItem.bangle)
        {
            playerElements.leftBangleImage.gameObject.SetActive(false);
            playerElements.rightBangleImage.gameObject.SetActive(false);
        }
        else if (selectedItem == WeddingSelectedItem.lehanga)
        {
            playerElements.lehangaImage.sprite = defaultlehanga;
        }
        else if (selectedItem == WeddingSelectedItem.earing)
        {
            playerElements.earingImage.gameObject.SetActive(false);
        }
        else if (selectedItem == WeddingSelectedItem.nosering)
        {
            playerElements.noseringImage.gameObject.SetActive(false);
        }
        else if (selectedItem == WeddingSelectedItem.eyeshade)
        {
            playerElements.eyeshadeImage.gameObject.SetActive(false);
        }
        else if (selectedItem == WeddingSelectedItem.necklace)
        {
            playerElements.necklaceImage.gameObject.SetActive(false);
        }
        else if (selectedItem == WeddingSelectedItem.hair)
        {
            playerElements.hairImage.sprite = defaultHair;
        }
        else if (selectedItem == WeddingSelectedItem.lips)
        {
            playerElements.lipsImage.sprite = defaultLips;
        }
        else if (selectedItem == WeddingSelectedItem.shoes)
        {
            playerElements.shoesImage.gameObject.SetActive(false);
        }
        else if (selectedItem == WeddingSelectedItem.blush)
        {
            playerElements.blushImage.gameObject.SetActive(false);
        }
        else if (selectedItem == WeddingSelectedItem.eyebrow)
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
            uIElements.lehangaScroller.SetActive(true);
            uIElements.dressUpCategoryScroller.SetActive(true);
            uIElements.makeUpCategoryScroller.SetActive(false);
            selectedItem = WeddingSelectedItem.lehanga;
            playerCharacterMover.Move(new Vector3(86, -110, 0), 0.5f, true, false);
            uIElements.dressUpBtn.SetActive(false);
            uIElements.makeUpBtn.SetActive(true);
        }
        else if (a == 1)
        {
            DisableScrollers();
            uIElements.lipsScroller.SetActive(true);
            uIElements.makeUpCategoryScroller.SetActive(true);
            uIElements.dressUpCategoryScroller.SetActive(false);
            selectedItem = WeddingSelectedItem.lips;
            playerCharacterMover.Move(new Vector3(57, -600, -1000), 0.5f, true, false);
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
        uIElements.lehangaScroller.SetActive(true);
        uIElements.dressUpCategoryScroller.SetActive(true);
        uIElements.makeUpCategoryScroller.SetActive(false);
        selectedItem = WeddingSelectedItem.lehanga;
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
        uIElements.cardImage.sprite = cardSprites[0];

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
        playerCharacterMover.Move(new Vector3(0, 0, 0), 0.7f, true, false);
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
        playerCharacterMover.Move(new Vector3(-290, -67.49f, 0), 0.7f, true, false);
        yield return new WaitForSeconds(0.3f);
        oppoCharacterMover.Move(new Vector3(290, -67.49f, 0), 0.7f, true, false);
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

        #region lehanga
        randomIndex = Random.Range(0, lehangaList.Count);
        if (lehangaList[randomIndex] && oppoElements.lehangaImage)
        {
            oppoElements.lehangaImage.gameObject.SetActive(true);
            oppoElements.lehangaImage.sprite = lehangaSprites[randomIndex];
        }
         
        oppoScore = oppoScore + int.Parse(lehangaList[randomIndex].ItemScore.text);
        oppolehangaScore = int.Parse(lehangaList[randomIndex].ItemScore.text);
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

        #region nosering
        randomIndex = Random.Range(0, noseringList.Count);
        if (noseringList[randomIndex] && oppoElements.noseringImage)
        {
            oppoElements.noseringImage.gameObject.SetActive(true);
            oppoElements.noseringImage.sprite = noseringSprites[randomIndex];
        }
        oppoScore = oppoScore + int.Parse(noseringList[randomIndex].ItemScore.text);
        opponoseringScore = int.Parse(noseringList[randomIndex].ItemScore.text);
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

        #region mathapati
        randomIndex = Random.Range(0, mathapattiList.Count);
        if (mathapattiList[randomIndex] && oppoElements.mathapattiImage)
        {
            oppoElements.mathapattiImage.gameObject.SetActive(true);
            oppoElements.mathapattiImage.sprite = mathapattiSprites[randomIndex];
        }
        oppoScore = oppoScore + int.Parse(mathapattiList[randomIndex].ItemScore.text);
        oppomathapattiScore = int.Parse(mathapattiList[randomIndex].ItemScore.text);
        #endregion

        #region bangle
        randomIndex = Random.Range(0, bangleList.Count);
        if (bangleList[randomIndex] && oppoElements.leftBangleImage)
        {
            oppoElements.leftBangleImage.gameObject.SetActive(true);
            oppoElements.rightBangleImage.gameObject.SetActive(true);
            oppoElements.leftBangleImage.sprite = bangleSprites[randomIndex];
            oppoElements.rightBangleImage.sprite = bangleSprites[randomIndex];
        }
        oppoScore = oppoScore + int.Parse(bangleList[randomIndex].ItemScore.text);
        oppobangleScore = int.Parse(bangleList[randomIndex].ItemScore.text);
        #endregion

        #region bindi
        randomIndex = Random.Range(0, bindiList.Count);
        if (bindiList[randomIndex] && oppoElements.bindiImage)
        {
            oppoElements.bindiImage.gameObject.SetActive(true);
            oppoElements.bindiImage.sprite = bindiSprites[randomIndex];
        }
        oppoScore = oppoScore + int.Parse(bindiList[randomIndex].ItemScore.text);
        oppobindiScore = int.Parse(bindiList[randomIndex].ItemScore.text);
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
        playerdressupTotal = playerlehangaScore + playershoesScore;
        playerTotal += playerdressupTotal;
        playerElements.voteScore.text = playerdressupTotal.ToString();
        //oppo
        oppodressupTotal = oppolehangaScore + opposhoesScore;
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
        playeraccessioresTotal = playerearingScore + playernoseringScore + playermathapattiScore + playerbindiScore + playernecklaceScore + playerbangleScore;
        playerTotal += playeraccessioresTotal;
        playerElements.voteScore.text = playeraccessioresTotal.ToString();
        //oppo
        oppoaccessioresTotal = oppoearingScore + opponoseringScore + oppomathapattiScore + oppobindiScore + opponecklaceScore + oppobangleScore;
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
            playerCharacterMover.Move(new Vector3(55, -220, 0), 0.5f, true, false);
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
            oppoCharacterMover.Move(new Vector3(55, -220, 0), 0.5f, true, false);
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
