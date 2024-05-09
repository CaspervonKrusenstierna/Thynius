import { useEffect, useState } from "react";
import useFetch from "../../../../shared/hooks/useFetch";

export default function useExtendedAssignmentInfo(assignmentId) {
    const [assignmentInfo, setAssignmentInfo] = useState();
    useEffect(() => {
        useFetch("/assignment?assignmentId=" + assignmentId, "GET").then(s => s.json()).then(s => {setAssignmentInfo(s)});
    }, [])
    return assignmentInfo;
}
