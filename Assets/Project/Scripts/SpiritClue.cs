using UnityEngine;

public class SpiritClue : MonoBehaviour
{
    [Header("Objekte des Hinweises")]

    // Alles, was sichtbar sein soll.
    [SerializeField] private GameObject visual;

    // Der Trigger-/Collider-Bereich des Hinweises.
    [SerializeField] private GameObject clueCollider;
    

    private void OnEnable()
    {
        // Dieses Script hört auf die Nachricht des WorldManagers.
        WorldManager.OnWorldChanged += HandleWorldChanged;
    }


    private void OnDisable()
    {
        // Wichtig: Beim Deaktivieren wieder abmelden.
        // Sonst könnte Unity später versuchen, ein nicht mehr aktives Objekt anzusprechen.
        WorldManager.OnWorldChanged -= HandleWorldChanged;
    }


    private void Start()
    {
        // Beim Start ist der Hinweis erstmal unsichtbar.
        // Später können wir den aktuellen Weltzustand noch sauberer abfragen.
        SetSpiritWorldActive(false);
    }


    private void HandleWorldChanged(bool isSpiritWorld)
    {
        // Der bool kommt direkt vom WorldManager:
        // true  = Geisterwelt
        // false = Normalwelt

        SetSpiritWorldActive(isSpiritWorld);
    }


    public void SetSpiritWorldActive(bool isActive)
    {
        // Sichtbares Objekt ein- oder ausschalten.
        visual.SetActive(isActive);

        // Collider ebenfalls ein- oder ausschalten.
        clueCollider.SetActive(isActive);
    }
}