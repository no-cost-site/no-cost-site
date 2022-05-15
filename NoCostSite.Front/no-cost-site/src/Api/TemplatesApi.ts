import { TemplatesUpsertRequest, ResultResponse, TemplatesReadAllResponse, TemplatesReadRequest, TemplatesReadResponse, TemplatesDeleteRequest } from "./dto"
import { ApiClient } from "./ApiClient";

export const TemplatesApi = {
    Upsert: async (request: TemplatesUpsertRequest): Promise<ResultResponse> => {
        const apiUrl = ApiClient.Current!.getUrl();
        const controller = "Templates";
        const action = "Upsert";

        const url = `${apiUrl}?Controller=${controller}&Action=${action}`;
        return await ApiClient.Current!.send(url, request);
    },

    ReadAll: async (): Promise<TemplatesReadAllResponse> => {
        const url = ApiClient.Current!.getUrl();
        return await ApiClient.Current!.send(url);
    },

    Read: async (request: TemplatesReadRequest): Promise<TemplatesReadResponse> => {
        const apiUrl = ApiClient.Current!.getUrl();
        const controller = "Templates";
        const action = "Read";

        const url = `${apiUrl}?Controller=${controller}&Action=${action}`;
        return await ApiClient.Current!.send(url, request);
    },

    Delete: async (request: TemplatesDeleteRequest): Promise<ResultResponse> => {
        const apiUrl = ApiClient.Current!.getUrl();
        const controller = "Templates";
        const action = "Delete";

        const url = `${apiUrl}?Controller=${controller}&Action=${action}`;
        return await ApiClient.Current!.send(url, request);
    }
}