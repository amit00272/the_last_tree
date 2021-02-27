using System.Collections;
using System.Collections.Generic;

using UnityEngine;
 
public class GizmoSphere : MonoBehaviour {
    void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 0.2f);
    }
}

