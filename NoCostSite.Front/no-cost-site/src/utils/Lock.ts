export const Lock = {
    in: async (action: () => Promise<void>, setLock: (value: boolean) => void): Promise<void> => {
        setLock(true);

        try {
            await action();
        } catch (e) {
            console.log(e);
        }

        setLock(false);
    }
}