using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HorizontalWallScript : MonoBehaviour
{
    // Start is called before the first frame update

    public float amp;
    public float freq;
    Vector3 initPos;

    private void Start()
    {
        initPos = transform.position;
    }
    private void Update()
    {
        transform.position = new Vector3(Mathf.Sin(Time.time * freq) * amp + initPos.x,initPos.y , 0);
    }
}