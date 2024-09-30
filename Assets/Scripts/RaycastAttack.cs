using Fusion;
using UnityEngine;

public class RaycastAttack : NetworkBehaviour
{
    public float Damage = 10;

    public PlayerMovement PlayerMovement;

    void Update()
    {
        if (!HasStateAuthority) return;

        Ray ray = PlayerMovement.mainCamera.ScreenPointToRay(Input.mousePosition);
        ray.origin += PlayerMovement.mainCamera.transform.forward;

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Debug.DrawRay(ray.origin, ray.direction, Color.red, 1f);
            if (Physics.Raycast(ray.origin,ray.direction, out var hit))
            {
                if (hit.transform.TryGetComponent<PlayerHealth>(out var health))
                {
                    health.DealDamageRpc(Damage);
                }
            }
        }
        
        
    }
}