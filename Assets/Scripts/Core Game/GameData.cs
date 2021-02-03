
public class GameData
{
    public string[] levelNames;
    public int[] levelStars;
    public float[] levelTimes;

    public GameData(int levelCount)
    {
        levelNames = new string[levelCount];
        levelStars = new int[levelCount];
        levelTimes = new float[levelCount];
    }
}
