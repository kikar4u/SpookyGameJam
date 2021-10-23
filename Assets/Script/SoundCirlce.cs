using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCirlce : MonoBehaviour
{
    [SerializeField] private LineRenderer line;
    [SerializeField] private float radius;
    [SerializeField] private float maxRadius;
    [SerializeField] private float depth;
    [SerializeField] private int segments;
    [SerializeField] private float thickness;
    [Tooltip("Temps que met le cercle à atteindre sa taille maximum")]
    [SerializeField] private float maxGrowTime;
    private float growTime;
    private bool canBeDrawn;


    private void OnValidate()
    {
        if(line && segments > 0) DrawCircle();
    }

    // Start is called before the first frame update
    private void Start()
    {
        radius = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        if (canBeDrawn) GrowCicle();
    }

    private void GrowCicle()
    {
        if (radius >= maxRadius) Destroy(gameObject);
        radius = Mathf.Lerp(0, maxRadius, growTime / maxGrowTime);
        growTime += Time.deltaTime;
        DrawCircle();
    }
    
    public void InitCircle(float maxSize, Color colorCircle)
    {
        Gradient newGradient = new Gradient();
        newGradient.SetKeys(
            new [] { new GradientColorKey(colorCircle, 0.0f), new GradientColorKey(colorCircle,1.0f) },
            new [] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(1.0f, 1.0f) }
            );
        line.colorGradient = newGradient;
        
        maxRadius = maxSize;
        canBeDrawn = true;
        radius = 0;
        growTime = 0;
        
        FireSoundDetection(maxSize);
    }
    
    private void DrawCircle()
    {
        line.positionCount = segments + 2;
        line.widthMultiplier = thickness;

        for (int i = 0; i < line.positionCount; i++)
        {
            float angle = 2 * Mathf.PI / segments * i;

            Vector3 newPos = new Vector3
            {
                x = Mathf.Cos(angle) * radius,
                y = Mathf.Sin(angle) * radius,
                z = depth
            };
            
            line.SetPosition(i,newPos);
        }
    }

    private void FireSoundDetection(float size)
    {
        LayerMask monsterMask = LayerMask.GetMask("Monster");
        size *= 5;
        Collider2D other = Physics2D.OverlapCircle(transform.position, size, monsterMask);
        if (other && other.TryGetComponent(out MonsterBehaviour monster))
        {
            monster.GetPlayerPosition();
        }
    }
}
