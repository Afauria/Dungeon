using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ModelEntity.asset", menuName = "MyEntity/ModelEntity")]
public class ModelEntity : ScriptableObject{
    public ModelType modelType;
    public GameObject model;
    public Sprite headPic;
    public string intro;
    public List<KeyValuePair<string, int>> baseAbility;
}
