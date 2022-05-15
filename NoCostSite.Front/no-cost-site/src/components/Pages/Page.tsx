import {useParams} from "react-router-dom";

export const Page = (): JSX.Element => {
    let params = useParams();

    return (
        <>
            <h2>Page</h2>
            <p>{params.pageId}</p>
        </>
    )
}