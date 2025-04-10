using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Movement Settings
    [Header("Movement Configuration")]
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _minX = -5f;
    [SerializeField] private float _maxX = 5f;
    #endregion

    private void Update()
    {
        HandleHorizontalMovement();
    }

    /// <summary>
    /// Handles horizontal input and movement constraints
    /// </summary>
    private void HandleHorizontalMovement()
    {
        float horizontalInput = GetHorizontalInput();
        Vector3 newPosition = CalculateNewPosition(horizontalInput);
        UpdatePosition(newPosition);
    }

    /// <summary>
    /// Gets normalized horizontal input
    /// </summary>
    private float GetHorizontalInput()
    {
        return Input.GetAxis("Horizontal");
    }

    /// <summary>
    /// Calculates clamped position based on input
    /// </summary>
    private Vector3 CalculateNewPosition(float horizontalInput)
    {
        Vector3 position = transform.position;
        position.x += horizontalInput * _moveSpeed * Time.deltaTime;
        position.x = Mathf.Clamp(position.x, _minX, _maxX);
        return position;
    }

    /// <summary>
    /// Applies the final calculated position
    /// </summary>
    private void UpdatePosition(Vector3 newPosition)
    {
        transform.position = newPosition;
    }
}