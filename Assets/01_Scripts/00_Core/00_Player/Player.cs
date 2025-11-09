using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerController _controller;
    private PlayerCondition _condition;

    private void Awake()
    {
        _controller = GetComponent<PlayerController>();
        _condition = GetComponent<PlayerCondition>();
    }
}
