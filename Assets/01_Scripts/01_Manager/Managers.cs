using UnityEngine;

public class Managers : MonoBehaviour
{
    private static Managers _intance;
    public static Managers Instance => _intance;

    public UIManager UI => UIManager.Instance;
    public GameManager Game => GameManager.Instance;

    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private BuffEffects _buffEffects;
    public BuffEffects BuffEffects => _buffEffects;

    private void Awake()
    {
        if (_intance != null && _intance != this)
        {
            Destroy(gameObject);
            return;
        }
        _intance = this;
        DontDestroyOnLoad(gameObject);

        Init();
    }

    private void OnDestroy()
    {
        UI.OnDestroy();
    }

    private void Init()
    {
        // Game
        GameObject playerObj = Instantiate(_playerPrefab, Vector3.zero, Quaternion.identity);
        Player player = playerObj.GetComponent<Player>();
        if (player == null) Logger.Warning("player is null");
        Game.Init(player);

        // UI
        UI.Init(GetComponentInChildren<PlayerConditionUI>(),
                GetComponentInChildren<PromptUI>(),
                GetComponentInChildren<InventoryUI>());
    }
}
