using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// BuffManager.cs
// 负责玩法流程：生成候选 Buff、响应选择、驱动数据更新
// 不负责 UI 显示，UI 交给 TopBarUI 处理
public class BuffManager : MonoBehaviour
{
    [Header("设置")]
    [SerializeField] private int buffCountPerTurn = 3;

    // 运行时通过代码查找的引用
    private Transform _buffPanel;
    private Transform _playerBuffPanel;
    private GameObject _buffSlotPrefab;

    // 玩家数据层（Model）
    private PlayerData _playerData;

    // TopBarUI 引用，用于刷新顶部显示
    // 通过 Inspector 拖入，不用 Find
    [SerializeField] private TopBarUI topBarUI;

    // 联动管理器引用，通过 Inspector 拖入
    [SerializeField] private SynergyManager synergyManager;

    // 联动面板 UI，Inspector 拖入 SynergyPanel
    [SerializeField] private SynergyPanelUI synergyPanelUI;

    // 所有可用的 Buff 配置表
    private List<BuffData> _allBuffs;

    private void Awake()
    {
        // 初始化玩家数据
        _playerData = new PlayerData();

        // 初始化 Buff 配置表
        _allBuffs = new List<BuffData>
        {
            new BuffData(1, "灼烧",  "每回合造成2点伤害", BuffType.Burn,     2, 3),
            new BuffData(2, "中毒",  "每回合造成1点伤害", BuffType.Poison,   1, 5),
            new BuffData(3, "护盾",  "抵挡一次伤害",      BuffType.Shield,   0, 1),
            new BuffData(4, "强化",  "攻击力+3",          BuffType.Attack,   3, 2),
            new BuffData(5, "暴击",  "暴击率+20%",        BuffType.Critical, 0, 2),
            new BuffData(6, "治愈",  "每回合回复2点血量", BuffType.Heal,     2, 3),
            new BuffData(7, "金币+", "每回合获得5金币",   BuffType.Income,   5, 2),
        };
    }

    private void Start()
    {
        // 用代码查找场景中的引用
        _buffPanel       = GameObject.Find("BuffPanel").transform;
        _playerBuffPanel = GameObject.Find("PlayerBuffPanel").transform;
        _buffSlotPrefab  = Resources.Load<GameObject>("Prefabs/BuffSlot");

        // 绑定按钮事件
        GameObject.Find("StartTurnButton")
            .GetComponent<Button>()
            .onClick.AddListener(OnStartTurn);

        // 初始化 TopBarUI，显示默认数据
        topBarUI.Refresh(_playerData);
    }

    // 开始回合：清空候选区，随机生成新 Buff
    public void OnStartTurn()
    {
        foreach (Transform child in _buffPanel)
            Destroy(child.gameObject);

        List<BuffData> selected = GetRandomBuffs(buffCountPerTurn);
        foreach (BuffData data in selected)
        {
            GameObject slot = Instantiate(_buffSlotPrefab, _buffPanel);
            slot.GetComponent<BuffSlotUI>().Setup(data, OnBuffSelected);
        }
    }

    // 玩家选择 Buff 后的完整流程
    private void OnBuffSelected(BuffData data)
    {
        // 1. 加入玩家数据
        _playerData.AddBuff(data);

        // 2. 应用 Buff 效果到数据层
        ApplyBuff(data);

        // 3. 判断联动，结果写入 PlayerData
        synergyManager.EvaluateSynergies(_playerData);

        // 4. 刷新联动面板 UI
        synergyPanelUI.Refresh(_playerData);

        // 5. 刷新 TopBar UI
        topBarUI.Refresh(_playerData);

        // 6. 在 PlayerBuffPanel 显示
        GameObject slot = Instantiate(_buffSlotPrefab, _playerBuffPanel);
        slot.GetComponent<BuffSlotUI>().Setup(data, null);

        // 7. 清空候选区
        foreach (Transform child in _buffPanel)
            Destroy(child.gameObject);

        Debug.Log($"已选择：{data.Name} | 金币：{_playerData.Gold} | 攻击：{_playerData.Attack}");
    }

    // 应用 Buff 效果到 PlayerData，不触碰任何 UI
    private void ApplyBuff(BuffData buff)
    {
        switch (buff.Type)
        {
            case BuffType.Attack:
                // 强化：增加攻击力
                _playerData.Attack += buff.Value;
                break;

            case BuffType.Income:
                // 金币+：增加金币
                _playerData.Gold += buff.Value;
                break;

            // TODO: 实现灼烧效果（每回合扣血）
            case BuffType.Burn:
                break;

            // TODO: 实现中毒效果
            case BuffType.Poison:
                break;

            // TODO: 实现护盾效果
            case BuffType.Shield:
                break;

            // TODO: 实现暴击效果
            case BuffType.Critical:
                break;

            // TODO: 实现治愈效果
            case BuffType.Heal:
                break;
        }
    }

    // 随机抽取不重复的 Buff
    private List<BuffData> GetRandomBuffs(int count)
    {
        List<BuffData> pool = new List<BuffData>(_allBuffs);
        List<BuffData> result = new List<BuffData>();
        count = Mathf.Min(count, pool.Count);

        for (int i = 0; i < count; i++)
        {
            int index = Random.Range(0, pool.Count);
            result.Add(pool[index]);
            pool.RemoveAt(index);
        }
        return result;
    }
}