using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Level/LevelData", order = 1)]
public class Level : ScriptableObject
{
    public string sceneName;

    public void Load()
    {
        GameManager.instance.LoadLevel(sceneName);
    }
}