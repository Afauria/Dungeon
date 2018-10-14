using System.Collections;
using System.Collections.Generic;
public class MyTag
{
    public const string FAST_ICON_TAG = "FastIcon";
    public const string PROP_ICON_TAG = "PropIcon";
    public const string SKILL_ICON_TAG = "SkillIcon";
    public const string FAST_ITEM_PLACE_TAG = "FastItemPlace";
    public const string PROP_ITEM_PLACE_TAG = "PropItemPlace";
    public const string GROUP_HEAD_TAG = "GroupHead";
    public const string GROUP_HEAD_PLACE = "GroupPlace";
}
public class RemoteResult<T>
{
    public int code;
    public string message;
    public T data;
}
public enum SkillType
{
    SkillType1,
    SkillType2,
    SkillType3,
    SkillType4,
    SkillType5
}
public enum ModelType
{
    Model1,
    Model2,
    Model3,
    Model4
}

public enum Gender
{
    M,
    F
}
public class UserEntity
{
    public int userId;
    public string username;
    public string password;
}
[System.Serializable]
public class MyKeyValuePair<T>
{
    public string key;
    public T value;
    public MyKeyValuePair(string key, T value)
    {
        this.key = key;
        this.value = value;
    }
}
public class PlayerEntity
{
    public int playerId;
    public int userId;

    public string playerName;
    public Gender gender;
    public ModelType modelType;

    public long playedTime;
    public float money;
    public float currentExp;
    public float needExp;
    public int level;
    public float curHp;
    public WorldLocation location;
    public List<MyKeyValuePair<int>> baseAbility;
    public List<MyKeyValuePair<float>> ability;
    public List<MyKeyValuePair<int>> propsPack;
    public List<MyKeyValuePair<int>> skills;
    public List<MyKeyValuePair<EquipType>> equiped;
    public List<MyKeyValuePair<int>> tasks;
    public FastItemEntity[] fastBar;
    public List<PlayerEntity> partners;
}
public class NpcEntity
{
    public int npcId;
    public string npcName;
    public WorldLocation worldLocation;
    // public NpcState state;
}
public class AbilityEntity
{
    //血量
    public int HP;
    //远程物理攻击
    public int RATN;
    //近程物理攻击
    public int SATN;
    //魔法攻击力
    public int MGK;
    //物理防御力
    public int DEF;
    //魔法防御力
    public int RES;
    //命中率
    public int HIT;
    //速度
    public int SPEED;
    //闪避率
    public int EVD;
    //行动距离
    public int actionDistance;
    //行动点
    public int actionPoint;
}
public enum BaseAbility
{
    //力量
    strength,
    //智力
    intelligence,
    //精准、命中
    accurate,
    //耐力
    stamina,
    //抗性
    resistance,
    //体质
    physique,
    //意志、精神
    spirit,
    //敏捷
    agile,
    //幸运
    luck
}
public class BaseAbilityEntity
{
    //力量
    public int strength;
    //智力
    public int intelligence;
    //精准、命中
    public int accurate;
    //耐力
    public int stamina;
    //抗性
    public int resistance;
    //体质
    public int physique;
    //意志、精神
    public int spirit;
    //敏捷
    public int agile;
    //幸运
    public int luck;
}

public class WorldLocation
{
    public float posx;
    public float posz;
    public float roty;
}

