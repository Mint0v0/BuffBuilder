// SynergySlotUI.cs
// 负责显示单条联动的状态（名称 + 是否激活）
// 不负责判断逻辑，只负责显示

using UnityEngine;
using TMPro;

public class SynergySlotUI : MonoBehaviour
{
    // 联动名称文本，Inspector 拖入
    [SerializeField] private TextMeshProUGUI nameText;

    // 激活状态文本，Inspector 拖入
    [SerializeField] private TextMeshProUGUI statusText;

    // 根据 ActiveSynergy 刷新显示
    public void Setup(ActiveSynergy synergy)
    {
        nameText.text = synergy.Data.SynergyName;

        if (synergy.IsActive)
        {
            statusText.text = "[ 已激活 ]";
            statusText.color = Color.yellow;
        }
        else
        {
            statusText.text = "未激活";
            statusText.color = Color.gray;
        }
    }
}