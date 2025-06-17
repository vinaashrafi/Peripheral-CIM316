using UnityEngine;

public interface IPickupable
{
    void Pickup(Transform handTransform);
    void Drop(Transform handTransform);
}