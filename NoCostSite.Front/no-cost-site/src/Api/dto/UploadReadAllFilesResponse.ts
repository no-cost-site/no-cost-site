import { FileItemDto, DirectoryDto } from "./"

export interface UploadReadAllFilesResponse {
   Files: FileItemDto[];
   Directories: DirectoryDto[]
}