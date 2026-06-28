using UnityEngine;
using TMPro;

// TopBarUI.cs
// 专门负责刷新顶部信息栏的 UI
// 不处理任何游戏逻辑，只负责显示
// 挂在场景中的 TopBar 物体上
public class TopBarUI : MonoBehaviour
{
    [Header("UI 引用 - 在 Inspector 里拖入")]
    // 金币显示文字
    [SerializeField] private TextMeshProUGUI goldText;
    // 攻击力显示文字
    [SerializeField] private TextMeshProUGUI attackText;

    // 接收 PlayerData，刷新所有显示
    // 由 BuffManager 在数据变化后调用
    public void Refresh(PlayerData playerData)
    {
        goldText.text  = $"金币: {playerData.Gold}";
        attackText.text = $"攻击: {playerData.Attack}";
    }
}