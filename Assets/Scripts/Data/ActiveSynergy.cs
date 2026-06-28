// ActiveSynergy.cs
// 运行时状态类，记录一条联动规则是否已被激活
// 不是 MonoBehaviour，只负责存数据

public class ActiveSynergy
{
    // 对应的联动规则（数据来源）
    public SynergyData Data { get; private set; }

    // 当前是否激活
    public bool IsActive { get; private set; }

    // 构造函数：创建时传入规则数据
    public ActiveSynergy(SynergyData data)
    {
        Data = data;
        IsActive = false; // 默认未激活
    }

    // 激活这条联动
    public void Activate()
    {
        IsActive = true;
    }

    // 取消激活（每回合开始前重置）
    public void Deactivate()
    {
        IsActive = false;
    }
}