using System;

namespace NoCostSite.ObjectStorage.Client
{
    public static class ObjectStorageClientAccessConfigContainer
    {
        private static ObjectStorageClientAccessConfig? _current;

        public static ObjectStorageClientAccessConfig Current
        {
            get => _current ?? throw new Exception($"Need configure {nameof(ObjectStorageClientAccessConfig)} by {nameof(ObjectStorageClientAccessConfigContainer)}");
            set => _current = value;
        }
    }
}