using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class MVC
{
    //存储MVC
    //模型名字与模型之间的
    public static Dictionary<string,Model> Models = new Dictionary<string,Model>();
    //名字与视图
    public static Dictionary<string,View> Views = new Dictionary<string, View>();
    //事件名字与控制器类型的(事件映射)(动态创建控制器)
    public static Dictionary<string,Type> CommandMap = new Dictionary<string, Type>(); 



    //注册
    public static void RegisterModel(Model model)
    {
        Models[model.Name] = model;
    }

    public static void RegisterView(View view)
    {
        //防止重复注册
        if (Views.ContainsKey(view.Name))
        {
            Views.Remove(view.Name);
        }
        //注册关心的事件
        view.RegisterEvents();
        //缓存
        Views[view.Name] = view;
    }

    public static void RegisterController(string eventName, Type controllerType)
    {
        CommandMap[eventName] = controllerType;

    }
    //获取
    public static T GetModel<T>() where T : Model
    {
        foreach (Model model in Models.Values)
        {
            if (model is T)
            {
                return (T)model;

            }
        }
        return null;


    }

    public static T GetView<T>() where T : View

    {
        foreach (View view in Views.Values)
        {
            if (view is T)
            {
                return (T)view;

            }
        }
        return null;
    }
    //发送事件
    public static void SendEvent(string eventName, object data = null)
    {
        //控制器响应事件

        if (CommandMap.ContainsKey(eventName))
        {
            Type t = CommandMap[eventName];
            Controller c = Activator.CreateInstance(t) as Controller;
            //控制器和执行
            c.Execute(data);
       
        }
        //视图响应事件
        foreach (View v in Views.Values)
        {
            if (v.AttentionEvents.Contains(eventName))
            {
                //视图响应事件
                v.HandleEvent(eventName,data);
            }
        }
    }
}
