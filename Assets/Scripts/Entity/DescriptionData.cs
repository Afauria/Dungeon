using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DescriptionData.asset", menuName = "MyEntity/DescriptionData")]
public class DescriptionsData : ScriptableObject
{
    public DescriptionEntity[] ability;
    public DescriptionEntity[] baseAbility;
}
//[System.Serializable]
public class DescriptionEntity
{
    public string desName;
    public string description;
}
