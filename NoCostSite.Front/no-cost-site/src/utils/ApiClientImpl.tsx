import {TokenContainer} from "./TokenContainer";
import {IApiClient} from "../Api/ApiClient";

export class ApiClientImpl {
    public static Current: IApiClient;

    private readonly url: string;

    constructor(url: string) {
        this.url = url;
    }

    public async send(url: string, data?: any): Promise<any> {
        const result = await fetch(url, {
            method: "POST",
            headers: {
                "Content-Type": "application/json;charset=utf-8",
                "Accept": "application/json",
                "Token": TokenContainer.get(),
            },
            body: data && JSON.stringify(data),
        });

        if (result.ok && result.status === 200) {
            return await ApiClientImpl.getResult(result);
        }

        if (result.status === 400) {
            const error = await ApiClientImpl.getResult(result) as any;
            throw new Error(error.Error.Message)
        }

        throw new Error("Request error")
    }

    public getUrl(): string {
        return this.url;
    }

    private static async getResult(result: Response): Promise<any> {
        try {
            const data = await result.json();
            console.log(data);
            return data;
        } catch (_) {
            return null;
        }
    }
}