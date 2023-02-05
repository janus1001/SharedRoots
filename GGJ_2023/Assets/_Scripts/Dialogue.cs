using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue")]
public class Dialogue : ScriptableObject
{
    [System.Serializable]
    public struct Entry
    {
        public string text;
        public string characterName;
        public Sprite characterImage;
    }

    public Entry[] dialogueEntries;

    /// <summary>
    /// If populated, game will change to this scene (name) after finishing the dialogue.
    /// </summary>
    public string navigateToScene;
}