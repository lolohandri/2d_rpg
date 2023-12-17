using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject _spawner;

    public static GameManager Instance { get; set; }

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }

    private void Update()
    {
        CanFinish();
    }

    public void CanFinish()
    {
        var children = _spawner.GetComponentsInChildren<Transform>();
        if(children.Length == 1)
        {
            var wall = GameObject.FindGameObjectWithTag("FinishWall");
            Destroy(wall);
        }
    }

    public void LoadNextLevel()
    {
        var index = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(index);
    }
}
