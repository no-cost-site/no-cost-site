import { AuthLoginRequest, AuthLoginResponse, AuthRegisterRequest, ResultResponse, AuthIsInitResponse } from "./dto"
import { ApiClient } from "./ApiClient";

export const AuthApi = {
    Login: async (request: AuthLoginRequest): Promise<AuthLoginResponse> => {
        const apiUrl = ApiClient.Current!.getUrl();
        const controller = "Auth";
        const action = "Login";

        const url = `${apiUrl}?Controller=${controller}&Action=${action}`;
        return await ApiClient.Current!.send(url, request);
    },

    Register: async (request: AuthRegisterRequest): Promise<ResultResponse> => {
        const apiUrl = ApiClient.Current!.getUrl();
        const controller = "Auth";
        const action = "Register";

        const url = `${apiUrl}?Controller=${controller}&Action=${action}`;
        return await ApiClient.Current!.send(url, request);
    },

    IsInit: async (): Promise<AuthIsInitResponse> => {
        const url = ApiClient.Current!.getUrl();
        return await ApiClient.Current!.send(url);
    }
}