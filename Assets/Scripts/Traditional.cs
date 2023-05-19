using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


[System.Serializable]
public class TraditionalElements
{
    [Header("Panels")]
    public GameObject vsPanel;
    public GameObject vsAnimPanel, gamePanel, submitPanel, screenShotPanel, judgementalPanel, stageDecuration, levelCompletePanel, adPanel, loadingPanel;
    [Header("PopUp")]
    public GameObject videoUnlockPopUp;
    public GameObject coinUnlockPopUp, enoughCoinPopUp, videoAdNotAvailablePopUp;
    [Header("Scrollers")]
    public GameObject allScroller;
    public GameObject dressUpCategoryScroller, makeUpCategoryScroller, frockScroller, bindiScroller, bangleScroller, dressScroller, earingScroller, eyebrowScroller, blushScroller,
                      eyeshadeScroller, necklaceScroller, hairScroller, handthingScroller, lipsScroller, shoesScroller;
    [Header("UI")]
    public GameObject dressUpBtn;
    public GameObject makeUpBtn, connected, startBtn, coinSlot, previewBtn, submitPanelbar, lastPanel;
    [Header("Traditional Image")]
    public Image bgImage;
    public Image cardImage, screenShotImage, fillbar;
}

[System.Serializable]
public class TraditionalPlayerElenemts
{
    [Header("Player Character")]
    public GameObject character;
    [Header("Player Images")]
    public Image frockImage;
    public Image bindiImage, bangleImage, dressImage, earingImage, eyeshadeImage, necklaceImage, hairImage, handthingImage, lipsImage, shoesImage, eyebrowImage, blushImage;
    [Header("Player Score Text")]
    public Text voteScore;
    public Text totalScore;
    [Header("Player Winner")]
    public GameObject winner;
}

[System.Serializable]
public class TraditionalOpponentElenemts
{
    [Header("Opponent Character")]
    public GameObject character;
    [Header("Opponent Images")]
    public Image frockImage;
    public Image bindiImage, bangleImage, dressImage, earingImage, eyeshadeImage, necklaceImage, hairImage, handthingImage, lipsImage, shoesImage, eyebrowImage, blushImage;
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
public enum TraditionalSelectedItem
{
    dress, shoes, bangle, earing, necklace, frock, bindi, handthing, lips, hair, eyeshade, blush, eyebrow
}

public class Traditional : MonoBehaviour
{
    public TraditionalSelectedItem selectedItem;
    [FoldoutGroup("UI Elements")]
    [HideLabel]
    public TraditionalElements uIElements;
    [FoldoutGroup("Player Elements")]
    [HideLabel]
    public TraditionalPlayerElenemts playerElements;
    [FoldoutGroup("Opponent Elements")]
    [HideLabel]
    public TraditionalOpponentElenemts oppoElements;
    [Header("Mover Item")]
    public MRS_Manager playerCharacterMover;
    public MRS_Manager oppoCharacterMover;
    public CoinsAdder coinsAdder;
    [Header("Sprites Array")]
    public Sprite[] cardSprites;
    public Sprite[] botSprites;
    public Sprite[] frockSprites;
    public Sprite[] bindiSprites;
    public Sprite[] bangleSprites;
    public Sprite[] dressSprites;
    public Sprite[] earingSprites;
    public Sprite[] eyeshadeSprites;
    public Sprite[] necklaceSprites;
    public Sprite[] hairSprites;
    public Sprite[] handthingSprites;
    public Sprite[] lipsSprites;
    public Sprite[] shoesSprites;
    public Sprite[] eyebrowSprites;
    public Sprite[] blushSprites;
    public Sprite[] defaultIconSprites;
    public Sprite[] selectedIconSprites;
    [Header("Scroller Btn Image Array")]
    public Image[] categoryBtn;
    [Header("Item List")]
    private List<ItemInfo> frockList = new List<ItemInfo>();
    private List<ItemInfo> bindiList = new List<ItemInfo>();
    private List<ItemInfo> bangleList = new List<ItemInfo>();
    private List<ItemInfo> dressList = new List<ItemInfo>();
    private List<ItemInfo> earingList = new List<ItemInfo>();
    private List<ItemInfo> eyeshadeList = new List<ItemInfo>();
    private List<ItemInfo> necklaceList = new List<ItemInfo>();
    private List<ItemInfo> hairList = new List<ItemInfo>();
    private List<ItemInfo> handthingList = new List<ItemInfo>();
    private List<ItemInfo> lipsList = new List<ItemInfo>();
    private List<ItemInfo> shoesList = new List<ItemInfo>();
    private List<ItemInfo> eyebrowList = new List<ItemInfo>();
    private List<ItemInfo> blushList = new List<ItemInfo>();
    [Header("Default Character Sprites")]
    public Sprite defaultDress;
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
    private int frockRank, bindiRank, bangleRank, dressRank, earingRank, eyeshadeRank, necklaceRank, hairRank, handthingRank, lipsRank, shoesRank, eyebrowRank, blushRank;
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
        selectedItem = TraditionalSelectedItem.dress;
        uIElements.dressScroller.SetActive(true);
        SetInitialValues();
        GetItemsInfo();
        StartCoroutine(AdDelay(45));
        totalCoins.text = SaveData.Instance.Coins.ToString();
        StartCoroutine(findOpponent());
        OpponentDressing();

        dressRank = hairRank = lipsRank = eyebrowRank = 1;
        blushRank = frockRank = bindiRank = bangleRank = earingRank = eyeshadeRank = necklaceRank = handthingRank = shoesRank = - 1;
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

        #region Initialing frock
        if (uIElements.frockScroller)
        {
            var frockInfo = uIElements.frockScroller.GetComponentsInChildren<ItemInfo>();
            for (int i = 0; i < frockInfo.Length; i++)
            {
                frockList.Add(frockInfo[i]);
            }
        }
        SetupItemData(SaveData.Instance.TraditionalModeElements.frock, frockList);
        SetItemIcon(frockList, frockSprites);
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
        SetupItemData(SaveData.Instance.TraditionalModeElements.bindi, bindiList);
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
        SetupItemData(SaveData.Instance.TraditionalModeElements.bangle, bangleList);
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
        SetupItemData(SaveData.Instance.TraditionalModeElements.dress, dressList);
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
        SetupItemData(SaveData.Instance.TraditionalModeElements.earing, earingList);
        SetItemIcon(earingList, earingSprites);
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
        SetupItemData(SaveData.Instance.TraditionalModeElements.eyeshade, eyeshadeList);
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
        SetupItemData(SaveData.Instance.TraditionalModeElements.necklace, necklaceList);
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
        SetupItemData(SaveData.Instance.TraditionalModeElements.hair, hairList);
        SetItemIcon(hairList, hairSprites);
        #endregion

        #region Initialing handthing
        if (uIElements.handthingScroller)
        {
            var handthingInfo = uIElements.handthingScroller.GetComponentsInChildren<ItemInfo>();
            for (int i = 0; i < handthingInfo.Length; i++)
            {
                handthingList.Add(handthingInfo[i]);
            }
        }
        SetupItemData(SaveData.Instance.TraditionalModeElements.handthing, handthingList);
        SetItemIcon(handthingList, handthingSprites);
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
        SetupItemData(SaveData.Instance.TraditionalModeElements.lips, lipsList);
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
        SetupItemData(SaveData.Instance.TraditionalModeElements.shoes, shoesList);
        SetItemIcon(shoesList, shoesSprites);
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
        SetupItemData(SaveData.Instance.TraditionalModeElements.blush, blushList);
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
        SetupItemData(SaveData.Instance.TraditionalModeElements.eyebrow, eyebrowList);
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
        if (selectedItem == TraditionalSelectedItem.frock)
        {
            CheckSelectedItem(frockList, frockSprites, playerElements.frockImage);
        }
        else if (selectedItem == TraditionalSelectedItem.bindi)
        {
            CheckSelectedItem(bindiList, bindiSprites, playerElements.bindiImage);
        }
        else if (selectedItem == TraditionalSelectedItem.bangle)
        {
            CheckSelectedItem(bangleList, bangleSprites, playerElements.bangleImage);
        }
        else if (selectedItem == TraditionalSelectedItem.dress)
        {
            CheckSelectedItem(dressList, dressSprites, playerElements.dressImage);
        }
        else if (selectedItem == TraditionalSelectedItem.earing)
        {
            CheckSelectedItem(earingList, earingSprites, playerElements.earingImage);
        }
        else if (selectedItem == TraditionalSelectedItem.eyeshade)
        {
            CheckSelectedItem(eyeshadeList, eyeshadeSprites, playerElements.eyeshadeImage);
        }
        else if (selectedItem == TraditionalSelectedItem.necklace)
        {
            CheckSelectedItem(necklaceList, necklaceSprites, playerElements.necklaceImage);
        }
        else if (selectedItem == TraditionalSelectedItem.hair)
        {
            CheckSelectedItem(hairList, hairSprites, playerElements.hairImage);
        }
        else if (selectedItem == TraditionalSelectedItem.handthing)
        {
            CheckSelectedItem(handthingList, handthingSprites, playerElements.handthingImage);
        }
        else if (selectedItem == TraditionalSelectedItem.lips)
        {
            CheckSelectedItem(lipsList, lipsSprites, playerElements.lipsImage);
        }
        else if (selectedItem == TraditionalSelectedItem.shoes)
        {
            CheckSelectedItem(shoesList, shoesSprites, playerElements.shoesImage);
        }
        else if (selectedItem == TraditionalSelectedItem.blush)
        {
            CheckSelectedItem(blushList, blushSprites, playerElements.blushImage);
        }
        else if (selectedItem == TraditionalSelectedItem.eyebrow)
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
                           
                            if (selectedItem == TraditionalSelectedItem.frock)
                            {
                                IsDressUp = true;
                                playerElements.dressImage.gameObject.SetActive(false);
                                playerElements.frockImage.gameObject.SetActive(true);
                                frockRank = GetRank(selectedIndex, frockList.Count);
                            }
                            else if (selectedItem == TraditionalSelectedItem.dress)
                            {
                                IsDressUp = true;
                                playerElements.dressImage.gameObject.SetActive(true);
                                playerElements.frockImage.gameObject.SetActive(false);
                                dressRank = GetRank(selectedIndex, dressList.Count);
                            }
                            else if (selectedItem == TraditionalSelectedItem.handthing)
                            {
                                IsDressUp = true;
                                handthingRank = GetRank(selectedIndex, handthingList.Count);
                            }
                            else if (selectedItem == TraditionalSelectedItem.shoes)
                            {
                                IsDressUp = true;
                                shoesRank = GetRank(selectedIndex, shoesList.Count);
                            }
                            else if (selectedItem == TraditionalSelectedItem.bindi)
                            {
                                IsJewellery = true;
                                bindiRank = GetRank(selectedIndex, bindiList.Count);
                            }
                            else if (selectedItem == TraditionalSelectedItem.bangle)
                            {
                                IsJewellery = true;
                                bangleRank = GetRank(selectedIndex, bangleList.Count);
                            }
                            else if (selectedItem == TraditionalSelectedItem.earing)
                            {
                                IsJewellery = true;
                                earingRank = GetRank(selectedIndex, earingList.Count);
                            }
                            else if (selectedItem == TraditionalSelectedItem.necklace)
                            {
                                IsJewellery = true;
                                necklaceRank = GetRank(selectedIndex, necklaceList.Count);
                            }
                            else if (selectedItem == TraditionalSelectedItem.eyeshade)
                            {
                                IsMakeUp = true;
                                eyeshadeRank = GetRank(selectedIndex, eyeshadeList.Count);
                            }
                            else if (selectedItem == TraditionalSelectedItem.hair)
                            {
                                IsMakeUp = true;
                                hairRank = GetRank(selectedIndex, hairList.Count);
                            }
                            else if (selectedItem == TraditionalSelectedItem.lips)
                            {
                                IsMakeUp = true;
                                lipsRank = GetRank(selectedIndex, lipsList.Count);
                            }
                            else if (selectedItem == TraditionalSelectedItem.eyebrow)
                            {
                                IsMakeUp = true;
                                eyebrowRank = GetRank(selectedIndex, eyebrowList.Count);
                            }
                            else if (selectedItem == TraditionalSelectedItem.blush)
                            {
                                IsMakeUp = true;
                                blushRank = GetRank(selectedIndex, blushList.Count);
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
        if (selectedItem == TraditionalSelectedItem.frock)
        {
            coinUnlockItem(frockList);
        }
        else if (selectedItem == TraditionalSelectedItem.bindi)
        {
            coinUnlockItem(bindiList);
        }
        else if (selectedItem == TraditionalSelectedItem.bangle)
        {
            coinUnlockItem(bangleList);
        }
        else if (selectedItem == TraditionalSelectedItem.dress)
        {
            coinUnlockItem(dressList);
        }
        else if (selectedItem == TraditionalSelectedItem.earing)
        {
            coinUnlockItem(earingList);
        }
        else if (selectedItem == TraditionalSelectedItem.eyeshade)
        {
            coinUnlockItem(eyeshadeList);
        }
        else if (selectedItem == TraditionalSelectedItem.necklace)
        {
            coinUnlockItem(necklaceList);
        }
        else if (selectedItem == TraditionalSelectedItem.hair)
        {
            coinUnlockItem(hairList);
        }
        else if (selectedItem == TraditionalSelectedItem.handthing)
        {
            coinUnlockItem(handthingList);
        }
        else if (selectedItem == TraditionalSelectedItem.lips)
        {
            coinUnlockItem(lipsList);
        }
        else if (selectedItem == TraditionalSelectedItem.shoes)
        {
            coinUnlockItem(shoesList);
        }
        else if (selectedItem == TraditionalSelectedItem.blush)
        {
            coinUnlockItem(blushList);
        }
        else if (selectedItem == TraditionalSelectedItem.eyebrow)
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
        if (selectedItem == TraditionalSelectedItem.frock)
        {
            SetItemsInfo(frockList);
        }
        else if (selectedItem == TraditionalSelectedItem.bindi)
        {
            SetItemsInfo(bindiList);
        }
        else if (selectedItem == TraditionalSelectedItem.bangle)
        {
            SetItemsInfo(bangleList);
        }
        if (selectedItem == TraditionalSelectedItem.dress)
        {
            SetItemsInfo(dressList);
        }
        else if (selectedItem == TraditionalSelectedItem.earing)
        {
            SetItemsInfo(earingList);
        }
        else if (selectedItem == TraditionalSelectedItem.eyeshade)
        {
            SetItemsInfo(eyeshadeList);
        }
        else if (selectedItem == TraditionalSelectedItem.necklace)
        {
            SetItemsInfo(necklaceList);
        }
        else if (selectedItem == TraditionalSelectedItem.hair)
        {
            SetItemsInfo(hairList);
        }
        else if (selectedItem == TraditionalSelectedItem.handthing)
        {
            SetItemsInfo(handthingList);
        }
        else if (selectedItem == TraditionalSelectedItem.lips)
        {
            SetItemsInfo(lipsList);
        }
        else if (selectedItem == TraditionalSelectedItem.shoes)
        {
            SetItemsInfo(shoesList);
        }
        else if (selectedItem == TraditionalSelectedItem.blush)
        {
            SetItemsInfo(blushList);
        }
        else if (selectedItem == TraditionalSelectedItem.eyebrow)
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
        
        uIElements.frockScroller.SetActive(false);
        uIElements.bindiScroller.SetActive(false);
        uIElements.bangleScroller.SetActive(false);
        uIElements.dressScroller.SetActive(false);
        uIElements.earingScroller.SetActive(false);
        uIElements.eyeshadeScroller.SetActive(false);
        uIElements.necklaceScroller.SetActive(false);
        uIElements.hairScroller.SetActive(false);
        uIElements.handthingScroller.SetActive(false);
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

        if (index == (int)TraditionalSelectedItem.frock)
        {
            playerCharacterMover.Move(new Vector3(90, 0, 0), 0.5f, true, false);
            selectedItem = TraditionalSelectedItem.frock;
            uIElements.frockScroller.SetActive(true);
        }
        else if (index == (int)TraditionalSelectedItem.bindi)
        {
            playerCharacterMover.Move(new Vector3(20, -600, -1000), 0.5f, true, false);
            selectedItem = TraditionalSelectedItem.bindi;
            uIElements.bindiScroller.SetActive(true);
        }
        else if (index == (int)TraditionalSelectedItem.bangle)
        {
            playerCharacterMover.Move(new Vector3(90, 0, 0), 0.5f, true, false);
            selectedItem = TraditionalSelectedItem.bangle;
            uIElements.bangleScroller.SetActive(true);
        }
        else if (index == (int)TraditionalSelectedItem.dress)
        {
            playerCharacterMover.Move(new Vector3(90, 0, 0), 0.5f, true, false);
            selectedItem = TraditionalSelectedItem.dress;
            uIElements.dressScroller.SetActive(true);
        }
        else if (index == (int)TraditionalSelectedItem.earing)
        {
            playerCharacterMover.Move(new Vector3(20, -600, -1000), 0.5f, true, false);
            selectedItem = TraditionalSelectedItem.earing;
            uIElements.earingScroller.SetActive(true);
        }
        else if (index == (int)TraditionalSelectedItem.eyeshade)
        {
            playerCharacterMover.Move(new Vector3(20, -600, -1000), 0.5f, true, false);
            selectedItem = TraditionalSelectedItem.eyeshade;
            uIElements.eyeshadeScroller.SetActive(true);
        }
        else if (index == (int)TraditionalSelectedItem.necklace)
        {
            playerCharacterMover.Move(new Vector3(20, -600, -1000), 0.5f, true, false);
            selectedItem = TraditionalSelectedItem.necklace;
            uIElements.necklaceScroller.SetActive(true);
        }
        else if (index == (int)TraditionalSelectedItem.hair)
        {
            playerCharacterMover.Move(new Vector3(20, -600, -1000), 0.5f, true, false);
            selectedItem = TraditionalSelectedItem.hair;
            uIElements.hairScroller.SetActive(true);
        }
        else if (index == (int)TraditionalSelectedItem.handthing)
        {
            playerCharacterMover.Move(new Vector3(90, 0, 0), 0.5f, true, false);
            selectedItem = TraditionalSelectedItem.handthing;
            uIElements.handthingScroller.SetActive(true);
        }
        else if (index == (int)TraditionalSelectedItem.lips)
        {
            playerCharacterMover.Move(new Vector3(20, -600, -1000), 0.5f, true, false);
            selectedItem = TraditionalSelectedItem.lips;
            uIElements.lipsScroller.SetActive(true);
        }
        else if (index == (int)TraditionalSelectedItem.blush)
        {
            playerCharacterMover.Move(new Vector3(20, -600, -1000), 0.5f, true, false);
            selectedItem = TraditionalSelectedItem.blush;
            uIElements.blushScroller.SetActive(true);
        }
        else if (index == (int)TraditionalSelectedItem.eyebrow)
        {
            playerCharacterMover.Move(new Vector3(20, -600, -1000), 0.5f, true, false);
            selectedItem = TraditionalSelectedItem.eyebrow;
            uIElements.eyebrowScroller.SetActive(true);
        }
        else if (index == (int)TraditionalSelectedItem.shoes)
        {
            playerCharacterMover.Move(new Vector3(90, 0, 0), 0.5f, true, false);
            selectedItem = TraditionalSelectedItem.shoes;
            uIElements.shoesScroller.SetActive(true);
        }
        GetItemsInfo();
    }
    #endregion

    #region UnlockSingleItem
    public void UnlockSingleItem()
    {
        if (selectedItem == TraditionalSelectedItem.frock)
        {
            SaveData.Instance.TraditionalModeElements.frock[selectedIndex] = false;
        }
        else if (selectedItem == TraditionalSelectedItem.bindi)
        {
            SaveData.Instance.TraditionalModeElements.bindi[selectedIndex] = false;
        }
        else if (selectedItem == TraditionalSelectedItem.bangle)
        {
            SaveData.Instance.TraditionalModeElements.bangle[selectedIndex] = false;
        }
        else if (selectedItem == TraditionalSelectedItem.dress)
        {
            SaveData.Instance.TraditionalModeElements.dress[selectedIndex] = false;
        }
        else if (selectedItem == TraditionalSelectedItem.earing)
        {
            SaveData.Instance.TraditionalModeElements.earing[selectedIndex] = false;
        }
        else if (selectedItem == TraditionalSelectedItem.eyeshade)
        {
            SaveData.Instance.TraditionalModeElements.eyeshade[selectedIndex] = false;
        }
        else if (selectedItem == TraditionalSelectedItem.necklace)
        {
            SaveData.Instance.TraditionalModeElements.necklace[selectedIndex] = false;
        }
        else if (selectedItem == TraditionalSelectedItem.hair)
        {
            SaveData.Instance.TraditionalModeElements.hair[selectedIndex] = false;
        }
        else if (selectedItem == TraditionalSelectedItem.handthing)
        {
            SaveData.Instance.TraditionalModeElements.handthing[selectedIndex] = false;
        }
        else if (selectedItem == TraditionalSelectedItem.lips)
        {
            SaveData.Instance.TraditionalModeElements.lips[selectedIndex] = false;
        }
        else if (selectedItem == TraditionalSelectedItem.shoes)
        {
            SaveData.Instance.TraditionalModeElements.shoes[selectedIndex] = false;
        }
        else if (selectedItem == TraditionalSelectedItem.blush)
        {
            SaveData.Instance.TraditionalModeElements.blush[selectedIndex] = false;
        }
        else if (selectedItem == TraditionalSelectedItem.eyebrow)
        {
            SaveData.Instance.TraditionalModeElements.eyebrow[selectedIndex] = false;
        }
        totalCoins.text = SaveData.Instance.Coins.ToString();
        Usman_SaveLoad.SaveProgress();
    }
    #endregion
    
    #region UnEquipFunction
    public void UnEquipet()
    {
        if (selectedItem == TraditionalSelectedItem.frock)
        {
            playerElements.frockImage.gameObject.SetActive(false);
        }
        else if (selectedItem == TraditionalSelectedItem.bindi)
        {
            playerElements.bindiImage.gameObject.SetActive(false);
        }
        else if (selectedItem == TraditionalSelectedItem.bangle)
        {
            playerElements.bangleImage.gameObject.SetActive(false);
        }
        else if (selectedItem == TraditionalSelectedItem.dress)
        {
            playerElements.dressImage.sprite = defaultDress;
        }
        else if (selectedItem == TraditionalSelectedItem.earing)
        {
            playerElements.earingImage.gameObject.SetActive(false);
        }
        else if (selectedItem == TraditionalSelectedItem.eyeshade)
        {
            playerElements.eyeshadeImage.gameObject.SetActive(false);
        }
        else if (selectedItem == TraditionalSelectedItem.necklace)
        {
            playerElements.necklaceImage.gameObject.SetActive(false);
        }
        else if (selectedItem == TraditionalSelectedItem.hair)
        {
            playerElements.hairImage.sprite = defaultHair;
        }
        else if (selectedItem == TraditionalSelectedItem.handthing)
        {
            playerElements.handthingImage.gameObject.SetActive(false);
        }
        else if (selectedItem == TraditionalSelectedItem.lips)
        {
            playerElements.lipsImage.sprite = defaultLips;
        }
        else if (selectedItem == TraditionalSelectedItem.eyebrow)
        {
            playerElements.eyebrowImage.sprite = defaultEyebrow;
        }
        else if (selectedItem == TraditionalSelectedItem.blush)
        {
            playerElements.blushImage.gameObject.SetActive(false);
        }
        else if (selectedItem == TraditionalSelectedItem.shoes)
        {
            playerElements.shoesImage.gameObject.SetActive(false);
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
            selectedItem = TraditionalSelectedItem.dress;
            playerCharacterMover.Move(new Vector3(90, 0, 0), 0.5f, true, false);
            uIElements.dressUpBtn.SetActive(false);
            uIElements.makeUpBtn.SetActive(true);
        }
        else if (a == 1)
        {
            DisableScrollers();
            uIElements.lipsScroller.SetActive(true);
            uIElements.makeUpCategoryScroller.SetActive(true);
            uIElements.dressUpCategoryScroller.SetActive(false);
            selectedItem = TraditionalSelectedItem.lips;
            playerCharacterMover.Move(new Vector3(20, -600, -1000), 0.5f, true, false);
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
        uIElements.dressScroller.SetActive(true);
        uIElements.dressUpCategoryScroller.SetActive(true);
        uIElements.makeUpCategoryScroller.SetActive(false);
        selectedItem = TraditionalSelectedItem.dress;
        playerCharacterMover.Move(new Vector3(90, 0, 0), 0.5f, true, false);
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
        uIElements.cardImage.sprite = cardSprites[1];

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
        playerCharacterMover.Move(new Vector3(0, 88, 0), 0.7f, true, false);
    }

    IEnumerator submitlook()
    {
        uIElements.bgImage.sprite = judgementalPanelBgSprite;
        uIElements.judgementalPanel.SetActive(true);
        uIElements.submitPanel.SetActive(false);
        yield return new WaitForSeconds(1f);
        oppoElements.character.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        playerCharacterMover.Move(new Vector3(-290, 63f, 0), 0.7f, true, false);
        yield return new WaitForSeconds(0.3f);
        oppoCharacterMover.Move(new Vector3(290, 63f, 0), 0.7f, true, false);
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

        if (dressRank > -1)
        {
            randomIndex = Random.Range(0, dressList.Count);
            if (dressList[randomIndex] && oppoElements.dressImage)
            {
                oppoElements.dressImage.gameObject.SetActive(true);
                oppoElements.frockImage.gameObject.SetActive(false);
                oppoElements.dressImage.sprite = dressSprites[randomIndex];
            }
            dressingTotalRank += 10;
            oppoDressingRank += GetRank(randomIndex, dressList.Count);
        }

        if (frockRank > -1)
        {
            randomIndex = Random.Range(0, frockList.Count);
            if (frockList[randomIndex] && oppoElements.frockImage)
            {
                oppoElements.frockImage.gameObject.SetActive(true);
                oppoElements.dressImage.gameObject.SetActive(false);
                oppoElements.frockImage.sprite = frockSprites[randomIndex];
            }
            dressingTotalRank += 10;
            oppoDressingRank += GetRank(randomIndex, frockList.Count);
        }

        if (handthingRank > -1)
        {
            randomIndex = Random.Range(0, handthingList.Count);
            if (handthingList[randomIndex] && oppoElements.handthingImage)
            {
                oppoElements.handthingImage.gameObject.SetActive(true);
                oppoElements.handthingImage.sprite = handthingSprites[randomIndex];
            }
            dressingTotalRank += 10;
            oppoDressingRank += GetRank(randomIndex, handthingList.Count);
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

        if (bindiRank > -1)
        {
            randomIndex = Random.Range(0, bindiList.Count);
            if (bindiList[randomIndex] && oppoElements.bindiImage)
            {
                oppoElements.bindiImage.gameObject.SetActive(true);
                oppoElements.bindiImage.sprite = bindiSprites[randomIndex];
            }
            jewelleryTotalRank += 10;
            oppoJewelleryRank += GetRank(randomIndex, bindiList.Count);
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
        voteCard.Play(0);
        yield return new WaitForSeconds(2f);
        totalRank = GetRankValue(dressRank) + GetRankValue(frockRank) + GetRankValue(shoesRank) + GetRankValue(handthingRank);
        playerTotal += totalRank;
        playerElements.voteScore.text = GetRank(totalRank, dressingTotalRank).ToString();
        oppoElements.voteScore.text = GetRank(oppoDressingRank, dressingTotalRank).ToString();
        voteCard.gameObject.SetActive(true);
        if (voteGivenSFX) voteGivenSFX.Play();
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
        totalRank = GetRankValue(earingRank) + GetRankValue(bangleRank) + GetRankValue(necklaceRank) + GetRankValue(bindiRank);
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
            playerCharacterMover.Move(new Vector3(54, -50, 0), 0.5f, true, false);
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
            oppoCharacterMover.Move(new Vector3(54, -50, 0), 0.5f, true, false);
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
