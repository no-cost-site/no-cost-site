import { ResultResponse, AuthLoginRequest, AuthLoginResponse, AuthRegisterRequest, AuthIsInitResponse } from "./dto"
import { ApiClient } from "./ApiClient";

export const AuthApi = {
    Check: async (): Promise<ResultResponse> => {
        const apiUrl = ApiClient.Current!.getUrl();
        const controller = "Auth";
        const action = "Check";

        const url = `${apiUrl}?Controller=${controller}&Action=${action}`;
        return await ApiClient.Current!.send(url);
    },

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
        const apiUrl = ApiClient.Current!.getUrl();
        const controller = "Auth";
        const action = "IsInit";

        const url = `${apiUrl}?Controller=${controller}&Action=${action}`;
        return await ApiClient.Current!.send(url);
    }
}