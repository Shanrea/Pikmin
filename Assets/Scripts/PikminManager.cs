using UnityEngine;
using System.Collections.Generic;

public class PikminManager : MonoBehaviour
{
    public List<PikminController> pikmins;
    public Transform player;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Clique gauche pour lancer un Pikmin
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                foreach (var pikmin in pikmins)
                {
                    if (!pikmin.IsShoot && !pikmin.IsComingBack)
                    {
                        pikmin.StartShootingCoroutine(hit.point);
                        break; // Lancer un seul Pikmin à la fois
                    }
                }
            }
        }
    }
}
