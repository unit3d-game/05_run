using System.Collections.Generic;
using System;
using UnityEngine;

// 消息走起来
public class PostNotification
{

    // 事件
    private static readonly Dictionary<string, Action<MessagePayload>> actions = new Dictionary<string, Action<MessagePayload>>();


    private static readonly Dictionary<object, List<ActionInfo>> targetActions = new Dictionary<object, List<ActionInfo>>();

    /**
     * <summary>注册一个事件</summary>
     * <param name="name">事件名称</param>
     * <param name="action">注册的事件函数</param>
     */
    public static void Register(string name, Action<MessagePayload> action)
    {
        if (actions.ContainsKey(name))
        {
            actions[name] += action;
        }
        else
        {
            actions.Add(name, action);
        }
        List<ActionInfo> list;
        if (targetActions.ContainsKey(action.Target))
        {
            list = targetActions[action.Target];
        }
        else
        {
            list = new List<ActionInfo>();
        }
        list.Add(new ActionInfo(name, action));
        targetActions[action.Target] = list;
    }


    /**
    * <summary>移除一个事件</summary>
    * <param name="name">事件名称</param>
    * <param name="action">注册的事件函数</param>
    */
    public static void UnRegister(string name, Action<MessagePayload> action)
    {
        // 是否包含此事件
        if (!actions.ContainsKey(name))
        {
            return;
        }
        // 去除事件
        actions[name] -= action;
        // 如果没有其他相关注册事件，则移除
        if (actions[name] == null)
        {
            actions.Remove(name);
        }
        // 移除 target 引用
        List<ActionInfo> list = targetActions[action.Target];
        foreach (ActionInfo info in list)
        {
            if (info.action == action)
            {
                list.Remove(info);
                break;
            }
        }
        // 重置
        targetActions[action.Target] = list;
    }

    /**
     * <summary>取消注册特定对象的所有数据</summary>
     * <param name="target">取消的对象</param>
     */
    public static void UnRegister(object target)
    {
        List<ActionInfo> list = targetActions[target];
        foreach (ActionInfo info in list)
        {
            actions[info.name] -= info.action;
            if (actions[info.name] == null)
            {
                actions.Remove(info.name);
            }
            Debug.Log($"remove name is {info.name}");
        }

        targetActions.Remove(target);
    }

    /**
    * <summary>触发一个事件</summary>
    * <param name="name"> 事件名称 </param>
    * <param name="sender">触发者</param>
    * <param name="data">传递的数据</param>
    */
    public static void Post(string name, object sender, object data)
    {
        if (!actions.ContainsKey(name))
        {
            return;
        }
        // 触发事件
        actions[name](new MessagePayload(name, sender, data));
    }

    /**
    * <summary>触发一个事件</summary>
    * <param name="name"> 事件名称 </param>
    * <param name="sender">触发者</param>
    */
    public static void Post(string name, object sender)
    {
        Post(name, sender, null);
    }

}



public class ActionInfo
{

    public string name;

    public Action<MessagePayload> action;

    public ActionInfo(string name, Action<MessagePayload> action)
    {
        this.name = name;
        this.action = action;
    }
}