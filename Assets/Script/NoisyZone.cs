using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class NoisyZone : MonoBehaviour
{
    public float soundMult;
    [SerializeField] private BoxCollider2D zone;
    [SerializeField] private List<AudioClip> walkSounds;
    [SerializeField] private List<AudioClip> runSounds;
    public List<AudioClip> WalkSounds => walkSounds;
    public List<AudioClip> RunSounds => runSounds;

    private void OnValidate()
    {
        if (!zone) TryGetComponent(out zone);
    }

    private void OnDrawGizmos()
    {
        if(!zone) return;
        
        Color newCol = Color.blue;
        newCol.a = 0.1f;
        Gizmos.color = newCol;
        Vector3 position = transform.position;
        Gizmos.DrawCube(position + (Vector3)zone.offset, zone.size);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(position + (Vector3)zone.offset,zone.size);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
