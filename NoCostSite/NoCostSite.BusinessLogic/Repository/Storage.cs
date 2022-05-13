using System;

namespace NoCostSite.BusinessLogic.Repository
{
    public class Storage<T> : IStorage
    {
        public Guid Id { get; set; }

        public T Data { get; set; } = default!;
    }
}