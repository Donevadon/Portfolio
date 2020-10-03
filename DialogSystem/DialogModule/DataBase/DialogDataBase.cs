﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

namespace Assets.DialogModule.DataBase
{

    public class DialogDataBase : MonoBehaviour, ILoadDialogs
    {
        private ILocalize localize;
        public string[][] LoadDialogs(DialogManager.Scenes scene, DialogManager.Places place)
        {
            return GetDialogs(scene, place);
        }

        private string[][] GetDialogs(DialogManager.Scenes scene, DialogManager.Places place)
        {
            List<string[]> list = new List<string[]>();
            XDocument xml = XDocument.Load(Application.dataPath + $"/Resources/Dialogs/{localize.language}/{scene}.xml");
            var dialogs = from dialog in xml.Element("root")
                          .Element(place.ToString())
                          .Elements("Hero")
                          select new
                          {
                              Name = dialog.Attribute("name").Value,
                              Text = dialog.Value
                          };
            foreach (var dialog in dialogs)
            {
                list.Add(new string[] { dialog.Name, dialog.Text });
            }
            return list.ToArray();
        }
    }
}