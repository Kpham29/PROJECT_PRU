using UnityEngine;
using TMPro;

public class DamageUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI damageText; // Kéo TextMeshPro vào đây
    private MainCharacterStat playerStats; // Không cần gán trong Inspector

    void Start()
    {
        if (damageText == null)
        {
            Debug.LogError("DamageUI: TextMeshProUGUI chưa được gán!");
            return;
        }

        // Thử tìm player ngay khi bắt đầu
        FindPlayer();
        UpdateDamageText();
    }

    void Update()
    {
        // Nếu player chưa spawn, thử tìm lại
        if (playerStats == null)
        {
            FindPlayer();
        }

        // Khi đã có player thì cập nhật liên tục
        if (playerStats != null)
        {
            damageText.text = $"ATK: {playerStats.damage}";
        }
    }

    private void FindPlayer()
    {
        playerStats = FindObjectOfType<MainCharacterStat>();
        if (playerStats != null)
        {
            Debug.Log($"DamageUI: Đã tìm thấy player {playerStats.name}");
        }
    }

    // Cho phép script spawn nhân vật gán player vào UI
    public void SetPlayer(MainCharacterStat stats)
    {
        playerStats = stats;
        UpdateDamageText();
    }

    public void UpdateDamageText()
    {
        if (damageText != null && playerStats != null)
        {
            damageText.text = $"/nATK: {playerStats.damage}";
        }
    }

    public void OnDamageChanged()
    {
        UpdateDamageText();
    }
}
