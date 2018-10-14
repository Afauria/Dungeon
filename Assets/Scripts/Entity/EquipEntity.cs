using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EquipEntity.asset", menuName = "MyEntity/EquipEntity")]
public class EquipEntity : ScriptableObject
{
    public EquipType equipType;
    //装备名称
    public string equipName;
    public string typeName;
    public string description;
    public Sprite equipIcon;
    //买入价格
    [HideInInspector]
    public float highValue { get { return lowerValue * 2; } }
    //卖出价格
    public float lowerValue;
    //攻击距离
    public float distance;
    //装备属性加成，格式：近战物理攻击：10~20
    public string ability;
    //基础属性加成，格式：力量+1，最多有三个，没有的可以不写
    public string baseAbility1;
    public string baseAbility2;
    public string baseAbility3;
}
public enum EquipType
{
    //头盔
    Header,
    //上装
    Upper,
    //下装
    Lower,
    //武器
    Weapon
}

