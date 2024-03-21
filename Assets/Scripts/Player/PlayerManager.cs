using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Player/MovementSettings", fileName = "PlayerMovementSettings")]
public class PlayerManager : ScriptableObject
{
    public LayerMask terrainLayer;
    public float groundFriction = 0.2f;
    [Header("Jumping")]
    public float jumpHeight = 5f;
    public float jumpCheckDistance = 0.5f;
    public float jumpCheckRadius = 0.5f;
    public float jumpCD = 0.5f;
    [HideInInspector]public float _currentHorizontalSpeed;
    [Header("WALKING")] [SerializeField] public float _acceleration = 90;
    [SerializeField] public float _moveClamp = 13;
    public float _moveClampDragging => _moveClamp / 2;
    [SerializeField] public float _deAcceleration = 60f;
    [SerializeField] public float _apexBonus = 2;

    [Header("SFX")]
    public AudioClip walkSFX;
    public AudioClip jumpSFX;
    public AudioClip landSFX;
    //public AttackAction attackAction;
    //[HideInInspector] public StickToPoint sticker;
    //[HideInInspector] public Rigidbody2D mountedObject;
}
