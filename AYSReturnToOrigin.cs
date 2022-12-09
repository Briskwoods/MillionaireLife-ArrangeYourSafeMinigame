using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AYSReturnToOrigin : MonoBehaviour
{
    //public Outline self;

    public float originalPositionX;
    public float originalPositionY;
    public float originalPositionZ;

    public bool inPlace = false;

    // Start is called before the first frame update
    void Start()
    {
        originalPositionX = this.gameObject.transform.position.x;
        originalPositionY = this.gameObject.transform.position.y;
        originalPositionZ = this.gameObject.transform.position.z;

        //self = this.gameObject.GetComponent<Outline>();
    }

    [ContextMenu("Test")]
    public void BackToOrigin()
    {
        switch (!inPlace)
        {
            case true:
                //self.enabled = true;
                this.gameObject.transform.DOMove(new Vector3(originalPositionX, originalPositionY, originalPositionZ), 1f);
                break;
            case false:
                break;
        }
    }

}
