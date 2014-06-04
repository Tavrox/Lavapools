using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BezierMaker : MonoBehaviour {

	public MeshFilter _mesh;
	public float _custom;
	public float size;
	public float width;
	public float height;

	// Use this for initialization
	void Start () 
	{
		GameObject plane = new GameObject("Plane");
		_mesh = (MeshFilter)plane.AddComponent(typeof(MeshFilter));
		_mesh.mesh = CreateMesh(width, height);
		MeshRenderer renderer = plane.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
		renderer.material.shader = Shader.Find ("Particles/Additive");
		Texture2D tex = new Texture2D(1, 1);
		tex.SetPixel(0, 0, Color.green);
		tex.Apply();
		renderer.material.mainTexture = tex;
		renderer.material.color = Color.green;
	}

	void Update()
	{
		Vector3[] vertices = _mesh.mesh.vertices;
		Vector3[] normals = _mesh.mesh.normals;
		for (var i = 0; i < vertices.Length; i++)
			vertices[i] += normals[i] * Mathf.Sin(Time.time);
		
		_mesh.mesh.vertices = vertices;
	}

	public Mesh CreateMesh(float width, float height)
	{
		Mesh m = new Mesh();
		m.name = "ScriptedMesh";
		m.vertices = new Vector3[] {
			new Vector3(-width, -height, 0.01f),
			new Vector3(width, -height, 0.01f),
			new Vector3(width, height, 0.01f),
			new Vector3(-width, height, 0.01f)
		};
		m.uv = new Vector2[] {
			new Vector2 (0, 0),
			new Vector2 (0, 1),
			new Vector2(1, 1),
			new Vector2 (1, 0)
		};
		m.triangles = new int[] { 0, 1, 2, 0, 2, 3};
		m.RecalculateNormals();
		
		return m;
	}

	void OnDrawGizmosSelected()
	{
		CreateMesh(width, height);
	}
	private Vector2 quadraticBezier(Vector2 P0, Vector2 P1, Vector2 C, float t)
	{
		float x = (1 - t) * (1 - t) * P0.x + (2 - 2 * t) * t * C.x + t * t * P1.x;
		float y = (1 - t) * (1 - t) * P0.y + (2 - 2 * t) * t * C.y + t * t * P1.y;
		return new Vector2(x, y);
	}

//	public Vector2 convertToPoints(int quality = 10)
//	{
//		Vector2 points = new Vector2(0f,0f);
//		
//		int precision = 1 / quality;
//		
//		// Pass through all nodes to generate line segments
//		for (int i = 0; i < _nodes.length - 1; i++)
//		{
//			CurveNode current = _nodes[i];
//			CurveNode next = _nodes[i + 1];
//			
//			// Sample Bezier curve between two nodes
//			// Number of steps is determined by quality parameter
//			for (int step = 0; step < 1; step += precision)
//			{
//				Vector2 newPoint = quadraticBezier(current.position, next.position, current.control, step);
//				points.push(newPoint);
//			}
//		}
//		return points;
//	}

}

public class CurveNode
{
	public float x;
	public float y;
}

public class Point
{
	public float x;
	public float y;

}
