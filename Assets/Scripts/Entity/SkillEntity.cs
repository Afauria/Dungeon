using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillEntity.asset", menuName = "MyEntity/SkillEntity")]
public class SkillEntity : ScriptableObject
{
    public SkillType skillType;
    public string skillName;
    public Sprite skillIcon;
    public int coldTime;
    public int pointCount;
    public string description;
    public SkillEntity preSkill;
    public int damageRate;
    public GameObject effect;
}
