# BuffBuilder 开发日志

## 2026-06-27

### 目标

- 搭建主界面 UI
- 学习 Unity UI 工作流程
- 配置 Git 与 GitHub

### 已完成

- 创建 Unity 项目
- 导入 TextMeshPro
- 搭建主界面 UI
- 创建 BuffSlot 预制体
- 初始化 Git 仓库
- 将项目发布到 GitHub

### 遇到的问题

- Unity Hub 下载验证失败
- TMP 中文字体缺失

### 解决方案

- 成功安装 Unity
- 生成自定义 TMP 字体资源

# BuffBuilder 开发日志

---

# Sprint 3 —— 建立 PlayerData 数据层

## 日期

2026-06-28

---

## 本次目标

建立正确的游戏数据流，将游戏状态与 UI 分离。

由原来的：

Buff
→ UI

升级为：

Buff
→ PlayerData（Model）
→ TopBarUI（View）

使 UI 不再保存游戏状态，而是读取 PlayerData 进行显示。

---

## 新增内容

### PlayerData.cs

新增玩家数据模型。

负责保存：

- Gold（金钱）
- Attack（攻击）
- Buffs（已拥有 Buff）

新增：

```csharp
AddBuff(BuffData buff)
```

统一管理玩家 Buff。

---

### TopBarUI.cs

新增 TopBarUI。

职责：

- 显示金币
- 显示攻击
- Refresh(PlayerData)

以后所有顶部 UI 都统一通过这里刷新。

---

## 修改内容

### BuffManager

新增：

- PlayerData 初始化
- ApplyBuff()

Buff 生效流程：

玩家点击 Buff

↓

加入 PlayerData

↓

ApplyBuff()

↓

刷新 TopBar

↓

刷新 PlayerBuffPanel

目前实现：

- Attack Buff
- Income Buff

其它 Buff 保留 TODO。

---

## 当前架构

PlayerData（Model）
        │
        ▼
BuffManager（Gameplay）
        │
        ▼
TopBarUI（View）

BuffData
        │
        ▼
BuffSlotUI（单个 Buff 显示）

---

## 学到的内容

### ① 数据与 UI 分离

真正的游戏数据保存在 PlayerData。

UI 不保存状态。

UI 负责读取 PlayerData。

---

### ② Gameplay 与 UI 解耦

BuffManager 不直接修改 Text。

所有 UI 更新统一由 TopBarUI 完成。

这样以后增加新的 UI 不需要修改玩法逻辑。

---

### ③ PlayerData 是唯一数据源

以后：

- 商店
- 事件
- 战斗
- 奖励

全部修改 PlayerData。

UI 永远读取 PlayerData。

---

## 目前完成的 Gameplay Loop

开始回合

↓

随机生成 3 个 Buff

↓

玩家选择一个

↓

加入 PlayerData

↓

Buff 生效

↓

TopBar 更新

↓

加入 PlayerBuffPanel

↓

等待下一回合

---

## 下一阶段计划（Sprint 4）

目标：

让 Buff 开始真正产生策略。

计划：

- Buff 联动（Synergy）
- 更多 Buff 类型
- 构筑（Build）玩法
- 为后续经济系统做准备

暂不实现：

- ScriptableObject
- Addressables
- 对象池
- 回合 Buff 倒计时

等玩法稳定后再重构。

---

## Git Commit

feat: 建立 PlayerData 数据层，完成 Buff 生效与 TopBar 分层刷新

# Sprint 4 —— Buff 联动系统

## 新增

- SynergyData（ScriptableObject）
- SynergyManager
- ActiveSynergy
- SynergyPanelUI
- SynergySlotUI

## 学到的内容

- ScriptableObject 配置玩法数据
- 运行时状态与配置数据分离
- UI 只读取 PlayerData
- Gameplay 与 View 解耦

## 下一步

Sprint 5：经济系统