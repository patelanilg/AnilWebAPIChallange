using System;
using System.Collections.Generic;
using System.Linq;
using AnilWebAPI.Models;

namespace AnilWebAPI.Storage
{
    /// <summary>
    /// Implementation of the IObjectStore which stored objects on memory
    /// </summary>
    public class InMemoryObjectStore : IObjectStore
    {
        /// <summary>
        /// Constructor which creates a new InMemoryObjectStore class
        /// </summary>
        public InMemoryObjectStore()
        {
            _storage = new Dictionary<Type, List<OperationModel>>();
        }

        /// <summary>
        /// Storage area of the objects
        /// </summary>
        private readonly Dictionary<Type, List<OperationModel>> _storage;

        /// <inheritdoc/>
        public List<TCrudModel> ListItems<TCrudModel>() where TCrudModel : OperationModel
        {
            if (!_storage.ContainsKey(typeof(TCrudModel)))
            {
                _storage[typeof(TCrudModel)] = new List<OperationModel>();
            }
            return _storage[typeof(TCrudModel)].Cast<TCrudModel>().ToList();
        }

        /// <inheritdoc/>
        public TCrudModel GetItem<TCrudModel>(int id) where TCrudModel : OperationModel
        {
            TCrudModel model = ListItems<TCrudModel>().FirstOrDefault(modelItr => modelItr.Id == id);
            if (model == default(TCrudModel))
            {
                throw new AccessViolationException("Object with ID not found");
            }
            return model;
        }

        /// <inheritdoc/>
        public void AddItem<TCrudModel>(TCrudModel model) where TCrudModel : OperationModel
        {
            List<TCrudModel> list = ListItems<TCrudModel>();
            TCrudModel oldModel = list.FirstOrDefault(modelItr => modelItr.Id == model.Id);
            if (oldModel != default(TCrudModel))
            {
                throw new AccessViolationException("Item with ID already exists");
            }
            
            _storage[typeof(TCrudModel)].Add(model);
        }

        /// <inheritdoc/>
        public void UpdateItem<TCrudModel>(int id, TCrudModel newModel) where TCrudModel : OperationModel
        {
            List<TCrudModel> list = ListItems<TCrudModel>();
            int listIndex = list.FindIndex(iteratorModel => iteratorModel.Id == id);
            if (newModel == null)
            {
                _storage[typeof(TCrudModel)].RemoveAt(listIndex);
            }
            else
            {
                _storage[typeof(TCrudModel)][listIndex] = newModel;
            }
        }

        /// <inheritdoc/>
        public void DeleteItem<TCrudModel>(int id) where TCrudModel : OperationModel
        {
            UpdateItem<TCrudModel>(id, null);
        }
    }
}