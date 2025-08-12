using UnityEngine;
using System.Collections;

public interface IInteractable
{
    public string GetInteractPrompt();
    public void OnInteract();
}

public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData data;

    [Header("Respawn")]
    public float respawnDelay = 10f;
    public GameObject visualsRoot;

    Vector3 spawnPos;
    Quaternion spawnRot;

    void Awake()
    {
        if (visualsRoot == null) visualsRoot = gameObject;
        spawnPos = transform.position;
        spawnRot = transform.rotation;
    }

    public string GetInteractPrompt()
    {
        string str = $"{data.displayName}\n{data.description}";
        return str;
    }

    public void OnInteract()
    {
        CharacterManager.Instance.Player.itemData = data;
        CharacterManager.Instance.Player.addItem?.Invoke();

        SetVisible(false);
        Invoke(nameof(Respawn), respawnDelay);
    }
    void Respawn()
    {
        // 원위치/자세로 돌려놓고 다시 보이기
        transform.SetPositionAndRotation(spawnPos, spawnRot);
        var rb = GetComponent<Rigidbody>();
        if (rb) { rb.velocity = Vector3.zero; rb.angularVelocity = Vector3.zero; }

        SetVisible(true);
    }

    void SetVisible(bool on)
    {
        foreach (var r in visualsRoot.GetComponentsInChildren<Renderer>(true)) r.enabled = on;
        foreach (var c in visualsRoot.GetComponentsInChildren<Collider>(true)) c.enabled = on;
    }
}