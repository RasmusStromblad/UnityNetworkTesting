using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviourPun
{
    [SerializeField] private float projectileSpeed = 5f;
    [SerializeField] private GameObject projectile = null;
    [SerializeField] private Transform spawnPoint = null;

    private void Update()
    {
        if (photonView.IsMine)
        {
            TakeInput();
        }
    }

    public void TakeInput()
    {
        // If the user did not press the mouse button
        if (!Input.GetMouseButtonDown(0))
        {
            return;
        }

        photonView.RPC("FireProjectile", RpcTarget.All);
    }

    // Everything inside here runs on all clients
    [PunRPC]
    public void FireProjectile()
    {
        var projectileInstance = Instantiate(projectile, spawnPoint.position, spawnPoint.rotation);

        projectileInstance.GetComponent<Rigidbody>().velocity = projectileInstance.transform.forward * projectileSpeed;
    }
}
