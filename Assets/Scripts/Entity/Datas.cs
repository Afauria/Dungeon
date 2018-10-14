using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PropsData.asset", menuName = "MyEntity/PropsData")]
public class PropsData : ScriptableObject
{
    public PropEntity[] medicines;
    public PropEntity[] materials;
    public PropEntity FindProp(string propName)
    {
        foreach(PropEntity prop in medicines)
        {
            if (prop.propName.Equals(propName))
                return prop;
        }
        foreach (PropEntity prop in materials)
        {
            if (prop.propName.Equals(propName))
                return prop;
        }
        return null;
    }
}
[CreateAssetMenu(fileName = "EquipsData.asset", menuName = "MyEntity/EquipsData")]
public class EquipsData : ScriptableObject
{
    public EquipEntity[] headers;
    public EquipEntity[] uppers;
    public EquipEntity[] lowers;
    public EquipEntity[] weapons;
    public EquipEntity FindEquip(string equipName)
    {
        foreach (EquipEntity equip in headers)
        {
            if (equip.equipName.Equals(equipName))
                return equip;
        }
        foreach (EquipEntity equip in weapons)
        {
            if (equip.equipName.Equals(equipName))
                return equip;
        }
        foreach (EquipEntity equip in uppers)
        {
            if (equip.equipName.Equals(equipName))
                return equip;
        }
        foreach (EquipEntity equip in lowers)
        {
            if (equip.equipName.Equals(equipName))
                return equip;
        }    
        return null;
    }
    public EquipEntity FindEquip(string equipName,EquipType equipType)
    {
        if (EquipType.Header == equipType)
        {
            foreach (EquipEntity equip in headers)
            {
                if (equip.equipName.Equals(equipName))
                    return equip;
            }
        }else if (EquipType.Upper == equipType)
        {
            foreach (EquipEntity equip in uppers)
            {
                if (equip.equipName.Equals(equipName))
                    return equip;
            }
        }
        else if (EquipType.Lower== equipType)
        {
            foreach (EquipEntity equip in lowers)
            {
                if (equip.equipName.Equals(equipName))
                    return equip;
            }
        }else if (EquipType.Weapon == equipType)
        {
            foreach (EquipEntity equip in weapons)
            {
                if (equip.equipName.Equals(equipName))
                    return equip;
            }
        }
        return null;
    }
}
[CreateAssetMenu(fileName = "ModelsData.asset", menuName = "MyEntity/ModelsData")]
public class ModelsData : ScriptableObject
{
    public ModelEntity[] models;
    public ModelEntity FindModel(ModelType modelType)
    {
        foreach (ModelEntity model in models)
        {
            if (model.modelType == modelType)
            {
                return model;
            }
        }
        return null;
    }
}

[CreateAssetMenu(fileName = "SkillsData.asset", menuName = "MyEntity/SkillsData")]
public class SkillsData : ScriptableObject
{
    public SkillEntity[] skillType1;
    public SkillEntity[] skillType2;
    public SkillEntity[] skillType3;
    public SkillEntity[] skillType4;
    public SkillEntity[] skillType5;
    public SkillEntity FindSkill(string skillName)
    {
        foreach (SkillEntity skill in skillType1)
        {
            if (skill.skillName.Equals(skillName))
            {
                return skill;
            }
        }
        foreach (SkillEntity skill in skillType2)
        {
            if (skill.skillName.Equals(skillName))
            {
                return skill;
            }
        }
        foreach (SkillEntity skill in skillType3)
        {
            if (skill.skillName.Equals(skillName))
            {
                return skill;
            }
        }
        foreach (SkillEntity skill in skillType4)
        {
            if (skill.skillName.Equals(skillName))
            {
                return skill;
            }
        }
        foreach (SkillEntity skill in skillType5)
        {
            if (skill.skillName.Equals(skillName))
            {
                return skill;
            }
        }
        return null;
    }
    public SkillEntity[] FindSkills(SkillType skillType)
    {
        SkillEntity[] skills = new SkillEntity[8];
        int i = 0;
        foreach(SkillEntity skill in skillType1)
        {
            if (skill.skillType == skillType)
            {
                skills[i]=skill;
                i++;
            }
        }
        return skills;
    }
}