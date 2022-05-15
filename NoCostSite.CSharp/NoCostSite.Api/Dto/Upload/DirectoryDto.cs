namespace NoCostSite.Api.Dto.Upload
{
    public class DirectoryDto
    {
        public string Name { get; set; } = null!;

        public string Url { get; set; } = null!;

        public DirectoryDto[] Child { get; set; } = null!;
    }
}