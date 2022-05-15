export const LocalStorage = {
    set(name: string, value: string) {
        localStorage.setItem(getName(name), value);
    },

    get(name: string): string | null {
        return localStorage.getItem(getName(name));
    },

    clear(name: string) {
        LocalStorage.set(name, "");
    },
}

function getName(name: string) {
    return `olrix.net - ${name}`;
}