using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetsDB : Singleton<AssetsDB>
{
    public SkillsData skillsData;
    public PropsData propsData;
    public EquipsData equipsData;
    public ModelsData modelsData;
    public List<KeyValuePair<string,string>> baseAbilityDes;
    protected override void Awake()
    {
        base.Awake();
        Debug.Log(11111);
        modelsData = new ModelsData();
        modelsData.models = new ModelEntity[4];
        for(int i = 0; i < 4; i++)
        {
            modelsData.models[i] = Resources.Load<ModelEntity>("Data/Entity/Models/ModelEntity0"+(i+1));
        }
        propsData = new PropsData();
        propsData.medicines = new PropEntity[2];
        for (int i = 0; i < 2; i++)
        {
            propsData.medicines[i] = Resources.Load<PropEntity>("Data/Entity/Props/Prop_Medicine_0" + (i + 1));
        }
        equipsData = new EquipsData();
        equipsData.weapons = new EquipEntity[2];
        for (int i = 0; i < 2; i++)
        {
            equipsData.weapons[i] = Resources.Load<EquipEntity>("Data/Entity/Equips/Equip_Weapon_0" + (i + 1));
        }
        equipsData.headers = new EquipEntity[1];
        for (int i = 0; i < equipsData.headers.Length; i++)
        {
            equipsData.headers[i] = Resources.Load<EquipEntity>("Data/Entity/Equips/Equip_Header_0" + (i + 1));
        }
        skillsData = new SkillsData();
        skillsData.skillType1 = new SkillEntity[8];
        for (int i = 0; i < skillsData.skillType1.Length; i++)
        {
            skillsData.skillType1[i] = Resources.Load<SkillEntity>("Data/Entity/Skills/Skill_Type1_0" + (i + 1));
        }
        skillsData.skillType2 = new SkillEntity[8];
        for (int i = 0; i < skillsData.skillType2.Length; i++)
        {
            skillsData.skillType2[i] = Resources.Load<SkillEntity>("Data/Entity/Skills/Skill_Type2_0" + (i + 1));
        }
        skillsData.skillType3 = new SkillEntity[8];
        for (int i = 0; i < skillsData.skillType3.Length; i++)
        {
            skillsData.skillType3[i] = Resources.Load<SkillEntity>("Data/Entity/Skills/Skill_Type3_0" + (i + 1));
        }
        skillsData.skillType4 = new SkillEntity[8];
        for (int i = 0; i < skillsData.skillType4.Length; i++)
        {
            skillsData.skillType4[i] = Resources.Load<SkillEntity>("Data/Entity/Skills/Skill_Type4_0" + (i + 1));
        }
        skillsData.skillType5 = new SkillEntity[8];
        for (int i = 0; i < skillsData.skillType5.Length; i++)
        {
            skillsData.skillType5[i] = Resources.Load<SkillEntity>("Data/Entity/Skills/Skill_Type5_0" + (i + 1));
        }
        Constant.InitDescriptions();
        baseAbilityDes = Constant.baseAbilityDes;
    }
    public Sprite FindFastItem(string itemType, string itemName)
    {
        if (itemType.Equals(FastItemType.Null.ToString())){
            return null;
        }
        else if (itemType.Equals(FastItemType.Equip.ToString()))
        {
            return equipsData.FindEquip(itemName).equipIcon;
        }
        else if (itemType.Equals(FastItemType.Prop.ToString()))
        {
            return propsData.FindProp(itemName).propIcon;
        }
        else if (itemType.Equals(FastItemType.Skill.ToString()))
        {
            return skillsData.FindSkill(itemName).skillIcon;
        }
        return null;
    }
}

