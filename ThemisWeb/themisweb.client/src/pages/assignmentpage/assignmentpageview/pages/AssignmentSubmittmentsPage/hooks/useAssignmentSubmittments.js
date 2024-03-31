import { useEffect, useState } from "react";
import useFetch from "../../../../../../shared/hooks/useFetch";

export default function useAssignmentSubmittments(assignmentId) {
    const [submittments, setSubmittments] = useState();
    useEffect(() => {
        useFetch("/submittment/getassignmentsubmittments?assignmentId=" + assignmentId, "GET").then(s => s.json()).then(s => {setSubmittments(s)});
    }, [])
    return submittments;
}
