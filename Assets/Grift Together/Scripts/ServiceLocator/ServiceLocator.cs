using System.Collections.Generic;
using UnityEngine;


namespace GriftTogether {

    public class ServiceLocator {

        private readonly ServiceLocator _parentServiceLocator;
        private readonly Dictionary<string, IService> _serviceDictionary;

        public ServiceLocator(ServiceLocator parentServiceLocator) {
            _parentServiceLocator = parentServiceLocator;
        }

        public bool AddService<T>(T service) where T : IService {

            string type = typeof(T).Name;

            if (_serviceDictionary.ContainsKey(type)) {
                Debug.LogWarning($"Service Locator {this} had service with name: {type}!");
                return false;
            }

            _serviceDictionary[type] = service;
            return true;
        }


        public bool Resolve<T>(out T service) where T : IService {

            string key = typeof(T).Name;

            if (_serviceDictionary.ContainsKey(key)) {
                service = (T)_serviceDictionary[key];
                return true;
            }

            if (_parentServiceLocator != null) {

                if (_parentServiceLocator.Resolve<T>(out T serviceParent)) {
                    service = serviceParent;
                    return true;
                }
            }

            service = default(T);
            return false;

        }
    }
}