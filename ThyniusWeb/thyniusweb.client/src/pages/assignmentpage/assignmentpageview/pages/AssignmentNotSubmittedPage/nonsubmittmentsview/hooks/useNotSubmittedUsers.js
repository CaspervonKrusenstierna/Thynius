import { useEffect, useState } from "react";
import useFetch from "../../../../../../../shared/hooks/useFetch";

export default function useNotSubmittedUsers(assignmentId) {
    const [users, setUsers] = useState({pages: []});
    useEffect(() => {
        useFetch("/assignment/missingusertexts?assignmentId="+assignmentId+"&hasSubmitted="+true, "GET").then(s => s.json()).then(s => {setUsers({pages: s})});
    }, [])
    return users;
}
