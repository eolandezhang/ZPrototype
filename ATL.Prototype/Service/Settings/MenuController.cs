using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;
using Model.Settings;
using BLL.Settings;

namespace Service.Settings
{
    public class MenuController : ApiController
    {
        Menu _menu = new Menu();
        [HttpGet]
        public string GetData()
        {
            return _menu.GetData();
        }
        [HttpGet]
        public string GetDataNoRoot()
        {
            return _menu.GetDataNoRoot();
        }
        [HttpPost]
        public int Add(MenuEntity entity)
        {
            return _menu.Add(entity);
        }
        [HttpPost]
        public int Edit(MenuEntity entity)
        {
            return _menu.Edit(entity);
        }
        [HttpGet]
        public int Delete(decimal ID)
        {
            return _menu.Delete(ID);
        }
    }
}
