import { UsersChangePasswordRequest, ResultResponse } from "./dto"
import { ApiClient } from "./ApiClient";

export const UsersApi = {
    ChangePassword: async (request: UsersChangePasswordRequest): Promise<ResultResponse> => {
        const apiUrl = ApiClient.Current!.getUrl();
        const controller = "Users";
        const action = "ChangePassword";

        const url = `${apiUrl}?Controller=${controller}&Action=${action}`;
        return await ApiClient.Current!.send(url, request);
    }
}