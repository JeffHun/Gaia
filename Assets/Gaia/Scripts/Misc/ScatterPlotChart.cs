using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScatterPlotChart : MonoBehaviour
{
    /*
     * https://www.youtube.com/watch?v=8cFALzCB3dA
     */
    private class Point
    {
        private Vector2 _coordinates;
        private string _glyph;
        private Color _color;

        private GameObject _pointObject;

        public Point(float x, float y, string glyph, Color color)
        {
            _coordinates = new Vector2(x, y);
            _glyph = glyph;
            _color = color;
        }

        public GameObject GetPointObject()
        {
            return _pointObject;
        }
        public void SetPointObject(GameObject pointObject)
        {
            _pointObject = pointObject;
        }

        public Vector2 GetCoordinates()
        {
            return _coordinates;
        }

        public void SetCoordinates(Vector2 coordinates)
        {
            _coordinates = coordinates;
        }

        public string GetGlyph()
        { return _glyph; }

        public Color GetColor() 
        { return _color; }
    }

    [SerializeField] private Canvas _canvas;
    [SerializeField] private Transform _xAxis;
    [SerializeField] private Transform _yAxis;
    [SerializeField] private Transform _main;
    [SerializeField] private Transform _panel;

    [SerializeField] private int _xIncrement = 100;
    [SerializeField] private int _yIncrement = 100;
    [SerializeField] private int _minXValue = 0;
    [SerializeField] private int _minYValue = 0;
    [SerializeField] private int _xLabelCount = 10;
    [SerializeField] private int _yLabelCount = 10;


    [SerializeField] private GameObject _pointPrefab;
    [SerializeField] private GameObject _xValPrefab;
    [SerializeField] private GameObject _yValPrefab;

    [SerializeField] private float _mainWidth = 1400;
    [SerializeField] private float _mainHeight = 800;

    private List<Point> _points = new List<Point>();

    private void Start()
    {
        //Get the size of the Canva
        Rect rectangle = RectTransformUtility.PixelAdjustRect(_panel.GetComponent<RectTransform>(), _canvas);

        rectangle.width = _mainWidth;
        rectangle.height = _mainHeight;

        _panel.GetComponent<RectTransform>().sizeDelta = new Vector2(_mainWidth, _mainHeight);

        _xAxis.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, -100, 100);
        _xAxis.GetComponent<RectTransform>().sizeDelta = new Vector2(_mainWidth, 100);

        _yAxis.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, -100, 100);
        _yAxis.GetComponent<RectTransform>().sizeDelta = new Vector2(100, _mainHeight);

        DrawAxis(_xLabelCount, _yLabelCount);
    }

    private void OnEnable()
    {
        UpdatePoints();
    }

    private void DrawAxis(int xAxisCount, int yAxisCount)
    {
        int xSpace = _xIncrement;
        int ySpace = _yIncrement;

        for (int i = 0; i < xAxisCount + 1; i++)
        {
            GameObject xObject = Instantiate(_xValPrefab, _xAxis);
            xObject.GetComponentInChildren<TextMeshProUGUI>().text = (xSpace * i + _minXValue).ToString();
        }

        for (int i = 0; i < yAxisCount + 1; i++)
        {
            GameObject yObject = Instantiate(_yValPrefab, _yAxis);
            yObject.GetComponentInChildren<TextMeshProUGUI>().text = (ySpace * i + _minYValue).ToString();
        }
    }

    private void DrawPoints()
    {
        float xFactor = _mainWidth / (_xIncrement * (_xLabelCount + 1));
        float yFactor = _mainHeight / (_yIncrement * (_yLabelCount + 1));

        float xOffset = (_minXValue - (_xIncrement / 2)) * xFactor;
        float yOffset = (_minYValue - (_yIncrement / 2)) * yFactor;

        foreach (Point point in _points)
        {
            CreateView(point, xFactor, yFactor, xOffset, yOffset);
        }
    }

    private void CreateView(Point point, float xFactor, float yFactor, float xOffset, float yOffset)
    {
        GameObject tempObject = Instantiate(_pointPrefab, _main);
        RectTransform rectangleTransform = tempObject.GetComponent<RectTransform>();
        rectangleTransform.anchorMax = Vector2.zero;
        rectangleTransform.anchorMin = Vector2.zero;
        rectangleTransform.anchoredPosition3D = new Vector3((point.GetCoordinates().x * xFactor) - xOffset, (point.GetCoordinates().y * yFactor) - yOffset, 0);
        point.SetPointObject(tempObject);
        tempObject.GetComponentInChildren<Text>().text = point.GetGlyph();
        tempObject.GetComponentInChildren<Text>().color = point.GetColor();
    }

    private void UpdatePoints()
    {
        ClearPoints();
        _points = new List<Point>();
        CreatePoint(36340, 23750, "O", Color.red);
        CreatePoint(58320, 47000, "O", Color.magenta);
        CreatePoint(17610, 32250, "O", Color.blue);
        CreatePoint(77140, 37500, "O", Color.cyan);
        CreatePoint(0, 0, "X", Color.green);
    }

    private void CreatePoint(float x, float y, string glyph, Color color)
    {
        Point point = new Point(x, y, glyph, color);
        _points.Add(point);
    }

    private void ClearPoints()
    {
        for (int i = _main.childCount; i > 0; i--) 
        {
            Destroy(_main.GetChild(i-1).gameObject);
        }
    }

    public void AddPoint(float x, float y)
    {
        if (_points.Count > 0) 
        {
            _points[4].SetCoordinates(new Vector2(x, y));
        }
        DrawPoints();
    }
}
