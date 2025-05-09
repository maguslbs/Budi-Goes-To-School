using UnityEngine;
using UnityEngine.SceneManagement;

public class SavingSystem : MonoBehaviour
{
    public static SavingSystem instance;
    private const string SceneKey = "Scene";

    private Vector3 playerPosition;

    void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            playerPosition = Vector3.zero;
        }
    }

    [System.Serializable]
    public struct SerializableVector3
    {
        public float x;
        public float y;
        public float z;

        public SerializableVector3(Vector3 vector3)
        {
            x = vector3.x;
            y = vector3.y;
            z = vector3.z;
        }

        public Vector3 ToVector3()
        {
            return new Vector3(x, y, z);
        }
    }

    public static void SaveVector3(string key, Vector3 vector3)
    {
        SerializableVector3 serializableVector3 = new SerializableVector3(vector3);
        string json = JsonUtility.ToJson(serializableVector3);
        PlayerPrefs.SetString(key, json);
        PlayerPrefs.Save();
    }

    public static Vector3 LoadVector3(string key, Vector3 defaultValue)
    {
        if (PlayerPrefs.HasKey(key))
        {
            string json = PlayerPrefs.GetString(key);
            SerializableVector3 serializableVector3 = JsonUtility.FromJson<SerializableVector3>(json);
            return serializableVector3.ToVector3();
        }
        else
        {
            return defaultValue;
        }
    }


    public void SaveGame()
    {
        Vector3 playerPos = PlayerMovement.instance.transform.position;
        SaveVector3("playerPosition", playerPos);

        // Save current scene name
        PlayerPrefs.SetString(SceneKey, SceneManager.GetActiveScene().name);

        PlayerPrefs.Save();

        Debug.Log("Game Tersimpan");
    }

    public void LoadGame()
    {
        Debug.Log("Game dimuat");

        if (PlayerPrefs.HasKey(SceneKey))
        {
            playerPosition = LoadVector3("playerPosition", Vector3.zero);

            // Load saved scene
            string sceneName = PlayerPrefs.GetString(SceneKey);
            AudioManager.Instance.PlayMusic("LevelBGM");
            SceneManager.LoadScene(sceneName);

            // Once the scene is loaded, place the player in the saved position
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Debug.LogWarning("No saved game found!");
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Unsubscribe from the event
        if (scene.buildIndex != 0 && playerPosition != Vector3.zero)
        {
            PlayerMovement.instance.transform.position = playerPosition;
        }
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void StartButton()
    {
        PlayerPrefs.DeleteKey("playerPosition");
        PlayerPrefs.DeleteKey(SceneKey);

        PlayerPrefs.Save();
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayMusic("LevelBGM");
        }
        Debug.Log("Game diulang");
    }
}
