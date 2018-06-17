using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
[RequireComponent(typeof(RectTransform))]
public class ResizeGridElementsToRect : MonoBehaviour {
	public GridLayoutGroup gridLayoutGroup;
    public RectTransform rectToCalculateFrom;
    public int cellCount = 0;
	public List<RectTransform> elements = new List<RectTransform>();
	//public float RowsColumns;
	public int RowCount = 3;
	public int ColumnCount = 2;
	Vector2 spacing;
    void Awake ()
    {
        //gridLayoutGroup = GetComponent<GridLayoutGroup> ();
    }
	void Start()
	{
		StartCoroutine(LateStart(1f));
	}
	
    public void Changed()
    {	
		gridLayoutGroup = GetComponent<GridLayoutGroup> ();
		elements.Clear();
		foreach (RectTransform rt in this.transform)
		{
			if (rt.parent == this.transform)
			{	
				elements.Add(rt);	
				cellCount = elements.Count;
			}
		}
		//RowsColumns = gridLayoutGroup.constraintCount;
		spacing = gridLayoutGroup.spacing;
		float cellWidth = (rectToCalculateFrom.rect.width - spacing.x)/ColumnCount;
		float cellHeight = (rectToCalculateFrom.rect.height - spacing.y)/RowCount;
		foreach (RectTransform rt in elements)
		{
			LayoutElement le = rt.GetComponent<LayoutElement>();
			le.minWidth = cellWidth;
			le.minHeight = cellHeight;
		}
        gridLayoutGroup.cellSize = new Vector2 (cellWidth,  cellHeight);
		LayoutRebuilder.ForceRebuildLayoutImmediate(rectToCalculateFrom);
    }

	IEnumerator LateStart(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
        Changed();
	}
}
