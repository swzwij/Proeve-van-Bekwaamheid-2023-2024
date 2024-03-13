using UnityEngine;

public class PlayerWrapper : MonoBehaviour
{
    private Rigidbody[] _rigidbodies;
    private Collider[] _colliders;
    public Rigidbody[] Rigidbodies => _rigidbodies;
    public Collider[] Colliders => _colliders;

    private void Awake()
    {
        _rigidbodies = GetComponentsInChildren<Rigidbody>();
        _colliders = GetComponentsInChildren<Collider>();
    }

    /// <summary>
    /// Sets the kinematic state of all rigidbodies in the player wrapper.
    /// </summary>
    /// <param name="isKinematic">The kinematic state to set.</param>
    public void SetKinematic(bool isKinematic)
    {
        foreach (Rigidbody rigidbody in _rigidbodies)
            rigidbody.isKinematic = isKinematic;
    }

    /// <summary>
    /// Sets the active state of all colliders attached to the player.
    /// </summary>
    /// <param name="isActive">The desired active state of the colliders.</param>
    public void SetCollidersActive(bool isActive)
    {
        foreach (Collider col in _colliders)
            col.enabled = isActive;
    }
}