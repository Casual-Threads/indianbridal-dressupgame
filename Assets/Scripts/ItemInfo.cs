using Sirenix.OdinInspector;
using UnityEngine.UI;
using UnityEngine;



public class ItemInfo : MonoBehaviour
{
    public Button itemBtn;
    public Image itemIcon;
    public Image btnBG;
    public GameObject LockIcon;
    public GameObject videoLock;
    public GameObject coinLock;
    public GameObject hotRibben;
    public GameObject premiumRibben;
    public Text unlockCoins;
    public bool isLocked;
    [ShowIf("isLocked")]
    public bool videoUnlock;
    [ShowIf("isLocked")]
    public bool coinsUnlock;
    [Range(0, 50000)]
    [ShowIf("coinsUnlock")]
    public int requiredCoins;
    public bool hotRib;
    public bool premiumRib;
    public Text ItemScore;
}