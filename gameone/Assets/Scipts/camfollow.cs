using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _player1;
    [SerializeField] private Transform _player2;
    [SerializeField] private float _height = 4f;
    [SerializeField] private float _baseDistance = 2f;
    [SerializeField] private float _zoomFactor = 1.5f;
    [SerializeField] private float _minDistance = 5f;
    [SerializeField] private float _maxDistance = 15f;
    [SerializeField] private float _smoothTime = 0.3f;

    private Vector3 _velocity = Vector3.zero;

    private void LateUpdate()
    {
        Vector3 midpoint = CalculateMidpoint();
        float desiredDistance = CalculateDesiredDistance();
        Vector3 desiredPosition = CalculateCameraPosition(midpoint, desiredDistance);

        SmoothCameraMovement(desiredPosition);
        UpdateCameraLookAt(midpoint);
    }

    /// <summary>
    /// Calculates midpoint between two players
    /// </summary>
    private Vector3 CalculateMidpoint()
    {
        return (_player1.position + _player2.position) * 0.5f;
    }

    /// <summary>
    /// Calculates desired camera distance based on player separation
    /// </summary>
    private float CalculateDesiredDistance()
    {
        float playersDistance = Vector3.Distance(_player1.position, _player2.position);
        return Mathf.Clamp(_baseDistance + playersDistance * _zoomFactor, _minDistance, _maxDistance);
    }

    /// <summary>
    /// Calculates target camera position based on midpoint and desired distance
    /// </summary>
    private Vector3 CalculateCameraPosition(Vector3 midpoint, float distance)
    {
        return midpoint +
               Vector3.up * _height +
               Vector3.back * distance;
    }

    /// <summary>
    /// Smoothly moves camera to target position using damping
    /// </summary>
    private void SmoothCameraMovement(Vector3 targetPosition)
    {
        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref _velocity,
            _smoothTime
        );
    }

    /// <summary>
    /// Updates camera rotation to focus on midpoint
    /// </summary>
    private void UpdateCameraLookAt(Vector3 targetPoint)
    {
        transform.LookAt(targetPoint);
    }
}