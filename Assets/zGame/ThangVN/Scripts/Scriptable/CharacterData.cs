using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[Serializable]

[CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObjects/CharacterData")]
public class CharacterData : ScriptableObject
{
    public List<Character> listCharacter;
    public int indexLastChar;
    public int indexToPlay;
    public string nameToPlay;
    [Serializable]
    public class Character
    {
        public int index;
        public string name;
        public bool unlocked;
        public int levelSkill;
    }
}
