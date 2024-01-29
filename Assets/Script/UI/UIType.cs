using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIType
{
    public string Path { get; private set; }
    public string Name { get; private set; }


    /// <summary>
    /// 获得UI信息
    /// </summary>
    /// <param name="ui_path">对应panel路径</param>
    /// <param name="ui_name">对应panel名称</param>
    public UIType(string ui_path)
    {
        Path = ui_path;
        Name = Path.Substring(Path.LastIndexOf('/') + 1);
    }

}
