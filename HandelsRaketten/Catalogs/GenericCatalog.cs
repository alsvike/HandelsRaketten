﻿using HandelsRaketten.Models.AdModels;
using HandelsRaketten.Services.DbServices;
using HandelsRaketten.Services.GenericServices;

namespace HandelsRaketten.Catalogs
{
    public class GenericCatalog<T> where T : Ad
    {
        public List<T> Objs { get; set; }
        private GenericJsonFileService<T> _objJsonService;

        IService<T> _dbService;
        public GenericCatalog(GenericJsonFileService<T> ftJsonService, IService<T> dbService)
        {
            _dbService = dbService;
            _objJsonService = ftJsonService;
            //Objs = _objJsonService.GetJson().ToList();
            Objs = _dbService.GetObjectsAsync().Result.ToList();
        }

        public List<Ad> GetAll()
        {
            return Objs.Cast<Ad>().ToList();
        }
        public T Add(T obj)
        {
            if (obj != null)
            {
                Objs.Add(obj);
                _dbService.SaveObjects(Objs);

                //_objJsonService.SaveJson(Objs);
                return obj;
            }
            return default(T);
        }

        public T Delete(T obj)
        {
            if (obj != null)
            {
                Objs.Remove(obj);
                _dbService.SaveObjects(Objs);

                //_objJsonService.SaveJson(Objs);
                return obj;
            }
            return default(T);
        }

        public T Edit(T obj, int id)
        {
            var objToEdit = Objs.FirstOrDefault(o => o.Id == id);

            if (objToEdit != null)
            {
                // Update properties of the found object with matching properties from obj
                foreach (var property in typeof(T).GetProperties())
                {
                    var value = property.GetValue(obj);
                    property.SetValue(objToEdit, value);
                }

                // Save changes to the JSON file
                _dbService.SaveObjects(Objs);

                //_objJsonService.SaveJson(Objs);

                return objToEdit;
            }
            return default(T);
        }

        public T Get(int id)
        {
            return Objs.FirstOrDefault(o => o.Id == id);
        }
    }
}
