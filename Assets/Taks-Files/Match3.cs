using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Match3 : MonoBehaviour
{
    public Vector2Int GridSize = new Vector2Int(8, 8);
    public Sprite[] Items;

    public GridLayoutGroup GridUI;
    public Vector2 Spacing = new Vector2(10, 10);
    public GridItem ItemTemplate;

    public float ItemShowDelay = 0.025f;

    [Space(5)]
    public Button RefreshBttn;

    private int[][] grid;
    private int[] allTypes;

    private void Awake()
    {
        allTypes = new int[Items.Length];

        for (int i = 0; i < Items.Length; i++)
            allTypes[i] = i + 1;

        GenerateGrid();

        RefreshBttn.onClick.AddListener(() =>
        {
            ClearGrid();
            GenerateGrid();
        });
    }

    public void GenerateGrid()
    {
        var itemSize = (GridUI.GetComponent<RectTransform>().rect.width - (GridSize.x - 1) * Spacing.x - GridUI.padding.left - GridUI.padding.right) / GridSize.x;
        GridUI.cellSize = Vector2.one * itemSize;

        var prevBelow = 0;
        var prevLeft = new int[GridSize.x];

        grid = new int[GridSize.y][];
        for (int x = 0; x < GridSize.y; x++)
        {
            grid[x] = new int[GridSize.x];
            for (int y = 0; y < GridSize.x; y++)
            {
                var item = Instantiate(ItemTemplate.gameObject, GridUI.transform, false).GetComponent<GridItem>();
                item.gameObject.SetActive(true);
                item.gameObject.name = $"Item [{x}][{y}]";
                var possibleTypes = new List<int>(allTypes);

                possibleTypes.Remove(prevLeft[y]);
                possibleTypes.Remove(prevBelow);

                var rand = possibleTypes[Random.Range(0, possibleTypes.Count)];
                item.Set(rand, Items[rand - 1], (x * y + 1) * ItemShowDelay);

                prevLeft[y] = rand;
                prevBelow = rand;
                grid[x][y] = rand;
            }
        }
    }

    public void ClearGrid()
    {
        foreach (Transform child in GridUI.transform)
            Destroy(child.gameObject);
    }
}

public enum MatchType
{
    Vertical,
    Horizontal,
}