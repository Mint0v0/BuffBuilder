// SynergyPanelUI.cs
// 负责管理联动面板，刷新所有联动条目
// 读取 PlayerData，不直接接触 SynergyManager

using System.Collections.Generic;
using UnityEngine;

public class SynergyPanelUI : MonoBehaviour
{
    // 联动条目的容器，Inspector 拖入 SynergyContainer
    [SerializeField] private Transform container;

    // 单条联动的 Prefab，Inspector 拖入
    [SerializeField] private GameObject synergySlotPrefab;

    // 刷新整个联动面板
    public void Refresh(PlayerData playerData)
    {
        // 清空旧条目
        foreach (Transform child in container)
            Destroy(child.gameObject);

        // 读取 PlayerData 中的联动状态
        List<ActiveSynergy> synergies = playerData.GetActiveSynergies();

        // 逐条生成显示
        foreach (ActiveSynergy synergy in synergies)
        {
            GameObject slot = Instantiate(synergySlotPrefab, container);
            slot.GetComponent<SynergySlotUI>().Setup(synergy);
        }
    }
}