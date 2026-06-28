using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System; // Action 需要这个

// BuffSlotUI.cs
// 挂在 BuffSlot Prefab 上
// 负责显示数据，以及响应点击事件
public class BuffSlotUI : MonoBehaviour
{
    [Header("UI 引用")]
    public TextMeshProUGUI nameText;

    // 存储这个 Slot 对应的数据
    private BuffData _data;

    // 点击回调，外部（BuffManager）注册这个事件来响应点击
    // 用 Action 而不是直接引用 BuffManager，保持解耦
    public Action<BuffData> OnClicked;

    // 初始化时调用，传入数据并注册按钮事件
    public void Setup(BuffData data, Action<BuffData> onClicked)
    {
        _data = data;
        OnClicked = onClicked;

        // 显示 Buff 名称
        nameText.text = data.Name;

        // 获取 Button 组件并监听点击
        // BuffSlot 根节点需要挂一个 Button 组件（下一步操作）
        Button btn = GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.AddListener(HandleClick);
        }
    }

    // 点击时触发
    private void HandleClick()
    {
        // 通知外部，把自己的数据传出去
        OnClicked?.Invoke(_data);
    }
}