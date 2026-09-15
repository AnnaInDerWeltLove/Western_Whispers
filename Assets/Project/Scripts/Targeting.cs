using UnityEngine;

public class Targeting : MonoBehaviour
{ 
    [SerializeField] private Camera aimCamera;
    [SerializeField] private float maxAimDistance = 100f;
    [SerializeField] private LayerMask aimLayerMask = ~0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (aimCamera == null)
        {
            Debug.LogError("No Aim Camera");
        }
        
    }

    /// <summary>
    /// Gibt den Punkt zurück, auf den gerade gezielt wird (Bildschirmmitte -> Raycast).
    /// Nützlich für Waffen, Fadenkreuz-Ausrichtung, Zielmarkierung etc.
    /// </summary>
    public bool TryGetAimPoint(out RaycastHit hit)
    {
        Ray ray = aimCamera.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out hit, maxAimDistance, aimLayerMask);
    }

   /* public Vector3 GetAimDirection()
    {
        return aimCamera.transform.forward;
    }*/
}
