// SynergyData.cs
// 定义一条 Buff 联动规则（数据驱动，挂在 ScriptableObject 上）
// 策划只需要创建 SO 资产，不需要改代码

using UnityEngine;

[CreateAssetMenu(fileName = "NewSynergy", menuName = "BuffBuilder/SynergyData")]
public class SynergyData : ScriptableObject
{
    [Header("联动基本信息")]
    [SerializeField] private string synergyName;        // 联动名称，例如"战士之魂"
    [SerializeField] private string description;        // 联动描述，显示在 UI 上
    
    [Header("激活条件")]
    [SerializeField] private BuffType[] requiredBuffTypes; // 需要同时拥有这些 BuffType
    
    [Header("奖励效果")]
    [SerializeField] private BuffType bonusType;    // 奖励加成的类型（Attack / Income...）
    [SerializeField] private int bonusValue;        // 奖励数值

    // 只读属性，外部只能读，不能改
    public string SynergyName => synergyName;
    public string Description => description;
    public BuffType[] RequiredBuffTypes => requiredBuffTypes;
    public BuffType BonusType => bonusType;
    public int BonusValue => bonusValue;
}