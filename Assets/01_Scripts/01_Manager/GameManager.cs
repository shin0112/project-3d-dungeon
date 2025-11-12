public class GameManager
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null) _instance = new();
            return _instance;
        }
    }

    private GameManager() { }

    private Player _player;
    public Player Player => _player;

    public void Init(Player player)
    {
        _player = player;
    }
}
