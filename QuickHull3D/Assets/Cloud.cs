using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    private List<Vector3> cloud = new List<Vector3>();
    List<GameObject> vertexes = new List<GameObject>();
    public int nrofvertices;
    public GameObject gobj;
    public Material material;
    private List<Vector3> vertices = new List<Vector3>();
    private List<int> triangles = new List<int>();
    private List<int[]> trianglesa = new List<int[]>();
    private Vector3 normal,test;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < nrofvertices; i++)
        {
            GameObject tempobj = new GameObject("vertexes nr " + (i+1));

            Vector3 vec = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            tempobj.transform.position = vec;
            cloud.Add(vec);
            
        }

        for (int i = 0; i < 4; i++)
        {
            vertices.Add(cloud.ToArray()[i]);
            
        }
        Vector3 center = GetAverageList(vertices);
        Vector3 normal;
        normal = Vector3.Cross(vertices.ToArray()[0] - vertices.ToArray()[1], vertices.ToArray()[0] - vertices.ToArray()[2]);
        if (Vector3.Dot(center-GetAverage(vertices.ToArray()[0],vertices.ToArray()[1], vertices.ToArray()[2]),normal)  < 0)
        {
            trianglesa.Add(new int[] { 0, 1, 2 });
        }
        else
        {
            trianglesa.Add(new int[] { 2, 1, 0 });
        }
        normal = Vector3.Cross(vertices.ToArray()[1] - vertices.ToArray()[3], vertices.ToArray()[2] - vertices.ToArray()[3]);
        if (Vector3.Dot(center - GetAverage(vertices.ToArray()[1], vertices.ToArray()[2], vertices.ToArray()[3]), normal) < 0)
        {
            trianglesa.Add(new int[] { 1, 2, 3 });
        }
        else
        {
            trianglesa.Add(new int[] { 3, 2, 1 });
        }
        normal = Vector3.Cross(vertices.ToArray()[1] - vertices.ToArray()[0], vertices.ToArray()[1] - vertices.ToArray()[3]);
        if (Vector3.Dot(center - GetAverage(vertices.ToArray()[1], vertices.ToArray()[0], vertices.ToArray()[3]), normal) < 0)
        {
            trianglesa.Add(new int[] { 1, 0, 3 });
        }
        else
        {
            trianglesa.Add(new int[] { 3, 0, 1 });
        }
        normal = Vector3.Cross(vertices.ToArray()[2] - vertices.ToArray()[0], vertices.ToArray()[2] - vertices.ToArray()[3]);
        if (Vector3.Dot(center - GetAverage(vertices.ToArray()[2], vertices.ToArray()[0], vertices.ToArray()[3]), normal) < 0)
        {
            trianglesa.Add(new int[] { 2, 0, 3 });
        }
        else
        {
            trianglesa.Add(new int[] { 2, 0, 3 });
        }
        //trianglesa.Add(new int[] { 1, 2, 3 });
        //trianglesa.Add(new int[] { 3, 2, 1 });

        //trianglesa.Add(new int[] { 0, 1, 2 });
        //trianglesa.Add(new int[] { 2, 1, 0 });

        //trianglesa.Add(new int[] { 1, 0, 3 });
        //trianglesa.Add(new int[] { 3, 0, 1 });

        //trianglesa.Add(new int[] { 2, 0, 3 });
        //trianglesa.Add(new int[] { 3, 0, 2 });


        Mesh mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = GetTriangles(trianglesa).ToArray();
        //mesh.triangles = new int[] { 0, 1, 2,0,2,1,1,2,3,0,2,3,3,2,1,3,0,1,2,0,3,2,1,3,3,2,0 };


        gobj.AddComponent<MeshFilter>();
        gobj.GetComponent<MeshFilter>().mesh = mesh;

        gobj.AddComponent<MeshRenderer>();
        gobj.GetComponent<MeshRenderer>().material = material;



    }

    private Vector3 GetAverageList(List<Vector3> _list)
    {
        Vector3 output;
        float xavg = 0, yavg = 0, zavg = 0;
        foreach (Vector3 v in _list)
        {
            xavg += v.x;
            yavg += v.y;
            zavg += v.z;
        }
        output = new Vector3(xavg / _list.Count, yavg / _list.Count, zavg / _list.Count);



        return output;

    }

    private Vector3 GetAverage(Vector3 a, Vector3 b, Vector3 c)

    {
        Vector3 output;
        List<Vector3> temp = new List<Vector3>();

        temp.Add(a);
        temp.Add(b);
        temp.Add(c);
        float xavg = 0, yavg = 0, zavg = 0;
        foreach (Vector3 v in temp)
        {
            xavg += v.x;
            yavg += v.y;
            zavg += v.z;
        }
         output = new Vector3(xavg / 3,yavg / 3, zavg / 3);
      

        return output;
    }
    private List<int> GetTriangles(List<int[]> input)
    {
        List<int> output=new List<int>();

        foreach(int[] i in input)
        {
            output.Add(i[0]);
            output.Add(i[1]);
            output.Add(i[2]);
            
        }

        return output;
    }


    private void iterate()
    {







    }


    private void OnDrawGizmos()
    {
        if (cloud != null)
        {
            Gizmos.color = Color.red;
            Vector3 size = new Vector3(0.05f, 0.05f, 0.05f);
            Vector3 center;
            center = GetAverageList(vertices);
            Gizmos.DrawCube(center, 3*size);
            foreach (Vector3 v in cloud)
            {
                Gizmos.DrawCube(v, size);
                
            }
            Gizmos.color = Color.yellow;
            //Gizmos.DrawLine(test, normal);
            //Gizmos.DrawCube(normal, size);
            //Gizmos.DrawCube(test, size);

        foreach(int[] nr in trianglesa)
            {
                normal = Vector3.Cross(cloud[nr[0]]- cloud[nr[1]], cloud[nr[0]]- cloud[nr[2]]);

                Vector3 lol = center - GetAverage(cloud[nr[0]], cloud[nr[1]], cloud[nr[2]]);
                if (Vector3.Dot(lol,normal)<0)
                { 
                Gizmos.DrawCube(GetAverage(cloud[nr[0]], cloud[nr[1]], cloud[nr[2]]), size);
                Gizmos.DrawLine(GetAverage(cloud[nr[0]], cloud[nr[1]], cloud[nr[2]]), GetAverage(cloud[nr[0]], cloud[nr[1]], cloud[nr[2]])+ normal);
                }
            }



        }
    }

    

    //{
    //    if(cloud!=null)
    //    { 
    //    Vector3 a = cloud.ToArray()[0];
    //    Vector3 b = cloud.ToArray()[1];
    //    Vector3 c = cloud.ToArray()[2];
    //    Vector3 d = cloud.ToArray()[3];
    //    Vector3 size = new Vector3(0.01f, 0.01f, 0.01f);
    //    Gizmos.color = Color.black;

    //        foreach(Vector3 v in cloud)
    //        {
    //            Gizmos.DrawCube(v, size);


    //        }

    //    Gizmos.DrawLine(a, b);
    //    Gizmos.DrawLine(a, c);
    //    Gizmos.DrawLine(a, d);

    //    Gizmos.DrawLine(b, c);
    //    Gizmos.DrawLine(b, d);

    //    Gizmos.DrawLine(c, d);





    //     }

    //}

    // Update is called once per frame
    void Update()
    {
        
    }




    //public bool IsPointInside(this Mesh aMesh, Vector3 aLocalPoint)
    //{
    //    var verts = aMesh.vertices;
    //    var tris = aMesh.triangles;
    //    int triangleCount = tris.Length / 3;
    //    for (int i = 0; i < triangleCount; i++)
    //    {
    //        var V1 = verts[tris[i * 3]];
    //        var V2 = verts[tris[i * 3 + 1]];
    //        var V3 = verts[tris[i * 3 + 2]];
    //        var P = new Plane(V1, V2, V3);
    //        if (P.GetSide(aLocalPoint))
    //            return false;
    //    }
    //    return true;
    //}
}
