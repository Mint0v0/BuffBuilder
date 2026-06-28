using System.Collections.Generic;

// PlayerData.cs
// 玩家的核心数据层（Model）
// 纯数据类，不继承 MonoBehaviour，不负责任何逻辑和显示
// UI 永远从这里读数据，不直接修改 Text
public class PlayerData
{
    // 玩家金币，默认 999
    public int Gold { get; set; } = 999;

    // 玩家攻击力，默认 15
    public int Attack { get; set; } = 15;

    // 玩家已拥有的 Buff 列表
    public List<BuffData> Buffs { get; private set; } = new List<BuffData>();

    // 添加一个 Buff 到列表
    public void AddBuff(BuffData buff)
    {
        Buffs.Add(buff);
    }

    // 当前激活的联动列表（每回合由 SynergyManager 写入）
    public List<ActiveSynergy> ActiveSynergies { get; private set; } = new List<ActiveSynergy>();

    // 设置联动列表（由 SynergyManager 调用）
    public void SetActiveSynergies(List<ActiveSynergy> synergies)
    {
        ActiveSynergies = synergies;
    }

    // 获取所有已激活的联动（UI 读取用）
    public List<ActiveSynergy> GetActiveSynergies()
    {
        return ActiveSynergies;
    }
}