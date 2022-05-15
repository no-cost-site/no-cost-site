import { FileItemDto, DirectoryDto } from "./"

export interface UploadReadAllFilesResponse {
   Files: FileItemDto[];
   Directory: DirectoryDto
}