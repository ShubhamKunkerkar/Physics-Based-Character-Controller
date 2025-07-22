using System.Collections;
using UnityEngine;

public class HitTest : MonoBehaviour
{
    [SerializeField]float Collision_Effect_Time = 0.1f;
    MeshRenderer mr;
    Color OGcolor;

    void Awake()
    {
        mr = GetComponent<MeshRenderer>();
        OGcolor = mr.material.color;
    }
    private void OnCollisionEnter(Collision other) {
        StartCoroutine(HitEffect(mr.material));
    }
  
    private IEnumerator HitEffect(Material mat){
        mat.color = Color.white;
        yield return new WaitForSeconds(Collision_Effect_Time);
        mat.color = OGcolor;
    }
    
}
