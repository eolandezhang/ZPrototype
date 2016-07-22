using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Settings;

namespace BLL.Settings
{
    public class Menu
    {
        DAL.Settings.Menu _menu = new DAL.Settings.Menu();

        public string GetData()
        {
            List<MenuEntity> list = _menu.GetData();
            StringBuilder psb = new StringBuilder();
            string cate;
            foreach (var m in list)
            {
                psb.Append("{");
                psb.Append("id");
                psb.Append(":");
                psb.Append(m.ID);
                psb.Append(",pId:");
                psb.Append(m.PID);
                psb.Append(",name:\"");
                psb.Append(String.IsNullOrEmpty(m.NAME) ? "" : m.NAME);
                psb.Append("\",title:\"");
                psb.Append(String.IsNullOrEmpty(m.TITLE) ? "" : m.TITLE);
                psb.Append("\",url");
                psb.Append(":\"");
                psb.Append(String.IsNullOrEmpty(m.URL) ? "" : m.URL);
                psb.Append("\",open");
                psb.Append(":\"");
                psb.Append(String.IsNullOrEmpty(m.OPEN) ? "" : m.OPEN);
                psb.Append("\",target");
                psb.Append(":\"");
                psb.Append(String.IsNullOrEmpty(m.TARGET) ? "_self" : m.TARGET);
                psb.Append("\",icon");
                psb.Append(":\"");
                psb.Append(String.IsNullOrEmpty(m.ICON) ? "" : m.ICON);

                psb.Append("\",sort");
                psb.Append(":\"");
                psb.Append(m.SORT);

                psb.Append("\",pname");
                psb.Append(":\"");
                psb.Append(m.PNAME);

                psb.Append("\"},");
            }


            cate = "[";

            cate += psb.ToString().TrimEnd(',');
            cate += "]";
            return cate;
        }
        public string GetDataNoRoot()
        {
            List<MenuEntity> list = _menu.GetDataNoRoot();
            StringBuilder psb = new StringBuilder();
            string cate;
            foreach (var m in list)
            {
                psb.Append("{");
                psb.Append("id");
                psb.Append(":");
                psb.Append(m.ID);
                psb.Append(",pId:");
                psb.Append(m.PID);
                psb.Append(",name:\"");
                psb.Append(String.IsNullOrEmpty(m.NAME) ? "" : m.NAME);
                psb.Append("\",title:\"");
                psb.Append(String.IsNullOrEmpty(m.TITLE) ? "" : m.TITLE);
                psb.Append("\",url");
                psb.Append(":\"");
                psb.Append(String.IsNullOrEmpty(m.URL) ? "" : m.URL + "?mid=" + m.ID);
                psb.Append("\",open");
                psb.Append(":\"");
                psb.Append(String.IsNullOrEmpty(m.OPEN) ? "" : m.OPEN);
                psb.Append("\",target");
                psb.Append(":\"");
                psb.Append(String.IsNullOrEmpty(m.TARGET) ? "_self" : m.TARGET);
                psb.Append("\",icon");
                psb.Append(":\"");
                psb.Append(String.IsNullOrEmpty(m.ICON) ? "" : m.ICON);

                psb.Append("\",sort");
                psb.Append(":\"");
                psb.Append(m.SORT);

                psb.Append("\",pname");
                psb.Append(":\"");
                psb.Append(m.PNAME);

                psb.Append("\"},");
            }


            cate = "[";

            cate += psb.ToString().TrimEnd(',');
            cate += "]";
            return cate;

        }
        public int Add(MenuEntity entity)
        {
            return _menu.Add(entity);
        }
        public int Edit(MenuEntity entity)
        {
            return _menu.Edit(entity);
        }
        public int Delete(decimal ID)
        {
            return _menu.Delete(ID);
        }
    }
}
