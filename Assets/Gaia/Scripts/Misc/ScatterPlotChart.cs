using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScatterPlotChart : MonoBehaviour
{
    /*
     * https://www.youtube.com/watch?v=8cFALzCB3dA
     */
    public class Point
    {
        public float x { get; private set; }
        public float y { get; private set; }
        public string glyph { get; private set; }
        public Color color { get; private set; }

        public GameObject pointObject;

        public Point(float x, float y, string glyph, Color color)
        {
            this.x = x;
            this.y = y;
            this.glyph = glyph;
            this.color = color;
        }
    }

    [SerializeField]
    private Canvas _canvas;
    [SerializeField]
    private Transform _xAxis;
    [SerializeField]
    private Transform _yAxis;
    [SerializeField]
    private Transform _main;

    [SerializeField]
    private int _xIncrement = 100;
    [SerializeField]
    private int _yIncrement = 100;
    [SerializeField]
    private int _minXValue = 0;
    [SerializeField]
    private int _minYValue = 0;
    [SerializeField]
    private int _xLabelCount = 10;
    [SerializeField]
    private int _yLabelCount = 10;


    [SerializeField]
    private GameObject _pointPrefab;
    [SerializeField]
    private GameObject _xValPrefab;
    [SerializeField]
    private GameObject _yValPrefab;

    private float _mainWidth;
    private float _mainHeight;

    private List<Point> _points = new List<Point>();

    private void Start()
    {
        //Get the size of the Canva
        Rect rectangle = RectTransformUtility.PixelAdjustRect(_main.GetComponent<RectTransform>(), _canvas);

        _mainWidth = rectangle.width;
        _mainHeight = rectangle.height;

        InitiatePoints();

        DrawPoints();
        
        DrawAxis(_xLabelCount, _yLabelCount);
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
        float xOffset = _mainWidth / (_xLabelCount + 1);
        float yOffset = _mainHeight / (_yLabelCount + 1);

        float xFactor = _mainWidth / ((_xIncrement + 2) * _xLabelCount);
        float yFactor = _mainHeight / ((_yIncrement + 2) * _yLabelCount);

        Debug.Log(_mainHeight + " " + _mainWidth);

        foreach (Point point in _points)
        {
            GameObject tempObject = Instantiate(_pointPrefab, _main);
            RectTransform rectangleTransform = tempObject.GetComponent<RectTransform>();
            rectangleTransform.anchorMax = Vector2.zero;
            rectangleTransform.anchorMin = Vector2.zero;
            rectangleTransform.anchoredPosition3D = new Vector3(point.x * xFactor - xOffset , point.y * yFactor - _mainHeight - yOffset ,0);
            point.pointObject = tempObject;
            tempObject.GetComponentInChildren<Text>().text = point.glyph;
            tempObject.GetComponentInChildren<Text>().color = point.color;
        }
    }

    private void InitiatePoints()
    {
        Point cheapestPoint = new Point(36340, 23750, "O", new Color(1f, 0f, 0f));
        Point mostExpensivePoint = new Point(58410, 46000, "O", new Color(0f, 1f, 0f));
        Point lessPollutingPoint = new Point(17610, 32250, "O", new Color(1f, 0f, 1f));
        Point mostPollutingPoint = new Point(77140, 37500, "O", new Color(1f, 1f, 0f));

        _points.Add(cheapestPoint);
        _points.Add(mostExpensivePoint);
        _points.Add(lessPollutingPoint);
        _points.Add(mostPollutingPoint);
    }
}
