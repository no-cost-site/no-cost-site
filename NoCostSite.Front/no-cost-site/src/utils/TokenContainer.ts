import { LocalStorage } from "./LocalStorage";

const key = "JwtToken";

export const TokenContainer = {
    get: (): string => {
        return LocalStorage.get(key) || "";
    },

    set: (token: string) => {
        LocalStorage.set(key, token);
    },

    clear: () => {
        LocalStorage.set(key, "");
    }
}