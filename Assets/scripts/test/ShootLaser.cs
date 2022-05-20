using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShootLaser : MonoBehaviour
{
    public static ShootLaser instance;

    public Material material;
    LaserBeam beam;

    public bool destinyFound;
    bool isLaserStopped;


    Vector3 temp, prev1, prev2; // modified
    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        Destroy(GameObject.Find("Laser Beam"));
        beam = new LaserBeam(transform.position, transform.forward, material);
    }
    public void StopLaser(LineRenderer laser, List<Vector3> laserPoints)
    {
        laser.positionCount = laserPoints.Count;
        laser.startColor = Color.green;
        laser.endColor = Color.green;
        for (int i = 0; i < laserPoints.Count; i++)
        {
            laser.SetPosition(i, laserPoints[i]);
        }
        if (!isLaserStopped)
        {
            destinyFound = true;
            //GameManager.instance.holeLid.GetChild(0).DOScaleZ(0, 2f);
            //GameManager.instance.holeLid.GetChild(1).DOScaleZ(0, 2f);
            //EventManager.instance.StartDestinationFoundEvent();
            //SoundManager.instance.PlayClip(SoundManager.instance.destFound);
            isLaserStopped = true;
        }
    }

    
 
    public void Increement(Vector3 lastPoint,Vector3 nextPoint)
    {
        if(prev1!=lastPoint && prev2!=nextPoint)
        {
            temp = Vector3.Lerp(prev1, prev2, 10 * Time.deltaTime);           
        }
        else
        {
            prev1 = lastPoint;
            prev2 = nextPoint;
        }

    }
}
