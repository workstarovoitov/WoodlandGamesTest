using UnityEngine;

public class LevelController : MonoBehaviour
{

    [SerializeField] private int levelsMax;
    public int LevelsMax
    {
        get => levelsMax;
    }


    private int levelCurrent = 0;
    public int LevelCurrent
    {
        get => levelCurrent;
        set => levelCurrent = value;
    }

    public void Start()
    {
        levelCurrent = 0;
    }
}
