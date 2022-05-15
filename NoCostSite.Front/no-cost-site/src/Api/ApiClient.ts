export interface IApiClient {
    send: (url: string, data?: any) => Promise<any>;
    getUrl: () => string;
}

export class ApiClient {
    public static Current?: IApiClient;
}