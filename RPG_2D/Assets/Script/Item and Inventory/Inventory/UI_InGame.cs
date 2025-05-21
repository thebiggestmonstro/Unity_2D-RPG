using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_InGame : MonoBehaviour
{
    [SerializeField] 
    private PlayerStats _playerStats;
    [SerializeField] 
    private Slider _healthBar;

    [SerializeField] 
    private Image _skillDashImage;
    [SerializeField] 
    private Image _skillParryImage;
    [SerializeField] 
    private Image _skillCrystalImage;
    [SerializeField] 
    private Image _skillThrowingSwordImage;
    [SerializeField] 
    private Image _skillBlackholeImage;
    [SerializeField] 
    private Image _itemFlaskImage;

    [SerializeField] 
    private TextMeshProUGUI _currentCurrencyForSkillUnlockText;
    private SkillManager _skillManager;

    void Start()
    {
        if (_playerStats != null)
            _playerStats.onHealthChanged += UpdateHealthBarUI;

        _skillManager = SkillManager._skillManagerInstance;
    }

    void Update()
    {
        _currentCurrencyForSkillUnlockText.text = PlayerManager._playerManagerInstance.GetCurrency().ToString("#,#");

        if (PlayerController._playerControllerInstance._isDashCliekd == true && _skillManager._skillDash._dashUnlocked)
            SetCooldown(_skillDashImage);

        if (PlayerController._playerControllerInstance._isCounterAttackClicked == true && _skillManager._skillParry._parryUnlocked)
            SetCooldown(_skillParryImage);

        if (PlayerController._playerControllerInstance._isMakingCrystal == true && _skillManager._skillCrystal._crystalUnlocked)
            SetCooldown(_skillCrystalImage);

        if (PlayerController._playerControllerInstance._isThrowSwordClicked == true && _skillManager._skillThrowingSword._swordUnlocked)
            SetCooldown(_skillThrowingSwordImage);

        if (PlayerController._playerControllerInstance._isCastingBlackHole == true && _skillManager._skillBlackHole._blackholeUnlocked)
            SetCooldown(_skillBlackholeImage);

        if (PlayerController._playerControllerInstance._isDrinkingPotion == true && InventoryManager._inventoryManagerInstance.GetEquipment(EquipmentType.Flask) != null)
            SetCooldown(_itemFlaskImage);

        CheckCooldown(_skillDashImage, _skillManager._skillDash._cooldown);
        CheckCooldown(_skillParryImage, _skillManager._skillParry._cooldown);
        CheckCooldown(_skillCrystalImage, _skillManager._skillCrystal._cooldown);
        CheckCooldown(_skillThrowingSwordImage, _skillManager._skillThrowingSword._cooldown);
        CheckCooldown(_skillBlackholeImage, _skillManager._skillBlackHole._cooldown);

        CheckCooldown(_itemFlaskImage, InventoryManager._inventoryManagerInstance._potionUsageCooldown);
    }

    private void UpdateHealthBarUI()
    {
        _healthBar.maxValue = _playerStats.GetMaxHealthValue();
        _healthBar.value = _playerStats._currentHealth;
    }

    private void SetCooldown(Image image)
    {
        if (image.fillAmount <= 0)
            image.fillAmount = 1;
    }

    private void CheckCooldown(Image image, float cooldown)
    {
        if (image.fillAmount > 0)
            image.fillAmount -= 1 / cooldown * Time.deltaTime;
    }
}
