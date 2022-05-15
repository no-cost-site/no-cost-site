import { PagesUpsertRequest, ResultResponse, PagesReadAllResponse, PagesReadRequest, PagesReadResponse, PagesDeleteRequest } from "./dto"
import { ApiClient } from "./ApiClient";

export const PagesApi = {
    Upsert: async (request: PagesUpsertRequest): Promise<ResultResponse> => {
        const apiUrl = ApiClient.Current!.getUrl();
        const controller = "Pages";
        const action = "Upsert";

        const url = `${apiUrl}?Controller=${controller}&Action=${action}`;
        return await ApiClient.Current!.send(url, request);
    },

    ReadAll: async (): Promise<PagesReadAllResponse> => {
        const url = ApiClient.Current!.getUrl();
        return await ApiClient.Current!.send(url);
    },

    Read: async (request: PagesReadRequest): Promise<PagesReadResponse> => {
        const apiUrl = ApiClient.Current!.getUrl();
        const controller = "Pages";
        const action = "Read";

        const url = `${apiUrl}?Controller=${controller}&Action=${action}`;
        return await ApiClient.Current!.send(url, request);
    },

    Delete: async (request: PagesDeleteRequest): Promise<ResultResponse> => {
        const apiUrl = ApiClient.Current!.getUrl();
        const controller = "Pages";
        const action = "Delete";

        const url = `${apiUrl}?Controller=${controller}&Action=${action}`;
        return await ApiClient.Current!.send(url, request);
    }
}