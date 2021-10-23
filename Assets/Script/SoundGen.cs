using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundGen : MonoBehaviour
{

    [SerializeField] private SoundCirlce soundCircleBase;
    [SerializeField] private Gradient feedBackColors;
    [SerializeField] private float maxColorThreshold;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TestGenSound());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenSound(float soundMult)
    {
        if (Instantiate(soundCircleBase.gameObject, transform.position, new Quaternion())
            .TryGetComponent(out SoundCirlce newSC))
        {
            Color sCColor = feedBackColors.Evaluate(soundMult / maxColorThreshold);
            newSC.InitCircle(soundMult,sCColor);
        }
    }

    private IEnumerator TestGenSound()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            float alea = Random.Range(0.1f, 5);
            GenSound(alea);
            
        }
    }
}
