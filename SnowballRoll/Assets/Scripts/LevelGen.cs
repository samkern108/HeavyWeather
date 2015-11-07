using UnityEngine;
using System.Collections;

public class LevelGen : MonoBehaviour {
	
	private int baseline = -100;
	private Vector3[] vertices;
	private int[] triangles;
	
	private Mesh mesh;
	
	private Vector3[] LevelPoints(Rect r, int points)
	{
		Debug.Log (points);
		float iter = (r.xMax - r.xMin) / points;
		
		Vector3[] triangulation = new Vector3[points*2];
		
		for (int i = 0; i < points*2; i+=2) {
			triangulation[i] = new Vector3(r.xMin + i*iter, r.yMax,5);
			triangulation[i+1] = new Vector3(r.xMin + i*iter, r.yMin,5);
		}
		
		return triangulation;
	}
	
	private int[] constructTrianglesFromVertices(Vector3[] vertices)
	{
		int[] triangles = new int[(vertices.Length-2)*3];
		
		for(int i = 0, j = 0; i < (vertices.Length-2)*3; i+=3, j++) {
			triangles[i] = j;
			triangles[i+1] = j+1;
			triangles[i+2] = j+2;
		}
		return triangles;
	}
	
	public void UpdateColliderPoints()
	{
		/*Vector2[] colliderPoints = new Vector2[this.vertices.Length/2 + 2];

		for (int i = 0; i < (this.vertices.Length/2); i++) {
			colliderPoints[i] = this.vertices[i*2];
		}
		colliderPoints [this.vertices.Length / 2] = this.vertices [this.vertices.Length -1];
		colliderPoints [this.vertices.Length / 2 + 1] = this.vertices [1];
		pc2D.points = colliderPoints;*/
		
		Vector2[] colliderPoints = new Vector2[this.vertices.Length];
		
		for (int i = 0; i < (this.vertices.Length/2); i++) {
			colliderPoints[i] = this.vertices[i*2];
			colliderPoints[this.vertices.Length - (i+1)] = this.vertices[i*2 + 1];
		}
		pc2D.points = colliderPoints;
	}
	
	private PolygonCollider2D pc2D;
	
	void Start () {
	/*	vertices = TriangulateRect (bounds, points);
		
		MeshRenderer renderer=gameObject.AddComponent<MeshRenderer>();
		renderer.material=new Material(Shader.Find("Hidden/Internal-Colored"));
		renderer.material.color=Color.cyan;
		
		MeshFilter filter=gameObject.AddComponent<MeshFilter>();
		mesh=new Mesh();
		mesh.vertices = vertices;
		mesh.triangles = constructTrianglesFromVertices(vertices);
		
		mesh.RecalculateNormals();
		mesh.Optimize();
		filter.mesh=mesh;
		
		this.gameObject.AddComponent<PolygonCollider2D> ();
		pc2D = this.GetComponent ("PolygonCollider2D") as PolygonCollider2D;
		
		UpdateColliderPoints();*/
	}
}