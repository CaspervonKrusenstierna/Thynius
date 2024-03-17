import {useParams } from "react-router-dom"

function GroupsViewPage() {
    const { id } = useParams();

    return (
        <p>Hello world!</p>
    );
}

export default GroupsViewPage;