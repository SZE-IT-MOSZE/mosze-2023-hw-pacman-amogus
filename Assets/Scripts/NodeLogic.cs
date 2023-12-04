using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeLogic : MonoBehaviour
{
    public List<string> avalibleDirections = new List<string>()
    {
        "Up",
        "Down",
        "Left",
        "Right"
    };

    public float triggerRange;

    private void Start()
    {
        for (int i = avalibleDirections.Count - 1; i >= 0; i--)
        {
            RaycastHit hit;
            Vector3 direction = Vector3.zero;

            switch (avalibleDirections[i])
            {
                case "Up":
                    direction = Vector3.forward;
                    break;
                case "Down":
                    direction = Vector3.back;
                    break;
                case "Left":
                    direction = Vector3.left;
                    break;
                case "Right":
                    direction = Vector3.right;
                    break;
                default:
                    break;
            }

            if (Physics.Raycast(transform.position, direction, out hit, triggerRange) && hit.collider.tag != "Node")
            {
                avalibleDirections.Remove(avalibleDirections[i]);
            }
        }

        if (avalibleDirections.Count <= 2)
        {
            Destroy(gameObject);
        }
    }
}
