import { UploadUpsertPageRequest, ResultResponse, UploadUpsertFileRequest, UploadUpsertTemplateRequest, UploadReadAllFilesResponse, UploadDeletePageRequest, UploadDeleteFileRequest } from "./dto"
import { ApiClient } from "./ApiClient";

export const UploadApi = {
    UpsertPage: async (request: UploadUpsertPageRequest): Promise<ResultResponse> => {
        const apiUrl = ApiClient.Current!.getUrl();
        const controller = "Upload";
        const action = "UpsertPage";

        const url = `${apiUrl}?Controller=${controller}&Action=${action}`;
        return await ApiClient.Current!.send(url, request);
    },

    UpsertFile: async (request: UploadUpsertFileRequest): Promise<ResultResponse> => {
        const apiUrl = ApiClient.Current!.getUrl();
        const controller = "Upload";
        const action = "UpsertFile";

        const url = `${apiUrl}?Controller=${controller}&Action=${action}`;
        return await ApiClient.Current!.send(url, request);
    },

    UpsertTemplate: async (request: UploadUpsertTemplateRequest): Promise<ResultResponse> => {
        const apiUrl = ApiClient.Current!.getUrl();
        const controller = "Upload";
        const action = "UpsertTemplate";

        const url = `${apiUrl}?Controller=${controller}&Action=${action}`;
        return await ApiClient.Current!.send(url, request);
    },

    ReadAllFiles: async (): Promise<UploadReadAllFilesResponse> => {
        const url = ApiClient.Current!.getUrl();
        return await ApiClient.Current!.send(url);
    },

    DeletePage: async (request: UploadDeletePageRequest): Promise<ResultResponse> => {
        const apiUrl = ApiClient.Current!.getUrl();
        const controller = "Upload";
        const action = "DeletePage";

        const url = `${apiUrl}?Controller=${controller}&Action=${action}`;
        return await ApiClient.Current!.send(url, request);
    },

    DeleteFile: async (request: UploadDeleteFileRequest): Promise<ResultResponse> => {
        const apiUrl = ApiClient.Current!.getUrl();
        const controller = "Upload";
        const action = "DeleteFile";

        const url = `${apiUrl}?Controller=${controller}&Action=${action}`;
        return await ApiClient.Current!.send(url, request);
    }
}