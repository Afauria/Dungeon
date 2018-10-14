using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
public class FastItemEntity
{
    public int index;
    public string fastItemName;
    public FastItemType fastItemType;
    [JsonIgnore]
    public Sprite icon;
    public FastItemEntity()
    {

    }
    public FastItemEntity(int index)
    {
        fastItemName = "";
        fastItemType = FastItemType.Null;
        icon = null;
        this.index = index;
    }
}
public enum FastItemType
{
    Null,
    Skill,
    Equip,
    Prop
   
}

