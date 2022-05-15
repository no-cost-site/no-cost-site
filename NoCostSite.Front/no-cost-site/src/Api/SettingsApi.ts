import { SettingsUpsertRequest, ResultResponse, SettingsReadResponse } from "./dto"
import { ApiClient } from "./ApiClient";

export const SettingsApi = {
    Upsert: async (request: SettingsUpsertRequest): Promise<ResultResponse> => {
        const apiUrl = ApiClient.Current!.getUrl();
        const controller = "Settings";
        const action = "Upsert";

        const url = `${apiUrl}?Controller=${controller}&Action=${action}`;
        return await ApiClient.Current!.send(url, request);
    },

    Read: async (): Promise<SettingsReadResponse> => {
        const apiUrl = ApiClient.Current!.getUrl();
        const controller = "Settings";
        const action = "Read";

        const url = `${apiUrl}?Controller=${controller}&Action=${action}`;
        return await ApiClient.Current!.send(url);
    }
}