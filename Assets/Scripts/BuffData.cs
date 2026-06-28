using System;

/// <summary>
/// Buff 数据类
/// 仅用于保存 Buff 的数据，不负责任何逻辑。
/// </summary>
[Serializable]
public class BuffData
{
    /// <summary>
    /// Buff 唯一编号
    /// 后续方便查找、保存、读取。
    /// </summary>
    public int Id;

    /// <summary>
    /// Buff 名称（例如：狂战、投资、幸运）
    /// </summary>
    public string Name;

    /// <summary>
    /// Buff 描述（显示在 UI 上）
    /// </summary>
    public string Description;

    /// <summary>
    /// Buff 类型
    /// </summary>
    public BuffType Type;

    /// <summary>
    /// Buff 数值
    /// 例如：
    /// 攻击+2 -> Value = 2
    /// 金币+5 -> Value = 5
    /// </summary>
    public int Value;

    /// <summary>
    /// 持续回合数
    /// -1 表示永久
    /// </summary>
    public int Duration;

    /// <summary>
    /// 创建 Buff
    /// </summary>
    public BuffData(
        int id,
        string name,
        string description,
        BuffType type,
        int value,
        int duration = -1)
    {
        Id = id;
        Name = name;
        Description = description;
        Type = type;
        Value = value;
        Duration = duration;
    }
}