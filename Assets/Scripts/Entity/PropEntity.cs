using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PropEntity.asset", menuName = "MyEntity/PropEntity")]
public class PropEntity : ScriptableObject
{
    //类型
    public PropType type;
    //道具名称
    public string propName;
    //类型名称，对应中文
    public string typeName;
    //道具介绍
    public string description;
    //道具图标
    public Sprite propIcon;
    //买入价格
    [HideInInspector]
    public float highValue { get { return lowerValue * 2; } }
    //卖出价格
    public float lowerValue;

}
public enum PropType{
    Medicine,
    Task,
    Equipment,
    Material,
    Other
}