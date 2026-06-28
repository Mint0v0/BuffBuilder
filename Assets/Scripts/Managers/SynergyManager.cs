// SynergyManager.cs
// 负责扫描玩家 Buff，判断联动是否激活，结果写入 PlayerData
// 不负责 UI，不负责回合流程

using System.Collections.Generic;
using UnityEngine;

public class SynergyManager : MonoBehaviour
{
    // 所有联动规则，在 Inspector 中拖入 SO 资产
    [SerializeField] private SynergyData[] allSynergies;

    // 当前所有联动的运行时状态列表
    private List<ActiveSynergy> activeSynergies = new List<ActiveSynergy>();

    // 初始化：根据 SO 资产创建运行时状态列表
    private void Awake()
    {
        activeSynergies.Clear();
        foreach (SynergyData data in allSynergies)
        {
            activeSynergies.Add(new ActiveSynergy(data));
        }
    }

    // 外部调用入口：传入 PlayerData，判断并写入联动结果
    public void EvaluateSynergies(PlayerData playerData)
    {
        // 第一步：统计玩家拥有的每种 BuffType 数量
        Dictionary<BuffType, int> buffCounts = CountBuffTypes(playerData.Buffs);

        // 第二步：逐条判断联动规则
        foreach (ActiveSynergy synergy in activeSynergies)
        {
            // 先重置状态
            synergy.Deactivate();

            // 检查该联动的所有需求是否满足
            if (CheckRequirements(synergy.Data.RequiredBuffTypes, buffCounts))
            {
                synergy.Activate();
                Debug.Log($"[SynergyManager] 联动激活：{synergy.Data.SynergyName}");
            }
        }

        // 第三步：写入 PlayerData
        playerData.SetActiveSynergies(activeSynergies);
    }

    // 统计玩家 Buff 列表中每种 BuffType 的数量
    private Dictionary<BuffType, int> CountBuffTypes(List<BuffData> buffs)
    {
        Dictionary<BuffType, int> counts = new Dictionary<BuffType, int>();
        foreach (BuffData buff in buffs)
        {
            if (counts.ContainsKey(buff.Type))
    counts[buff.Type]++;
else
    counts[buff.Type] = 1;
        }
        return counts;
    }

    // 检查需求数组是否在 buffCounts 中全部满足
    private bool CheckRequirements(BuffType[] required, Dictionary<BuffType, int> buffCounts)
    {
        // 统计 required 里每种 BuffType 需要几个
        Dictionary<BuffType, int> needed = new Dictionary<BuffType, int>();
        foreach (BuffType type in required)
        {
            if (needed.ContainsKey(type))
                needed[type]++;
            else
                needed[type] = 1;
        }

        // 逐一比对玩家是否拥有足够数量
        foreach (KeyValuePair<BuffType, int> requirement in needed)
        {
            if (!buffCounts.ContainsKey(requirement.Key))
                return false;
            if (buffCounts[requirement.Key] < requirement.Value)
                return false;
        }
        return true;
    }
}