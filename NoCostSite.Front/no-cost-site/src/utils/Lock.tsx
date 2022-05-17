import {Message, toaster} from "rsuite";

export const Lock = {
    in: async (action: () => Promise<void>, setLock: (value: boolean) => void): Promise<void> => {
        setLock(true);

        try {
            await action();
        } catch (e: any) {
            console.log(e);

            toaster.push(
                <Message showIcon type="error">{e.message}</Message>,
                {placement: "topCenter"}
            );
        }

        setLock(false);
    },
}