using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


[System.Serializable]
public class SahreeElements
{
    [Header("Panels")]
    public GameObject vsPanel;
    public GameObject vsAnimPanel, gamePanel, submitPanel, screenShotPanel, judgementalPanel, stageDecuration, levelCompletePanel, adPanel, loadingPanel;
    [Header("PopUp")]
    public GameObject videoUnlockPopUp;
    public GameObject coinUnlockPopUp, enoughCoinPopUp, videoAdNotAvailablePopUp;
    [Header("Scrollers")]
    public GameObject allScroller;
    public GameObject dressUpCategoryScroller, makeUpCategoryScroller, mathapattiScroller, bangleScroller, sahreeScroller, earingScroller, noseringScroller, eyebrowScroller, blushScroller,
                      eyeshadeScroller, necklaceScroller, hairScroller, clutchScroller, lipsScroller, shoesScroller;
    [Header("UI")]
    public GameObject dressUpBtn;
    public GameObject makeUpBtn, connected, startBtn, coinSlot, previewBtn, submitPanelbar, lastPanel;
    [Header("Sahree Image")]
    public Image bgImage;
    public Image cardImage, screenShotImage, fillbar;
}

[System.Serializable]
public class SahreePlayerElenemts
{
    [Header("Player Character")]
    public GameObject character;
    [Header("Player Images")]
    public Image mathapattiImage;
    public Image bangleImage, sahreeImage, earingImage, noseringImage, eyeshadeImage, necklaceImage, hairImage, lipsImage, clutchImage, shoesImage, eyebrowImage, blushImage;
    [Header("Player Score Text")]
    public Text voteScore;
    public Text totalScore;
    [Header("Player Winner")]
    public GameObject winner;
}

[System.Serializable]
public class SahreeOpponentElenemts
{
    [Header("Opponent Character")]
    public GameObject character;
    [Header("Opponent Images")]
    public Image mathapattiImage;
    public Image bangleImage, sahreeImage, earingImage, noseringImage, eyeshadeImage, necklaceImage, hairImage, lipsImage, shoesImage, clutchImage, eyebrowImage, blushImage;
    [Header("Opponent Bot Images")]
    public Image botInVsPanel;
    public Image botInVsAnimPanel, botInJudgementalPanel;
    [Header("Opponent Text")]
    public Text nameVsPanel;
    public Text voteScore, totalScore;
    [Header("Opponent Winner")]
    public GameObject winner;
}

[System.Serializable]
public enum SahreeSelectedItem
{
    sahree, shoes, bangle, earing, necklace, mathapatti, nosering, clutch, lips, hair, eyeshade, eyebrow, blush
}

public class Sahree : MonoBehaviour
{
    public SahreeSelectedItem selectedItem;
    [FoldoutGroup("UI Elements")]
    [HideLabel]
    public SahreeElements uIElements;
    [FoldoutGroup("Player Elements")]
    [HideLabel]
    public SahreePlayerElenemts playerElements;
    [FoldoutGroup("Opponent Elements")]
    [HideLabel]
    public SahreeOpponentElenemts oppoElements;
    [Header("Mover Item")]
    public MRS_Manager playerCharacterMover;
    public MRS_Manager oppoCharacterMover;
    public CoinsAdder coinsAdder;
    [Header("Sprites Array")]
    public Sprite[] cardSprites;
    public Sprite[] botSprites;
    public Sprite[] mathapattiSprites;
    public Sprite[] bangleSprites;
    public Sprite[] sahreeSprites;
    public Sprite[] earingSprites;
    public Sprite[] noseringSprites;
    public Sprite[] eyeshadeSprites;
    public Sprite[] necklaceSprites;
    public Sprite[] hairSprites;
    public Sprite[] lipsSprites;
    public Sprite[] shoesSprites;
    public Sprite[] clutchSprites;
    public Sprite[] eyebrowSprites;
    public Sprite[] blushSprites;
    public Sprite[] defaultIconSprites;
    public Sprite[] selectedIconSprites;
    [Header("Scroller Btn Image Array")]
    public Image[] categoryBtn;
    [Header("Item List")]
    private List<ItemInfo> mathapattiList = new List<ItemInfo>();
    private List<ItemInfo> bangleList = new List<ItemInfo>();
    private List<ItemInfo> sahreeList = new List<ItemInfo>();
    private List<ItemInfo> earingList = new List<ItemInfo>();
    private List<ItemInfo> noseringList = new List<ItemInfo>();
    private List<ItemInfo> eyeshadeList = new List<ItemInfo>();
    private List<ItemInfo> necklaceList = new List<ItemInfo>();
    private List<ItemInfo> hairList = new List<ItemInfo>();
    private List<ItemInfo> lipsList = new List<ItemInfo>();
    private List<ItemInfo> shoesList = new List<ItemInfo>();
    private List<ItemInfo> clutchList = new List<ItemInfo>();
    private List<ItemInfo> eyebrowList = new List<ItemInfo>();
    private List<ItemInfo> blushList = new List<ItemInfo>();
    [Header("Default Character Sprites")]
    public Sprite defaultSahree;
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
    bool IsDressUp, IsMakeUp, IsJewellery = false;
    [Header("Animator")]
    public Animator categorySlot;
    public Animator voteCard;
    [Header("Particles")]
    public ParticleSystem itemParticle;
    public GameObject submitPartical;
    public GameObject finalPartical;
    [Header("AudioSources")]
    public AudioSource categorySFX;
    public AudioSource purchaseSFX, itemSelectSFX, vsAnimSFX, winSFX, loseSFX, voteCatSFX, voteGivenSFX;
    public AudioSource[] voiceSounds;

    int playerTotalScore, oppoTotalScore = 0;
    private int mathapattiRank, bangleRank, sahreeRank, earingRank, noseringRank, eyeshadeRank, necklaceRank, hairRank, lipsRank, shoesRank, clutchRank, eyebrowRank, blushRank;
    private int oppoDressingRank, oppoMakeupRank, oppoJewelleryRank;
    private int dressingTotalRank, makeupTotalRank, jewelleryTotalRank;
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
        selectedItem = SahreeSelectedItem.sahree;
        uIElements.sahreeScroller.SetActive(true);
        SetInitialValues();
        GetItemsInfo();
        StartCoroutine(AdDelay(45));
        totalCoins.text = SaveData.Instance.Coins.ToString();
        StartCoroutine(findOpponent());
        OpponentDressing();

        sahreeRank = hairRank = lipsRank = eyebrowRank = 1;
        blushRank = mathapattiRank = bangleRank = earingRank = eyeshadeRank = necklaceRank = shoesRank = noseringRank = - 1;
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
        SetupItemData(SaveData.Instance.SahreeModeElements.mathapatti, mathapattiList);
        SetItemIcon(mathapattiList, mathapattiSprites);
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
        SetupItemData(SaveData.Instance.SahreeModeElements.bangle, bangleList);
        SetItemIcon(bangleList, bangleSprites);
        #endregion

        #region Initialing sahree
        if (uIElements.sahreeScroller)
        {
            var sahreeInfo = uIElements.sahreeScroller.GetComponentsInChildren<ItemInfo>();
            for (int i = 0; i < sahreeInfo.Length; i++)
            {
                sahreeList.Add(sahreeInfo[i]);
            }
        }
        SetupItemData(SaveData.Instance.SahreeModeElements.sahree, sahreeList);
        SetItemIcon(sahreeList, sahreeSprites);
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
        SetupItemData(SaveData.Instance.SahreeModeElements.earing, earingList);
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
        SetupItemData(SaveData.Instance.SahreeModeElements.nosering, noseringList);
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
        SetupItemData(SaveData.Instance.SahreeModeElements.eyeshade, eyeshadeList);
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
        SetupItemData(SaveData.Instance.SahreeModeElements.necklace, necklaceList);
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
        SetupItemData(SaveData.Instance.SahreeModeElements.hair, hairList);
        SetItemIcon(hairList, hairSprites);
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
        SetupItemData(SaveData.Instance.SahreeModeElements.lips, lipsList);
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
        SetupItemData(SaveData.Instance.SahreeModeElements.shoes, shoesList);
        SetItemIcon(shoesList, shoesSprites);
        #endregion
        
        #region Initialing clutch
        if (uIElements.clutchScroller)
        {
            var clutchInfo = uIElements.clutchScroller.GetComponentsInChildren<ItemInfo>();
            for (int i = 0; i < clutchInfo.Length; i++)
            {
                clutchList.Add(clutchInfo[i]);
            }
        }
        SetupItemData(SaveData.Instance.SahreeModeElements.clutch, clutchList);
        SetItemIcon(clutchList, clutchSprites);
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
        SetupItemData(SaveData.Instance.SahreeModeElements.blush, blushList);
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
        SetupItemData(SaveData.Instance.SahreeModeElements.eyebrow, eyebrowList);
        SetItemIcon(eyebrowList, eyebrowSprites);
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

        if (selectedItem == SahreeSelectedItem.mathapatti)
        {
            CheckSelectedItem(mathapattiList, mathapattiSprites, playerElements.mathapattiImage);
        }
        else if (selectedItem == SahreeSelectedItem.bangle)
        {
            CheckSelectedItem(bangleList, bangleSprites, playerElements.bangleImage);
        }
        else if (selectedItem == SahreeSelectedItem.sahree)
        {
            CheckSelectedItem(sahreeList, sahreeSprites, playerElements.sahreeImage);
        }
        else if (selectedItem == SahreeSelectedItem.earing)
        {
            CheckSelectedItem(earingList, earingSprites, playerElements.earingImage);
        } 
        else if (selectedItem == SahreeSelectedItem.nosering)
        {
            CheckSelectedItem(noseringList, noseringSprites, playerElements.noseringImage);
        }
        else if (selectedItem == SahreeSelectedItem.eyeshade)
        {
            CheckSelectedItem(eyeshadeList, eyeshadeSprites, playerElements.eyeshadeImage);
        }
        else if (selectedItem == SahreeSelectedItem.necklace)
        {
            CheckSelectedItem(necklaceList, necklaceSprites, playerElements.necklaceImage);
        }
        else if (selectedItem == SahreeSelectedItem.hair)
        {
            CheckSelectedItem(hairList, hairSprites, playerElements.hairImage);
        }
        else if (selectedItem == SahreeSelectedItem.lips)
        {
            CheckSelectedItem(lipsList, lipsSprites, playerElements.lipsImage);
        }
        else if (selectedItem == SahreeSelectedItem.shoes)
        {
            CheckSelectedItem(shoesList, shoesSprites, playerElements.shoesImage);
        }
        else if (selectedItem == SahreeSelectedItem.clutch)
        {
            CheckSelectedItem(clutchList, clutchSprites, playerElements.clutchImage);
        }
        else if (selectedItem == SahreeSelectedItem.blush)
        {
            CheckSelectedItem(blushList, blushSprites, playerElements.blushImage);
        }
        else if (selectedItem == SahreeSelectedItem.eyebrow)
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
                           
                            if (selectedItem == SahreeSelectedItem.sahree)
                            {
                                IsDressUp = true;
                                sahreeRank = GetRank(selectedIndex, sahreeList.Count);
                            }
                            else if (selectedItem == SahreeSelectedItem.clutch)
                            {
                                IsDressUp = true;
                                clutchRank = GetRank(selectedIndex, clutchList.Count);
                            }
                            else if (selectedItem == SahreeSelectedItem.shoes)
                            {
                                IsDressUp = true;
                                shoesRank = GetRank(selectedIndex, shoesList.Count);
                            }
                            else if (selectedItem == SahreeSelectedItem.mathapatti)
                            {
                                IsJewellery = true;
                                mathapattiRank = GetRank(selectedIndex, mathapattiList.Count);
                            }
                            else if (selectedItem == SahreeSelectedItem.bangle)
                            {
                                IsJewellery = true;
                                bangleRank = GetRank(selectedIndex, bangleList.Count);
                            }
                            else if (selectedItem == SahreeSelectedItem.earing)
                            {
                                IsJewellery = true;
                                earingRank = GetRank(selectedIndex, earingList.Count);
                            } 
                            else if (selectedItem == SahreeSelectedItem.nosering)
                            {
                                IsJewellery = true;
                                noseringRank = GetRank(selectedIndex, noseringList.Count);
                            }
                            else if (selectedItem == SahreeSelectedItem.necklace)
                            {
                                IsJewellery = true;
                                necklaceRank = GetRank(selectedIndex, necklaceList.Count);
                            }
                            else if (selectedItem == SahreeSelectedItem.eyeshade)
                            {
                                IsMakeUp = true;
                                eyeshadeRank = GetRank(selectedIndex, eyeshadeList.Count);
                            }
                            else if (selectedItem == SahreeSelectedItem.eyebrow)
                            {
                                IsMakeUp = true;
                                eyebrowRank = GetRank(selectedIndex, eyebrowList.Count);
                            }
                            else if (selectedItem == SahreeSelectedItem.blush)
                            {
                                IsMakeUp = true;
                                blushRank = GetRank(selectedIndex, blushList.Count);
                            }
                            else if (selectedItem == SahreeSelectedItem.hair)
                            {
                                IsMakeUp = true;
                                hairRank = GetRank(selectedIndex, hairList.Count);
                            }
                            else if (selectedItem == SahreeSelectedItem.lips)
                            {
                                IsMakeUp = true;
                                lipsRank = GetRank(selectedIndex, lipsList.Count);
                            }

                            if (IsDressUp == true && IsMakeUp == true && IsJewellery == true)
                            {
                                uIElements.previewBtn.GetComponent<Button>().interactable = true;
                            }
                            voiceSounds[Random.Range(0, voiceSounds.Length)].Play();
                            if (itemParticle) itemParticle.Play();
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
        if (selectedItem == SahreeSelectedItem.mathapatti)
        {
            coinUnlockItem(mathapattiList);
        }
        else if (selectedItem == SahreeSelectedItem.bangle)
        {
            coinUnlockItem(bangleList);
        }
        else if (selectedItem == SahreeSelectedItem.sahree)
        {
            coinUnlockItem(sahreeList);
        }
        else if (selectedItem == SahreeSelectedItem.earing)
        {
            coinUnlockItem(earingList);
        }     
        else if (selectedItem == SahreeSelectedItem.nosering)
        {
            coinUnlockItem(noseringList);
        }
        else if (selectedItem == SahreeSelectedItem.eyeshade)
        {
            coinUnlockItem(eyeshadeList);
        }
        else if (selectedItem == SahreeSelectedItem.necklace)
        {
            coinUnlockItem(necklaceList);
        }
        else if (selectedItem == SahreeSelectedItem.hair)
        {
            coinUnlockItem(hairList);
        }
        else if (selectedItem == SahreeSelectedItem.lips)
        {
            coinUnlockItem(lipsList);
        }
        else if (selectedItem == SahreeSelectedItem.shoes)
        {
            coinUnlockItem(shoesList);
        }
        else if (selectedItem == SahreeSelectedItem.clutch)
        {
            coinUnlockItem(clutchList);
        }
        else if (selectedItem == SahreeSelectedItem.blush)
        {
            coinUnlockItem(blushList);
        }
        else if (selectedItem == SahreeSelectedItem.eyebrow)
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
        if (selectedItem == SahreeSelectedItem.mathapatti)
        {
            SetItemsInfo(mathapattiList);
        }
        else if (selectedItem == SahreeSelectedItem.bangle)
        {
            SetItemsInfo(bangleList);
        }
        if (selectedItem == SahreeSelectedItem.sahree)
        {
            SetItemsInfo(sahreeList);
        }
        else if (selectedItem == SahreeSelectedItem.earing)
        {
            SetItemsInfo(earingList);
        }
        else if (selectedItem == SahreeSelectedItem.nosering)
        {
            SetItemsInfo(noseringList);
        }
        else if (selectedItem == SahreeSelectedItem.eyeshade)
        {
            SetItemsInfo(eyeshadeList);
        }
        else if (selectedItem == SahreeSelectedItem.necklace)
        {
            SetItemsInfo(necklaceList);
        }
        else if (selectedItem == SahreeSelectedItem.hair)
        {
            SetItemsInfo(hairList);
        }
        else if (selectedItem == SahreeSelectedItem.lips)
        {
            SetItemsInfo(lipsList);
        }
        else if (selectedItem == SahreeSelectedItem.shoes)
        {
            SetItemsInfo(shoesList);
        }
        else if (selectedItem == SahreeSelectedItem.clutch)
        {
            SetItemsInfo(clutchList);
        }
        else if (selectedItem == SahreeSelectedItem.blush)
        {
            SetItemsInfo(blushList);
        }
        else if (selectedItem == SahreeSelectedItem.eyebrow)
        {
            SetItemsInfo(eyebrowList);
        }
    }
    #endregion

    #region SetItemsInfo
    private void SetItemsInfo(List<ItemInfo> _ItemInfo)
    {
        if (_ItemInfo == null) return;
        for (int i = 0; i < _ItemInfo.Count; i++)
        {
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
        uIElements.bangleScroller.SetActive(false);
        uIElements.sahreeScroller.SetActive(false);
        uIElements.earingScroller.SetActive(false);
        uIElements.noseringScroller.SetActive(false);
        uIElements.eyeshadeScroller.SetActive(false);
        uIElements.necklaceScroller.SetActive(false);
        uIElements.hairScroller.SetActive(false);
        uIElements.lipsScroller.SetActive(false);
        uIElements.shoesScroller.SetActive(false);
        uIElements.clutchScroller.SetActive(false);
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

        if (index == (int)SahreeSelectedItem.mathapatti)
        {
            playerCharacterMover.Move(new Vector3(60, -600, -1000), 0.5f, true, false);
            selectedItem = SahreeSelectedItem.mathapatti;
            uIElements.mathapattiScroller.SetActive(true);
        }
        else if (index == (int)SahreeSelectedItem.bangle)
        {
            playerCharacterMover.Move(new Vector3(60, 0, 0), 0.5f, true, false);
            selectedItem = SahreeSelectedItem.bangle;
            uIElements.bangleScroller.SetActive(true);
        }
        else if (index == (int)SahreeSelectedItem.sahree)
        {
            playerCharacterMover.Move(new Vector3(60, 0, 0), 0.5f, true, false);
            selectedItem = SahreeSelectedItem.sahree;
            uIElements.sahreeScroller.SetActive(true);
        }
        else if (index == (int)SahreeSelectedItem.earing)
        {
            playerCharacterMover.Move(new Vector3(50, -600, -1000), 0.5f, true, false);
            selectedItem = SahreeSelectedItem.earing;
            uIElements.earingScroller.SetActive(true);
        }
        else if (index == (int)SahreeSelectedItem.nosering)
        {
            playerCharacterMover.Move(new Vector3(50, -600, -1000), 0.5f, true, false);
            selectedItem = SahreeSelectedItem.nosering;
            uIElements.noseringScroller.SetActive(true);
        }
        else if (index == (int)SahreeSelectedItem.eyeshade)
        {
            playerCharacterMover.Move(new Vector3(50, -600, -1000), 0.5f, true, false);
            selectedItem = SahreeSelectedItem.eyeshade;
            uIElements.eyeshadeScroller.SetActive(true);
        }
        else if (index == (int)SahreeSelectedItem.necklace)
        {
            playerCharacterMover.Move(new Vector3(50, -600, -1000), 0.5f, true, false);
            selectedItem = SahreeSelectedItem.necklace;
            uIElements.necklaceScroller.SetActive(true);
        }
        else if (index == (int)SahreeSelectedItem.hair)
        {
            playerCharacterMover.Move(new Vector3(50, -600, -1000), 0.5f, true, false);
            selectedItem = SahreeSelectedItem.hair;
            uIElements.hairScroller.SetActive(true);
        }
        else if (index == (int)SahreeSelectedItem.lips)
        {
            playerCharacterMover.Move(new Vector3(50, -600, -1000), 0.5f, true, false);
            selectedItem = SahreeSelectedItem.lips;
            uIElements.lipsScroller.SetActive(true);
        }
        else if (index == (int)SahreeSelectedItem.blush)
        {
            playerCharacterMover.Move(new Vector3(50, -600, -1000), 0.5f, true, false);
            selectedItem = SahreeSelectedItem.blush;
            uIElements.blushScroller.SetActive(true);
        }
        else if (index == (int)SahreeSelectedItem.eyebrow)
        {
            playerCharacterMover.Move(new Vector3(50, -600, -1000), 0.5f, true, false);
            selectedItem = SahreeSelectedItem.eyebrow;
            uIElements.eyebrowScroller.SetActive(true);
        }
        else if (index == (int)SahreeSelectedItem.shoes)
        {
            playerCharacterMover.Move(new Vector3(60, 0, 0), 0.5f, true, false);
            selectedItem = SahreeSelectedItem.shoes;
            uIElements.shoesScroller.SetActive(true);
        }
        else if (index == (int)SahreeSelectedItem.clutch)
        {
            playerCharacterMover.Move(new Vector3(60, 0, 0), 0.5f, true, false);
            selectedItem = SahreeSelectedItem.clutch;
            uIElements.clutchScroller.SetActive(true);
        }
        GetItemsInfo();
    }
    #endregion

    #region UnlockSingleItem
    public void UnlockSingleItem()
    {
        if (selectedItem == SahreeSelectedItem.mathapatti)
        {
            SaveData.Instance.SahreeModeElements.mathapatti[selectedIndex] = false;
        }
        else if (selectedItem == SahreeSelectedItem.bangle)
        {
            SaveData.Instance.SahreeModeElements.bangle[selectedIndex] = false;
        }
        else if (selectedItem == SahreeSelectedItem.sahree)
        {
            SaveData.Instance.SahreeModeElements.sahree[selectedIndex] = false;
        }
        else if (selectedItem == SahreeSelectedItem.earing)
        {
            SaveData.Instance.SahreeModeElements.earing[selectedIndex] = false;
        }
        else if (selectedItem == SahreeSelectedItem.nosering)
        {
            SaveData.Instance.SahreeModeElements.nosering[selectedIndex] = false;
        }
        else if (selectedItem == SahreeSelectedItem.eyeshade)
        {
            SaveData.Instance.SahreeModeElements.eyeshade[selectedIndex] = false;
        }
        else if (selectedItem == SahreeSelectedItem.necklace)
        {
            SaveData.Instance.SahreeModeElements.necklace[selectedIndex] = false;
        }
        else if (selectedItem == SahreeSelectedItem.hair)
        {
            SaveData.Instance.SahreeModeElements.hair[selectedIndex] = false;
        }
        else if (selectedItem == SahreeSelectedItem.blush)
        {
            SaveData.Instance.SahreeModeElements.blush[selectedIndex] = false;
        }
        else if (selectedItem == SahreeSelectedItem.eyebrow)
        {
            SaveData.Instance.SahreeModeElements.eyebrow[selectedIndex] = false;
        }
        else if (selectedItem == SahreeSelectedItem.lips)
        {
            SaveData.Instance.SahreeModeElements.lips[selectedIndex] = false;
        }
        else if (selectedItem == SahreeSelectedItem.shoes)
        {
            SaveData.Instance.SahreeModeElements.shoes[selectedIndex] = false;
        }
        else if (selectedItem == SahreeSelectedItem.clutch)
        {
            SaveData.Instance.SahreeModeElements.clutch[selectedIndex] = false;
        }
        totalCoins.text = SaveData.Instance.Coins.ToString();
        Usman_SaveLoad.SaveProgress();
    }
    #endregion
    
    #region UnEquipFunction
    public void UnEquipet()
    {
        if (selectedItem == SahreeSelectedItem.mathapatti)
        {
            playerElements.mathapattiImage.gameObject.SetActive(false);
        }
        else if (selectedItem == SahreeSelectedItem.bangle)
        {
            playerElements.bangleImage.gameObject.SetActive(false);
        }
        else if (selectedItem == SahreeSelectedItem.sahree)
        {
            playerElements.sahreeImage.sprite = defaultSahree;
        }
        else if (selectedItem == SahreeSelectedItem.earing)
        {
            playerElements.earingImage.gameObject.SetActive(false);
        }
        else if (selectedItem == SahreeSelectedItem.nosering)
        {
            playerElements.noseringImage.gameObject.SetActive(false);
        }
        else if (selectedItem == SahreeSelectedItem.eyeshade)
        {
            playerElements.eyeshadeImage.gameObject.SetActive(false);
        }
        else if (selectedItem == SahreeSelectedItem.necklace)
        {
            playerElements.necklaceImage.gameObject.SetActive(false);
        }
        else if (selectedItem == SahreeSelectedItem.hair)
        {
            playerElements.hairImage.sprite = defaultHair;
        }
        else if (selectedItem == SahreeSelectedItem.lips)
        {
            playerElements.lipsImage.sprite = defaultLips;
        }
        else if (selectedItem == SahreeSelectedItem.shoes)
        {
            playerElements.shoesImage.gameObject.SetActive(false);
        }
        else if (selectedItem == SahreeSelectedItem.eyebrow)
        {
            playerElements.eyebrowImage.sprite = defaultEyebrow;
        }
        else if (selectedItem == SahreeSelectedItem.blush)
        {
            playerElements.blushImage.gameObject.SetActive(false);
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
            uIElements.sahreeScroller.SetActive(true);
            uIElements.dressUpCategoryScroller.SetActive(true);
            uIElements.makeUpCategoryScroller.SetActive(false);
            selectedItem = SahreeSelectedItem.sahree;
            playerCharacterMover.Move(new Vector3(60, 0, 0), 0.5f, true, false);
            uIElements.dressUpBtn.SetActive(false);
            uIElements.makeUpBtn.SetActive(true);
        }
        else if (a == 1)
        {
            DisableScrollers();
            uIElements.lipsScroller.SetActive(true);
            uIElements.makeUpCategoryScroller.SetActive(true);
            uIElements.dressUpCategoryScroller.SetActive(false);
            selectedItem = SahreeSelectedItem.lips;
            playerCharacterMover.Move(new Vector3(50, -600, -1000), 0.5f, true, false);
            uIElements.makeUpBtn.SetActive(false);
            uIElements.dressUpBtn.SetActive(true);
        }
        GetItemsInfo();
    }
    //public void BGChange()
    //{
    //    if (bgSprites.Length > bgIndex)
    //    {
    //        print("abc");
    //        if (bgSprites[bgIndex])
    //        {
    //            uIElements.bgImage.sprite = bgSprites[bgIndex];
    //            bgIndex++;
    //            if (bgIndex >= bgSprites.Length) bgIndex = 0;
    //        }
    //    }
    //}
    #endregion

    #region BtnsTask
    public void Preview()
    {
        StartCoroutine(preview());
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
        uIElements.sahreeScroller.SetActive(true);
        uIElements.dressUpCategoryScroller.SetActive(true);
        uIElements.makeUpCategoryScroller.SetActive(false);
        selectedItem = SahreeSelectedItem.sahree;
        playerCharacterMover.Move(new Vector3(60, 0, 0), 0.5f, true, false);
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
        uIElements.cardImage.sprite = cardSprites[2];

        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < Random.Range(10, 25); i++)
        {
            oppoElements.botInVsPanel.gameObject.SetActive(false);
            oppoElements.botInVsPanel.gameObject.SetActive(true);
            oppoElements.botInVsPanel.GetComponent<AudioSource>().Play();
            oppoElements.botInVsPanel.sprite = botSprites[Random.Range(0, botSprites.Length)];
            oppoElements.botInVsAnimPanel.sprite = oppoElements.botInVsPanel.sprite;
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
            //oppoElements.nameVsAnimPanel.text = oppoElements.nameVsPanel.text;
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
        uIElements.coinSlot.SetActive(false);
        playerCharacterMover.Move(new Vector3(0, -300, -800), 0.7f, true, false);
        yield return new WaitForSeconds(0.68f);
        playerCharacterMover.Move(new Vector3(0, 200, -800), 0.7f, true, false);
        yield return new WaitForSeconds(0.68f);
        playerCharacterMover.Move(new Vector3(0, 96, 0), 0.7f, true, false);
    }

    IEnumerator submitlook()
    {
        uIElements.bgImage.sprite = judgementalPanelBgSprite;
        uIElements.judgementalPanel.SetActive(true);
        uIElements.submitPanel.SetActive(false);
        yield return new WaitForSeconds(1f);
        oppoElements.character.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        playerCharacterMover.Move(new Vector3(-290, 90, 0), 0.7f, true, false);
        yield return new WaitForSeconds(0.3f);
        oppoCharacterMover.Move(new Vector3(290, 90, 0), 0.7f, true, false);
        SaveData.Instance.vsMode = false;
        yield return new WaitForSeconds(0.1f);
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

    #region GetRewardedCoins
    public void GetRewardedCoins()
    {
        rewardType = RewardType.coins;
        CheckVideoStatus();
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

        if (sahreeRank > -1)
        {
            randomIndex = Random.Range(0, sahreeList.Count);
            if (sahreeList[randomIndex] && oppoElements.sahreeImage)
            {
                oppoElements.sahreeImage.gameObject.SetActive(true);
                oppoElements.sahreeImage.sprite = sahreeSprites[randomIndex];
            }
            dressingTotalRank += 10;
            oppoDressingRank += GetRank(randomIndex, sahreeList.Count);
        }

        if (shoesRank > -1)
        {
            randomIndex = Random.Range(0, shoesList.Count);
            if (shoesList[randomIndex] && oppoElements.shoesImage)
            {
                oppoElements.shoesImage.gameObject.SetActive(true);
                oppoElements.shoesImage.sprite = shoesSprites[randomIndex];
            }
            dressingTotalRank += 10;
            oppoDressingRank += GetRank(randomIndex, shoesList.Count);
        }

        if (clutchRank > -1)
        {
            randomIndex = Random.Range(0, clutchList.Count);
            if (clutchList[randomIndex] && oppoElements.clutchImage)
            {
                oppoElements.clutchImage.gameObject.SetActive(true);
                oppoElements.clutchImage.sprite = clutchSprites[randomIndex];
            }
            dressingTotalRank += 10;
            oppoDressingRank += GetRank(randomIndex, clutchList.Count);
        }

        if (hairRank > -1)
        {
            randomIndex = Random.Range(0, hairList.Count);
            if (hairList[randomIndex] && oppoElements.hairImage)
            {
                oppoElements.hairImage.gameObject.SetActive(true);
                oppoElements.hairImage.sprite = hairSprites[randomIndex];
            }
            makeupTotalRank += 10;
            oppoMakeupRank += GetRank(randomIndex, hairList.Count);
        }

        if (lipsRank > -1)
        {
            randomIndex = Random.Range(0, lipsList.Count);
            if (lipsList[randomIndex] && oppoElements.lipsImage)
            {
                oppoElements.lipsImage.gameObject.SetActive(true);
                oppoElements.lipsImage.sprite = lipsSprites[randomIndex];
            }
            makeupTotalRank += 10;
            oppoMakeupRank += GetRank(randomIndex, lipsList.Count);
        }

        if (eyeshadeRank > -1)
        {
            randomIndex = Random.Range(0, eyeshadeList.Count);
            if (eyeshadeList[randomIndex] && oppoElements.eyeshadeImage)
            {
                oppoElements.eyeshadeImage.gameObject.SetActive(true);
                oppoElements.eyeshadeImage.sprite = eyeshadeSprites[randomIndex];
            }
            makeupTotalRank += 10;
            oppoMakeupRank += GetRank(randomIndex, eyeshadeList.Count);
        }

        if (blushRank > -1)
        {
            randomIndex = Random.Range(0, blushList.Count);
            if (blushList[randomIndex] && oppoElements.blushImage)
            {
                oppoElements.blushImage.gameObject.SetActive(true);
                oppoElements.blushImage.sprite = blushSprites[randomIndex];
            }
            makeupTotalRank += 10;
            oppoMakeupRank += GetRank(randomIndex, eyeshadeList.Count);
        }

        if (eyebrowRank > -1)
        {
            randomIndex = Random.Range(0, eyebrowList.Count);
            if (eyebrowList[randomIndex] && oppoElements.eyebrowImage)
            {
                oppoElements.eyebrowImage.gameObject.SetActive(true);
                oppoElements.eyebrowImage.sprite = eyebrowSprites[randomIndex];
            }
            makeupTotalRank += 10;
            oppoMakeupRank += GetRank(randomIndex, eyebrowList.Count);
        }

        if (noseringRank > -1)
        {
            randomIndex = Random.Range(0, noseringList.Count);
            if (noseringList[randomIndex] && oppoElements.noseringImage)
            {
                oppoElements.noseringImage.gameObject.SetActive(true);
                oppoElements.noseringImage.sprite = noseringSprites[randomIndex];
            }
            jewelleryTotalRank += 10;
            oppoJewelleryRank += GetRank(randomIndex, noseringList.Count);
        }

        if (earingRank > -1)
        {
            randomIndex = Random.Range(0, earingList.Count);
            if (earingList[randomIndex] && oppoElements.earingImage)
            {
                oppoElements.earingImage.gameObject.SetActive(true);
                oppoElements.earingImage.sprite = earingSprites[randomIndex];
            }
            jewelleryTotalRank += 10;
            oppoJewelleryRank += GetRank(randomIndex, earingList.Count);
        }

        if (mathapattiRank > -1)
        {
            randomIndex = Random.Range(0, mathapattiList.Count);
            if (mathapattiList[randomIndex] && oppoElements.mathapattiImage)
            {
                oppoElements.mathapattiImage.gameObject.SetActive(true);
                oppoElements.mathapattiImage.sprite = mathapattiSprites[randomIndex];
            }
            jewelleryTotalRank += 10;
            oppoJewelleryRank += GetRank(randomIndex, mathapattiList.Count);
        }
        
        if (bangleRank > -1)
        {
            randomIndex = Random.Range(0, bangleList.Count);
            if (bangleList[randomIndex] && oppoElements.bangleImage)
            {
                oppoElements.bangleImage.gameObject.SetActive(true);
                oppoElements.bangleImage.sprite = bangleSprites[randomIndex];
            }
            jewelleryTotalRank += 10;
            oppoJewelleryRank += GetRank(randomIndex, bangleList.Count);
        }

        if (necklaceRank > -1)
        {
            randomIndex = Random.Range(0, necklaceList.Count);
            if (necklaceList[randomIndex] && oppoElements.necklaceImage)
            {
                oppoElements.necklaceImage.gameObject.SetActive(true);
                oppoElements.necklaceImage.sprite = necklaceSprites[randomIndex];
            }
            jewelleryTotalRank += 10;
            oppoJewelleryRank += GetRank(randomIndex, necklaceList.Count);
        }    
    }
    #endregion

    #region Comparing
    IEnumerator startComparing()
    {
        int totalRank = 0;
        int playerTotal = 0, oppoTotal = 0;
        yield return new WaitForSeconds(1);
        categorySlot.gameObject.SetActive(true);
        categoriesText.text = "DressUp";
        if (voteCatSFX) voteCatSFX.Play();
        categorySlot.Play(0);
        yield return new WaitForSeconds(2f);
        totalRank = GetRankValue(sahreeRank) + GetRankValue(shoesRank) + GetRankValue(clutchRank);
        playerTotal += totalRank;
        playerElements.voteScore.text = GetRank(totalRank, dressingTotalRank).ToString();
        oppoElements.voteScore.text = GetRank(dressingTotalRank, dressingTotalRank).ToString();
        voteCard.gameObject.SetActive(true);
        if (voteGivenSFX) voteGivenSFX.Play();
        voteCard.Play(0);
        TotalScoring();

        yield return new WaitForSeconds(2f);
        categoriesText.text = "MakeUp";
        if (voteCatSFX) voteCatSFX.Play();
        categorySlot.Play(0);
        voteCard.Play(0);
        yield return new WaitForSeconds(1f);
        totalRank = GetRankValue(eyeshadeRank) + GetRankValue(lipsRank) + GetRankValue(hairRank) + GetRankValue(blushRank) + GetRankValue(eyebrowRank);
        playerTotal += totalRank;
        playerElements.voteScore.text = GetRank(totalRank, makeupTotalRank).ToString();
        oppoElements.voteScore.text = GetRank(oppoMakeupRank, makeupTotalRank).ToString();
        if (voteGivenSFX) voteGivenSFX.Play();
        TotalScoring();

        yield return new WaitForSeconds(2f);
        categoriesText.text = "Jewellery";
        if (voteCatSFX) voteCatSFX.Play();
        categorySlot.Play(0);
        voteCard.Play(0);
        yield return new WaitForSeconds(1f);
        totalRank = GetRankValue(earingRank) + GetRankValue(noseringRank) + GetRankValue(mathapattiRank) + GetRankValue(necklaceRank) + GetRankValue(bangleRank);
        playerTotal += totalRank;
        playerElements.voteScore.text = GetRank(totalRank, jewelleryTotalRank).ToString();
        oppoElements.voteScore.text = GetRank(oppoJewelleryRank, jewelleryTotalRank).ToString();
        if (voteGivenSFX) voteGivenSFX.Play();
        TotalScoring();

        oppoTotal = oppoDressingRank + oppoJewelleryRank + oppoMakeupRank;
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
            playerCharacterMover.Move(new Vector3(42, -90, 0), 0.5f, true, false);
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
            oppoCharacterMover.Move(new Vector3(42, -90, 0), 0.5f, true, false);
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
