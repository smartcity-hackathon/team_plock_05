using Mapbox.Utils;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;

public class EventModelsManager
{
    public static List<EventModel> EventModels { get; set; } = new List<EventModel>();
    public static void InitializeEventModel(EventModel em, GeoJSON.FeatureObject fit, GameObject go)
    {
        em.EventType = (EventTypes)Enum.Parse(typeof(EventTypes), fit.properties["eventType"]);
        em.StartDate = fit.properties["startDate"];// DateTime.ParseExact(fit.properties["startDate"], "yyyy-mm-dd", CultureInfo.InvariantCulture),
        em.EndDate = fit.properties["endDate"];//DateTime.ParseExact(fit.properties["endDate"], "yyyy-mm-dd", CultureInfo.InvariantCulture),
        em.Name = fit.properties["name"];
        em.Place = fit.properties["place"];
        em.Coords = new Vector2d(double.Parse(fit.properties["y"].Replace('.', ',')), double.Parse(fit.properties["x"].Replace('.', ',')));
        em.Description = fit.properties["description"];
        em.Price = fit.properties["price"];
        em.Url = fit.properties["website"];
        em.GraphicsDir = fit.properties["graphics"];
        em.Models3DDir = fit.properties["3DModels"];
        em.RelatedGameObject = go;
        em.IsAvailable = false;
    }
    public static void ClearEventModels()
    {
        EventModels.Clear();
    }
    public static void InitializeEventModelGraphics(EventModel em)
    {
        string appDir = Application.dataPath + "/Resources/Photos/";
        string dir = "Photos/" + em.GraphicsDir;
        var b = Resources.LoadAll(dir).Cast<Texture2D>().ToList();
        foreach(var bb in b)
        {
            em.Graphics.Add(bb);
        }
        //if (dir == null || dir.Length == 0)
        //    return;
        //DirectoryInfo d = new DirectoryInfo(appDir + dir);
        //FileInfo[] files = d.GetFiles("*.jpg");
        //em.Graphics = new List<string>();
        //foreach (var f in files)
        //{
        //    em.Graphics.Add(f.Name);
        //}
    }
    public static void InitializeEvnetModel3DModels(EventModel em)
    {
        string appDir = Application.dataPath + "/Resources/Models3D/";
        string dir = em.Models3DDir;
        if (dir == null || dir.Length == 0)
            return;
        //todo
    }
}
