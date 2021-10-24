using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyUnitUI : MonoBehaviour
{
    [SerializeField] private Image stone;

    private Item key;

    public Item Item => key;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(Item _key)
    {
        key = _key;
        stone.color = _key.color;
    }
}
