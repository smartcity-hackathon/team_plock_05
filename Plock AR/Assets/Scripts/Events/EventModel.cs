using Mapbox.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class EventModel : MonoBehaviour
{
    public EventTypes EventType { get; set; } = EventTypes.Future;
    public string StartDate { get; set; }
    public string EndDate { get; set; }
    public string Name { get; set; } = "";
    public string Place { get; set; } = "";
    public Vector2d Coords { get; set; } = new Vector2d();
    public string Description { get; set; } = "";
    public string Price { get; set; } = "";
    public string Url { get; set; } = "";
    public string GraphicsDir { get; set; } = "";
    public string Models3DDir { get; set; } = "";
    public List<Texture2D> Graphics { get; set; } = new List<Texture2D>();
    public List<string> Models3D { get; set; } = new List<string>();
    public GameObject RelatedGameObject { get; set; }
    private bool _isAvailable = false;
    public bool IsAvailable
    {
        get
        {
            return _isAvailable;
        }
        set
        {
            _isAvailable = value;
            OnIsAvaiableChange();
        }
    }
    private void OnIsAvaiableChange()
    {
        gameObject.SetActive(_isAvailable);
    }
    public bool IsInMonth(int month)
    {
        bool result = false;
        try
        {
            DateTime dateTime;
            if (DateTime.TryParseExact(StartDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
            {
                result = dateTime.Month == month;
            }
            return result;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    //public DateTime StartDateAsDateTime
    //{
    //    get
    //    {
    //        if(DateTime.TryParseExact(StartDate, "yyyy-mm-dd"))
    //        return 
    //    }
    //}
}
