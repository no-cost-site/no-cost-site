namespace NoCostSite.BusinessLogic.ObjectStorage
{
    public class ObjectStorageFileData
    {
        public ObjectStorageFileInfo Info { get; set; } = null!;
        
        public byte[] Data { get; set; } = null!;
    }
}