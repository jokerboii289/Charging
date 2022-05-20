using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam
{
    Vector3 pos, dir;

    GameObject laserObj;
    LineRenderer laser;
    List<Vector3> laserIndices = new List<Vector3>();

    public LaserBeam(Vector3 pos, Vector3 dir, Material material)
    {
        this.laser = new LineRenderer();
        this.laserObj = new GameObject();
        this.laserObj.name = "Laser Beam";
        this.pos = pos;
        this.dir = dir;

        this.laser = this.laserObj.AddComponent(typeof(LineRenderer)) as LineRenderer;
        this.laser.startWidth = 0.2f;
        this.laser.endWidth = 0.2f;
        this.laser.material = material;
        this.laser.startColor = Color.red;
        this.laser.endColor = Color.red;

        CastRay(pos, dir, laser);
    }

    void CastRay(Vector3 pos, Vector3 dir, LineRenderer laser)
    {
        laserIndices.Add(pos);
        
        Ray ray = new Ray(pos, dir);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 10, 1))
        {
            CheckHit(hit, dir, laser);
        }else{
            laserIndices.Add(ray.GetPoint(10)); //if it doesnt hit then laser goes to 10 unit in world space
      
            UpdateLaser();
        }
    }

    void UpdateLaser()
    {
        int count = 0; 
        laser.positionCount = laserIndices.Count;
        
        foreach(Vector3 idx in laserIndices) 
        {
            laser.SetPosition(count, idx);
            count++;
        }
    }

   // DragScript dragScript;
    void CheckHit(RaycastHit hitInfo, Vector3 direction, LineRenderer laser)
    {
        if(hitInfo.collider.gameObject.tag == "reflect")
        {
            Vector3 pos = hitInfo.point;
            Vector3 dir = Vector3.Reflect(direction, hitInfo.normal);
           
            CastRay(pos, dir, laser);
        }
        else if(hitInfo.collider.gameObject.tag == "Player" || hitInfo.collider.gameObject.tag == "childBall")
        {
            //Debug.Log("ok");
            return;
        }
        else if(hitInfo.collider.gameObject.tag == "Phone")
        {
            //Debug.Log("ok");
            laserIndices.Add(hitInfo.point);
            GameObject.FindObjectOfType<ShootLaser>().StopLaser(laser, laserIndices);
        }
        else
        {
            //LaserFlow(hitInfo.point, laserIndices[laserIndices.Count - 1]);
            
            laserIndices.Add(hitInfo.point);
            UpdateLaser();
        }
    }

    //void LaserFlow(Vector3 point)
    //{
    //    var temp = Vector3.Lerp(laserIndices[laserIndices.Count - 1], point, 10 * Time.deltaTime);
    //    laserIndices.Add(temp);
    //}

}
